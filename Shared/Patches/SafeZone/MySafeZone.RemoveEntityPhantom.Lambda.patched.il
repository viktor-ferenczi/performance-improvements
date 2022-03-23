### Patch: System.Void Sandbox.Game.Entities.<>c__DisplayClass103_0::<RemoveEntityPhantom>b__0()
### Replacement: static System.Void Sandbox.Game.Entities.MySafeZone+<>c__DisplayClass103_0::Sandbox.Game.Entities.MySafeZone+c__DisplayClass103_0.<RemoveEntityPhantom>b__0_Patch0(Sandbox.Game.Entities.<>c__DisplayClass103_0 this)
Local var 0: Sandbox.Game.Entities.Character.MyCharacter
Local var 1: System.Boolean
Local var 2: VRageMath.Vector3D
Local var 3: VRageMath.Quaternion
Local var 4: Havok.HkShape
Local var 5: VRageMath.MatrixD
Local var 6: Sandbox.Engine.Physics.MyPhysicsBody
Local var 7: Sandbox.Game.Entities.MyCubeGrid
Local var 8: Sandbox.Game.Entities.Cube.MyFatBlockReader`1<Sandbox.Game.Entities.MyShipController>
Local var 9: System.Collections.Generic.IEnumerator`1<Sandbox.Game.Entities.MyShipController>
Local var 10: Sandbox.Game.Entities.MyShipController
// start original
ldarg.0
ldfld      Sandbox.Game.Entities.MySafeZone Sandbox.Game.Entities.<>c__DisplayClass103_0::<>4__this
ldfld      System.Collections.Generic.HashSet`1<VRage.ModAPI.IMyEntity> Sandbox.Game.Entities.MySafeZone::m_RemoveEntityPhantomTaskList
ldarg.0
ldfld      VRage.ModAPI.IMyEntity Sandbox.Game.Entities.<>c__DisplayClass103_0::entity
call       static System.Boolean Shared.Patches.MySafeZonePatch::PhantomTaskListRemove(System.Collections.Generic.HashSet`1<VRage.Game.ModAPI.Ingame.IMyEntity> map, VRage.Game.ModAPI.Ingame.IMyEntity item)
pop
ldarg.0
ldfld      Sandbox.Game.Entities.MySafeZone Sandbox.Game.Entities.<>c__DisplayClass103_0::<>4__this
call       VRage.Game.Components.MyPhysicsComponentBase VRage.Game.Entity.MyEntity::get_Physics()
brtrue =>  Label0
br =>      Label31
Label0
ldarg.0
ldfld      VRage.ModAPI.IMyEntity Sandbox.Game.Entities.<>c__DisplayClass103_0::entity
callvirt   abstract virtual System.Boolean VRage.ModAPI.IMyEntity::get_MarkedForClose()
brfalse => Label1
ldarg.0
ldfld      Sandbox.Game.Entities.MySafeZone Sandbox.Game.Entities.<>c__DisplayClass103_0::<>4__this
ldarg.0
ldfld      VRage.Game.Entity.MyEntity Sandbox.Game.Entities.<>c__DisplayClass103_0::topEntity
ldarg.0
ldfld      System.Boolean Sandbox.Game.Entities.<>c__DisplayClass103_0::addedOrRemoved
call       System.Boolean Sandbox.Game.Entities.MySafeZone::RemoveEntityInternal(VRage.Game.Entity.MyEntity entity, System.Boolean addedOrRemoved)
brfalse => Label2
ldarg.0
ldfld      Sandbox.Game.Entities.MySafeZone Sandbox.Game.Entities.<>c__DisplayClass103_0::<>4__this
ldarg.0
ldfld      VRage.Game.Entity.MyEntity Sandbox.Game.Entities.<>c__DisplayClass103_0::topEntity
callvirt   virtual System.Int64 VRage.Game.Entity.MyEntity::get_EntityId()
ldarg.0
ldfld      System.Boolean Sandbox.Game.Entities.<>c__DisplayClass103_0::addedOrRemoved
call       System.Void Sandbox.Game.Entities.MySafeZone::SendRemovedEntity(System.Int64 entityId, System.Boolean addedOrRemoved)
Label2
br =>      Label32
Label1
ldarg.0
ldfld      VRage.ModAPI.IMyEntity Sandbox.Game.Entities.<>c__DisplayClass103_0::entity
isinst     Sandbox.Game.Entities.Character.MyCharacter
stloc.0
ldloc.0
brtrue =>  Label3
ldc.i4.0
br =>      Label4
Label3
ldloc.0
callvirt   System.Boolean Sandbox.Game.Entities.Character.MyCharacter::get_IsDead()
Label4
brtrue =>  Label5
ldarg.0
ldfld      Havok.HkRigidBody Sandbox.Game.Entities.<>c__DisplayClass103_0::body
callvirt   System.Boolean Havok.HkHandle::get_IsDisposed()
brtrue =>  Label6
ldarg.0
ldfld      VRage.ModAPI.IMyEntity Sandbox.Game.Entities.<>c__DisplayClass103_0::entity
callvirt   abstract virtual VRage.Game.Components.MyPhysicsComponentBase VRage.ModAPI.IMyEntity::get_Physics()
callvirt   virtual System.Boolean VRage.Game.Components.MyPhysicsComponentBase::get_IsInWorld()
ldc.i4.0
ceq
br =>      Label7
Label5
Label6
ldc.i4.1
Label7
stloc.1
ldarg.0
ldfld      VRage.ModAPI.IMyEntity Sandbox.Game.Entities.<>c__DisplayClass103_0::entity
callvirt   abstract virtual VRage.Game.Components.MyPhysicsComponentBase VRage.ModAPI.IMyEntity::get_Physics()
brfalse => Label8
ldloc.1
brtrue =>  Label9
ldarg.0
ldarg.0
ldfld      VRage.ModAPI.IMyEntity Sandbox.Game.Entities.<>c__DisplayClass103_0::entity
callvirt   abstract virtual VRage.Game.Components.MyPhysicsComponentBase VRage.ModAPI.IMyEntity::get_Physics()
ldarg.0
ldfld      Havok.HkRigidBody Sandbox.Game.Entities.<>c__DisplayClass103_0::body
callvirt   VRageMath.Vector3 Havok.HkEntity::get_Position()
callvirt   abstract virtual VRageMath.Vector3D VRage.Game.Components.MyPhysicsComponentBase::ClusterToWorld(VRageMath.Vector3 clusterPos)
stfld      VRageMath.Vector3D Sandbox.Game.Entities.<>c__DisplayClass103_0::position1
ldarg.0
ldarg.0
ldfld      Havok.HkRigidBody Sandbox.Game.Entities.<>c__DisplayClass103_0::body
callvirt   VRageMath.Matrix Havok.HkEntity::GetRigidBodyMatrix()
call       static VRageMath.Quaternion VRageMath.Quaternion::CreateFromRotationMatrix(VRageMath.Matrix matrix)
stfld      VRageMath.Quaternion Sandbox.Game.Entities.<>c__DisplayClass103_0::rotation1
Label8
Label9
ldarg.0
ldfld      Sandbox.Game.Entities.MySafeZone Sandbox.Game.Entities.<>c__DisplayClass103_0::<>4__this
call       virtual VRage.Game.Components.MyPositionComponentBase VRage.Game.Entity.MyEntity::get_PositionComp()
callvirt   VRageMath.Vector3D VRage.Game.Components.MyPositionComponentBase::GetPosition()
stloc.2
ldarg.0
ldfld      Sandbox.Game.Entities.MySafeZone Sandbox.Game.Entities.<>c__DisplayClass103_0::<>4__this
call       virtual VRage.Game.Components.MyPositionComponentBase VRage.Game.Entity.MyEntity::get_PositionComp()
callvirt   VRageMath.MatrixD VRage.Game.Components.MyPositionComponentBase::GetOrientation()
stloc.s    5 (VRageMath.MatrixD)
ldloca.s   5 (VRageMath.MatrixD)
call       static VRageMath.Quaternion VRageMath.Quaternion::CreateFromRotationMatrix(VRageMath.MatrixD& matrix)
stloc.3
call       static Havok.HkShape Havok.HkShape::get_Empty()
stloc.s    4 (Havok.HkShape)
ldarg.0
ldfld      VRage.ModAPI.IMyEntity Sandbox.Game.Entities.<>c__DisplayClass103_0::entity
callvirt   abstract virtual VRage.Game.Components.MyPhysicsComponentBase VRage.ModAPI.IMyEntity::get_Physics()
brfalse => Label10
ldarg.0
ldfld      VRage.ModAPI.IMyEntity Sandbox.Game.Entities.<>c__DisplayClass103_0::entity
callvirt   abstract virtual VRage.Game.Components.MyPhysicsComponentBase VRage.ModAPI.IMyEntity::get_Physics()
callvirt   abstract virtual Havok.HkRigidBody VRage.Game.Components.MyPhysicsComponentBase::get_RigidBody()
ldnull
call       static System.Boolean Havok.HkReferenceObject::op_Inequality(Havok.HkReferenceObject objectA, Havok.HkReferenceObject objectB)
brfalse => Label11
ldarg.0
ldfld      VRage.ModAPI.IMyEntity Sandbox.Game.Entities.<>c__DisplayClass103_0::entity
callvirt   abstract virtual VRage.Game.Components.MyPhysicsComponentBase VRage.ModAPI.IMyEntity::get_Physics()
callvirt   abstract virtual Havok.HkRigidBody VRage.Game.Components.MyPhysicsComponentBase::get_RigidBody()
callvirt   Havok.HkShape Havok.HkRigidBody::GetShape()
stloc.s    4 (Havok.HkShape)
br =>      Label12
Label11
ldarg.0
ldfld      VRage.ModAPI.IMyEntity Sandbox.Game.Entities.<>c__DisplayClass103_0::entity
callvirt   abstract virtual VRage.Game.Components.MyPhysicsComponentBase VRage.ModAPI.IMyEntity::get_Physics()
isinst     Sandbox.Engine.Physics.MyPhysicsBody
dup
stloc.s    6 (Sandbox.Engine.Physics.MyPhysicsBody)
brfalse => Label13
ldloc.0
brfalse => Label14
ldloc.s    6 (Sandbox.Engine.Physics.MyPhysicsBody)
callvirt   Sandbox.Engine.Physics.MyCharacterProxy Sandbox.Engine.Physics.MyPhysicsBody::get_CharacterProxy()
brfalse => Label15
ldloc.s    6 (Sandbox.Engine.Physics.MyPhysicsBody)
callvirt   Sandbox.Engine.Physics.MyCharacterProxy Sandbox.Engine.Physics.MyPhysicsBody::get_CharacterProxy()
callvirt   Havok.HkRigidBody Sandbox.Engine.Physics.MyCharacterProxy::GetHitRigidBody()
callvirt   Havok.HkShape Havok.HkRigidBody::GetShape()
stloc.s    4 (Havok.HkShape)
Label10
Label12
Label13
Label14
Label15
ldloc.1
brtrue =>  Label16
ldloca.s   4 (Havok.HkShape)
call       System.Boolean Havok.HkShape::get_IsValid()
brtrue =>  Label17
ldc.i4.1
br =>      Label18
Label17
ldloc.s    4 (Havok.HkShape)
ldarg.0
ldflda     VRageMath.Vector3D Sandbox.Game.Entities.<>c__DisplayClass103_0::position1
ldarg.0
ldflda     VRageMath.Quaternion Sandbox.Game.Entities.<>c__DisplayClass103_0::rotation1
ldarg.0
ldfld      Sandbox.Game.Entities.MySafeZone Sandbox.Game.Entities.<>c__DisplayClass103_0::<>4__this
call       VRage.Game.Components.MyPhysicsComponentBase VRage.Game.Entity.MyEntity::get_Physics()
callvirt   abstract virtual Havok.HkRigidBody VRage.Game.Components.MyPhysicsComponentBase::get_RigidBody()
callvirt   Havok.HkShape Havok.HkRigidBody::GetShape()
ldloca.s   2 (VRageMath.Vector3D)
ldloca.s   3 (VRageMath.Quaternion)
call       static System.Boolean Sandbox.Engine.Physics.MyPhysics::IsPenetratingShapeShape(Havok.HkShape shape1, VRageMath.Vector3D& translation1, VRageMath.Quaternion& rotation1, Havok.HkShape shape2, VRageMath.Vector3D& translation2, VRageMath.Quaternion& rotation2)
ldc.i4.0
ceq
br =>      Label19
Label16
ldc.i4.1
Label18
Label19
brfalse => Label20
ldarg.0
ldfld      Sandbox.Game.Entities.MySafeZone Sandbox.Game.Entities.<>c__DisplayClass103_0::<>4__this
ldarg.0
ldfld      VRage.Game.Entity.MyEntity Sandbox.Game.Entities.<>c__DisplayClass103_0::topEntity
ldarg.0
ldfld      System.Boolean Sandbox.Game.Entities.<>c__DisplayClass103_0::addedOrRemoved
call       System.Boolean Sandbox.Game.Entities.MySafeZone::RemoveEntityInternal(VRage.Game.Entity.MyEntity entity, System.Boolean addedOrRemoved)
brfalse => Label21
ldarg.0
ldfld      Sandbox.Game.Entities.MySafeZone Sandbox.Game.Entities.<>c__DisplayClass103_0::<>4__this
ldarg.0
ldfld      VRage.Game.Entity.MyEntity Sandbox.Game.Entities.<>c__DisplayClass103_0::topEntity
callvirt   virtual System.Int64 VRage.Game.Entity.MyEntity::get_EntityId()
ldarg.0
ldfld      System.Boolean Sandbox.Game.Entities.<>c__DisplayClass103_0::addedOrRemoved
call       System.Void Sandbox.Game.Entities.MySafeZone::SendRemovedEntity(System.Int64 entityId, System.Boolean addedOrRemoved)
ldarg.0
ldfld      VRage.Game.Entity.MyEntity Sandbox.Game.Entities.<>c__DisplayClass103_0::topEntity
isinst     Sandbox.Game.Entities.MyCubeGrid
dup
stloc.s    7 (Sandbox.Game.Entities.MyCubeGrid)
brfalse => Label22
ldloc.s    7 (Sandbox.Game.Entities.MyCubeGrid)
callvirt   Sandbox.Game.Entities.Cube.MyFatBlockReader`1<Sandbox.Game.Entities.MyShipController> Sandbox.Game.Entities.MyCubeGrid::GetFatBlocks()
stloc.s    8 (Sandbox.Game.Entities.Cube.MyFatBlockReader`1[Sandbox.Game.Entities.MyShipController])
ldloca.s   8 (Sandbox.Game.Entities.Cube.MyFatBlockReader`1[Sandbox.Game.Entities.MyShipController])
call       virtual System.Collections.Generic.IEnumerator`1<Sandbox.Game.Entities.MyShipController> Sandbox.Game.Entities.Cube.MyFatBlockReader`1<Sandbox.Game.Entities.MyShipController>::GetEnumerator()
stloc.s    9 (System.Collections.Generic.IEnumerator`1[Sandbox.Game.Entities.MyShipController])
.try
{
	br =>      Label23
	Label28
	ldloc.s    9 (System.Collections.Generic.IEnumerator`1[Sandbox.Game.Entities.MyShipController])
	callvirt   abstract virtual Sandbox.Game.Entities.MyShipController System.Collections.Generic.IEnumerator`1<Sandbox.Game.Entities.MyShipController>::get_Current()
	stloc.s    10 (Sandbox.Game.Entities.MyShipController)
	ldloc.s    10 (Sandbox.Game.Entities.MyShipController)
	isinst     Sandbox.Game.Entities.MyRemoteControl
	brtrue =>  Label24
	ldloc.s    10 (Sandbox.Game.Entities.MyShipController)
	callvirt   virtual Sandbox.Game.Entities.Character.MyCharacter Sandbox.Game.Entities.MyShipController::get_Pilot()
	brfalse => Label25
	ldloc.s    10 (Sandbox.Game.Entities.MyShipController)
	callvirt   virtual Sandbox.Game.Entities.Character.MyCharacter Sandbox.Game.Entities.MyShipController::get_Pilot()
	ldarg.0
	ldfld      VRage.Game.Entity.MyEntity Sandbox.Game.Entities.<>c__DisplayClass103_0::topEntity
	beq =>     Label26
	ldarg.0
	ldfld      Sandbox.Game.Entities.MySafeZone Sandbox.Game.Entities.<>c__DisplayClass103_0::<>4__this
	ldloc.s    10 (Sandbox.Game.Entities.MyShipController)
	callvirt   virtual Sandbox.Game.Entities.Character.MyCharacter Sandbox.Game.Entities.MyShipController::get_Pilot()
	ldarg.0
	ldfld      System.Boolean Sandbox.Game.Entities.<>c__DisplayClass103_0::addedOrRemoved
	call       System.Boolean Sandbox.Game.Entities.MySafeZone::RemoveEntityInternal(VRage.Game.Entity.MyEntity entity, System.Boolean addedOrRemoved)
	brfalse => Label27
	ldarg.0
	ldfld      Sandbox.Game.Entities.MySafeZone Sandbox.Game.Entities.<>c__DisplayClass103_0::<>4__this
	ldloc.s    10 (Sandbox.Game.Entities.MyShipController)
	callvirt   virtual Sandbox.Game.Entities.Character.MyCharacter Sandbox.Game.Entities.MyShipController::get_Pilot()
	callvirt   virtual System.Int64 VRage.Game.Entity.MyEntity::get_EntityId()
	ldarg.0
	ldfld      System.Boolean Sandbox.Game.Entities.<>c__DisplayClass103_0::addedOrRemoved
	call       System.Void Sandbox.Game.Entities.MySafeZone::SendRemovedEntity(System.Int64 entityId, System.Boolean addedOrRemoved)
	Label23
	Label24
	Label25
	Label26
	Label27
	ldloc.s    9 (System.Collections.Generic.IEnumerator`1[Sandbox.Game.Entities.MyShipController])
	callvirt   abstract virtual System.Boolean System.Collections.IEnumerator::MoveNext()
	brtrue =>  Label28
	leave =>   Label29
	leave =>   (autogenerated)
} // end try
.finally
{
	ldloc.s    9 (System.Collections.Generic.IEnumerator`1[Sandbox.Game.Entities.MyShipController])
	brfalse => Label30
	ldloc.s    9 (System.Collections.Generic.IEnumerator`1[Sandbox.Game.Entities.MyShipController])
	callvirt   abstract virtual System.Void System.IDisposable::Dispose()
	Label30
	endfinally
	leave =>   (autogenerated)
} // end handler
Label21
Label22
Label29
ldarg.0
ldfld      VRage.Game.Entity.MyEntity Sandbox.Game.Entities.<>c__DisplayClass103_0::topEntity
ldarg.0
ldfld      Sandbox.Game.Entities.MySafeZone Sandbox.Game.Entities.<>c__DisplayClass103_0::<>4__this
ldftn      System.Void Sandbox.Game.Entities.MySafeZone::entity_OnClose(VRage.Game.Entity.MyEntity obj)
newobj     System.Void System.Action`1<VRage.Game.Entity.MyEntity>::.ctor(System.Object object, System.IntPtr method)
callvirt   System.Void VRage.Game.Entity.MyEntity::remove_OnClose(System.Action`1<VRage.Game.Entity.MyEntity> value)
// end original
Label20
Label31
Label32
ret
DONE