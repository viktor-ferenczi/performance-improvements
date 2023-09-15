/* ConnectorFix by zznty

- Use Krafs.Publicizer, MyShipConnector.State is private

- Test it with a world with many connected ships

- Give credit to zznty in README

- Publish on DS and Torch

 */


#if UNTESTED

using System;
using HarmonyLib;
using Sandbox.Game.Entities;
using Sandbox.Game.Entities.Cube;
using Sandbox.Game.Multiplayer;
using VRage.Sync;

namespace Shared.Patches
{
    public class MyShipConnectorPatch
    {
        [HarmonyPatch]
        internal static class ConnectorFix
        {
            [HarmonyPatch(typeof(MyShipConnector), "UpdateConnectionState")]
            [HarmonyPrefix]
            private static bool UpdatePrefix(MyShipConnector __instance, ref bool ___m_isInitOnceBeforeFrameUpdate,
                bool ___m_isMaster, Sync<MyShipConnector.State, SyncDirection.FromServer> ___m_connectionState)
            {
                if (___m_isInitOnceBeforeFrameUpdate)
                {
                    UpdateConnectionState(__instance);
                    ___m_connectionState.ValueChanged += _ =>
                    {
                        // if we do that immediately or before update, state will reset and calling method be unable to continue
                        __instance.CubeGrid.Schedule(MyCubeGrid.UpdateQueue.OnceAfterSimulation, () => UpdateConnectionState(__instance));
                    };

                }
                else if (__instance.Other == null && ___m_connectionState.Value.OtherEntityId != 0 && Sync.IsServer)
                {
                    ___m_connectionState.Value = default;
                }

                return ___m_isMaster &&
                       (___m_connectionState.Value is { OtherEntityId: 0 } or { MasterToSlave: null } ||
                        ___m_connectionState.Value.OtherEntityId != __instance.Other?.EntityId);
            }

            [HarmonyReversePatch]
            [HarmonyPatch(typeof(MyShipConnector), "UpdateConnectionState")]
            private static void UpdateConnectionState(MyShipConnector instance)
            {
                // its a stub so it has no initial content
                throw new NotImplementedException("It's a stub");
            }
        }
    }
}

#endif