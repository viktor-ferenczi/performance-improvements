using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using HarmonyLib;

namespace Shared.Tools
{
    [AttributeUsage(AttributeTargets.Method)]
    public class EnsureCode : Attribute
    {
        // Allowed method body hashes in hexadecimal, multiple entries can be separated by a | (pipe) character
        private readonly string allowedHashes;

        private bool IsAllowed(string hash) => $"|{allowedHashes}|".Contains($"|{hash}|");

        public EnsureCode(string allowedHashes)
        {
            this.allowedHashes = allowedHashes;
        }

        public static IEnumerable<CodeChange> Verify()
        {
            var reflectedType = new StackTrace().GetFrame(1).GetMethod().ReflectedType;
            if (reflectedType == null)
                throw new Exception("Cannot determine the caller's assembly");

            var callingAssembly = reflectedType.Assembly;
            return Verify(callingAssembly);
        }

        private static IEnumerable<CodeChange> Verify(Assembly pluginAssembly)
        {
            return AccessTools.GetTypesFromAssembly(pluginAssembly).SelectMany(Verify);
        }

        private static IEnumerable<CodeChange> Verify(Type patchType)
        {
            return AccessTools.GetDeclaredMethods(patchType).SelectMany(Verify);
        }

        private static IEnumerable<CodeChange> Verify(MethodInfo patchMethod)
        {
            var validateAttribute = patchMethod.GetCustomAttributes<EnsureCode>().FirstOrDefault();
            if (validateAttribute == null)
                return Enumerable.Empty<CodeChange>();

            return validateAttribute.VerifyMethod(patchMethod);
        }

        private IEnumerable<CodeChange> VerifyMethod(MethodInfo patchMethod)
        {
            var methodPatch = patchMethod.GetCustomAttributes<HarmonyPatch>().FirstOrDefault();
            if (methodPatch == null)
                yield break;

            var patchType = patchMethod.DeclaringType;
            if (patchType == null)
                throw new Exception($"Method info has no DeclaringType: {patchMethod.Name}");

            var patchedType = methodPatch.info.declaringType ?? patchType.GetCustomAttributes<HarmonyPatch>().FirstOrDefault()?.info.declaringType;
            if (patchedType == null)
                throw new Exception($"Could not determine the patched type for patch method {patchType.Name}.{patchMethod.Name}");

            MethodInfo patchedMethod = null;
            ConstructorInfo patchedConstructor = null;
            switch (methodPatch.info.methodType)
            {
                case MethodType.Getter:
                    patchedMethod = AccessTools.PropertyGetter(patchedType, methodPatch.info.methodName);
                    break;

                case MethodType.Setter:
                    patchedMethod = AccessTools.PropertySetter(patchedType, methodPatch.info.methodName);
                    break;

                case MethodType.Constructor:
                    patchedConstructor = AccessTools.Constructor(patchedType, methodPatch.info.argumentTypes);
                    break;

                case MethodType.StaticConstructor:
                    patchedConstructor = AccessTools.Constructor(patchedType, methodPatch.info.argumentTypes, true);
                    break;

                default:
                    patchedMethod = AccessTools.DeclaredMethod(patchedType, methodPatch.info.methodName, methodPatch.info.argumentTypes);
                    break;
            }

            if (patchedMethod == null && patchedConstructor == null)
                throw new Exception($"Could not get patched method information for {patchType.Name}.{patchMethod.Name}");

            var methodBodyHash = (patchedMethod != null ? patchedMethod.HashBody() : patchedConstructor.HashBody()).ToString("x8");
            if (IsAllowed(methodBodyHash))
                yield break;

            yield return new CodeChange(patchedMethod, patchedConstructor, allowedHashes, methodBodyHash);
        }
    }
}