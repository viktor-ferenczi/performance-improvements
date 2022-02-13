using System;
using System.Linq;
using System.Reflection;
using Shared.Patches.Patching;
using Torch.API.Managers;
using Torch.Managers.PatchManager;

namespace TorchPlugin;

public class TorchPatcher : PatcherBase<PatchKeyAttribute>
{
    private readonly PatchManager patchManager;

    public TorchPatcher()
    {
        patchManager = Plugin.Instance.Torch.Managers.GetManager<PatchManager>();
    }

    protected override PatchInfo CreatePatchInfo(Type type, string[] categories)
    {
        return new(type, categories);
    }

    public override void ApplyEnabled()
    {
        var ctx = patchManager.AcquireContext();
        var parameters = new object[] {ctx};
        foreach (var (type, _) in PatchInfos.Values.Where(b => b.Enabled))
        {
            if (FindPatchMethod(type) is not { } method)
                throw new InvalidOperationException($"Type {type.FullName} is declared without static Patch method");
            method.Invoke(null, parameters);
        }
        patchManager.Commit();
    }

    private MethodInfo FindPatchMethod(Type type) => type.GetMethod("Patch", BindingFlags.Public | BindingFlags.Static);
}

[AttributeUsage(AttributeTargets.Class)]
public class PatchKeyAttribute : PatchKeyAttributeBase
{
    public PatchKeyAttribute(string key, params string[] categories) : base(key, categories)
    {
    }
}