using System;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using Shared.Logging;
using Shared.Plugin;

namespace Shared.Patches.Patching;

public class HarmonyPatcher : PatcherBase<HarmonyPatchKeyAttribute>
{
    private static IPluginLogger Log => Common.Logger;
    
    private readonly Harmony harmony = new(Assembly.GetExecutingAssembly().GetName().Name);

    
    protected override PatchInfo CreatePatchInfo(Type type, string[] categories)
    {
        return new HarmonyPatchInfo(harmony.CreateClassProcessor(type), type, categories);
    }

    public override void ApplyEnabled()
    {
        foreach (var patchInfo in PatchInfos.Values.Where(b => b.Enabled))
        {
#if DEBUG
            Log.Debug($"Applying patches for {patchInfo.PatchType.FullName}");
            var count = ((HarmonyPatchInfo) patchInfo).ClassProcessor.Patch().Count;
            Log.Debug($"Applied {count} patches");
#else
            _ = ((HarmonyPatchInfo) patchInfo).ClassProcessor.Patch();
#endif
        }
    }

    private record HarmonyPatchInfo(PatchClassProcessor ClassProcessor, Type TargetType, string[] Category) : PatchInfo(TargetType, Category);
}