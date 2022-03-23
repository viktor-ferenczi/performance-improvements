// a3a2ba48
ldarg.1 NULL
brtrue.s Label0
ldc.i4.0 NULL
ret NULL
ldarg.1 NULL [Label0]
callvirt System.Boolean VRage.Game.Entity.MyEntity::get_MarkedForClose()
brtrue.s Label1
ldarg.1 NULL
callvirt System.Boolean VRage.Game.Entity.MyEntity::get_Closed()
brfalse.s Label2
ldc.i4.0 NULL [Label1]
ret NULL
ldarg.0 NULL [Label2]
ldfld Sandbox.Game.Entities.Interfaces.IMyTargetingReceiver Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_targetReceiver
ldarg.1 NULL
call static System.Boolean Shared.Patches.MyLargeTurretTargetingSystemPatch::IsTargetCachedAsNotVisible(Sandbox.Game.Entities.Interfaces.IMyTargetingReceiver targetReceiver, VRage.Game.Entity.MyEntity target)
brfalse Label4
ldc.i4.0 NULL
ret NULL
ldarg.1 NULL [Label3, Label4]
isinst Sandbox.Game.Entities.MyCubeGrid
dup NULL
stloc.1 NULL
brfalse.s Label5
ldarg.0 NULL
ldloc.1 NULL
ldarg.2 NULL
ldarg.3 NULL
ldarg.s 4
ldarg.s 5
call System.Boolean Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::TestPotentialTargetCubeGrid(Sandbox.Game.Entities.MyCubeGrid grid, VRage.Game.Entity.MyEntity& nearestTarget, System.Double& minDistanceSq, System.Double maxDistance, System.Collections.Generic.List`1<Sandbox.Game.Weapons.MyEntityWithDistSq> outPotentialTargets)
ret NULL
ldarg.1 NULL [Label5]
isinst Sandbox.Game.Entities.Character.MyCharacter
dup NULL
stloc.2 NULL
brfalse.s Label6
call static Sandbox.Game.World.MySession Sandbox.Game.World.MySession::get_Static()
ldfld Sandbox.Game.Multiplayer.MyPlayerCollection Sandbox.Game.World.MySession::Players
ldloc.2 NULL
callvirt System.Int64 Sandbox.Game.Entities.Character.MyCharacter::GetPlayerIdentityId()
callvirt System.UInt64 Sandbox.Game.Multiplayer.MyPlayerCollection::TryGetSteamId(System.Int64 identityId)
stloc.s 7 (System.UInt64)
ldloc.s 7 (System.UInt64)
brfalse.s Label7
call static Sandbox.Game.World.MySession Sandbox.Game.World.MySession::get_Static()
callvirt System.Collections.Generic.Dictionary`2<System.UInt64, Sandbox.Game.World.AdminSettingsEnum> Sandbox.Game.World.MySession::get_RemoteAdminSettings()
ldloc.s 7 (System.UInt64)
ldloca.s 8 (Sandbox.Game.World.AdminSettingsEnum)
callvirt virtual System.Boolean System.Collections.Generic.Dictionary`2<System.UInt64, Sandbox.Game.World.AdminSettingsEnum>::TryGetValue(System.UInt64 key, Sandbox.Game.World.AdminSettingsEnum& value)
brfalse.s Label8
ldloc.s 8 (Sandbox.Game.World.AdminSettingsEnum)
box Sandbox.Game.World.AdminSettingsEnum
ldc.i4.8 NULL
box Sandbox.Game.World.AdminSettingsEnum
call System.Boolean System.Enum::HasFlag(System.Enum flag)
brfalse.s Label9
ldc.i4.0 NULL
ret NULL
ldarg.0 NULL [Label6, Label7, Label8, Label9]
ldarg.1 NULL
call System.Boolean Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::IsTarget(VRage.Game.Entity.MyEntity entity)
dup NULL
brfalse.s Label10
ldarg.0 NULL
ldarg.1 NULL
call System.Boolean Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::IsValidTarget(VRage.Game.Entity.MyEntity target)
br.s Label11
ldc.i4.0 NULL [Label10]
stloc.3 NULL [Label11]
brfalse.s Label12
ldloc.3 NULL
brtrue.s Label13
ldc.i4.0 NULL [Label12]
ret NULL
ldc.r4 1 [Label13]
stloc.s 4 (System.Single)
ldarg.1 NULL
isinst Sandbox.Game.Entities.MyCubeBlock
dup NULL
stloc.s 5 (Sandbox.Game.Entities.MyCubeBlock)
brfalse.s Label14
ldloc.s 5 (Sandbox.Game.Entities.MyCubeBlock)
callvirt Sandbox.Definitions.MyCubeBlockDefinition Sandbox.Game.Entities.MyCubeBlock::get_BlockDefinition()
callvirt System.Single Sandbox.Definitions.MyCubeBlockDefinition::get_PriorityModifier()
stloc.s 4 (System.Single)
ldarg.1 NULL
isinst Sandbox.Game.Entities.Cube.MyFunctionalBlock
dup NULL
stloc.s 9 (Sandbox.Game.Entities.Cube.MyFunctionalBlock)
brfalse.s Label15
ldloc.s 9 (Sandbox.Game.Entities.Cube.MyFunctionalBlock)
callvirt virtual System.Boolean Sandbox.Game.Entities.MyCubeBlock::get_IsWorking()
brtrue.s Label16
ldloc.s 4 (System.Single)
ldloc.s 9 (Sandbox.Game.Entities.Cube.MyFunctionalBlock)
callvirt Sandbox.Definitions.MyFunctionalBlockDefinition Sandbox.Game.Entities.Cube.MyFunctionalBlock::get_BlockDefinition()
callvirt System.Single Sandbox.Definitions.MyCubeBlockDefinition::get_NotWorkingPriorityMultiplier()
mul NULL
stloc.s 4 (System.Single)
ldloc.s 4 (System.Single) [Label14, Label15, Label16]
ldc.r4 0
bne.un.s Label17
ldc.i4.0 NULL
ret NULL
ldarg.1 NULL [Label17]
callvirt virtual VRage.Game.Components.MyPositionComponentBase VRage.Game.Entity.MyEntity::get_PositionComp()
callvirt VRageMath.Vector3D VRage.Game.Components.MyPositionComponentBase::GetPosition()
ldarg.0 NULL
ldfld Sandbox.Game.Entities.Interfaces.IMyTargetingReceiver Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_targetReceiver
callvirt abstract virtual VRageMath.Vector3D Sandbox.Game.Entities.Interfaces.IMyTargetingReceiver::get_EntityPosition()
call static System.Double VRageMath.Vector3D::DistanceSquared(VRageMath.Vector3D value1, VRageMath.Vector3D value2)
stloc.s 6 (System.Double)
ldloc.s 6 (System.Double)
ldarg.s 4
ble.un.s Label18
ldc.i4.0 NULL
ret NULL
ldloc.s 6 (System.Double) [Label18]
ldarg.3 NULL
ldind.r8 NULL
blt.un.s Label19
ldarg.s 5
ldloca.s 10 (Sandbox.Game.Weapons.MyLargeTurretTargetingSystem+MyEntityWithDistSq)
initobj Sandbox.Game.Weapons.MyLargeTurretTargetingSystem+MyEntityWithDistSq
ldloca.s 10 (Sandbox.Game.Weapons.MyLargeTurretTargetingSystem+MyEntityWithDistSq)
ldarg.1 NULL
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
ldc.i4.0 NULL
ret NULL
ldarg.0 NULL [Label19]
ldarg.1 NULL
call System.Boolean Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::IsTargetAimedByOtherTurret(VRage.Game.Entity.MyEntity target)
brtrue.s Label20
ldarg.3 NULL
ldloc.s 6 (System.Double)
stind.r8 NULL
ldarg.2 NULL
ldarg.1 NULL
stind.ref NULL
ldarg.s 5
ldloca.s 10 (Sandbox.Game.Weapons.MyLargeTurretTargetingSystem+MyEntityWithDistSq)
initobj Sandbox.Game.Weapons.MyLargeTurretTargetingSystem+MyEntityWithDistSq
ldloca.s 10 (Sandbox.Game.Weapons.MyLargeTurretTargetingSystem+MyEntityWithDistSq)
ldarg.1 NULL
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
ldc.i4.1 NULL
ret NULL
ldc.i4.0 NULL [Label20]
ret NULL
