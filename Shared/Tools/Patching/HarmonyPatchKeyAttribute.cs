using System;

namespace Shared.Patches.Patching
{
    [AttributeUsage(AttributeTargets.Class)]
    public class HarmonyPatchKeyAttribute : PatchKeyAttributeBase
    {
        public HarmonyPatchKeyAttribute(string key, params string[] categories) : base(key, categories)
        {
        }
    }
}