// 578d3310
newobj System.Void Sandbox.Game.Entities.<>c__DisplayClass103_0::.ctor()
stloc.0 NULL
ldloc.0 NULL
ldarg.0 NULL
stfld Sandbox.Game.Entities.MySafeZone Sandbox.Game.Entities.<>c__DisplayClass103_0::<>4__this
ldloc.0 NULL
ldarg.2 NULL
stfld VRage.ModAPI.IMyEntity Sandbox.Game.Entities.<>c__DisplayClass103_0::entity
ldloc.0 NULL
ldarg.1 NULL
stfld Havok.HkRigidBody Sandbox.Game.Entities.<>c__DisplayClass103_0::body
ldloc.0 NULL
ldloc.0 NULL
ldfld VRage.ModAPI.IMyEntity Sandbox.Game.Entities.<>c__DisplayClass103_0::entity
ldnull NULL
callvirt abstract virtual VRage.ModAPI.IMyEntity VRage.ModAPI.IMyEntity::GetTopMostParent(System.Type type)
isinst VRage.Game.Entity.MyEntity
stfld VRage.Game.Entity.MyEntity Sandbox.Game.Entities.<>c__DisplayClass103_0::topEntity
ldloc.0 NULL
ldfld VRage.Game.Entity.MyEntity Sandbox.Game.Entities.<>c__DisplayClass103_0::topEntity
ldloc.0 NULL
ldfld VRage.ModAPI.IMyEntity Sandbox.Game.Entities.<>c__DisplayClass103_0::entity
beq.s Label0
ret NULL
ldloc.0 NULL [Label0]
ldfld VRage.Game.Entity.MyEntity Sandbox.Game.Entities.<>c__DisplayClass103_0::topEntity
callvirt VRage.Game.Components.MyPhysicsComponentBase VRage.Game.Entity.MyEntity::get_Physics()
brfalse.s Label1
ldloc.0 NULL
ldfld VRage.Game.Entity.MyEntity Sandbox.Game.Entities.<>c__DisplayClass103_0::topEntity
callvirt VRage.Game.Components.MyPhysicsComponentBase VRage.Game.Entity.MyEntity::get_Physics()
callvirt virtual System.Boolean VRage.Game.Components.MyPhysicsComponentBase::get_ShapeChangeInProgress()
brfalse.s Label2
ret NULL [Label1]
ldloc.0 NULL [Label2]
ldloc.0 NULL
ldfld VRage.Game.Entity.MyEntity Sandbox.Game.Entities.<>c__DisplayClass103_0::topEntity
call static System.Boolean Sandbox.Game.Entities.MySessionComponentSafeZones::IsRecentlyAddedOrRemoved(VRage.Game.Entity.MyEntity obj)
brtrue.s Label3
ldloc.0 NULL
ldfld VRage.ModAPI.IMyEntity Sandbox.Game.Entities.<>c__DisplayClass103_0::entity
callvirt abstract virtual System.Boolean VRage.ModAPI.IMyEntity::get_InScene()
ldc.i4.0 NULL
ceq NULL
br.s Label4
ldc.i4.1 NULL [Label3]
stfld System.Boolean Sandbox.Game.Entities.<>c__DisplayClass103_0::addedOrRemoved [Label4]
ldarg.0 NULL
ldfld System.Collections.Generic.HashSet`1<VRage.ModAPI.IMyEntity> Sandbox.Game.Entities.MySafeZone::m_RemoveEntityPhantomTaskList
ldloc.0 NULL
ldfld VRage.ModAPI.IMyEntity Sandbox.Game.Entities.<>c__DisplayClass103_0::entity
call static System.Boolean Shared.Patches.MySafeZonePatch::PhantomTaskListContains(System.Collections.Generic.HashSet`1<VRage.Game.ModAPI.Ingame.IMyEntity> map, VRage.Game.ModAPI.Ingame.IMyEntity item)
brfalse.s Label5
ret NULL
ldarg.0 NULL [Label5]
ldfld System.Collections.Generic.HashSet`1<VRage.ModAPI.IMyEntity> Sandbox.Game.Entities.MySafeZone::m_RemoveEntityPhantomTaskList
ldloc.0 NULL
ldfld VRage.ModAPI.IMyEntity Sandbox.Game.Entities.<>c__DisplayClass103_0::entity
call static System.Boolean Shared.Patches.MySafeZonePatch::PhantomTaskListAdd(System.Collections.Generic.HashSet`1<VRage.Game.ModAPI.Ingame.IMyEntity> map, VRage.Game.ModAPI.Ingame.IMyEntity item)
pop NULL
ldloc.0 NULL
ldloc.0 NULL
ldfld VRage.ModAPI.IMyEntity Sandbox.Game.Entities.<>c__DisplayClass103_0::entity
callvirt abstract virtual VRage.Game.Components.MyPhysicsComponentBase VRage.ModAPI.IMyEntity::get_Physics()
ldloc.0 NULL
ldfld Havok.HkRigidBody Sandbox.Game.Entities.<>c__DisplayClass103_0::body
callvirt VRageMath.Vector3 Havok.HkEntity::get_Position()
callvirt abstract virtual VRageMath.Vector3D VRage.Game.Components.MyPhysicsComponentBase::ClusterToWorld(VRageMath.Vector3 clusterPos)
stfld VRageMath.Vector3D Sandbox.Game.Entities.<>c__DisplayClass103_0::position1
ldloc.0 NULL
ldloc.0 NULL
ldfld Havok.HkRigidBody Sandbox.Game.Entities.<>c__DisplayClass103_0::body
callvirt VRageMath.Matrix Havok.HkEntity::GetRigidBodyMatrix()
call static VRageMath.Quaternion VRageMath.Quaternion::CreateFromRotationMatrix(VRageMath.Matrix matrix)
stfld VRageMath.Quaternion Sandbox.Game.Entities.<>c__DisplayClass103_0::rotation1
ldsfld Sandbox.MySandboxGame Sandbox.MySandboxGame::Static
ldloc.0 NULL
ldftn System.Void Sandbox.Game.Entities.<>c__DisplayClass103_0::<RemoveEntityPhantom>b__0()
newobj System.Void System.Action::.ctor(System.Object object, System.IntPtr method)
ldstr "Phantom leave"
ldc.i4.m1 NULL
ldc.i4.0 NULL
callvirt System.Void Sandbox.MySandboxGame::Invoke(System.Action action, System.String invokerName, System.Int32 startAtFrame, System.Int32 repeatTimes)
ret NULL
