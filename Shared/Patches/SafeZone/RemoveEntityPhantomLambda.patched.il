// 3fdc765f
ldarg.0 NULL
ldfld Sandbox.Game.Entities.MySafeZone Sandbox.Game.Entities.<>c__DisplayClass103_0::<>4__this
ldfld System.Collections.Generic.HashSet`1<VRage.ModAPI.IMyEntity> Sandbox.Game.Entities.MySafeZone::m_RemoveEntityPhantomTaskList
ldarg.0 NULL
ldfld VRage.ModAPI.IMyEntity Sandbox.Game.Entities.<>c__DisplayClass103_0::entity
call static System.Boolean Shared.Patches.MySafeZonePatch::PhantomTaskListRemove(System.Collections.Generic.HashSet`1<VRage.Game.ModAPI.Ingame.IMyEntity> map, VRage.Game.ModAPI.Ingame.IMyEntity item)
pop NULL
ldarg.0 NULL
ldfld Sandbox.Game.Entities.MySafeZone Sandbox.Game.Entities.<>c__DisplayClass103_0::<>4__this
call VRage.Game.Components.MyPhysicsComponentBase VRage.Game.Entity.MyEntity::get_Physics()
brtrue.s Label0
ret NULL
ldarg.0 NULL [Label0]
ldfld VRage.ModAPI.IMyEntity Sandbox.Game.Entities.<>c__DisplayClass103_0::entity
callvirt abstract virtual System.Boolean VRage.ModAPI.IMyEntity::get_MarkedForClose()
brfalse.s Label1
ldarg.0 NULL
ldfld Sandbox.Game.Entities.MySafeZone Sandbox.Game.Entities.<>c__DisplayClass103_0::<>4__this
ldarg.0 NULL
ldfld VRage.Game.Entity.MyEntity Sandbox.Game.Entities.<>c__DisplayClass103_0::topEntity
ldarg.0 NULL
ldfld System.Boolean Sandbox.Game.Entities.<>c__DisplayClass103_0::addedOrRemoved
call System.Boolean Sandbox.Game.Entities.MySafeZone::RemoveEntityInternal(VRage.Game.Entity.MyEntity entity, System.Boolean addedOrRemoved)
brfalse.s Label2
ldarg.0 NULL
ldfld Sandbox.Game.Entities.MySafeZone Sandbox.Game.Entities.<>c__DisplayClass103_0::<>4__this
ldarg.0 NULL
ldfld VRage.Game.Entity.MyEntity Sandbox.Game.Entities.<>c__DisplayClass103_0::topEntity
callvirt virtual System.Int64 VRage.Game.Entity.MyEntity::get_EntityId()
ldarg.0 NULL
ldfld System.Boolean Sandbox.Game.Entities.<>c__DisplayClass103_0::addedOrRemoved
call System.Void Sandbox.Game.Entities.MySafeZone::SendRemovedEntity(System.Int64 entityId, System.Boolean addedOrRemoved)
ret NULL [Label2]
ldarg.0 NULL [Label1]
ldfld VRage.ModAPI.IMyEntity Sandbox.Game.Entities.<>c__DisplayClass103_0::entity
isinst Sandbox.Game.Entities.Character.MyCharacter
stloc.0 NULL
ldloc.0 NULL
brtrue.s Label3
ldc.i4.0 NULL
br.s Label4
ldloc.0 NULL [Label3]
callvirt System.Boolean Sandbox.Game.Entities.Character.MyCharacter::get_IsDead()
brtrue.s Label5 [Label4]
ldarg.0 NULL
ldfld Havok.HkRigidBody Sandbox.Game.Entities.<>c__DisplayClass103_0::body
callvirt System.Boolean Havok.HkHandle::get_IsDisposed()
brtrue.s Label6
ldarg.0 NULL
ldfld VRage.ModAPI.IMyEntity Sandbox.Game.Entities.<>c__DisplayClass103_0::entity
callvirt abstract virtual VRage.Game.Components.MyPhysicsComponentBase VRage.ModAPI.IMyEntity::get_Physics()
callvirt virtual System.Boolean VRage.Game.Components.MyPhysicsComponentBase::get_IsInWorld()
ldc.i4.0 NULL
ceq NULL
br.s Label7
ldc.i4.1 NULL [Label5, Label6]
stloc.1 NULL [Label7]
ldarg.0 NULL
ldfld VRage.ModAPI.IMyEntity Sandbox.Game.Entities.<>c__DisplayClass103_0::entity
callvirt abstract virtual VRage.Game.Components.MyPhysicsComponentBase VRage.ModAPI.IMyEntity::get_Physics()
brfalse.s Label8
ldloc.1 NULL
brtrue.s Label9
ldarg.0 NULL
ldarg.0 NULL
ldfld VRage.ModAPI.IMyEntity Sandbox.Game.Entities.<>c__DisplayClass103_0::entity
callvirt abstract virtual VRage.Game.Components.MyPhysicsComponentBase VRage.ModAPI.IMyEntity::get_Physics()
ldarg.0 NULL
ldfld Havok.HkRigidBody Sandbox.Game.Entities.<>c__DisplayClass103_0::body
callvirt VRageMath.Vector3 Havok.HkEntity::get_Position()
callvirt abstract virtual VRageMath.Vector3D VRage.Game.Components.MyPhysicsComponentBase::ClusterToWorld(VRageMath.Vector3 clusterPos)
stfld VRageMath.Vector3D Sandbox.Game.Entities.<>c__DisplayClass103_0::position1
ldarg.0 NULL
ldarg.0 NULL
ldfld Havok.HkRigidBody Sandbox.Game.Entities.<>c__DisplayClass103_0::body
callvirt VRageMath.Matrix Havok.HkEntity::GetRigidBodyMatrix()
call static VRageMath.Quaternion VRageMath.Quaternion::CreateFromRotationMatrix(VRageMath.Matrix matrix)
stfld VRageMath.Quaternion Sandbox.Game.Entities.<>c__DisplayClass103_0::rotation1
ldarg.0 NULL [Label8, Label9]
ldfld Sandbox.Game.Entities.MySafeZone Sandbox.Game.Entities.<>c__DisplayClass103_0::<>4__this
call virtual VRage.Game.Components.MyPositionComponentBase VRage.Game.Entity.MyEntity::get_PositionComp()
callvirt VRageMath.Vector3D VRage.Game.Components.MyPositionComponentBase::GetPosition()
stloc.2 NULL
ldarg.0 NULL
ldfld Sandbox.Game.Entities.MySafeZone Sandbox.Game.Entities.<>c__DisplayClass103_0::<>4__this
call virtual VRage.Game.Components.MyPositionComponentBase VRage.Game.Entity.MyEntity::get_PositionComp()
callvirt VRageMath.MatrixD VRage.Game.Components.MyPositionComponentBase::GetOrientation()
stloc.s 5 (VRageMath.MatrixD)
ldloca.s 5 (VRageMath.MatrixD)
call static VRageMath.Quaternion VRageMath.Quaternion::CreateFromRotationMatrix(VRageMath.MatrixD& matrix)
stloc.3 NULL
call static Havok.HkShape Havok.HkShape::get_Empty()
stloc.s 4 (Havok.HkShape)
ldarg.0 NULL
ldfld VRage.ModAPI.IMyEntity Sandbox.Game.Entities.<>c__DisplayClass103_0::entity
callvirt abstract virtual VRage.Game.Components.MyPhysicsComponentBase VRage.ModAPI.IMyEntity::get_Physics()
brfalse.s Label10
ldarg.0 NULL
ldfld VRage.ModAPI.IMyEntity Sandbox.Game.Entities.<>c__DisplayClass103_0::entity
callvirt abstract virtual VRage.Game.Components.MyPhysicsComponentBase VRage.ModAPI.IMyEntity::get_Physics()
callvirt abstract virtual Havok.HkRigidBody VRage.Game.Components.MyPhysicsComponentBase::get_RigidBody()
ldnull NULL
call static System.Boolean Havok.HkReferenceObject::op_Inequality(Havok.HkReferenceObject objectA, Havok.HkReferenceObject objectB)
brfalse.s Label11
ldarg.0 NULL
ldfld VRage.ModAPI.IMyEntity Sandbox.Game.Entities.<>c__DisplayClass103_0::entity
callvirt abstract virtual VRage.Game.Components.MyPhysicsComponentBase VRage.ModAPI.IMyEntity::get_Physics()
callvirt abstract virtual Havok.HkRigidBody VRage.Game.Components.MyPhysicsComponentBase::get_RigidBody()
callvirt Havok.HkShape Havok.HkRigidBody::GetShape()
stloc.s 4 (Havok.HkShape)
br.s Label12
ldarg.0 NULL [Label11]
ldfld VRage.ModAPI.IMyEntity Sandbox.Game.Entities.<>c__DisplayClass103_0::entity
callvirt abstract virtual VRage.Game.Components.MyPhysicsComponentBase VRage.ModAPI.IMyEntity::get_Physics()
isinst Sandbox.Engine.Physics.MyPhysicsBody
dup NULL
stloc.s 6 (Sandbox.Engine.Physics.MyPhysicsBody)
brfalse.s Label13
ldloc.0 NULL
brfalse.s Label14
ldloc.s 6 (Sandbox.Engine.Physics.MyPhysicsBody)
callvirt Sandbox.Engine.Physics.MyCharacterProxy Sandbox.Engine.Physics.MyPhysicsBody::get_CharacterProxy()
brfalse.s Label15
ldloc.s 6 (Sandbox.Engine.Physics.MyPhysicsBody)
callvirt Sandbox.Engine.Physics.MyCharacterProxy Sandbox.Engine.Physics.MyPhysicsBody::get_CharacterProxy()
callvirt Havok.HkRigidBody Sandbox.Engine.Physics.MyCharacterProxy::GetHitRigidBody()
callvirt Havok.HkShape Havok.HkRigidBody::GetShape()
stloc.s 4 (Havok.HkShape)
ldloc.1 NULL [Label10, Label12, Label13, Label14, Label15]
brtrue.s Label16
ldloca.s 4 (Havok.HkShape)
call System.Boolean Havok.HkShape::get_IsValid()
brtrue.s Label17
ldc.i4.1 NULL
br.s Label18
ldloc.s 4 (Havok.HkShape) [Label17]
ldarg.0 NULL
ldflda VRageMath.Vector3D Sandbox.Game.Entities.<>c__DisplayClass103_0::position1
ldarg.0 NULL
ldflda VRageMath.Quaternion Sandbox.Game.Entities.<>c__DisplayClass103_0::rotation1
ldarg.0 NULL
ldfld Sandbox.Game.Entities.MySafeZone Sandbox.Game.Entities.<>c__DisplayClass103_0::<>4__this
call VRage.Game.Components.MyPhysicsComponentBase VRage.Game.Entity.MyEntity::get_Physics()
callvirt abstract virtual Havok.HkRigidBody VRage.Game.Components.MyPhysicsComponentBase::get_RigidBody()
callvirt Havok.HkShape Havok.HkRigidBody::GetShape()
ldloca.s 2 (VRageMath.Vector3D)
ldloca.s 3 (VRageMath.Quaternion)
call static System.Boolean Sandbox.Engine.Physics.MyPhysics::IsPenetratingShapeShape(Havok.HkShape shape1, VRageMath.Vector3D& translation1, VRageMath.Quaternion& rotation1, Havok.HkShape shape2, VRageMath.Vector3D& translation2, VRageMath.Quaternion& rotation2)
ldc.i4.0 NULL
ceq NULL
br.s Label19
ldc.i4.1 NULL [Label16]
brfalse Label20 [Label18, Label19]
ldarg.0 NULL
ldfld Sandbox.Game.Entities.MySafeZone Sandbox.Game.Entities.<>c__DisplayClass103_0::<>4__this
ldarg.0 NULL
ldfld VRage.Game.Entity.MyEntity Sandbox.Game.Entities.<>c__DisplayClass103_0::topEntity
ldarg.0 NULL
ldfld System.Boolean Sandbox.Game.Entities.<>c__DisplayClass103_0::addedOrRemoved
call System.Boolean Sandbox.Game.Entities.MySafeZone::RemoveEntityInternal(VRage.Game.Entity.MyEntity entity, System.Boolean addedOrRemoved)
brfalse Label21
ldarg.0 NULL
ldfld Sandbox.Game.Entities.MySafeZone Sandbox.Game.Entities.<>c__DisplayClass103_0::<>4__this
ldarg.0 NULL
ldfld VRage.Game.Entity.MyEntity Sandbox.Game.Entities.<>c__DisplayClass103_0::topEntity
callvirt virtual System.Int64 VRage.Game.Entity.MyEntity::get_EntityId()
ldarg.0 NULL
ldfld System.Boolean Sandbox.Game.Entities.<>c__DisplayClass103_0::addedOrRemoved
call System.Void Sandbox.Game.Entities.MySafeZone::SendRemovedEntity(System.Int64 entityId, System.Boolean addedOrRemoved)
ldarg.0 NULL
ldfld VRage.Game.Entity.MyEntity Sandbox.Game.Entities.<>c__DisplayClass103_0::topEntity
isinst Sandbox.Game.Entities.MyCubeGrid
dup NULL
stloc.s 7 (Sandbox.Game.Entities.MyCubeGrid)
brfalse Label22
ldloc.s 7 (Sandbox.Game.Entities.MyCubeGrid)
callvirt Sandbox.Game.Entities.Cube.MyFatBlockReader`1<Sandbox.Game.Entities.MyShipController> Sandbox.Game.Entities.MyCubeGrid::GetFatBlocks()
stloc.s 8 (Sandbox.Game.Entities.Cube.MyFatBlockReader`1[Sandbox.Game.Entities.MyShipController])
ldloca.s 8 (Sandbox.Game.Entities.Cube.MyFatBlockReader`1[Sandbox.Game.Entities.MyShipController])
call virtual System.Collections.Generic.IEnumerator`1<Sandbox.Game.Entities.MyShipController> Sandbox.Game.Entities.Cube.MyFatBlockReader`1<Sandbox.Game.Entities.MyShipController>::GetEnumerator()
stloc.s 9 (System.Collections.Generic.IEnumerator`1[Sandbox.Game.Entities.MyShipController])
br.s Label23 [EX_BeginException]
ldloc.s 9 (System.Collections.Generic.IEnumerator`1[Sandbox.Game.Entities.MyShipController]) [Label28]
callvirt abstract virtual Sandbox.Game.Entities.MyShipController System.Collections.Generic.IEnumerator`1<Sandbox.Game.Entities.MyShipController>::get_Current()
stloc.s 10 (Sandbox.Game.Entities.MyShipController)
ldloc.s 10 (Sandbox.Game.Entities.MyShipController)
isinst Sandbox.Game.Entities.MyRemoteControl
brtrue.s Label24
ldloc.s 10 (Sandbox.Game.Entities.MyShipController)
callvirt virtual Sandbox.Game.Entities.Character.MyCharacter Sandbox.Game.Entities.MyShipController::get_Pilot()
brfalse.s Label25
ldloc.s 10 (Sandbox.Game.Entities.MyShipController)
callvirt virtual Sandbox.Game.Entities.Character.MyCharacter Sandbox.Game.Entities.MyShipController::get_Pilot()
ldarg.0 NULL
ldfld VRage.Game.Entity.MyEntity Sandbox.Game.Entities.<>c__DisplayClass103_0::topEntity
beq.s Label26
ldarg.0 NULL
ldfld Sandbox.Game.Entities.MySafeZone Sandbox.Game.Entities.<>c__DisplayClass103_0::<>4__this
ldloc.s 10 (Sandbox.Game.Entities.MyShipController)
callvirt virtual Sandbox.Game.Entities.Character.MyCharacter Sandbox.Game.Entities.MyShipController::get_Pilot()
ldarg.0 NULL
ldfld System.Boolean Sandbox.Game.Entities.<>c__DisplayClass103_0::addedOrRemoved
call System.Boolean Sandbox.Game.Entities.MySafeZone::RemoveEntityInternal(VRage.Game.Entity.MyEntity entity, System.Boolean addedOrRemoved)
brfalse.s Label27
ldarg.0 NULL
ldfld Sandbox.Game.Entities.MySafeZone Sandbox.Game.Entities.<>c__DisplayClass103_0::<>4__this
ldloc.s 10 (Sandbox.Game.Entities.MyShipController)
callvirt virtual Sandbox.Game.Entities.Character.MyCharacter Sandbox.Game.Entities.MyShipController::get_Pilot()
callvirt virtual System.Int64 VRage.Game.Entity.MyEntity::get_EntityId()
ldarg.0 NULL
ldfld System.Boolean Sandbox.Game.Entities.<>c__DisplayClass103_0::addedOrRemoved
call System.Void Sandbox.Game.Entities.MySafeZone::SendRemovedEntity(System.Int64 entityId, System.Boolean addedOrRemoved)
ldloc.s 9 (System.Collections.Generic.IEnumerator`1[Sandbox.Game.Entities.MyShipController]) [Label23, Label24, Label25, Label26, Label27]
callvirt abstract virtual System.Boolean System.Collections.IEnumerator::MoveNext()
brtrue.s Label28
leave.s Label29
ldloc.s 9 (System.Collections.Generic.IEnumerator`1[Sandbox.Game.Entities.MyShipController]) [EX_BeginFinally]
brfalse.s Label30
ldloc.s 9 (System.Collections.Generic.IEnumerator`1[Sandbox.Game.Entities.MyShipController])
callvirt abstract virtual System.Void System.IDisposable::Dispose()
endfinally NULL [Label30, EX_EndException]
ldarg.0 NULL [Label21, Label22, Label29]
ldfld VRage.Game.Entity.MyEntity Sandbox.Game.Entities.<>c__DisplayClass103_0::topEntity
ldarg.0 NULL
ldfld Sandbox.Game.Entities.MySafeZone Sandbox.Game.Entities.<>c__DisplayClass103_0::<>4__this
ldftn System.Void Sandbox.Game.Entities.MySafeZone::entity_OnClose(VRage.Game.Entity.MyEntity obj)
newobj System.Void System.Action`1<VRage.Game.Entity.MyEntity>::.ctor(System.Object object, System.IntPtr method)
callvirt System.Void VRage.Game.Entity.MyEntity::remove_OnClose(System.Action`1<VRage.Game.Entity.MyEntity> value)
ret NULL [Label20]
