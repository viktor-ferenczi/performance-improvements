// ce659ce9
ldarg.0
ldflda System.Collections.Generic.List`1<VRage.Game.Entity.MyEntity> Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_buffer
call static VRage.Utils.ClearCollectionToken`2<System.Collections.Generic.List`1<VRage.Game.Entity.MyEntity>, VRage.Game.Entity.MyEntity> VRage.Utils.MyUtils::ReuseCollection(System.Collections.Generic.List`1& collection)
stloc.1
[EX_BeginException]
ldarg.0
ldfld Sandbox.Game.Weapons.MyTargetSelectionWorkData Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_targetSelectionWorkData
ldfld Sandbox.Game.EntityComponents.MyGridTargeting Sandbox.Game.Weapons.MyTargetSelectionWorkData::GridTargeting
ldarg.0
ldfld Sandbox.Game.Entities.Interfaces.IMyTargetingReceiver Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_targetReceiver
callvirt abstract virtual System.Boolean Sandbox.Game.Entities.Interfaces.IMyTargetingReceiver::get_TargetEnemies()
ldarg.0
ldfld Sandbox.Game.Entities.Interfaces.IMyTargetingReceiver Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_targetReceiver
callvirt abstract virtual System.Boolean Sandbox.Game.Entities.Interfaces.IMyTargetingReceiver::get_TargetNeutrals()
ldarg.0
ldfld Sandbox.Game.Entities.Interfaces.IMyTargetingReceiver Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_targetReceiver
callvirt abstract virtual System.Boolean Sandbox.Game.Entities.Interfaces.IMyTargetingReceiver::get_TargetFriends()
ldarg.0
ldfld System.Collections.Generic.List`1<VRage.Game.Entity.MyEntity> Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_buffer
callvirt System.Void Sandbox.Game.EntityComponents.MyGridTargeting::GetTargetRoots(System.Boolean enemies, System.Boolean neutrals, System.Boolean friends, System.Collections.Generic.List`1<VRage.Game.Entity.MyEntity> buffer)
ldarg.0
ldfld System.Collections.Generic.List`1<VRage.Game.Entity.MyEntity> Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_buffer
ldarg.0
ldflda System.Single[] Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_distanceEntityKeys
ldarg.0
ldfld Sandbox.Game.Entities.Interfaces.IMyTargetingReceiver Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_targetReceiver
call static VRage.Game.Entity.MyEntity[] Shared.Patches.MyLargeTurretTargetingSystemPatch::CopyEntitiesIntoArray(System.Collections.Generic.List`1<VRage.Game.Entity.MyEntity> targetRoots, System.Single[]& distanceEntityKeys, Sandbox.Game.Entities.Interfaces.IMyTargetingReceiver targetReceiver)
stloc.0
leave.s L0
[EX_BeginFinally]
ldloca.s 1 (VRage.Utils.MyUtils+ClearCollectionToken`2[System.Collections.Generic.List`1[VRage.Game.Entity.MyEntity],VRage.Game.Entity.MyEntity])
constrained. VRage.Utils.MyUtils+ClearCollectionToken`2[System.Collections.Generic.List`1[VRage.Game.Entity.MyEntity],VRage.Game.Entity.MyEntity]
callvirt abstract virtual System.Void System.IDisposable::Dispose()
[EX_EndException]
endfinally
L0:
ldarg.0
ldfld Sandbox.Game.Entities.MyCubeGrid Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_focusedTarget
brfalse.s L1
ldloc.0
ldlen
conv.i4
pop
ldloca.s 0 (VRage.Game.Entity.MyEntity[])
ldlen
conv.i4
ldc.i4.1
sub
ldloc.0
ldc.i4.0
ldelem.ref
stelem.ref
ldloc.0
ldc.i4.0
ldarg.0
ldfld Sandbox.Game.Entities.MyCubeGrid Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_focusedTarget
stelem.ref
L1:
ldloc.0
ldlen
conv.i4
ldc.i4.1
ble L2
ldarg.0
ldfld Sandbox.Game.Entities.Interfaces.IMyTargetingReceiver Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_targetReceiver
callvirt abstract virtual VRageMath.Vector3D Sandbox.Game.Entities.Interfaces.IMyTargetingReceiver::get_EntityPosition()
stloc.2
ldarg.0
ldflda System.Single[] Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_distanceEntityKeys
ldloc.0
ldlen
conv.i4
ldc.r4 1
call static System.Void System.ArrayExtensions::EnsureCapacity(System.Single[]& array, System.Int32 size, System.Single growFactor)
ldc.i4.0
stloc.3
br.s L3
L11:
ldloc.0
ldloc.3
ldelem.ref
stloc.s 4 (VRage.Game.Entity.MyEntity)
ldloc.s 4 (VRage.Game.Entity.MyEntity)
ldarg.0
ldfld Sandbox.Game.Entities.MyCubeGrid Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_focusedTarget
bne.un.s L4
ldloc.s 4 (VRage.Game.Entity.MyEntity)
brfalse.s L5
ldloc.s 4 (VRage.Game.Entity.MyEntity)
callvirt System.Boolean VRage.Game.Entity.MyEntity::get_Closed()
brtrue.s L6
ldarg.0
ldfld System.Single[] Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_distanceEntityKeys
ldloc.3
ldc.r4 -1
stelem.r4
br.s L7
L4:
L5:
L6:
ldloc.s 4 (VRage.Game.Entity.MyEntity)
brfalse.s L8
ldloc.s 4 (VRage.Game.Entity.MyEntity)
callvirt System.Boolean VRage.Game.Entity.MyEntity::get_Closed()
brtrue.s L9
ldloc.s 4 (VRage.Game.Entity.MyEntity)
callvirt virtual VRage.Game.Components.MyPositionComponentBase VRage.Game.Entity.MyEntity::get_PositionComp()
callvirt VRageMath.Vector3D VRage.Game.Components.MyPositionComponentBase::GetPosition()
ldloc.2
call static VRageMath.Vector3D VRageMath.Vector3D::op_Subtraction(VRageMath.Vector3D value1, VRageMath.Vector3D value2)
stloc.s 6 (VRageMath.Vector3D)
ldloca.s 6 (VRageMath.Vector3D)
call System.Double VRageMath.Vector3D::LengthSquared()
conv.r4
stloc.s 5 (System.Single)
br.s L10
L8:
L9:
ldc.r4 3.402823E+38
stloc.s 5 (System.Single)
L10:
ldarg.0
ldfld System.Single[] Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_distanceEntityKeys
ldloc.3
ldloc.s 5 (System.Single)
stelem.r4
L7:
ldloc.3
ldc.i4.1
add
stloc.3
L3:
ldloc.3
ldloc.0
ldlen
conv.i4
blt.s L11
ldarg.0
ldfld System.Single[] Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_distanceEntityKeys
ldloc.0
ldc.i4.0
ldloc.0
ldlen
conv.i4
ldsfld System.FloatComparer System.FloatComparer::Instance
call static System.Void System.Array::Sort(System.Single[] keys, VRage.Game.Entity.MyEntity[] items, System.Int32 index, System.Int32 length, System.Collections.Generic.IComparer`1<System.Single> comparer)
L2:
ldloc.0
ret
