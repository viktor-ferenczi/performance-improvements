using System.Reflection;
using HarmonyLib;

namespace Shared.Tools
{
    public class CodeChange
    {
        public readonly MethodInfo Method;
        public readonly ConstructorInfo Constructor;
        public readonly string Expected;
        public readonly string Actual;

        public CodeChange(MethodInfo method, ConstructorInfo constructor, string expected, string actual)
        {
            Method = method;
            Constructor = constructor;
            Expected = expected;
            Actual = actual;
        }

        public override string ToString() => $"Code change in {Method?.FullDescription() ?? Constructor.FullDescription()}; expected {Expected}, actual {Actual}";
    }
}