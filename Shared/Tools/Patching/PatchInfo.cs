using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Shared.Patches.Patching;

public record PatchInfo(Type PatchType, string[] Categories)
{
    public bool Enabled { get; set; }
}