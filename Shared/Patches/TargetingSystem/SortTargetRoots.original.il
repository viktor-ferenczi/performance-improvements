// edfb7619
ldarg.0
ldfld Sandbox.Game.Weapons.MyTargetSelectionWorkData Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_targetSelectionWorkData
ldfld Sandbox.Game.EntityComponents.MyGridTargeting Sandbox.Game.Weapons.MyTargetSelectionWorkData::GridTargeting
ldc.i4.1
ldarg.0
ldfld Sandbox.Game.Entities.Interfaces.IMyTargetingReceiver Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_targetReceiver
callvirt abstract virtual System.Boolean Sandbox.Game.Entities.Interfaces.IMyTargetingReceiver::get_TargetNeutrals()
ldarg.0
ldfld Sandbox.Game.Entities.Interfaces.IMyTargetingReceiver Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_targetReceiver
callvirt abstract virtual System.Boolean Sandbox.Game.Entities.Interfaces.IMyTargetingReceiver::get_TargetFriends()
callvirt System.Void Sandbox.Game.EntityComponents.MyGridTargeting::SetRelationFlags(System.Boolean targetEnemies, System.Boolean targetNeutrals, System.Boolean targetFriends)
ldarg.0
ldfld Sandbox.Game.Weapons.MyTargetSelectionWorkData Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_targetSelectionWorkData
ldfld Sandbox.Game.EntityComponents.MyGridTargeting Sandbox.Game.Weapons.MyTargetSelectionWorkData::GridTargeting
callvirt System.Collections.Generic.List`1<VRage.Game.Entity.MyEntity> Sandbox.Game.EntityComponents.MyGridTargeting::get_TargetRoots()
callvirt VRage.Game.Entity.MyEntity[] System.Collections.Generic.List`1<VRage.Game.Entity.MyEntity>::ToArray()
stloc.0
ldloc.0
ldlen
conv.i4
ldc.i4.1
ble L0
ldarg.0
ldfld Sandbox.Game.Entities.Interfaces.IMyTargetingReceiver Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_targetReceiver
callvirt abstract virtual VRageMath.Vector3D Sandbox.Game.Entities.Interfaces.IMyTargetingReceiver::get_EntityPosition()
stloc.1
ldarg.0
ldflda System.Single[] Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_distanceEntityKeys
ldloc.0
ldlen
conv.i4
ldc.r4 1
call static System.Void System.ArrayExtensions::EnsureCapacity(System.Single[]& array, System.Int32 size, System.Single growFactor)
ldc.i4.0
stloc.2
br.s L1
L5:
ldloc.0
ldloc.2
ldelem.ref
stloc.3
ldloc.3
brfalse.s L2
ldloc.3
callvirt System.Boolean VRage.Game.Entity.MyEntity::get_Closed()
brtrue.s L3
ldloc.3
callvirt virtual VRage.Game.Components.MyPositionComponentBase VRage.Game.Entity.MyEntity::get_PositionComp()
callvirt VRageMath.Vector3D VRage.Game.Components.MyPositionComponentBase::GetPosition()
ldloc.1
call static VRageMath.Vector3D VRageMath.Vector3D::op_Subtraction(VRageMath.Vector3D value1, VRageMath.Vector3D value2)
stloc.s 5 (VRageMath.Vector3D)
ldloca.s 5 (VRageMath.Vector3D)
call System.Double VRageMath.Vector3D::LengthSquared()
conv.r4
stloc.s 4 (System.Single)
br.s L4
L2:
L3:
ldc.r4 3.402823E+38
stloc.s 4 (System.Single)
L4:
ldarg.0
ldfld System.Single[] Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_distanceEntityKeys
ldloc.2
ldloc.s 4 (System.Single)
stelem.r4
ldloc.2
ldc.i4.1
add
stloc.2
L1:
ldloc.2
ldloc.0
ldlen
conv.i4
blt.s L5
ldarg.0
ldfld System.Single[] Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_distanceEntityKeys
ldloc.0
ldc.i4.0
ldloc.0
ldlen
conv.i4
ldsfld System.FloatComparer System.FloatComparer::Instance
call static System.Void System.Array::Sort(System.Single[] keys, VRage.Game.Entity.MyEntity[] items, System.Int32 index, System.Int32 length, System.Collections.Generic.IComparer`1<System.Single> comparer)
L0:
ldloc.0
ret
