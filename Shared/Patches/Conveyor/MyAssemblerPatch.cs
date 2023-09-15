/* Assembler fix from zznty

"I'm using this one to completely remove usage of reachable from assembler,
so it will always use route cache for conveyor operations."

TODO:

- Use Krafs.Publicizer for DS and Torch builds, because these are private:
  - conveyorSystem.GetConveyorEndpointMapping
  - MyGridConveyorSystem.ConveyorEndpointMapping

    <Publicize Include="Sandbox.Game:Sandbox.Game.GameSystems.MyGridConveyorSystem+ConveyorEndpointMapping" />
    <Publicize Include="Sandbox.Game:Sandbox.Game.GameSystems.MyGridConveyorSystem.GetConveyorEndpointMapping" />

- Disable this on CLIENT where there is no publiciser

- Test carefully

- Give credit in README

*/


#if UNTESTED

using HarmonyLib;
using Sandbox.Game.Entities.Cube;
using Sandbox.Game.GameSystems.Conveyors;
using VRage.Library.Collections;
using VRage.Utils;

namespace Shared.Patches
{
    [HarmonyPatch(typeof(MyAssembler), "GetMasterAssembler")]
    internal static class AssemblerFix
    {
        private static readonly CacheList<IMyConveyorEndpointBlock> EndpointBlocksCache = new CacheList<IMyConveyorEndpointBlock>();

        private static bool Prefix(MyAssembler __instance, ref MyAssembler __result)
        {
            var conveyorSystem = __instance.CubeGrid.GridSystems.ConveyorSystem;
            var mapping = conveyorSystem.GetConveyorEndpointMapping(__instance);

            if (mapping.pullElements is null)
                return false;

            using (var list = EndpointBlocksCache)
            {
                list.AddRange(mapping.pullElements);
                list.ShuffleList();

                foreach (var block in list)
                {
                    // Original C# 8
                    // if (block is not MyAssembler { IsSlave: false, IsQueueEmpty: false } assembler || assembler == __instance ||
                    //     !__instance.FriendlyWithBlock(assembler)) continue;

                    // C# 7.3
                    if (!(block is MyAssembler assembler) ||
                        assembler.IsSlave ||
                        assembler.IsQueueEmpty ||
                        assembler == __instance ||
                        !__instance.FriendlyWithBlock(assembler))
                    {
                        continue;
                    }

                    __result = assembler;
                    break;
                }
            }

            return false;
        }
    }
}

#endif