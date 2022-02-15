using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Shared.Patches.Patching
{
    public class PatchInfo
    {
        public PatchInfo(Type patchType, string[] categories)
        {
            PatchType = patchType;
            Categories = categories;
        }

        public Type PatchType { get; }
        public string[] Categories { get; }
        public bool Enabled { get; set; }
    }
}