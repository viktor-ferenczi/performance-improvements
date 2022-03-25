// ab1ec6a6
ldarg.1
brtrue.s L0
ldc.i4.0
ret
L0:
ldarg.1
callvirt System.Boolean VRage.Game.Entity.MyEntity::get_MarkedForClose()
brtrue.s L1
ldarg.1
callvirt System.Boolean VRage.Game.Entity.MyEntity::get_Closed()
brfalse.s L2
L1:
ldc.i4.0
ret
L2:
ldarg.0
ldfld System.Collections.Concurrent.ConcurrentDictionary`2<VRage.Game.Entity.MyEntity, System.Int32> Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_notVisibleTargets
ldarg.1
ldloca.s 0 (System.Int32)
callvirt virtual System.Boolean System.Collections.Concurrent.ConcurrentDictionary`2<VRage.Game.Entity.MyEntity, System.Int32>::TryGetValue(VRage.Game.Entity.MyEntity key, System.Int32& value)
brfalse.s L3
ldloc.0
ldc.i4.0
ble.s L4
ldc.i4.0
ret
L3:
L4:
ldarg.1
isinst Sandbox.Game.Entities.MyCubeGrid
dup
stloc.1
brfalse.s L5
ldarg.0
ldloc.1
ldarg.2
ldarg.3
ldarg.s 4
ldarg.s 5
call System.Boolean Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::TestPotentialTargetCubeGrid(Sandbox.Game.Entities.MyCubeGrid grid, VRage.Game.Entity.MyEntity& nearestTarget, System.Double& minDistanceSq, System.Double maxDistance, System.Collections.Generic.List`1<Sandbox.Game.Weapons.MyEntityWithDistSq> outPotentialTargets)
ret
L5:
ldarg.1
isinst Sandbox.Game.Entities.Character.MyCharacter
dup
stloc.2
brfalse.s L6
call static Sandbox.Game.World.MySession Sandbox.Game.World.MySession::get_Static()
ldfld Sandbox.Game.Multiplayer.MyPlayerCollection Sandbox.Game.World.MySession::Players
ldloc.2
callvirt System.Int64 Sandbox.Game.Entities.Character.MyCharacter::GetPlayerIdentityId()
callvirt System.UInt64 Sandbox.Game.Multiplayer.MyPlayerCollection::TryGetSteamId(System.Int64 identityId)
stloc.s 7 (System.UInt64)
ldloc.s 7 (System.UInt64)
brfalse.s L7
call static Sandbox.Game.World.MySession Sandbox.Game.World.MySession::get_Static()
callvirt System.Collections.Generic.Dictionary`2<System.UInt64, Sandbox.Game.World.AdminSettingsEnum> Sandbox.Game.World.MySession::get_RemoteAdminSettings()
ldloc.s 7 (System.UInt64)
ldloca.s 8 (Sandbox.Game.World.AdminSettingsEnum)
callvirt virtual System.Boolean System.Collections.Generic.Dictionary`2<System.UInt64, Sandbox.Game.World.AdminSettingsEnum>::TryGetValue(System.UInt64 key, Sandbox.Game.World.AdminSettingsEnum& value)
brfalse.s L8
ldloc.s 8 (Sandbox.Game.World.AdminSettingsEnum)
box Sandbox.Game.World.AdminSettingsEnum
ldc.i4.8
box Sandbox.Game.World.AdminSettingsEnum
call System.Boolean System.Enum::HasFlag(System.Enum flag)
brfalse.s L9
ldc.i4.0
ret
L6:
L7:
L8:
L9:
ldarg.0
ldarg.1
call System.Boolean Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::IsTarget(VRage.Game.Entity.MyEntity entity)
dup
brfalse.s L10
ldarg.0
ldarg.1
call System.Boolean Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::IsValidTarget(VRage.Game.Entity.MyEntity target)
br.s L11
L10:
ldc.i4.0
L11:
stloc.3
brfalse.s L12
ldloc.3
brtrue.s L13
L12:
ldc.i4.0
ret
L13:
ldc.r4 1
stloc.s 4 (System.Single)
ldarg.1
isinst Sandbox.Game.Entities.MyCubeBlock
dup
stloc.s 5 (Sandbox.Game.Entities.MyCubeBlock)
brfalse.s L14
ldloc.s 5 (Sandbox.Game.Entities.MyCubeBlock)
callvirt Sandbox.Definitions.MyCubeBlockDefinition Sandbox.Game.Entities.MyCubeBlock::get_BlockDefinition()
callvirt System.Single Sandbox.Definitions.MyCubeBlockDefinition::get_PriorityModifier()
stloc.s 4 (System.Single)
ldarg.1
isinst Sandbox.Game.Entities.Cube.MyFunctionalBlock
dup
stloc.s 9 (Sandbox.Game.Entities.Cube.MyFunctionalBlock)
brfalse.s L15
ldloc.s 9 (Sandbox.Game.Entities.Cube.MyFunctionalBlock)
callvirt virtual System.Boolean Sandbox.Game.Entities.MyCubeBlock::get_IsWorking()
brtrue.s L16
ldloc.s 4 (System.Single)
ldloc.s 9 (Sandbox.Game.Entities.Cube.MyFunctionalBlock)
callvirt Sandbox.Definitions.MyFunctionalBlockDefinition Sandbox.Game.Entities.Cube.MyFunctionalBlock::get_BlockDefinition()
callvirt System.Single Sandbox.Definitions.MyCubeBlockDefinition::get_NotWorkingPriorityMultiplier()
mul
stloc.s 4 (System.Single)
L14:
L15:
L16:
ldloc.s 4 (System.Single)
ldc.r4 0
bne.un.s L17
ldc.i4.0
ret
L17:
ldarg.1
callvirt virtual VRage.Game.Components.MyPositionComponentBase VRage.Game.Entity.MyEntity::get_PositionComp()
callvirt VRageMath.Vector3D VRage.Game.Components.MyPositionComponentBase::GetPosition()
ldarg.0
ldfld Sandbox.Game.Entities.Interfaces.IMyTargetingReceiver Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_targetReceiver
callvirt abstract virtual VRageMath.Vector3D Sandbox.Game.Entities.Interfaces.IMyTargetingReceiver::get_EntityPosition()
call static System.Double VRageMath.Vector3D::DistanceSquared(VRageMath.Vector3D value1, VRageMath.Vector3D value2)
stloc.s 6 (System.Double)
ldloc.s 6 (System.Double)
ldarg.s 4
ble.un.s L18
ldc.i4.0
ret
L18:
ldloc.s 6 (System.Double)
ldarg.3
ldind.r8
blt.un.s L19
ldarg.s 5
ldloca.s 10 (Sandbox.Game.Weapons.MyLargeTurretTargetingSystem+MyEntityWithDistSq)
initobj Sandbox.Game.Weapons.MyLargeTurretTargetingSystem+MyEntityWithDistSq
ldloca.s 10 (Sandbox.Game.Weapons.MyLargeTurretTargetingSystem+MyEntityWithDistSq)
ldarg.1
stfld VRage.Game.Entity.MyEntity Sandbox.Game.Weapons.MyEntityWithDistSq::Entity
ldloca.s 10 (Sandbox.Game.Weapons.MyLargeTurretTargetingSystem+MyEntityWithDistSq)
ldloc.s 4 (System.Single)
stfld System.Single Sandbox.Game.Weapons.MyEntityWithDistSq::Priority
ldloca.s 10 (Sandbox.Game.Weapons.MyLargeTurretTargetingSystem+MyEntityWithDistSq)
ldloc.s 6 (System.Double)
call static System.Double System.Math::Sqrt(System.Double d)
stfld System.Double Sandbox.Game.Weapons.MyEntityWithDistSq::Distance
ldloc.s 10 (Sandbox.Game.Weapons.MyLargeTurretTargetingSystem+MyEntityWithDistSq)
callvirt virtual System.Void System.Collections.Generic.List`1<Sandbox.Game.Weapons.MyEntityWithDistSq>::Add(Sandbox.Game.Weapons.MyEntityWithDistSq item)
ldc.i4.0
ret
L19:
ldarg.0
ldarg.1
call System.Boolean Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::IsTargetAimedByOtherTurret(VRage.Game.Entity.MyEntity target)
brtrue.s L20
ldarg.3
ldloc.s 6 (System.Double)
stind.r8
ldarg.2
ldarg.1
stind.ref
ldarg.s 5
ldloca.s 10 (Sandbox.Game.Weapons.MyLargeTurretTargetingSystem+MyEntityWithDistSq)
initobj Sandbox.Game.Weapons.MyLargeTurretTargetingSystem+MyEntityWithDistSq
ldloca.s 10 (Sandbox.Game.Weapons.MyLargeTurretTargetingSystem+MyEntityWithDistSq)
ldarg.1
stfld VRage.Game.Entity.MyEntity Sandbox.Game.Weapons.MyEntityWithDistSq::Entity
ldloca.s 10 (Sandbox.Game.Weapons.MyLargeTurretTargetingSystem+MyEntityWithDistSq)
ldloc.s 4 (System.Single)
stfld System.Single Sandbox.Game.Weapons.MyEntityWithDistSq::Priority
ldloca.s 10 (Sandbox.Game.Weapons.MyLargeTurretTargetingSystem+MyEntityWithDistSq)
ldloc.s 6 (System.Double)
call static System.Double System.Math::Sqrt(System.Double d)
stfld System.Double Sandbox.Game.Weapons.MyEntityWithDistSq::Distance
ldloc.s 10 (Sandbox.Game.Weapons.MyLargeTurretTargetingSystem+MyEntityWithDistSq)
callvirt virtual System.Void System.Collections.Generic.List`1<Sandbox.Game.Weapons.MyEntityWithDistSq>::Add(Sandbox.Game.Weapons.MyEntityWithDistSq item)
ldc.i4.1
ret
L20:
ldc.i4.0
ret
