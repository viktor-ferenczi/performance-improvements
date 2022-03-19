using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using Sandbox.Game.Entities.Interfaces;
using Sandbox.Game.Weapons;
using Shared.Config;
using Shared.Plugin;
using VRage.Game.Entity;

namespace Shared.Patches
{
    // ReSharper disable once UnusedType.Global
    [HarmonyPatch(typeof(MyLargeTurretTargetingSystem))]
    public static class MyLargeTurretTargetingSystemPatch
    {
        private static IPluginConfig Config => Common.Config;
        private static bool enabled;

        static MyLargeTurretTargetingSystemPatch()
        {
            UpdateEnabled();
            Config.PropertyChanged += OnConfigChanged;
        }

        private static void UpdateEnabled()
        {
            enabled = Config.Enabled && Config.FixTargetingAlloc;

            if (!enabled)
                Cache.Clear();
        }

        private static void OnConfigChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateEnabled();
        }

        private class CachedArray
        {
            public long Expires;
            public MyEntity[] Array;
        }

        private const long AverageExpiration = 10 * 60; // ticks
        private const long CleanupPeriod = 77 * 60; // ticks

        private static readonly MethodInfo CopyEntitiesIntoArrayInfo = AccessTools.DeclaredMethod(typeof(MyLargeTurretTargetingSystemPatch), nameof(CopyEntitiesIntoArray));

        private static readonly ConcurrentDictionary<long, CachedArray> Cache = new ConcurrentDictionary<long, CachedArray>();

        private const int MaxDeleteCount = 128;
        private static readonly long[] KeysToDelete = new long[MaxDeleteCount];

        private static long tick;

        public static void Clean()
        {
            if (!enabled)
                return;

            tick = Common.Plugin.Tick;
            if (tick % CleanupPeriod != 0)
                return;

            var count = 0;
            foreach (var (entityId, cachedResult) in Cache)
            {
                if (cachedResult.Expires >= tick)
                    continue;

                KeysToDelete[count++] = entityId;
                if (count == MaxDeleteCount)
                    break;
            }

            for (var i = 0; i < count; i++)
                Cache.Remove(KeysToDelete[i]);
        }

        // ReSharper disable once UnusedMember.Local
        [HarmonyTranspiler]
        [HarmonyPatch("SortTargetRoots")]
        private static IEnumerable<CodeInstruction> SortTargetRootsTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            var il = instructions.ToList();

            var targetReceiver = il.GetField(fi => fi.Name.Contains("m_targetReceiver"));
            var distanceEntityKeys = il.GetField(fi => fi.Name.Contains("m_distanceEntityKeys"));

            // Instead of creating a new array every time reuse the existing one (if present and reasonable)
            var j = il.FindIndex(ci => ci.opcode == OpCodes.Callvirt && ci.operand is MethodInfo mi && mi.Name.Contains("ToArray"));
            il.RemoveAt(j);
            il.Insert(j++, new CodeInstruction(OpCodes.Ldarg_0));
            il.Insert(j++, new CodeInstruction(OpCodes.Ldflda, distanceEntityKeys));
            il.Insert(j++, new CodeInstruction(OpCodes.Ldarg_0));
            il.Insert(j++, new CodeInstruction(OpCodes.Ldfld, targetReceiver));
            il.Insert(j, new CodeInstruction(OpCodes.Call, CopyEntitiesIntoArrayInfo));

            // Delete the call to EnsureCapacity, since CopyEntitiesIntoArray resizes m_distanceEntityKeys as well
            var k = il.FindIndex(ci => ci.opcode == OpCodes.Call && ci.operand is MethodInfo mi && mi.Name.Contains("EnsureCapacity"));
            Debug.Assert(il[k - 6].opcode == OpCodes.Ldarg_0);
            Debug.Assert(il[k - 5].opcode == OpCodes.Ldflda);
            Debug.Assert(il[k - 4].opcode == OpCodes.Ldloc_0);
            Debug.Assert(il[k - 3].opcode == OpCodes.Ldlen);
            Debug.Assert(il[k + 1].opcode == OpCodes.Ldc_I4_0);
            Debug.Assert(il[k + 2].opcode == OpCodes.Stloc_2);
            il.RemoveRange(k - 6, 9);

            return il.AsEnumerable();
        }

        private static MyEntity[] CopyEntitiesIntoArray(List<MyEntity> targetRoots, ref float[] distanceEntityKeys, IMyTargetingReceiver targetReceiver)
        {
            if (!enabled)
                return targetRoots.ToArray();

            var count = targetRoots.Count;
            if (count == 0)
                return Array.Empty<MyEntity>();

            if (distanceEntityKeys == null || distanceEntityKeys.Length < count)
                distanceEntityKeys = new float[count];

            var targetReceiverEntity = targetReceiver.Entity;
            var entityId = targetReceiverEntity.EntityId;
            if (targetReceiverEntity.Closed)
            {
                Cache.Remove(entityId);
                return Array.Empty<MyEntity>();
            }

            if (Cache.TryGetValue(entityId, out var cache))
            {
                cache.Expires = Common.Plugin.Tick + AverageExpiration + (entityId & 7) - 4;

                // Do not use ArrayExtensions.EnsureCapacity, because that's copying the existing items on resizing.
                // It would be wasted work, since we overwrite those items anyway.
                // The array is extended if needed or replaced entirely if at least 4 times longer than needed.
                var arrayLength = cache.Array.Length;
                if (arrayLength < count || arrayLength >= count * 4)
                {
                    cache.Array = new MyEntity[count];
                    arrayLength = count;
                }

                var array = cache.Array;
                for (var i = 0; i < count; i++)
                    array[i] = targetRoots[i];

                // Set all non-null trailing items to null to terminate the loop in the caller and to release former references
                for (var i = count; i < arrayLength && array[i] != null; i++)
                    array[i] = null;

                return array;
            }

            var newArray = targetRoots.ToArray();
            Cache[entityId] = new CachedArray { Expires = Common.Plugin.Tick + AverageExpiration + (entityId & 31) - 16, Array = newArray };

            return newArray;
        }
    }
}