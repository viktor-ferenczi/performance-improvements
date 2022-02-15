using System;
using System.Reflection;
using HarmonyLib;
using Sandbox.Game.Entities;
using Shared.Config;
using Shared.Plugin;

namespace Shared.Patches
{
    // ReSharper disable once ClassNeverInstantiated.Global
    [HarmonyPatch]
    public class MyThrusterBlockThrustComponentPatch
    {
        private const float Threshold = 0.001f;

        private const int Bits = 13;
        private const int Count = 1 << Bits;
        private const int Mask = Count - 1;

        private static readonly uint[] Entity = new uint[Count];
        private static readonly float[] Previous = new float[Count];

        private static IPluginConfig Config => Common.Config;

        public static void Update()
        {
            Entity[Common.Plugin.Tick & Mask] = 0;
        }

        // ReSharper disable once UnusedMember.Local
        private static MethodBase TargetMethod()
        {
            var type = AccessTools.TypeByName("Sandbox.Game.EntityComponents.MyThrusterBlockThrustComponent");
            return AccessTools.Method(type, "MyThrust_ThrustOverrideChanged");
        }

        // ReSharper disable once UnusedMember.Local
        private static bool Prefix(MyThrust block, float newValue)
        {
            if (!Config.Enabled || !Config.FixThrusters)
                return true;

            var id = (uint)(block.EntityId >> Bits);
            var hash = block.EntityId & Mask;
            if (Entity[hash] == 0)
            {
                Entity[hash] = id;
                Previous[hash] = newValue;
                return true;
            }

            if (Entity[hash] != id)
                return true;

            var previous = Previous[hash];
            if (newValue == 0f && previous != 0f)
            {
                Previous[hash] = 0f;
                return true;
            }

            var step = Threshold * block.ThrustForceLength;
            if (Math.Abs(newValue - previous) < step)
                return false;

            Previous[hash] = newValue;
            return true;
        }
    }
}