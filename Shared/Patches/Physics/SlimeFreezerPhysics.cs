#if FROM_SLIME_VIA_DORIMANX

// Original comment:
// what we tried to do is this, it's NOT working as expected... but we tried.

using System;
using System.Collections.Generic;
using Sandbox.Game.Entities;
using System.Diagnostics;
using NAPI;
using Sandbox.Game.Entities.Cube;
using VRageMath;
using Freezer.Freezer.Data;
using TorchPlugin;
using Torch.Managers.PatchManager;
using VRage.Game.Components;

namespace Slime
{
    public class FreezerPhysics
    {
        public static Guid PhysFreezeGuid = new Guid("2ace9a8c-25d0-4205-80b6-cdf5435f2846");

        public static void Init(PatchContext patchContext)
        {
            if (!FreezerPlugin.freezer.Config.FreezePhysicsEnabled) return;

            patchContext.Prefix(typeof(MyGridPhysics), typeof(FreezerPhysics), "AddForce");
        }

        public static bool AddForce(MyGridPhysics __instance, MyPhysicsForceType type,
            Vector3? force,
            Vector3D? position,
            Vector3? torque,
            float? maxSpeed = null,
            bool applyImmediately = true,
            bool activeOnly = false)
        {
            if (activeOnly)
            {
                return true;
            }
            else
            {
                if (__instance.Entity.isFrozen())
                {
                    if (FreezerPlugin.freezer.Config.Profiling)
                    {
                        Log.LogSpamming(37747, (builder, i) => { builder.Append($"AddForce from: {new StackTrace()}"); });
                    }
                    return false;
                }
                return true;
            }
        }

        internal static void FreezeGroup(List<MyCubeGrid> grids, FrozenGridGroup info)
        {
            if (!FreezerPlugin.freezer.Config.FreezePhysicsEnabled) return;
            info.Grids = info.Grids ?? new List<FrozenGridInfo>();
            info.Grids.Clear();
            foreach (var g in grids)
            {
                if (!g.IsStatic && g.Physics != null)
                {
                    g.Physics.ClearSpeed();
                    g.Physics.RigidBody.IsActive = false;
                    g.Physics.Enabled = false;
                    g.Physics.Gravity = Vector3.Zero;
                    //(g as MyCubeGrid).OverFatBlocks(FreezeBlock);
                }
            }
        }

        internal static void UnFreezeGroup(List<MyCubeGrid> grids, FrozenGridGroup info)
        {
            if (!FreezerPlugin.freezer.Config.FreezePhysicsEnabled) return;
            foreach (var x in grids)
            {
                if (x.Physics == null) continue;

                if (x.Physics.RigidBody != null) x.Physics.RigidBody.IsActive = true;
                x.Physics.Enabled = true;

                if (info.Grids == null) continue;

                var gridInfo = info.Grids.Find(g => g.EntityId == x.EntityId);
                if (gridInfo != null)
                {
                    x.Physics.SetSpeeds(gridInfo.Speed, gridInfo.Aspeed);
                }
            }
        }
    }
}

#endif