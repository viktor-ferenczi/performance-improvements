### Patch: System.Boolean Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::TestPotentialTarget(VRage.Game.Entity.MyEntity target, VRage.Game.Entity.MyEntity& nearestTarget, System.Double& minDistanceSq, System.Double maxDistance, System.Collections.Generic.List`1<Sandbox.Game.Weapons.MyEntityWithDistSq> outPotentialTargets)
### Replacement: static System.Boolean Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::Sandbox.Game.Weapons.MyLargeTurretTargetingSystem.TestPotentialTarget_Patch0(Sandbox.Game.Weapons.MyLargeTurretTargetingSystem this, VRage.Game.Entity.MyEntity target, VRage.Game.Entity.MyEntity& nearestTarget, System.Double& minDistanceSq, System.Double maxDistance, System.Collections.Generic.List`1<Sandbox.Game.Weapons.MyEntityWithDistSq> outPotentialTargets)
Local var 0: System.Int32
Local var 1: Sandbox.Game.Entities.MyCubeGrid
Local var 2: Sandbox.Game.Entities.Character.MyCharacter
Local var 3: System.Boolean
Local var 4: System.Single
Local var 5: Sandbox.Game.Entities.MyCubeBlock
Local var 6: System.Double
Local var 7: System.UInt64
Local var 8: Sandbox.Game.World.AdminSettingsEnum
Local var 9: Sandbox.Game.Entities.Cube.MyFunctionalBlock
Local var 10: Sandbox.Game.Weapons.MyLargeTurretTargetingSystem/MyEntityWithDistSq
// start original
ldarg.1
brtrue =>  Label0
ldc.i4.0
br =>      Label21
Label0
ldarg.1
callvirt   System.Boolean VRage.Game.Entity.MyEntity::get_MarkedForClose()
brtrue =>  Label1
ldarg.1
callvirt   System.Boolean VRage.Game.Entity.MyEntity::get_Closed()
brfalse => Label2
Label1
ldc.i4.0
br =>      Label22
Label2
ldarg.0
ldfld      Sandbox.Game.Entities.Interfaces.IMyTargetingReceiver Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_targetReceiver
ldarg.1
call       static System.Boolean Shared.Patches.MyLargeTurretTargetingSystemPatch::IsTargetCachedAsNotVisible(Sandbox.Game.Entities.Interfaces.IMyTargetingReceiver targetReceiver, VRage.Game.Entity.MyEntity target)
brfalse => Label4
ldc.i4.0
br =>      Label23
Label3
Label4
ldarg.1
isinst     Sandbox.Game.Entities.MyCubeGrid
dup
stloc.1
brfalse => Label5
ldarg.0
ldloc.1
ldarg.2
ldarg.3
ldarg.s    4
ldarg.s    5
call       System.Boolean Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::TestPotentialTargetCubeGrid(Sandbox.Game.Entities.MyCubeGrid grid, VRage.Game.Entity.MyEntity& nearestTarget, System.Double& minDistanceSq, System.Double maxDistance, System.Collections.Generic.List`1<Sandbox.Game.Weapons.MyEntityWithDistSq> outPotentialTargets)
br =>      Label24
Label5
ldarg.1
isinst     Sandbox.Game.Entities.Character.MyCharacter
dup
stloc.2
brfalse => Label6
call       static Sandbox.Game.World.MySession Sandbox.Game.World.MySession::get_Static()
ldfld      Sandbox.Game.Multiplayer.MyPlayerCollection Sandbox.Game.World.MySession::Players
ldloc.2
callvirt   System.Int64 Sandbox.Game.Entities.Character.MyCharacter::GetPlayerIdentityId()
callvirt   System.UInt64 Sandbox.Game.Multiplayer.MyPlayerCollection::TryGetSteamId(System.Int64 identityId)
stloc.s    7 (System.UInt64)
ldloc.s    7 (System.UInt64)
brfalse => Label7
call       static Sandbox.Game.World.MySession Sandbox.Game.World.MySession::get_Static()
callvirt   System.Collections.Generic.Dictionary`2<System.UInt64, Sandbox.Game.World.AdminSettingsEnum> Sandbox.Game.World.MySession::get_RemoteAdminSettings()
ldloc.s    7 (System.UInt64)
ldloca.s   8 (Sandbox.Game.World.AdminSettingsEnum)
callvirt   virtual System.Boolean System.Collections.Generic.Dictionary`2<System.UInt64, Sandbox.Game.World.AdminSettingsEnum>::TryGetValue(System.UInt64 key, Sandbox.Game.World.AdminSettingsEnum& value)
brfalse => Label8
ldloc.s    8 (Sandbox.Game.World.AdminSettingsEnum)
box        Sandbox.Game.World.AdminSettingsEnum
ldc.i4.8
box        Sandbox.Game.World.AdminSettingsEnum
call       System.Boolean System.Enum::HasFlag(System.Enum flag)
brfalse => Label9
ldc.i4.0
br =>      Label25
Label6
Label7
Label8
Label9
ldarg.0
ldarg.1
call       System.Boolean Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::IsTarget(VRage.Game.Entity.MyEntity entity)
dup
brfalse => Label10
ldarg.0
ldarg.1
call       System.Boolean Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::IsValidTarget(VRage.Game.Entity.MyEntity target)
br =>      Label11
Label10
ldc.i4.0
Label11
stloc.3
brfalse => Label12
ldloc.3
brtrue =>  Label13
Label12
ldc.i4.0
br =>      Label26
Label13
ldc.r4     1
stloc.s    4 (System.Single)
ldarg.1
isinst     Sandbox.Game.Entities.MyCubeBlock
dup
stloc.s    5 (Sandbox.Game.Entities.MyCubeBlock)
brfalse => Label14
ldloc.s    5 (Sandbox.Game.Entities.MyCubeBlock)
callvirt   Sandbox.Definitions.MyCubeBlockDefinition Sandbox.Game.Entities.MyCubeBlock::get_BlockDefinition()
callvirt   System.Single Sandbox.Definitions.MyCubeBlockDefinition::get_PriorityModifier()
stloc.s    4 (System.Single)
ldarg.1
isinst     Sandbox.Game.Entities.Cube.MyFunctionalBlock
dup
stloc.s    9 (Sandbox.Game.Entities.Cube.MyFunctionalBlock)
brfalse => Label15
ldloc.s    9 (Sandbox.Game.Entities.Cube.MyFunctionalBlock)
callvirt   virtual System.Boolean Sandbox.Game.Entities.MyCubeBlock::get_IsWorking()
brtrue =>  Label16
ldloc.s    4 (System.Single)
ldloc.s    9 (Sandbox.Game.Entities.Cube.MyFunctionalBlock)
callvirt   Sandbox.Definitions.MyFunctionalBlockDefinition Sandbox.Game.Entities.Cube.MyFunctionalBlock::get_BlockDefinition()
callvirt   System.Single Sandbox.Definitions.MyCubeBlockDefinition::get_NotWorkingPriorityMultiplier()
mul
stloc.s    4 (System.Single)
Label14
Label15
Label16
ldloc.s    4 (System.Single)
ldc.r4     0
bne.un =>  Label17
ldc.i4.0
br =>      Label27
Label17
ldarg.1
callvirt   virtual VRage.Game.Components.MyPositionComponentBase VRage.Game.Entity.MyEntity::get_PositionComp()
callvirt   VRageMath.Vector3D VRage.Game.Components.MyPositionComponentBase::GetPosition()
ldarg.0
ldfld      Sandbox.Game.Entities.Interfaces.IMyTargetingReceiver Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_targetReceiver
callvirt   abstract virtual VRageMath.Vector3D Sandbox.Game.Entities.Interfaces.IMyTargetingReceiver::get_EntityPosition()
call       static System.Double VRageMath.Vector3D::DistanceSquared(VRageMath.Vector3D value1, VRageMath.Vector3D value2)
stloc.s    6 (System.Double)
ldloc.s    6 (System.Double)
ldarg.s    4
ble.un =>  Label18
ldc.i4.0
br =>      Label28
Label18
ldloc.s    6 (System.Double)
ldarg.3
ldind.r8
blt.un =>  Label19
ldarg.s    5
ldloca.s   10 (Sandbox.Game.Weapons.MyLargeTurretTargetingSystem+MyEntityWithDistSq)
initobj    Sandbox.Game.Weapons.MyLargeTurretTargetingSystem+MyEntityWithDistSq
ldloca.s   10 (Sandbox.Game.Weapons.MyLargeTurretTargetingSystem+MyEntityWithDistSq)
ldarg.1
stfld      VRage.Game.Entity.MyEntity Sandbox.Game.Weapons.MyEntityWithDistSq::Entity
ldloca.s   10 (Sandbox.Game.Weapons.MyLargeTurretTargetingSystem+MyEntityWithDistSq)
ldloc.s    4 (System.Single)
stfld      System.Single Sandbox.Game.Weapons.MyEntityWithDistSq::Priority
ldloca.s   10 (Sandbox.Game.Weapons.MyLargeTurretTargetingSystem+MyEntityWithDistSq)
ldloc.s    6 (System.Double)
call       static System.Double System.Math::Sqrt(System.Double d)
stfld      System.Double Sandbox.Game.Weapons.MyEntityWithDistSq::Distance
ldloc.s    10 (Sandbox.Game.Weapons.MyLargeTurretTargetingSystem+MyEntityWithDistSq)
callvirt   virtual System.Void System.Collections.Generic.List`1<Sandbox.Game.Weapons.MyEntityWithDistSq>::Add(Sandbox.Game.Weapons.MyEntityWithDistSq item)
ldc.i4.0
br =>      Label29
Label19
ldarg.0
ldarg.1
call       System.Boolean Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::IsTargetAimedByOtherTurret(VRage.Game.Entity.MyEntity target)
brtrue =>  Label20
ldarg.3
ldloc.s    6 (System.Double)
stind.r8
ldarg.2
ldarg.1
stind.ref
ldarg.s    5
ldloca.s   10 (Sandbox.Game.Weapons.MyLargeTurretTargetingSystem+MyEntityWithDistSq)
initobj    Sandbox.Game.Weapons.MyLargeTurretTargetingSystem+MyEntityWithDistSq
ldloca.s   10 (Sandbox.Game.Weapons.MyLargeTurretTargetingSystem+MyEntityWithDistSq)
ldarg.1
stfld      VRage.Game.Entity.MyEntity Sandbox.Game.Weapons.MyEntityWithDistSq::Entity
ldloca.s   10 (Sandbox.Game.Weapons.MyLargeTurretTargetingSystem+MyEntityWithDistSq)
ldloc.s    4 (System.Single)
stfld      System.Single Sandbox.Game.Weapons.MyEntityWithDistSq::Priority
ldloca.s   10 (Sandbox.Game.Weapons.MyLargeTurretTargetingSystem+MyEntityWithDistSq)
ldloc.s    6 (System.Double)
call       static System.Double System.Math::Sqrt(System.Double d)
stfld      System.Double Sandbox.Game.Weapons.MyEntityWithDistSq::Distance
ldloc.s    10 (Sandbox.Game.Weapons.MyLargeTurretTargetingSystem+MyEntityWithDistSq)
callvirt   virtual System.Void System.Collections.Generic.List`1<Sandbox.Game.Weapons.MyEntityWithDistSq>::Add(Sandbox.Game.Weapons.MyEntityWithDistSq item)
ldc.i4.1
br =>      Label30
Label20
ldc.i4.0
// end original
Label21
Label22
Label23
Label24
Label25
Label26
Label27
Label28
Label29
Label30
ret
DONE