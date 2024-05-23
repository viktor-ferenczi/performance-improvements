// c1e4183d
newobj System.Void Sandbox.Game.Entities.Character.<>c__DisplayClass469_0::.ctor()
stloc.0
ldloc.0
ldarg.0
stfld Sandbox.Game.Entities.Character.MyCharacter Sandbox.Game.Entities.Character.<>c__DisplayClass469_0::<>4__this
ldarg.0
call System.Boolean Sandbox.Game.Entities.Character.MyCharacter::get_IsDead()
brfalse.s L0
ret
L0:
ldarg.0
call Sandbox.Engine.Physics.MyPhysicsBody Sandbox.Game.Entities.Character.MyCharacter::get_Physics()
brtrue.s L1
ret
L1:
ldarg.0
call Sandbox.Engine.Physics.MyPhysicsBody Sandbox.Game.Entities.Character.MyCharacter::get_Physics()
callvirt Sandbox.Engine.Physics.MyCharacterProxy Sandbox.Engine.Physics.MyPhysicsBody::get_CharacterProxy()
brtrue.s L2
ret
L2:
call static Sandbox.Game.World.MySession Sandbox.Game.World.MySession::get_Static()
brtrue.s L3
ret
L3:
ldarg.1
ldfld Havok.HkCollisionEvent Havok.HkContactPointEvent::Base
stloc.s 23 (Havok.HkCollisionEvent)
ldloca.s 23 (Havok.HkCollisionEvent)
call Havok.HkRigidBody Havok.HkCollisionEvent::get_BodyA()
stloc.1
ldarg.1
ldfld Havok.HkCollisionEvent Havok.HkContactPointEvent::Base
stloc.s 23 (Havok.HkCollisionEvent)
ldloca.s 23 (Havok.HkCollisionEvent)
call Havok.HkRigidBody Havok.HkCollisionEvent::get_BodyB()
stloc.2
ldloc.1
ldnull
call static System.Boolean Havok.HkReferenceObject::op_Equality(Havok.HkReferenceObject objectA, Havok.HkReferenceObject objectB)
brtrue.s L4
ldloc.2
ldnull
call static System.Boolean Havok.HkReferenceObject::op_Equality(Havok.HkReferenceObject objectA, Havok.HkReferenceObject objectB)
brfalse.s L5
L4:
ret
L5:
ldloc.1
callvirt System.Object Havok.HkEntity::get_UserObject()
brfalse.s L6
ldloc.2
callvirt System.Object Havok.HkEntity::get_UserObject()
brtrue.s L7
L6:
ret
L7:
ldloc.0
ldarg.1
ldobj Havok.HkContactPointEvent
ldarg.0
call static VRage.ModAPI.IMyEntity Sandbox.Engine.Physics.MyPhysicsExtensions::GetOtherEntity(Havok.HkContactPointEvent eventInfo, VRage.ModAPI.IMyEntity sourceEntity)
callvirt abstract virtual VRage.Game.Components.MyPhysicsComponentBase VRage.ModAPI.IMyEntity::get_Physics()
isinst Sandbox.Engine.Voxels.MyVoxelPhysicsBody
stfld Sandbox.Engine.Voxels.MyVoxelPhysicsBody Sandbox.Game.Entities.Character.<>c__DisplayClass469_0::otherPhysicsBody
ldloc.0
ldarg.1
ldfld Havok.HkContactPoint Havok.HkContactPointEvent::ContactPoint
stloc.s 24 (Havok.HkContactPoint)
ldloca.s 24 (Havok.HkContactPoint)
call VRageMath.Vector3 Havok.HkContactPoint::get_Position()
stfld VRageMath.Vector3 Sandbox.Game.Entities.Character.<>c__DisplayClass469_0::contactPointPosition
ldloc.0
ldarg.1
ldfld Havok.HkContactPoint Havok.HkContactPointEvent::ContactPoint
stloc.s 24 (Havok.HkContactPoint)
ldloca.s 24 (Havok.HkContactPoint)
call VRageMath.Vector3 Havok.HkContactPoint::get_Normal()
stfld VRageMath.Vector3 Sandbox.Game.Entities.Character.<>c__DisplayClass469_0::contactPointNormal
ldloc.0
ldfld Sandbox.Engine.Voxels.MyVoxelPhysicsBody Sandbox.Game.Entities.Character.<>c__DisplayClass469_0::otherPhysicsBody
brfalse L8
ldsfld Sandbox.MySandboxGame Sandbox.MySandboxGame::Static
ldloc.0
ldftn System.Void Sandbox.Game.Entities.Character.<>c__DisplayClass469_0::<RigidBody_ContactPointCallback>b__0()
newobj System.Void System.Action::.ctor(System.Object object, System.IntPtr method)
ldstr "MyCharacter.RigidBody_ContactPointCallback.TrySpawnWalkingParticles"
ldc.i4.m1
ldc.i4.0
callvirt System.Void Sandbox.MySandboxGame::Invoke(System.Action action, System.String invokerName, System.Int32 startAtFrame, System.Int32 repeatTimes)
ldarg.1
ldobj Havok.HkContactPointEvent
ldarg.0
call static VRage.ModAPI.IMyEntity Sandbox.Engine.Physics.MyPhysicsExtensions::GetOtherEntity(Havok.HkContactPointEvent eventInfo, VRage.ModAPI.IMyEntity sourceEntity)
castclass Sandbox.Game.Entities.MyVoxelBase
stloc.s 25 (Sandbox.Game.Entities.MyVoxelBase)
ldarg.0
ldloc.s 25 (Sandbox.Game.Entities.MyVoxelBase)
call System.Boolean Sandbox.Game.Entities.Character.MyCharacter::CanProcessTrails(Sandbox.Game.Entities.MyVoxelBase otherEntity)
brfalse.s L9
ldarg.0
call Sandbox.Engine.Physics.MyPhysicsBody Sandbox.Game.Entities.Character.MyCharacter::get_Physics()
ldarg.1
ldfld Havok.HkContactPoint Havok.HkContactPointEvent::ContactPoint
stloc.s 24 (Havok.HkContactPoint)
ldloca.s 24 (Havok.HkContactPoint)
call VRageMath.Vector3 Havok.HkContactPoint::get_Position()
callvirt abstract virtual VRageMath.Vector3D VRage.Game.Components.MyPhysicsComponentBase::ClusterToWorld(VRageMath.Vector3 clusterPos)
stloc.s 26 (VRageMath.Vector3D)
ldloc.s 25 (Sandbox.Game.Entities.MyVoxelBase)
ldloca.s 26 (VRageMath.Vector3D)
callvirt VRage.Game.MyVoxelMaterialDefinition Sandbox.Game.Entities.MyVoxelBase::GetMaterialAt(VRageMath.Vector3D& worldPosition)
stloc.s 27 (VRage.Game.MyVoxelMaterialDefinition)
ldloc.s 27 (VRage.Game.MyVoxelMaterialDefinition)
brfalse.s L10
ldloca.s 29 (Sandbox.Game.Entities.Character.MyCharacter+TrailContactProperties)
initobj Sandbox.Game.Entities.Character.MyCharacter+TrailContactProperties
ldloca.s 29 (Sandbox.Game.Entities.Character.MyCharacter+TrailContactProperties)
ldloc.s 25 (Sandbox.Game.Entities.MyVoxelBase)
callvirt virtual System.Int64 VRage.Game.Entity.MyEntity::get_EntityId()
stfld System.Int64 Sandbox.Game.Entities.Character.TrailContactProperties::ContactEntityId
ldloca.s 29 (Sandbox.Game.Entities.Character.MyCharacter+TrailContactProperties)
ldloc.s 26 (VRageMath.Vector3D)
call static VRageMath.Vector3 VRageMath.Vector3D::op_Implicit(VRageMath.Vector3D v)
stfld VRageMath.Vector3 Sandbox.Game.Entities.Character.TrailContactProperties::ContactPosition
ldloca.s 29 (Sandbox.Game.Entities.Character.MyCharacter+TrailContactProperties)
ldloc.s 27 (VRage.Game.MyVoxelMaterialDefinition)
ldflda VRage.Game.MyDefinitionId VRage.Game.MyDefinitionBase::Id
ldfld VRage.Utils.MyStringHash VRage.Game.MyDefinitionId::SubtypeId
stfld VRage.Utils.MyStringHash Sandbox.Game.Entities.Character.TrailContactProperties::VoxelMaterial
ldloca.s 29 (Sandbox.Game.Entities.Character.MyCharacter+TrailContactProperties)
ldloc.s 27 (VRage.Game.MyVoxelMaterialDefinition)
callvirt VRage.Utils.MyStringHash VRage.Game.MyVoxelMaterialDefinition::get_MaterialTypeNameHash()
stfld VRage.Utils.MyStringHash Sandbox.Game.Entities.Character.TrailContactProperties::PhysicalMaterial
ldloc.s 29 (Sandbox.Game.Entities.Character.MyCharacter+TrailContactProperties)
stloc.s 28 (Sandbox.Game.Entities.Character.MyCharacter+TrailContactProperties)
ldarg.0
ldloc.s 28 (Sandbox.Game.Entities.Character.MyCharacter+TrailContactProperties)
call System.Void Sandbox.Game.Entities.Character.MyCharacter::ProcessTrails(Sandbox.Game.Entities.Character.TrailContactProperties contactProperties)
L8:
L9:
L10:
ldloc.1
call static Sandbox.Engine.Physics.MyPhysicsBody Sandbox.Engine.Physics.MyPhysicsExtensions::GetBody(Havok.HkEntity hkEntity)
stloc.3
ldloc.2
call static Sandbox.Engine.Physics.MyPhysicsBody Sandbox.Engine.Physics.MyPhysicsExtensions::GetBody(Havok.HkEntity hkEntity)
stloc.s 4 (Sandbox.Engine.Physics.MyPhysicsBody)
ldc.i4.0
stloc.s 5 (System.Int32)
ldloc.0
ldloc.3
callvirt VRage.ModAPI.IMyEntity VRage.Game.Components.MyPhysicsComponentBase::get_Entity()
isinst VRage.Game.Entity.MyEntity
stfld VRage.Game.Entity.MyEntity Sandbox.Game.Entities.Character.<>c__DisplayClass469_0::other
ldloc.1
stloc.s 6 (Havok.HkRigidBody)
ldarg.1
ldfld Havok.HkContactPoint Havok.HkContactPointEvent::ContactPoint
stloc.s 24 (Havok.HkContactPoint)
ldloca.s 24 (Havok.HkContactPoint)
call VRageMath.Vector3 Havok.HkContactPoint::get_Normal()
stloc.s 7 (VRageMath.Vector3)
ldloc.0
ldfld VRage.Game.Entity.MyEntity Sandbox.Game.Entities.Character.<>c__DisplayClass469_0::other
ldarg.0
bne.un.s L11
ldc.i4.1
stloc.s 5 (System.Int32)
ldloc.0
ldloc.s 4 (Sandbox.Engine.Physics.MyPhysicsBody)
callvirt VRage.ModAPI.IMyEntity VRage.Game.Components.MyPhysicsComponentBase::get_Entity()
isinst VRage.Game.Entity.MyEntity
stfld VRage.Game.Entity.MyEntity Sandbox.Game.Entities.Character.<>c__DisplayClass469_0::other
ldloc.2
stloc.s 6 (Havok.HkRigidBody)
ldloc.s 7 (VRageMath.Vector3)
call static VRageMath.Vector3 VRageMath.Vector3::op_UnaryNegation(VRageMath.Vector3 value)
stloc.s 7 (VRageMath.Vector3)
L11:
ldloc.0
ldfld VRage.Game.Entity.MyEntity Sandbox.Game.Entities.Character.<>c__DisplayClass469_0::other
isinst Sandbox.Game.Entities.Character.MyCharacter
stloc.s 8 (Sandbox.Game.Entities.Character.MyCharacter)
ldloc.s 8 (Sandbox.Game.Entities.Character.MyCharacter)
brfalse.s L12
ldloc.s 8 (Sandbox.Game.Entities.Character.MyCharacter)
callvirt Sandbox.Engine.Physics.MyPhysicsBody Sandbox.Game.Entities.Character.MyCharacter::get_Physics()
brfalse.s L13
ldloc.s 8 (Sandbox.Game.Entities.Character.MyCharacter)
callvirt System.Boolean Sandbox.Game.Entities.Character.MyCharacter::get_IsDead()
brtrue.s L14
ldloc.s 8 (Sandbox.Game.Entities.Character.MyCharacter)
callvirt Sandbox.Engine.Physics.MyPhysicsBody Sandbox.Game.Entities.Character.MyCharacter::get_Physics()
callvirt Sandbox.Engine.Physics.MyCharacterProxy Sandbox.Engine.Physics.MyPhysicsBody::get_CharacterProxy()
brfalse.s L15
ldarg.0
call Sandbox.Engine.Physics.MyPhysicsBody Sandbox.Game.Entities.Character.MyCharacter::get_Physics()
callvirt Sandbox.Engine.Physics.MyCharacterProxy Sandbox.Engine.Physics.MyPhysicsBody::get_CharacterProxy()
callvirt System.Boolean Sandbox.Engine.Physics.MyCharacterProxy::get_Supported()
brfalse.s L16
ldloc.s 8 (Sandbox.Game.Entities.Character.MyCharacter)
callvirt Sandbox.Engine.Physics.MyPhysicsBody Sandbox.Game.Entities.Character.MyCharacter::get_Physics()
callvirt Sandbox.Engine.Physics.MyCharacterProxy Sandbox.Engine.Physics.MyPhysicsBody::get_CharacterProxy()
callvirt System.Boolean Sandbox.Engine.Physics.MyCharacterProxy::get_Supported()
brfalse.s L17
L15:
ret
L12:
L13:
L14:
L16:
L17:
ldloc.0
ldfld VRage.Game.Entity.MyEntity Sandbox.Game.Entities.Character.<>c__DisplayClass469_0::other
isinst Sandbox.Game.Entities.MyCubeGrid
stloc.s 9 (Sandbox.Game.Entities.MyCubeGrid)
ldloc.s 9 (Sandbox.Game.Entities.MyCubeGrid)
brfalse L18
ldarg.0
call System.Boolean Sandbox.Game.Entities.Character.MyCharacter::get_IsOnLadder()
brfalse.s L19
ldarg.1
ldloc.s 5 (System.Int32)
call System.UInt32 Havok.HkContactPointEvent::GetShapeKey(System.Int32 bodyIdx)
stloc.s 30 (System.UInt32)
ldloc.s 30 (System.UInt32)
ldc.i4.m1
ceq
stloc.s 31 (System.Boolean)
ldloc.s 31 (System.Boolean)
brtrue.s L20
ldloc.s 9 (Sandbox.Game.Entities.MyCubeGrid)
callvirt Sandbox.Game.Entities.Cube.MyGridPhysics Sandbox.Game.Entities.MyCubeGrid::get_Physics()
callvirt Sandbox.Game.Entities.Cube.MyGridShape Sandbox.Game.Entities.Cube.MyGridPhysics::get_Shape()
ldloc.s 30 (System.UInt32)
callvirt Sandbox.Game.Entities.Cube.MySlimBlock Sandbox.Game.Entities.Cube.MyGridShape::GetBlockFromShapeKey(System.UInt32 shapeKey)
stloc.s 32 (Sandbox.Game.Entities.Cube.MySlimBlock)
ldloc.s 32 (Sandbox.Game.Entities.Cube.MySlimBlock)
brfalse.s L21
ldloc.s 32 (Sandbox.Game.Entities.Cube.MySlimBlock)
callvirt Sandbox.Game.Entities.MyCubeBlock Sandbox.Game.Entities.Cube.MySlimBlock::get_FatBlock()
isinst Sandbox.Game.Entities.Cube.MyLadder
stloc.s 33 (Sandbox.Game.Entities.Cube.MyLadder)
ldloc.s 33 (Sandbox.Game.Entities.Cube.MyLadder)
brfalse.s L22
ldarg.0
ldloc.s 33 (Sandbox.Game.Entities.Cube.MyLadder)
call System.Boolean Sandbox.Game.Entities.Character.MyCharacter::ShouldCollideWith(Sandbox.Game.Entities.Cube.MyLadder ladder)
ldc.i4.0
ceq
br.s L23
L22:
ldc.i4.0
L23:
stloc.s 31 (System.Boolean)
L20:
L21:
ldloc.s 31 (System.Boolean)
brfalse.s L24
ldarg.1
ldfld Havok.HkContactPointProperties Havok.HkContactPointEvent::ContactProperties
stloc.s 34 (Havok.HkContactPointProperties)
ldloca.s 34 (Havok.HkContactPointProperties)
ldc.i4.1
call System.Void Havok.HkContactPointProperties::set_IsDisabled(System.Boolean value)
L19:
L24:
ldsfld System.Boolean Sandbox.Engine.Utils.MyFakes::ENABLE_REALISTIC_ON_TOUCH
brfalse.s L25
ldarg.0
call Sandbox.Game.Components.MyCharacterSoundComponent Sandbox.Game.Entities.Character.MyCharacter::get_SoundComp()
brfalse.s L26
ldarg.0
call Sandbox.Game.Components.MyCharacterSoundComponent Sandbox.Game.Entities.Character.MyCharacter::get_SoundComp()
ldloc.s 9 (Sandbox.Game.Entities.MyCubeGrid)
callvirt System.Void Sandbox.Game.Components.MyCharacterSoundComponent::UpdateEntityEmitters(Sandbox.Game.Entities.MyCubeGrid cubeGrid)
L18:
L25:
L26:
ldarg.1
call System.Single Havok.HkContactPointEvent::get_SeparatingVelocity()
call static System.Single System.Math::Abs(System.Single value)
ldc.r4 3
bge.un.s L27
ret
L27:
ldarg.0
call Sandbox.Engine.Physics.MyPhysicsBody Sandbox.Game.Entities.Character.MyCharacter::get_Physics()
callvirt virtual VRageMath.Vector3 VRage.Game.Components.MyPhysicsComponentBase::get_LinearVelocity()
stloc.s 10 (VRageMath.Vector3)
ldloc.s 10 (VRageMath.Vector3)
ldarg.0
ldfld VRageMath.Vector3 Sandbox.Game.Entities.Character.MyCharacter::m_previousLinearVelocity
call static VRageMath.Vector3 VRageMath.Vector3::op_Subtraction(VRageMath.Vector3 value1, VRageMath.Vector3 value2)
stloc.s 11 (VRageMath.Vector3)
ldloca.s 11 (VRageMath.Vector3)
call System.Single VRageMath.Vector3::Length()
ldc.r4 10
ble.un.s L28
ret
L28:
ldloc.s 6 (Havok.HkRigidBody)
ldarg.1
ldfld Havok.HkContactPoint Havok.HkContactPointEvent::ContactPoint
stloc.s 24 (Havok.HkContactPoint)
ldloca.s 24 (Havok.HkContactPoint)
call VRageMath.Vector3 Havok.HkContactPoint::get_Position()
callvirt VRageMath.Vector3 Havok.HkRigidBody::GetVelocityAtPoint(VRageMath.Vector3 worldPos)
stloc.s 12 (VRageMath.Vector3)
ldloca.s 10 (VRageMath.Vector3)
call System.Single VRageMath.Vector3::Length()
stloc.s 13 (System.Single)
ldloca.s 12 (VRageMath.Vector3)
call System.Single VRageMath.Vector3::Length()
stloc.s 14 (System.Single)
ldloc.s 13 (System.Single)
ldc.r4 0
bgt.s L29
ldsfld VRageMath.Vector3 VRageMath.Vector3::Zero
br.s L30
L29:
ldloc.s 10 (VRageMath.Vector3)
call static VRageMath.Vector3 VRageMath.Vector3::Normalize(VRageMath.Vector3 value)
L30:
stloc.s 15 (VRageMath.Vector3)
ldloc.s 14 (System.Single)
ldc.r4 0
bgt.s L31
ldsfld VRageMath.Vector3 VRageMath.Vector3::Zero
br.s L32
L31:
ldloc.s 12 (VRageMath.Vector3)
call static VRageMath.Vector3 VRageMath.Vector3::Normalize(VRageMath.Vector3 value)
L32:
stloc.s 16 (VRageMath.Vector3)
ldloc.s 13 (System.Single)
ldc.r4 0
bgt.s L33
ldc.r4 0
br.s L34
L33:
ldloc.s 15 (VRageMath.Vector3)
ldloc.s 7 (VRageMath.Vector3)
call static System.Single VRageMath.Vector3::Dot(VRageMath.Vector3 vector1, VRageMath.Vector3 vector2)
L34:
stloc.s 17 (System.Single)
ldloc.s 14 (System.Single)
ldc.r4 0
bgt.s L35
ldc.r4 0
br.s L36
L35:
ldloc.s 16 (VRageMath.Vector3)
ldloc.s 7 (VRageMath.Vector3)
call static System.Single VRageMath.Vector3::Dot(VRageMath.Vector3 vector1, VRageMath.Vector3 vector2)
neg
L36:
stloc.s 18 (System.Single)
ldloc.s 13 (System.Single)
ldloc.s 17 (System.Single)
mul
stloc.s 13 (System.Single)
ldloc.s 14 (System.Single)
ldloc.s 18 (System.Single)
mul
stloc.s 14 (System.Single)
ldloc.s 13 (System.Single)
ldloc.s 14 (System.Single)
add
ldarg.1
call System.Single Havok.HkContactPointEvent::get_SeparatingVelocity()
call static System.Single System.Math::Abs(System.Single value)
ldc.r4 17
sub
call static System.Single System.Math::Min(System.Single val1, System.Single val2)
stloc.s 19 (System.Single)
ldloc.s 19 (System.Single)
ldc.r4 -8
blt.un L37
ldarg.0
ldfld System.Single Sandbox.Game.Entities.Character.MyCharacter::m_canPlayImpact
ldc.r4 0
bgt.un L38
newobj System.Void Sandbox.Game.Entities.Character.<>c__DisplayClass469_1::.ctor()
ldarg.0
ldc.r4 0.3
stfld System.Single Sandbox.Game.Entities.Character.MyCharacter::m_canPlayImpact
dup
ldarg.1
ldobj Havok.HkContactPointEvent
stfld Havok.HkContactPointEvent Sandbox.Game.Entities.Character.<>c__DisplayClass469_1::hkContactPointEvent
ldftn System.Boolean Sandbox.Game.Entities.Character.<>c__DisplayClass469_1::<RigidBody_ContactPointCallback>b__1()
newobj System.Void System.Func`1<System.Boolean>::.ctor(System.Object object, System.IntPtr method)
stloc.s 35 (System.Func`1[System.Boolean])
ldarg.0
call Sandbox.Engine.Physics.MyPhysicsBody Sandbox.Game.Entities.Character.MyCharacter::get_Physics()
ldarg.1
ldfld Havok.HkContactPoint Havok.HkContactPointEvent::ContactPoint
stloc.s 24 (Havok.HkContactPoint)
ldloca.s 24 (Havok.HkContactPoint)
call VRageMath.Vector3 Havok.HkContactPoint::get_Position()
callvirt abstract virtual VRageMath.Vector3D VRage.Game.Components.MyPhysicsComponentBase::ClusterToWorld(VRageMath.Vector3 clusterPos)
stloc.s 36 (VRageMath.Vector3D)
ldloc.s 4 (Sandbox.Engine.Physics.MyPhysicsBody)
ldloc.s 36 (VRageMath.Vector3D)
ldarg.1
ldfld Havok.HkContactPoint Havok.HkContactPointEvent::ContactPoint
stloc.s 24 (Havok.HkContactPoint)
ldloca.s 24 (Havok.HkContactPoint)
call VRageMath.Vector3 Havok.HkContactPoint::get_Normal()
ldc.r4 0.1
call static VRageMath.Vector3 VRageMath.Vector3::op_Multiply(VRageMath.Vector3 value, System.Single scaleFactor)
call static VRageMath.Vector3D VRageMath.Vector3D::op_Subtraction(VRageMath.Vector3D value1, VRageMath.Vector3 value2)
callvirt virtual VRage.Utils.MyStringHash VRage.Game.Components.MyPhysicsComponentBase::GetMaterialAt(VRageMath.Vector3D worldPos)
stloc.s 37 (VRage.Utils.MyStringHash)
ldarg.1
call System.Single Havok.HkContactPointEvent::get_SeparatingVelocity()
call static System.Single System.Math::Abs(System.Single value)
ldc.r4 15
blt.s L39
ldc.r4 1
br.s L40
L39:
ldc.r4 0.5
ldarg.1
call System.Single Havok.HkContactPointEvent::get_SeparatingVelocity()
call static System.Single System.Math::Abs(System.Single value)
ldc.r4 30
div
add
L40:
stloc.s 38 (System.Single)
ldarg.0
call virtual VRage.Game.Entity.MyEntity Sandbox.Game.Entities.Character.MyCharacter::get_Entity()
callvirt virtual System.Int64 VRage.Game.Entity.MyEntity::get_EntityId()
ldsfld VRage.Utils.MyStringId Sandbox.Game.Entities.Character.MyCharacter::m_stringIdHit
ldloc.s 36 (VRageMath.Vector3D)
ldarg.0
ldfld VRage.Utils.MyStringHash Sandbox.Game.Entities.Character.MyCharacter::m_physicalMaterialHash
ldloc.s 37 (VRage.Utils.MyStringHash)
ldloc.s 38 (System.Single)
ldloc.s 35 (System.Func`1[System.Boolean])
ldnull
ldc.r4 0
call static System.Boolean Sandbox.Game.World.MyAudioComponent::PlayContactSound(System.Int64 entityId, VRage.Utils.MyStringId strID, VRageMath.Vector3D position, VRage.Utils.MyStringHash materialA, VRage.Utils.MyStringHash materialB, System.Single volume, System.Func`1<System.Boolean> canHear, VRage.Game.Entity.MyEntity surfaceEntity, System.Single separatingVelocity)
pop
L37:
L38:
call static System.Boolean Sandbox.Game.Multiplayer.Sync::get_IsServer()
brtrue.s L41
ret
L41:
ldloc.s 19 (System.Single)
ldc.r4 0
bge.un.s L42
ret
L42:
ldarg.0
call Sandbox.Engine.Physics.MyPhysicsBody Sandbox.Game.Entities.Character.MyCharacter::get_Physics()
callvirt abstract virtual System.Single VRage.Game.Components.MyPhysicsComponentBase::get_Mass()
call static System.Single Sandbox.Engine.Physics.MyDestructionHelper::MassFromHavok(System.Single m)
stloc.s 20 (System.Single)
ldloc.s 6 (Havok.HkRigidBody)
callvirt System.Single Havok.HkRigidBody::get_Mass()
call static System.Single Sandbox.Engine.Physics.MyDestructionHelper::MassFromHavok(System.Single m)
stloc.s 21 (System.Single)
ldloc.s 20 (System.Single)
ldloc.s 21 (System.Single)
ble.un.s L43
ldloc.s 6 (Havok.HkRigidBody)
callvirt System.Boolean Havok.HkEntity::get_IsFixedOrKeyframed()
brtrue.s L44
ldloc.s 21 (System.Single)
stloc.s 22 (System.Single)
br.s L45
L43:
L44:
ldc.r4 70
call static System.Single Sandbox.Engine.Physics.MyDestructionHelper::MassToHavok(System.Single m)
stloc.s 22 (System.Single)
ldarg.0
call Sandbox.Engine.Physics.MyPhysicsBody Sandbox.Game.Entities.Character.MyCharacter::get_Physics()
callvirt Sandbox.Engine.Physics.MyCharacterProxy Sandbox.Engine.Physics.MyPhysicsBody::get_CharacterProxy()
callvirt System.Boolean Sandbox.Engine.Physics.MyCharacterProxy::get_Supported()
brfalse.s L46
ldloc.s 6 (Havok.HkRigidBody)
callvirt System.Boolean Havok.HkEntity::get_IsFixedOrKeyframed()
brtrue.s L47
ldloc.s 22 (System.Single)
ldloc.s 12 (VRageMath.Vector3)
call static VRageMath.Vector3 VRageMath.Vector3::Normalize(VRageMath.Vector3 value)
ldarg.0
call Sandbox.Engine.Physics.MyPhysicsBody Sandbox.Game.Entities.Character.MyCharacter::get_Physics()
callvirt Sandbox.Engine.Physics.MyCharacterProxy Sandbox.Engine.Physics.MyPhysicsBody::get_CharacterProxy()
callvirt VRageMath.Vector3 Sandbox.Engine.Physics.MyCharacterProxy::get_SupportNormal()
call static System.Single VRageMath.Vector3::Dot(VRageMath.Vector3 vector1, VRageMath.Vector3 vector2)
call static System.Single System.Math::Abs(System.Single value)
ldloc.s 21 (System.Single)
mul
ldc.r4 10
div
add
stloc.s 22 (System.Single)
L45:
L46:
L47:
ldloc.s 22 (System.Single)
call static System.Single Sandbox.Engine.Physics.MyDestructionHelper::MassFromHavok(System.Single m)
stloc.s 22 (System.Single)
ldloc.0
ldloc.s 22 (System.Single)
ldloc.s 19 (System.Single)
mul
ldloc.s 19 (System.Single)
mul
ldc.r4 2
div
stfld System.Single Sandbox.Game.Entities.Character.<>c__DisplayClass469_0::impact
ldloc.s 14 (System.Single)
ldc.r4 2
ble.un.s L48
ldloc.0
ldloc.0
ldfld System.Single Sandbox.Game.Entities.Character.<>c__DisplayClass469_0::impact
ldc.r4 400
sub
stfld System.Single Sandbox.Game.Entities.Character.<>c__DisplayClass469_0::impact
br.s L49
L48:
ldloc.s 14 (System.Single)
ldc.r4 0
bne.un.s L50
ldloc.0
ldfld System.Single Sandbox.Game.Entities.Character.<>c__DisplayClass469_0::impact
ldc.r4 100
ble.un.s L51
ldloc.0
ldloc.0
ldfld System.Single Sandbox.Game.Entities.Character.<>c__DisplayClass469_0::impact
ldc.r4 80
div
stfld System.Single Sandbox.Game.Entities.Character.<>c__DisplayClass469_0::impact
L49:
L50:
L51:
ldloc.0
ldloc.0
ldfld System.Single Sandbox.Game.Entities.Character.<>c__DisplayClass469_0::impact
ldc.r4 10
div
stfld System.Single Sandbox.Game.Entities.Character.<>c__DisplayClass469_0::impact
ldloc.0
ldfld System.Single Sandbox.Game.Entities.Character.<>c__DisplayClass469_0::impact
ldc.r4 1
blt.un L52
call static System.Boolean Sandbox.Game.Multiplayer.Sync::get_IsServer()
brfalse L53
ldarg.1
ldobj Havok.HkContactPointEvent
ldc.i4.0
call static Sandbox.Engine.Physics.MyPhysicsBody Sandbox.Engine.Physics.MyPhysicsExtensions::GetPhysicsBody(Havok.HkContactPointEvent eventInfo, System.Int32 index)
callvirt VRage.ModAPI.IMyEntity VRage.Game.Components.MyPhysicsComponentBase::get_Entity()
stloc.s 39 (VRage.ModAPI.IMyEntity)
ldloc.s 39 (VRage.ModAPI.IMyEntity)
ldarg.0
bne.un.s L54
ldarg.1
ldobj Havok.HkContactPointEvent
ldc.i4.1
call static Sandbox.Engine.Physics.MyPhysicsBody Sandbox.Engine.Physics.MyPhysicsExtensions::GetPhysicsBody(Havok.HkContactPointEvent eventInfo, System.Int32 index)
callvirt VRage.ModAPI.IMyEntity VRage.Game.Components.MyPhysicsComponentBase::get_Entity()
stloc.s 39 (VRage.ModAPI.IMyEntity)
L54:
ldloc.s 39 (VRage.ModAPI.IMyEntity)
isinst Sandbox.Game.Weapons.MyMissile
dup
stloc.s 40 (Sandbox.Game.Weapons.MyMissile)
brfalse L55
call static Sandbox.Game.World.MySession Sandbox.Game.World.MySession::get_Static()
ldfld VRage.Game.MyObjectBuilder_SessionSettings Sandbox.Game.World.MySession::Settings
ldfld System.Boolean VRage.Game.MyObjectBuilder_SessionSettings::EnableFriendlyFire
brfalse.s L56
ldloc.s 40 (Sandbox.Game.Weapons.MyMissile)
ldarg.0
call Sandbox.Game.World.MyIdentity Sandbox.Game.Entities.Character.MyCharacter::GetIdentity()
callvirt System.Int64 Sandbox.Game.World.MyIdentity::get_IdentityId()
callvirt virtual System.Boolean Sandbox.Game.Weapons.MyMissile::IsCharacterIdFriendly(System.Int64 charId)
brtrue L57
newobj System.Void Sandbox.Game.Entities.Character.<>c__DisplayClass469_2::.ctor()
stloc.s 41 (Sandbox.Game.Entities.Character.MyCharacter+<>c__DisplayClass469_2)
ldloc.s 41 (Sandbox.Game.Entities.Character.MyCharacter+<>c__DisplayClass469_2)
ldloc.0
stfld Sandbox.Game.Entities.Character.<>c__DisplayClass469_0 Sandbox.Game.Entities.Character.<>c__DisplayClass469_2::CS$<>8__locals1
ldloc.s 41 (Sandbox.Game.Entities.Character.MyCharacter+<>c__DisplayClass469_2)
ldloc.s 40 (Sandbox.Game.Weapons.MyMissile)
callvirt virtual VRage.Game.MyDefinitionBase Sandbox.Game.Weapons.MyMissile::get_AmmoDefinition()
ldflda VRage.Game.MyDefinitionId VRage.Game.MyDefinitionBase::Id
ldfld VRage.Utils.MyStringHash VRage.Game.MyDefinitionId::SubtypeId
stfld VRage.Utils.MyStringHash Sandbox.Game.Entities.Character.<>c__DisplayClass469_2::sn
ldsfld Sandbox.MySandboxGame Sandbox.MySandboxGame::Static
ldloc.s 41 (Sandbox.Game.Entities.Character.MyCharacter+<>c__DisplayClass469_2)
ldftn System.Void Sandbox.Game.Entities.Character.<>c__DisplayClass469_2::<RigidBody_ContactPointCallback>b__3()
newobj System.Void System.Action::.ctor(System.Object object, System.IntPtr method)
ldstr "MyCharacter.DoDamage"
ldc.i4.m1
ldc.i4.0
callvirt System.Void Sandbox.MySandboxGame::Invoke(System.Action action, System.String invokerName, System.Int32 startAtFrame, System.Int32 repeatTimes)
ret
L56:
newobj System.Void Sandbox.Game.Entities.Character.<>c__DisplayClass469_3::.ctor()
stloc.s 42 (Sandbox.Game.Entities.Character.MyCharacter+<>c__DisplayClass469_3)
ldloc.s 42 (Sandbox.Game.Entities.Character.MyCharacter+<>c__DisplayClass469_3)
ldloc.0
stfld Sandbox.Game.Entities.Character.<>c__DisplayClass469_0 Sandbox.Game.Entities.Character.<>c__DisplayClass469_3::CS$<>8__locals2
ldloc.s 42 (Sandbox.Game.Entities.Character.MyCharacter+<>c__DisplayClass469_3)
ldloc.s 40 (Sandbox.Game.Weapons.MyMissile)
callvirt virtual VRage.Game.MyDefinitionBase Sandbox.Game.Weapons.MyMissile::get_AmmoDefinition()
ldflda VRage.Game.MyDefinitionId VRage.Game.MyDefinitionBase::Id
ldfld VRage.Utils.MyStringHash VRage.Game.MyDefinitionId::SubtypeId
stfld VRage.Utils.MyStringHash Sandbox.Game.Entities.Character.<>c__DisplayClass469_3::sn
ldsfld Sandbox.MySandboxGame Sandbox.MySandboxGame::Static
ldloc.s 42 (Sandbox.Game.Entities.Character.MyCharacter+<>c__DisplayClass469_3)
ldftn System.Void Sandbox.Game.Entities.Character.<>c__DisplayClass469_3::<RigidBody_ContactPointCallback>b__4()
newobj System.Void System.Action::.ctor(System.Object object, System.IntPtr method)
ldstr "MyCharacter.DoDamage"
ldc.i4.m1
ldc.i4.0
callvirt System.Void Sandbox.MySandboxGame::Invoke(System.Action action, System.String invokerName, System.Int32 startAtFrame, System.Int32 repeatTimes)
ret
L55:
ldsfld Sandbox.MySandboxGame Sandbox.MySandboxGame::Static
ldloc.0
ldftn System.Void Sandbox.Game.Entities.Character.<>c__DisplayClass469_0::<RigidBody_ContactPointCallback>b__2()
newobj System.Void System.Action::.ctor(System.Object object, System.IntPtr method)
ldstr "MyCharacter.DoDamage"
ldc.i4.m1
ldc.i4.0
callvirt System.Void Sandbox.MySandboxGame::Invoke(System.Action action, System.String invokerName, System.Int32 startAtFrame, System.Int32 repeatTimes)
L52:
L53:
L57:
ret
