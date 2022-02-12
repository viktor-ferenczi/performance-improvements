using System.Threading;
using HarmonyLib;
using Sandbox.Game.Entities;
using Sandbox.Game.Entities.Character.Components;
using Shared.Config;
using Shared.Plugin;

namespace Shared.Patches
{
    // ReSharper disable once UnusedType.Global
    [HarmonyPatch(typeof(MyCharacterJetpackComponent))]
    // ReSharper disable once ClassNeverInstantiated.Global
    public class MyCharacterJetpackComponentPatch
    {
        private static readonly ThreadLocal<int> CallDepth = new ThreadLocal<int>();
        public static bool IsInTurnOnJetpack => CallDepth.Value > 0;
        
        // ReSharper disable once UnusedMember.Local
        [HarmonyPrefix]
        [HarmonyPatch("TurnOnJetpack")]
        private static bool TurnOnJetpackPrefix()
        {
            CallDepth.Value++;

            return true;
        }

        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once InconsistentNaming
        [HarmonyPostfix]
        [HarmonyPatch("TurnOnJetpack")]
        private static void TurnOnJetpackPostfix()
        {
            if (!IsInTurnOnJetpack)
                return;

            --CallDepth.Value;
        }
    }
}