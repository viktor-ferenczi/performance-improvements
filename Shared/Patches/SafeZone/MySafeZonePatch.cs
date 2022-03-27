using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using HarmonyLib;
using Sandbox.Game.Entities;
using Shared.Config;
using Shared.Plugin;
using Shared.Tools;
using TorchPlugin.Shared.Tools;
using VRage.Game.Entity;
using VRage.Game.ModAPI.Ingame;

namespace Shared.Patches
{
    [HarmonyPatch(typeof(MySafeZone))]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public static class MySafeZonePatch
    {
        private static IPluginConfig Config => Common.Config;
        private static bool enabled;

        public static void Configure()
        {
            enabled = Config.Enabled && Config.FixSafeZone;
            Config.PropertyChanged += OnConfigChanged;
        }

        private static void OnConfigChanged(object sender, PropertyChangedEventArgs e)
        {
            enabled = Config.Enabled && Config.FixSafeZone;
            if (!enabled)
                Cache.Clear();
        }

        #region "IsSafe fix, see: https://support.keenswh.com/spaceengineers/pc/topic/24146-performance-mysafezone-issafe-is-called-frequently-but-not-cached"

        private static readonly UintCache<long> Cache = new UintCache<long>(27 * 60);

#if DEBUG
        public static string Report => Cache.Report;
#endif

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Update()
        {
            Cache.Clean();
        }

        // ReSharper disable once UnusedMember.Local
        [HarmonyPrefix]
        [HarmonyPatch("IsSafe")]
        [EnsureCode("98164fe2")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsSafePrefix(MyEntity entity, ref bool __result, ref bool __state)
        {
            if (!enabled)
                return true;

            if (Cache.TryGetValue(entity.EntityId, out var value))
            {
                __result = value != 0;
                return false;
            }

            __state = true;
            return true;
        }

        // ReSharper disable once UnusedMember.Local
        [HarmonyPostfix]
        [HarmonyPatch("IsSafe")]
        [EnsureCode("98164fe2")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void IsSafePostfix(MyEntity entity, bool __result, bool __state)
        {
            if (!__state)
                return;

            var entityId = entity.EntityId;
            Cache.Store(entityId, __result ? 1u : 0u, 120u + (uint)(entityId & 15));
        }

        #endregion

        #region "RemoveEntityPhantom fix, see: https://support.keenswh.com/spaceengineers/pc/topic/24149-safezone-m_removeentityphantomtasklist-hashset-corruption-due-to-race-condition"

        [HarmonyTranspiler]
        [HarmonyPatch("RemoveEntityPhantom")]
        [EnsureCode("55db36e5")]
        // ReSharper disable once UnusedMember.Local
        private static IEnumerable<CodeInstruction> RemoveEntityPhantomTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            if (!enabled)
                return instructions;

            var il = instructions.ToList();
            il.RecordOriginalCode();

            var i = il.FindIndex(ci => ci.opcode == OpCodes.Callvirt && ci.operand is MethodInfo mi && mi.Name.Contains("Contains"));
            il[i] = new CodeInstruction(OpCodes.Call, AccessTools.DeclaredMethod(typeof(MySafeZonePatch), nameof(PhantomTaskListContains)));

            // Lock around the Add call
            var j = il.FindIndex(ci => ci.opcode == OpCodes.Callvirt && ci.operand is MethodInfo mi && mi.Name.Contains("Add"));
            il[j] = new CodeInstruction(OpCodes.Call, AccessTools.DeclaredMethod(typeof(MySafeZonePatch), nameof(PhantomTaskListAdd)));

            il.RecordPatchedCode();
            return il;
        }

        [HarmonyTranspiler]
        [HarmonyPatch("Sandbox.Game.Entities.MySafeZone+<>c__DisplayClass103_0", "<RemoveEntityPhantom>b__0")]
        [EnsureCode("b39ccae8")]
        // ReSharper disable once UnusedMember.Local
        private static IEnumerable<CodeInstruction> RemoveEntityPhantomLambdaTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            if (!enabled)
                return instructions;

            var il = instructions.ToList();
            il.RecordOriginalCode();

            // Lock around the Remove call
            var i = il.FindIndex(ci => ci.opcode == OpCodes.Callvirt && ci.operand is MethodInfo mi && mi.Name.Contains("Remove"));
            il[i] = new CodeInstruction(OpCodes.Call, AccessTools.DeclaredMethod(typeof(MySafeZonePatch), nameof(PhantomTaskListRemove)));

            il.RecordPatchedCode();
            return il;
        }

        private static bool PhantomTaskListContains(HashSet<IMyEntity> map, IMyEntity item)
        {
            lock(map)
                return map.Contains(item);
        }

        private static bool PhantomTaskListAdd(HashSet<IMyEntity> map, IMyEntity item)
        {
            lock (map)
                return map.Add(item);
        }

        private static bool PhantomTaskListRemove(HashSet<IMyEntity> map, IMyEntity item)
        {
            lock (map)
                return map.Remove(item);
        }

        #endregion
    }
}