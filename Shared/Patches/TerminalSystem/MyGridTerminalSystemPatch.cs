// May be buggy
#if BUGGY

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using HarmonyLib;
using Sandbox.Game.GameSystems;
using Shared.Config;
using Shared.Plugin;
using Shared.Tools;
using TorchPlugin.Shared.Tools;

namespace Shared.Patches
{
    [HarmonyPatch(typeof(MyGridTerminalSystem))]
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public static class MyGridTerminalSystemPatch
    {
        private static IPluginConfig Config => Common.Config;
        private static bool enabled;

        public static void Configure()
        {
            enabled = Config.Enabled && Config.FixTerminal;
            Config.PropertyChanged += OnConfigChanged;
        }

        private static void OnConfigChanged(object sender, PropertyChangedEventArgs e)
        {
            enabled = Config.Enabled && Config.FixTerminal;
            if (!enabled)
                Inhibitor.Clear();
        }

        private static readonly UintCache<long> Inhibitor = new UintCache<long>(337 * 60);

#if DEBUG
        public static string InhibitorReport => Inhibitor.Report;
#endif

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Update()
        {
            Inhibitor.Cleanup();
        }

        /* Called from MyProgrammableBlock.RunSandboxedProgramAction,
        which can be frequent on multiplayer servers with PBs triggered a lot.
        In this case the ownerID is the owner of the programmable block.
        Since ownership changes rarely, frequent calls can be inhibited. */
        [HarmonyPrefix]
        [HarmonyPatch(nameof(MyGridTerminalSystem.UpdateGridBlocksOwnership))]
        [EnsureCode("98a58a26")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool UpdateGridBlocksOwnershipPrefix(MyGridTerminalSystem __instance, long ownerID)
        {
            if (!enabled)
                return true;

            var key = __instance.GetHashCode() ^ ownerID;
            if (Inhibitor.TryGetValue(key, out var value))
            {
                // In the very rare case of key collision just let the call through
                if (value != (uint)ownerID)
                    return true;

                // Inhibit repeated updates until the cache entry expires
                return false;
            }

            // Inhibit subsequent calls after this one using cache entry expiration as a timer
            Inhibitor.Store(key, (uint)ownerID, 4 * 60 + ((uint)key & 63));
            return true;
        }
    }
}

#endif