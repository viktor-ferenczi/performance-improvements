using System;

namespace Shared.Patches.Patching
{
    public abstract class PatchKeyAttributeBase : Attribute
    {
        protected PatchKeyAttributeBase(string key, string[] categories)
        {
            Key = key;
            Categories = categories;
        }
        public string Key { get; }
        public string[] Categories { get; }
    }
}