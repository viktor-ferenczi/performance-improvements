### Patch: System.Void Sandbox.Game.Entities.MySafeZone::RemoveEntityPhantom(Havok.HkRigidBody body, VRage.ModAPI.IMyEntity entity)
### Replacement: static System.Void Sandbox.Game.Entities.MySafeZone::Sandbox.Game.Entities.MySafeZone.RemoveEntityPhantom_Patch0(Sandbox.Game.Entities.MySafeZone this, Havok.HkRigidBody body, VRage.ModAPI.IMyEntity entity)
Local var 0: Sandbox.Game.Entities.MySafeZone/<>c__DisplayClass103_0
// start original
newobj     System.Void Sandbox.Game.Entities.<>c__DisplayClass103_0::.ctor()
stloc.0
ldloc.0
ldarg.0
stfld      Sandbox.Game.Entities.MySafeZone Sandbox.Game.Entities.<>c__DisplayClass103_0::<>4__this
ldloc.0
ldarg.2
stfld      VRage.ModAPI.IMyEntity Sandbox.Game.Entities.<>c__DisplayClass103_0::entity
ldloc.0
ldarg.1
stfld      Havok.HkRigidBody Sandbox.Game.Entities.<>c__DisplayClass103_0::body
ldloc.0
ldloc.0
ldfld      VRage.ModAPI.IMyEntity Sandbox.Game.Entities.<>c__DisplayClass103_0::entity
ldnull
callvirt   abstract virtual VRage.ModAPI.IMyEntity VRage.ModAPI.IMyEntity::GetTopMostParent(System.Type type)
isinst     VRage.Game.Entity.MyEntity
stfld      VRage.Game.Entity.MyEntity Sandbox.Game.Entities.<>c__DisplayClass103_0::topEntity
ldloc.0
ldfld      VRage.Game.Entity.MyEntity Sandbox.Game.Entities.<>c__DisplayClass103_0::topEntity
ldloc.0
ldfld      VRage.ModAPI.IMyEntity Sandbox.Game.Entities.<>c__DisplayClass103_0::entity
beq =>     Label0
br =>      Label6
Label0
ldloc.0
ldfld      VRage.Game.Entity.MyEntity Sandbox.Game.Entities.<>c__DisplayClass103_0::topEntity
callvirt   VRage.Game.Components.MyPhysicsComponentBase VRage.Game.Entity.MyEntity::get_Physics()
brfalse => Label1
ldloc.0
ldfld      VRage.Game.Entity.MyEntity Sandbox.Game.Entities.<>c__DisplayClass103_0::topEntity
callvirt   VRage.Game.Components.MyPhysicsComponentBase VRage.Game.Entity.MyEntity::get_Physics()
callvirt   virtual System.Boolean VRage.Game.Components.MyPhysicsComponentBase::get_ShapeChangeInProgress()
brfalse => Label2
Label1
br =>      Label7
Label2
ldloc.0
ldloc.0
ldfld      VRage.Game.Entity.MyEntity Sandbox.Game.Entities.<>c__DisplayClass103_0::topEntity
call       static System.Boolean Sandbox.Game.Entities.MySessionComponentSafeZones::IsRecentlyAddedOrRemoved(VRage.Game.Entity.MyEntity obj)
brtrue =>  Label3
ldloc.0
ldfld      VRage.ModAPI.IMyEntity Sandbox.Game.Entities.<>c__DisplayClass103_0::entity
callvirt   abstract virtual System.Boolean VRage.ModAPI.IMyEntity::get_InScene()
ldc.i4.0
ceq
br =>      Label4
Label3
ldc.i4.1
Label4
stfld      System.Boolean Sandbox.Game.Entities.<>c__DisplayClass103_0::addedOrRemoved
ldarg.0
ldfld      System.Collections.Generic.HashSet`1<VRage.ModAPI.IMyEntity> Sandbox.Game.Entities.MySafeZone::m_RemoveEntityPhantomTaskList
ldloc.0
ldfld      VRage.ModAPI.IMyEntity Sandbox.Game.Entities.<>c__DisplayClass103_0::entity
callvirt   virtual System.Boolean System.Collections.Generic.HashSet`1<VRage.ModAPI.IMyEntity>::Contains(VRage.ModAPI.IMyEntity item)
brfalse => Label5
br =>      Label8
Label5
ldarg.0
ldfld      System.Collections.Generic.HashSet`1<VRage.ModAPI.IMyEntity> Sandbox.Game.Entities.MySafeZone::m_RemoveEntityPhantomTaskList
ldloc.0
ldfld      VRage.ModAPI.IMyEntity Sandbox.Game.Entities.<>c__DisplayClass103_0::entity
callvirt   virtual System.Boolean System.Collections.Generic.HashSet`1<VRage.ModAPI.IMyEntity>::Add(VRage.ModAPI.IMyEntity item)
pop
ldloc.0
ldloc.0
ldfld      VRage.ModAPI.IMyEntity Sandbox.Game.Entities.<>c__DisplayClass103_0::entity
callvirt   abstract virtual VRage.Game.Components.MyPhysicsComponentBase VRage.ModAPI.IMyEntity::get_Physics()
ldloc.0
ldfld      Havok.HkRigidBody Sandbox.Game.Entities.<>c__DisplayClass103_0::body
callvirt   VRageMath.Vector3 Havok.HkEntity::get_Position()
callvirt   abstract virtual VRageMath.Vector3D VRage.Game.Components.MyPhysicsComponentBase::ClusterToWorld(VRageMath.Vector3 clusterPos)
stfld      VRageMath.Vector3D Sandbox.Game.Entities.<>c__DisplayClass103_0::position1
ldloc.0
ldloc.0
ldfld      Havok.HkRigidBody Sandbox.Game.Entities.<>c__DisplayClass103_0::body
callvirt   VRageMath.Matrix Havok.HkEntity::GetRigidBodyMatrix()
call       static VRageMath.Quaternion VRageMath.Quaternion::CreateFromRotationMatrix(VRageMath.Matrix matrix)
stfld      VRageMath.Quaternion Sandbox.Game.Entities.<>c__DisplayClass103_0::rotation1
ldsfld     Sandbox.MySandboxGame Sandbox.MySandboxGame::Static
ldloc.0
ldftn      System.Void Sandbox.Game.Entities.<>c__DisplayClass103_0::<RemoveEntityPhantom>b__0()
newobj     System.Void System.Action::.ctor(System.Object object, System.IntPtr method)
ldstr      "Phantom leave"
ldc.i4.m1
ldc.i4.0
callvirt   System.Void Sandbox.MySandboxGame::Invoke(System.Action action, System.String invokerName, System.Int32 startAtFrame, System.Int32 repeatTimes)
// end original
Label6
Label7
Label8
ret
DONE