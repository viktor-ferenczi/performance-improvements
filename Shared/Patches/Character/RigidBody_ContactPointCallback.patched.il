// 7b68e0c9
newobj System.Void Sandbox.Game.Entities.Character.<>c__DisplayClass448_0::.ctor()
stloc.0 NULL
ldloc.0 NULL
ldarg.0 NULL
stfld Sandbox.Game.Entities.Character.MyCharacter Sandbox.Game.Entities.Character.<>c__DisplayClass448_0::<>4__this
ldarg.0 NULL
call System.Boolean Sandbox.Game.Entities.Character.MyCharacter::get_IsDead()
brfalse.s Label0
ret NULL
ldarg.0 NULL [Label0]
call Sandbox.Engine.Physics.MyPhysicsBody Sandbox.Game.Entities.Character.MyCharacter::get_Physics()
brtrue.s Label1
ret NULL
ldarg.0 NULL [Label1]
call Sandbox.Engine.Physics.MyPhysicsBody Sandbox.Game.Entities.Character.MyCharacter::get_Physics()
callvirt Sandbox.Engine.Physics.MyCharacterProxy Sandbox.Engine.Physics.MyPhysicsBody::get_CharacterProxy()
brtrue.s Label2
ret NULL
call static Sandbox.Game.World.MySession Sandbox.Game.World.MySession::get_Static() [Label2]
brtrue.s Label3
ret NULL
ldarg.1 NULL [Label3]
ldfld Havok.HkCollisionEvent Havok.HkContactPointEvent::Base
stloc.s 23 (Havok.HkCollisionEvent)
ldloca.s 23 (Havok.HkCollisionEvent)
call Havok.HkRigidBody Havok.HkCollisionEvent::get_BodyA()
stloc.1 NULL
ldarg.1 NULL
ldfld Havok.HkCollisionEvent Havok.HkContactPointEvent::Base
stloc.s 23 (Havok.HkCollisionEvent)
ldloca.s 23 (Havok.HkCollisionEvent)
call Havok.HkRigidBody Havok.HkCollisionEvent::get_BodyB()
stloc.2 NULL
ldloc.1 NULL
ldnull NULL
call static System.Boolean Havok.HkReferenceObject::op_Equality(Havok.HkReferenceObject objectA, Havok.HkReferenceObject objectB)
brtrue.s Label4
ldloc.2 NULL
ldnull NULL
call static System.Boolean Havok.HkReferenceObject::op_Equality(Havok.HkReferenceObject objectA, Havok.HkReferenceObject objectB)
brfalse.s Label5
ret NULL [Label4]
ldloc.1 NULL [Label5]
callvirt System.Object Havok.HkEntity::get_UserObject()
brfalse.s Label6
ldloc.2 NULL
callvirt System.Object Havok.HkEntity::get_UserObject()
brtrue.s Label7
ret NULL [Label6]
ldloc.0 NULL [Label7]
ldarg.1 NULL
ldobj Havok.HkContactPointEvent
ldarg.0 NULL
call static VRage.ModAPI.IMyEntity Sandbox.Engine.Physics.MyPhysicsExtensions::GetOtherEntity(Havok.HkContactPointEvent eventInfo, VRage.ModAPI.IMyEntity sourceEntity)
callvirt abstract virtual VRage.Game.Components.MyPhysicsComponentBase VRage.ModAPI.IMyEntity::get_Physics()
isinst Sandbox.Engine.Voxels.MyVoxelPhysicsBody
stfld Sandbox.Engine.Voxels.MyVoxelPhysicsBody Sandbox.Game.Entities.Character.<>c__DisplayClass448_0::otherPhysicsBody
ldloc.0 NULL
ldarg.1 NULL
ldfld Havok.HkContactPoint Havok.HkContactPointEvent::ContactPoint
stloc.s 24 (Havok.HkContactPoint)
ldloca.s 24 (Havok.HkContactPoint)
call VRageMath.Vector3 Havok.HkContactPoint::get_Position()
stfld VRageMath.Vector3 Sandbox.Game.Entities.Character.<>c__DisplayClass448_0::contactPointPosition
ldloc.0 NULL
ldarg.1 NULL
ldfld Havok.HkContactPoint Havok.HkContactPointEvent::ContactPoint
stloc.s 24 (Havok.HkContactPoint)
ldloca.s 24 (Havok.HkContactPoint)
call VRageMath.Vector3 Havok.HkContactPoint::get_Normal()
stfld VRageMath.Vector3 Sandbox.Game.Entities.Character.<>c__DisplayClass448_0::contactPointNormal
call static System.Boolean Sandbox.Game.Multiplayer.Sync::get_IsDedicated()
brtrue Label8
ldloc.0 NULL
ldfld Sandbox.Engine.Voxels.MyVoxelPhysicsBody Sandbox.Game.Entities.Character.<>c__DisplayClass448_0::otherPhysicsBody
brfalse Label8
ldsfld Sandbox.MySandboxGame Sandbox.MySandboxGame::Static
ldloc.0 NULL
ldftn System.Void Sandbox.Game.Entities.Character.<>c__DisplayClass448_0::<RigidBody_ContactPointCallback>b__0()
newobj System.Void System.Action::.ctor(System.Object object, System.IntPtr method)
ldstr "MyCharacter.RigidBody_ContactPointCallback.TrySpawnWalkingParticles"
ldc.i4.m1 NULL
ldc.i4.0 NULL
callvirt System.Void Sandbox.MySandboxGame::Invoke(System.Action action, System.String invokerName, System.Int32 startAtFrame, System.Int32 repeatTimes)
ldarg.1 NULL
ldobj Havok.HkContactPointEvent
ldarg.0 NULL
call static VRage.ModAPI.IMyEntity Sandbox.Engine.Physics.MyPhysicsExtensions::GetOtherEntity(Havok.HkContactPointEvent eventInfo, VRage.ModAPI.IMyEntity sourceEntity)
castclass Sandbox.Game.Entities.MyVoxelBase
stloc.s 25 (Sandbox.Game.Entities.MyVoxelBase)
ldarg.0 NULL
ldloc.s 25 (Sandbox.Game.Entities.MyVoxelBase)
call System.Boolean Sandbox.Game.Entities.Character.MyCharacter::CanProcessTrails(Sandbox.Game.Entities.MyVoxelBase otherEntity)
brfalse.s Label9
ldarg.0 NULL
call Sandbox.Engine.Physics.MyPhysicsBody Sandbox.Game.Entities.Character.MyCharacter::get_Physics()
ldarg.1 NULL
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
brfalse.s Label10
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
ldarg.0 NULL
ldloc.s 28 (Sandbox.Game.Entities.Character.MyCharacter+TrailContactProperties)
call System.Void Sandbox.Game.Entities.Character.MyCharacter::ProcessTrails(Sandbox.Game.Entities.Character.TrailContactProperties contactProperties)
ldloc.1 NULL [Label8, Label9, Label10]
call static Sandbox.Engine.Physics.MyPhysicsBody Sandbox.Engine.Physics.MyPhysicsExtensions::GetBody(Havok.HkEntity hkEntity)
stloc.3 NULL
ldloc.2 NULL
call static Sandbox.Engine.Physics.MyPhysicsBody Sandbox.Engine.Physics.MyPhysicsExtensions::GetBody(Havok.HkEntity hkEntity)
stloc.s 4 (Sandbox.Engine.Physics.MyPhysicsBody)
ldc.i4.0 NULL
stloc.s 5 (System.Int32)
ldloc.0 NULL
ldloc.3 NULL
callvirt VRage.ModAPI.IMyEntity VRage.Game.Components.MyPhysicsComponentBase::get_Entity()
isinst VRage.Game.Entity.MyEntity
stfld VRage.Game.Entity.MyEntity Sandbox.Game.Entities.Character.<>c__DisplayClass448_0::other
ldloc.1 NULL
stloc.s 6 (Havok.HkRigidBody)
ldarg.1 NULL
ldfld Havok.HkContactPoint Havok.HkContactPointEvent::ContactPoint
stloc.s 24 (Havok.HkContactPoint)
ldloca.s 24 (Havok.HkContactPoint)
call VRageMath.Vector3 Havok.HkContactPoint::get_Normal()
stloc.s 7 (VRageMath.Vector3)
ldloc.0 NULL
ldfld VRage.Game.Entity.MyEntity Sandbox.Game.Entities.Character.<>c__DisplayClass448_0::other
ldarg.0 NULL
bne.un.s Label11
ldc.i4.1 NULL
stloc.s 5 (System.Int32)
ldloc.0 NULL
ldloc.s 4 (Sandbox.Engine.Physics.MyPhysicsBody)
callvirt VRage.ModAPI.IMyEntity VRage.Game.Components.MyPhysicsComponentBase::get_Entity()
isinst VRage.Game.Entity.MyEntity
stfld VRage.Game.Entity.MyEntity Sandbox.Game.Entities.Character.<>c__DisplayClass448_0::other
ldloc.2 NULL
stloc.s 6 (Havok.HkRigidBody)
ldloc.s 7 (VRageMath.Vector3)
call static VRageMath.Vector3 VRageMath.Vector3::op_UnaryNegation(VRageMath.Vector3 value)
stloc.s 7 (VRageMath.Vector3)
ldloc.0 NULL [Label11]
ldfld VRage.Game.Entity.MyEntity Sandbox.Game.Entities.Character.<>c__DisplayClass448_0::other
isinst Sandbox.Game.Entities.Character.MyCharacter
stloc.s 8 (Sandbox.Game.Entities.Character.MyCharacter)
ldloc.s 8 (Sandbox.Game.Entities.Character.MyCharacter)
brfalse.s Label12
ldloc.s 8 (Sandbox.Game.Entities.Character.MyCharacter)
callvirt Sandbox.Engine.Physics.MyPhysicsBody Sandbox.Game.Entities.Character.MyCharacter::get_Physics()
brfalse.s Label13
ldloc.s 8 (Sandbox.Game.Entities.Character.MyCharacter)
callvirt System.Boolean Sandbox.Game.Entities.Character.MyCharacter::get_IsDead()
brtrue.s Label14
ldloc.s 8 (Sandbox.Game.Entities.Character.MyCharacter)
callvirt Sandbox.Engine.Physics.MyPhysicsBody Sandbox.Game.Entities.Character.MyCharacter::get_Physics()
callvirt Sandbox.Engine.Physics.MyCharacterProxy Sandbox.Engine.Physics.MyPhysicsBody::get_CharacterProxy()
brfalse.s Label15
ldarg.0 NULL
call Sandbox.Engine.Physics.MyPhysicsBody Sandbox.Game.Entities.Character.MyCharacter::get_Physics()
callvirt Sandbox.Engine.Physics.MyCharacterProxy Sandbox.Engine.Physics.MyPhysicsBody::get_CharacterProxy()
callvirt System.Boolean Sandbox.Engine.Physics.MyCharacterProxy::get_Supported()
brfalse.s Label16
ldloc.s 8 (Sandbox.Game.Entities.Character.MyCharacter)
callvirt Sandbox.Engine.Physics.MyPhysicsBody Sandbox.Game.Entities.Character.MyCharacter::get_Physics()
callvirt Sandbox.Engine.Physics.MyCharacterProxy Sandbox.Engine.Physics.MyPhysicsBody::get_CharacterProxy()
callvirt System.Boolean Sandbox.Engine.Physics.MyCharacterProxy::get_Supported()
brfalse.s Label17
ret NULL [Label15]
ldloc.0 NULL [Label12, Label13, Label14, Label16, Label17]
ldfld VRage.Game.Entity.MyEntity Sandbox.Game.Entities.Character.<>c__DisplayClass448_0::other
isinst Sandbox.Game.Entities.MyCubeGrid
stloc.s 9 (Sandbox.Game.Entities.MyCubeGrid)
ldloc.s 9 (Sandbox.Game.Entities.MyCubeGrid)
brfalse Label18
ldarg.0 NULL
call System.Boolean Sandbox.Game.Entities.Character.MyCharacter::get_IsOnLadder()
brfalse.s Label19
ldarg.1 NULL
ldloc.s 5 (System.Int32)
call System.UInt32 Havok.HkContactPointEvent::GetShapeKey(System.Int32 bodyIdx)
stloc.s 30 (System.UInt32)
ldloc.s 30 (System.UInt32)
ldc.i4.m1 NULL
ceq NULL
stloc.s 31 (System.Boolean)
ldloc.s 31 (System.Boolean)
brtrue.s Label20
ldloc.s 9 (Sandbox.Game.Entities.MyCubeGrid)
callvirt Sandbox.Game.Entities.Cube.MyGridPhysics Sandbox.Game.Entities.MyCubeGrid::get_Physics()
callvirt Sandbox.Game.Entities.Cube.MyGridShape Sandbox.Game.Entities.Cube.MyGridPhysics::get_Shape()
ldloc.s 30 (System.UInt32)
callvirt Sandbox.Game.Entities.Cube.MySlimBlock Sandbox.Game.Entities.Cube.MyGridShape::GetBlockFromShapeKey(System.UInt32 shapeKey)
stloc.s 32 (Sandbox.Game.Entities.Cube.MySlimBlock)
ldloc.s 32 (Sandbox.Game.Entities.Cube.MySlimBlock)
brfalse.s Label21
ldloc.s 32 (Sandbox.Game.Entities.Cube.MySlimBlock)
callvirt Sandbox.Game.Entities.MyCubeBlock Sandbox.Game.Entities.Cube.MySlimBlock::get_FatBlock()
isinst Sandbox.Game.Entities.Cube.MyLadder
stloc.s 33 (Sandbox.Game.Entities.Cube.MyLadder)
ldloc.s 33 (Sandbox.Game.Entities.Cube.MyLadder)
brfalse.s Label22
ldarg.0 NULL
ldloc.s 33 (Sandbox.Game.Entities.Cube.MyLadder)
call System.Boolean Sandbox.Game.Entities.Character.MyCharacter::ShouldCollideWith(Sandbox.Game.Entities.Cube.MyLadder ladder)
ldc.i4.0 NULL
ceq NULL
br.s Label23
ldc.i4.0 NULL [Label22]
stloc.s 31 (System.Boolean) [Label23]
ldloc.s 31 (System.Boolean) [Label20, Label21]
brfalse.s Label24
ldarg.1 NULL
ldfld Havok.HkContactPointProperties Havok.HkContactPointEvent::ContactProperties
stloc.s 34 (Havok.HkContactPointProperties)
ldloca.s 34 (Havok.HkContactPointProperties)
ldc.i4.1 NULL
call System.Void Havok.HkContactPointProperties::set_IsDisabled(System.Boolean value)
ldsfld System.Boolean Sandbox.Engine.Utils.MyFakes::ENABLE_REALISTIC_ON_TOUCH [Label19, Label24]
brfalse.s Label25
ldarg.0 NULL
call Sandbox.Game.Components.MyCharacterSoundComponent Sandbox.Game.Entities.Character.MyCharacter::get_SoundComp()
brfalse.s Label26
ldarg.0 NULL
call Sandbox.Game.Components.MyCharacterSoundComponent Sandbox.Game.Entities.Character.MyCharacter::get_SoundComp()
ldloc.s 9 (Sandbox.Game.Entities.MyCubeGrid)
callvirt System.Void Sandbox.Game.Components.MyCharacterSoundComponent::UpdateEntityEmitters(Sandbox.Game.Entities.MyCubeGrid cubeGrid)
ldarg.1 NULL [Label18, Label25, Label26]
call System.Single Havok.HkContactPointEvent::get_SeparatingVelocity()
call static System.Single System.Math::Abs(System.Single value)
ldc.r4 3
bge.un.s Label27
ret NULL
ldarg.0 NULL [Label27]
call Sandbox.Engine.Physics.MyPhysicsBody Sandbox.Game.Entities.Character.MyCharacter::get_Physics()
callvirt virtual VRageMath.Vector3 VRage.Game.Components.MyPhysicsComponentBase::get_LinearVelocity()
stloc.s 10 (VRageMath.Vector3)
ldloc.s 10 (VRageMath.Vector3)
ldarg.0 NULL
ldfld VRageMath.Vector3 Sandbox.Game.Entities.Character.MyCharacter::m_previousLinearVelocity
call static VRageMath.Vector3 VRageMath.Vector3::op_Subtraction(VRageMath.Vector3 value1, VRageMath.Vector3 value2)
stloc.s 11 (VRageMath.Vector3)
ldloca.s 11 (VRageMath.Vector3)
call System.Single VRageMath.Vector3::Length()
ldc.r4 10
ble.un.s Label28
ret NULL
ldloc.s 6 (Havok.HkRigidBody) [Label28]
ldarg.1 NULL
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
bgt.s Label29
ldsfld VRageMath.Vector3 VRageMath.Vector3::Zero
br.s Label30
ldloc.s 10 (VRageMath.Vector3) [Label29]
call static VRageMath.Vector3 VRageMath.Vector3::Normalize(VRageMath.Vector3 value)
stloc.s 15 (VRageMath.Vector3) [Label30]
ldloc.s 14 (System.Single)
ldc.r4 0
bgt.s Label31
ldsfld VRageMath.Vector3 VRageMath.Vector3::Zero
br.s Label32
ldloc.s 12 (VRageMath.Vector3) [Label31]
call static VRageMath.Vector3 VRageMath.Vector3::Normalize(VRageMath.Vector3 value)
stloc.s 16 (VRageMath.Vector3) [Label32]
ldloc.s 13 (System.Single)
ldc.r4 0
bgt.s Label33
ldc.r4 0
br.s Label34
ldloc.s 15 (VRageMath.Vector3) [Label33]
ldloc.s 7 (VRageMath.Vector3)
call static System.Single VRageMath.Vector3::Dot(VRageMath.Vector3 vector1, VRageMath.Vector3 vector2)
stloc.s 17 (System.Single) [Label34]
ldloc.s 14 (System.Single)
ldc.r4 0
bgt.s Label35
ldc.r4 0
br.s Label36
ldloc.s 16 (VRageMath.Vector3) [Label35]
ldloc.s 7 (VRageMath.Vector3)
call static System.Single VRageMath.Vector3::Dot(VRageMath.Vector3 vector1, VRageMath.Vector3 vector2)
neg NULL
stloc.s 18 (System.Single) [Label36]
ldloc.s 13 (System.Single)
ldloc.s 17 (System.Single)
mul NULL
stloc.s 13 (System.Single)
ldloc.s 14 (System.Single)
ldloc.s 18 (System.Single)
mul NULL
stloc.s 14 (System.Single)
ldloc.s 13 (System.Single)
ldloc.s 14 (System.Single)
add NULL
ldarg.1 NULL
call System.Single Havok.HkContactPointEvent::get_SeparatingVelocity()
call static System.Single System.Math::Abs(System.Single value)
ldc.r4 17
sub NULL
call static System.Single System.Math::Min(System.Single val1, System.Single val2)
stloc.s 19 (System.Single)
call static System.Boolean Sandbox.Game.Multiplayer.Sync::get_IsDedicated()
brtrue Label37
ldloc.s 19 (System.Single)
ldc.r4 -8
blt.un Label37
ldarg.0 NULL
ldfld System.Single Sandbox.Game.Entities.Character.MyCharacter::m_canPlayImpact
ldc.r4 0
bgt.un Label38
newobj System.Void Sandbox.Game.Entities.Character.<>c__DisplayClass448_1::.ctor()
ldarg.0 NULL
ldc.r4 0.3
stfld System.Single Sandbox.Game.Entities.Character.MyCharacter::m_canPlayImpact
dup NULL
ldarg.1 NULL
ldobj Havok.HkContactPointEvent
stfld Havok.HkContactPointEvent Sandbox.Game.Entities.Character.<>c__DisplayClass448_1::hkContactPointEvent
ldftn System.Boolean Sandbox.Game.Entities.Character.<>c__DisplayClass448_1::<RigidBody_ContactPointCallback>b__1()
newobj System.Void System.Func`1<System.Boolean>::.ctor(System.Object object, System.IntPtr method)
stloc.s 35 (System.Func`1[System.Boolean])
ldarg.0 NULL
call Sandbox.Engine.Physics.MyPhysicsBody Sandbox.Game.Entities.Character.MyCharacter::get_Physics()
ldarg.1 NULL
ldfld Havok.HkContactPoint Havok.HkContactPointEvent::ContactPoint
stloc.s 24 (Havok.HkContactPoint)
ldloca.s 24 (Havok.HkContactPoint)
call VRageMath.Vector3 Havok.HkContactPoint::get_Position()
callvirt abstract virtual VRageMath.Vector3D VRage.Game.Components.MyPhysicsComponentBase::ClusterToWorld(VRageMath.Vector3 clusterPos)
stloc.s 36 (VRageMath.Vector3D)
ldloc.s 4 (Sandbox.Engine.Physics.MyPhysicsBody)
ldloc.s 36 (VRageMath.Vector3D)
ldarg.1 NULL
ldfld Havok.HkContactPoint Havok.HkContactPointEvent::ContactPoint
stloc.s 24 (Havok.HkContactPoint)
ldloca.s 24 (Havok.HkContactPoint)
call VRageMath.Vector3 Havok.HkContactPoint::get_Normal()
ldc.r4 0.1
call static VRageMath.Vector3 VRageMath.Vector3::op_Multiply(VRageMath.Vector3 value, System.Single scaleFactor)
call static VRageMath.Vector3D VRageMath.Vector3D::op_Subtraction(VRageMath.Vector3D value1, VRageMath.Vector3 value2)
callvirt virtual VRage.Utils.MyStringHash VRage.Game.Components.MyPhysicsComponentBase::GetMaterialAt(VRageMath.Vector3D worldPos)
stloc.s 37 (VRage.Utils.MyStringHash)
ldarg.1 NULL
call System.Single Havok.HkContactPointEvent::get_SeparatingVelocity()
call static System.Single System.Math::Abs(System.Single value)
ldc.r4 15
blt.s Label39
ldc.r4 1
br.s Label40
ldc.r4 0.5 [Label39]
ldarg.1 NULL
call System.Single Havok.HkContactPointEvent::get_SeparatingVelocity()
call static System.Single System.Math::Abs(System.Single value)
ldc.r4 30
div NULL
add NULL
stloc.s 38 (System.Single) [Label40]
ldarg.0 NULL
call virtual VRage.Game.Entity.MyEntity Sandbox.Game.Entities.Character.MyCharacter::get_Entity()
callvirt virtual System.Int64 VRage.Game.Entity.MyEntity::get_EntityId()
ldsfld VRage.Utils.MyStringId Sandbox.Game.Entities.Character.MyCharacter::m_stringIdHit
ldloc.s 36 (VRageMath.Vector3D)
ldarg.0 NULL
ldfld VRage.Utils.MyStringHash Sandbox.Game.Entities.Character.MyCharacter::m_physicalMaterialHash
ldloc.s 37 (VRage.Utils.MyStringHash)
ldloc.s 38 (System.Single)
ldloc.s 35 (System.Func`1[System.Boolean])
ldnull NULL
ldc.r4 0
call static System.Boolean Sandbox.Game.World.MyAudioComponent::PlayContactSound(System.Int64 entityId, VRage.Utils.MyStringId strID, VRageMath.Vector3D position, VRage.Utils.MyStringHash materialA, VRage.Utils.MyStringHash materialB, System.Single volume, System.Func`1<System.Boolean> canHear, VRage.Game.Entity.MyEntity surfaceEntity, System.Single separatingVelocity)
pop NULL
call static System.Boolean Sandbox.Game.Multiplayer.Sync::get_IsServer() [Label37, Label38]
brtrue.s Label41
ret NULL
ldloc.s 19 (System.Single) [Label41]
ldc.r4 0
bge.un.s Label42
ret NULL
ldarg.0 NULL [Label42]
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
ble.un.s Label43
ldloc.s 6 (Havok.HkRigidBody)
callvirt System.Boolean Havok.HkEntity::get_IsFixedOrKeyframed()
brtrue.s Label44
ldloc.s 21 (System.Single)
stloc.s 22 (System.Single)
br.s Label45
ldc.r4 70 [Label43, Label44]
call static System.Single Sandbox.Engine.Physics.MyDestructionHelper::MassToHavok(System.Single m)
stloc.s 22 (System.Single)
ldarg.0 NULL
call Sandbox.Engine.Physics.MyPhysicsBody Sandbox.Game.Entities.Character.MyCharacter::get_Physics()
callvirt Sandbox.Engine.Physics.MyCharacterProxy Sandbox.Engine.Physics.MyPhysicsBody::get_CharacterProxy()
callvirt System.Boolean Sandbox.Engine.Physics.MyCharacterProxy::get_Supported()
brfalse.s Label46
ldloc.s 6 (Havok.HkRigidBody)
callvirt System.Boolean Havok.HkEntity::get_IsFixedOrKeyframed()
brtrue.s Label47
ldloc.s 22 (System.Single)
ldloc.s 12 (VRageMath.Vector3)
call static VRageMath.Vector3 VRageMath.Vector3::Normalize(VRageMath.Vector3 value)
ldarg.0 NULL
call Sandbox.Engine.Physics.MyPhysicsBody Sandbox.Game.Entities.Character.MyCharacter::get_Physics()
callvirt Sandbox.Engine.Physics.MyCharacterProxy Sandbox.Engine.Physics.MyPhysicsBody::get_CharacterProxy()
callvirt VRageMath.Vector3 Sandbox.Engine.Physics.MyCharacterProxy::get_SupportNormal()
call static System.Single VRageMath.Vector3::Dot(VRageMath.Vector3 vector1, VRageMath.Vector3 vector2)
call static System.Single System.Math::Abs(System.Single value)
ldloc.s 21 (System.Single)
mul NULL
ldc.r4 10
div NULL
add NULL
stloc.s 22 (System.Single)
ldloc.s 22 (System.Single) [Label45, Label46, Label47]
call static System.Single Sandbox.Engine.Physics.MyDestructionHelper::MassFromHavok(System.Single m)
stloc.s 22 (System.Single)
ldloc.0 NULL
ldloc.s 22 (System.Single)
ldloc.s 19 (System.Single)
mul NULL
ldloc.s 19 (System.Single)
mul NULL
ldc.r4 2
div NULL
stfld System.Single Sandbox.Game.Entities.Character.<>c__DisplayClass448_0::impact
ldloc.s 14 (System.Single)
ldc.r4 2
ble.un.s Label48
ldloc.0 NULL
ldloc.0 NULL
ldfld System.Single Sandbox.Game.Entities.Character.<>c__DisplayClass448_0::impact
ldc.r4 400
sub NULL
stfld System.Single Sandbox.Game.Entities.Character.<>c__DisplayClass448_0::impact
br.s Label49
ldloc.s 14 (System.Single) [Label48]
ldc.r4 0
bne.un.s Label50
ldloc.0 NULL
ldfld System.Single Sandbox.Game.Entities.Character.<>c__DisplayClass448_0::impact
ldc.r4 100
ble.un.s Label51
ldloc.0 NULL
ldloc.0 NULL
ldfld System.Single Sandbox.Game.Entities.Character.<>c__DisplayClass448_0::impact
ldc.r4 80
div NULL
stfld System.Single Sandbox.Game.Entities.Character.<>c__DisplayClass448_0::impact
ldloc.0 NULL [Label49, Label50, Label51]
ldloc.0 NULL
ldfld System.Single Sandbox.Game.Entities.Character.<>c__DisplayClass448_0::impact
ldc.r4 10
div NULL
stfld System.Single Sandbox.Game.Entities.Character.<>c__DisplayClass448_0::impact
ldloc.0 NULL
ldfld System.Single Sandbox.Game.Entities.Character.<>c__DisplayClass448_0::impact
ldc.r4 1
blt.un Label52
call static System.Boolean Sandbox.Game.Multiplayer.Sync::get_IsServer()
brfalse Label53
ldarg.1 NULL
ldobj Havok.HkContactPointEvent
ldc.i4.0 NULL
call static Sandbox.Engine.Physics.MyPhysicsBody Sandbox.Engine.Physics.MyPhysicsExtensions::GetPhysicsBody(Havok.HkContactPointEvent eventInfo, System.Int32 index)
callvirt VRage.ModAPI.IMyEntity VRage.Game.Components.MyPhysicsComponentBase::get_Entity()
stloc.s 39 (VRage.ModAPI.IMyEntity)
ldloc.s 39 (VRage.ModAPI.IMyEntity)
ldarg.0 NULL
bne.un.s Label54
ldarg.1 NULL
ldobj Havok.HkContactPointEvent
ldc.i4.1 NULL
call static Sandbox.Engine.Physics.MyPhysicsBody Sandbox.Engine.Physics.MyPhysicsExtensions::GetPhysicsBody(Havok.HkContactPointEvent eventInfo, System.Int32 index)
callvirt VRage.ModAPI.IMyEntity VRage.Game.Components.MyPhysicsComponentBase::get_Entity()
stloc.s 39 (VRage.ModAPI.IMyEntity)
call static Sandbox.Game.World.MySession Sandbox.Game.World.MySession::get_Static() [Label54]
ldfld VRage.Game.MyObjectBuilder_SessionSettings Sandbox.Game.World.MySession::Settings
ldfld System.Boolean VRage.Game.MyObjectBuilder_SessionSettings::EnableFriendlyFire
brtrue.s Label55
ldloc.s 39 (VRage.ModAPI.IMyEntity)
isinst Sandbox.Game.Weapons.MyMissile
dup NULL
stloc.s 40 (Sandbox.Game.Weapons.MyMissile)
brfalse.s Label56
ldloc.s 40 (Sandbox.Game.Weapons.MyMissile)
ldarg.0 NULL
call Sandbox.Game.World.MyIdentity Sandbox.Game.Entities.Character.MyCharacter::GetIdentity()
callvirt System.Int64 Sandbox.Game.World.MyIdentity::get_IdentityId()
callvirt virtual System.Boolean Sandbox.Game.Weapons.MyMissile::IsCharacterIdFriendly(System.Int64 charId)
brtrue.s Label57
ldsfld Sandbox.MySandboxGame Sandbox.MySandboxGame::Static
ldloc.0 NULL
ldftn System.Void Sandbox.Game.Entities.Character.<>c__DisplayClass448_0::<RigidBody_ContactPointCallback>b__2()
newobj System.Void System.Action::.ctor(System.Object object, System.IntPtr method)
ldstr "MyCharacter.DoDamage"
ldc.i4.m1 NULL
ldc.i4.0 NULL
callvirt System.Void Sandbox.MySandboxGame::Invoke(System.Action action, System.String invokerName, System.Int32 startAtFrame, System.Int32 repeatTimes)
ret NULL
ldsfld Sandbox.MySandboxGame Sandbox.MySandboxGame::Static [Label55, Label56]
ldloc.0 NULL
ldftn System.Void Sandbox.Game.Entities.Character.<>c__DisplayClass448_0::<RigidBody_ContactPointCallback>b__3()
newobj System.Void System.Action::.ctor(System.Object object, System.IntPtr method)
ldstr "MyCharacter.DoDamage"
ldc.i4.m1 NULL
ldc.i4.0 NULL
callvirt System.Void Sandbox.MySandboxGame::Invoke(System.Action action, System.String invokerName, System.Int32 startAtFrame, System.Int32 repeatTimes)
ret NULL [Label52, Label53, Label57]
