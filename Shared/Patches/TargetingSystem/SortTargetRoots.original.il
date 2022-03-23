// edfb7619
ldarg.0 NULL
ldfld Sandbox.Game.Weapons.MyTargetSelectionWorkData Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_targetSelectionWorkData
ldfld Sandbox.Game.EntityComponents.MyGridTargeting Sandbox.Game.Weapons.MyTargetSelectionWorkData::GridTargeting
ldc.i4.1 NULL
ldarg.0 NULL
ldfld Sandbox.Game.Entities.Interfaces.IMyTargetingReceiver Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_targetReceiver
callvirt abstract virtual System.Boolean Sandbox.Game.Entities.Interfaces.IMyTargetingReceiver::get_TargetNeutrals()
ldarg.0 NULL
ldfld Sandbox.Game.Entities.Interfaces.IMyTargetingReceiver Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_targetReceiver
callvirt abstract virtual System.Boolean Sandbox.Game.Entities.Interfaces.IMyTargetingReceiver::get_TargetFriends()
callvirt System.Void Sandbox.Game.EntityComponents.MyGridTargeting::SetRelationFlags(System.Boolean targetEnemies, System.Boolean targetNeutrals, System.Boolean targetFriends)
ldarg.0 NULL
ldfld Sandbox.Game.Weapons.MyTargetSelectionWorkData Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_targetSelectionWorkData
ldfld Sandbox.Game.EntityComponents.MyGridTargeting Sandbox.Game.Weapons.MyTargetSelectionWorkData::GridTargeting
callvirt System.Collections.Generic.List`1<VRage.Game.Entity.MyEntity> Sandbox.Game.EntityComponents.MyGridTargeting::get_TargetRoots()
callvirt VRage.Game.Entity.MyEntity[] System.Collections.Generic.List`1<VRage.Game.Entity.MyEntity>::ToArray()
stloc.0 NULL
ldloc.0 NULL
ldlen NULL
conv.i4 NULL
ldc.i4.1 NULL
ble Label0
ldarg.0 NULL
ldfld Sandbox.Game.Entities.Interfaces.IMyTargetingReceiver Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_targetReceiver
callvirt abstract virtual VRageMath.Vector3D Sandbox.Game.Entities.Interfaces.IMyTargetingReceiver::get_EntityPosition()
stloc.1 NULL
ldarg.0 NULL
ldflda System.Single[] Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_distanceEntityKeys
ldloc.0 NULL
ldlen NULL
conv.i4 NULL
ldc.r4 1
call static System.Void System.ArrayExtensions::EnsureCapacity(System.Single[]& array, System.Int32 size, System.Single growFactor)
ldc.i4.0 NULL
stloc.2 NULL
br.s Label1
ldloc.0 NULL [Label5]
ldloc.2 NULL
ldelem.ref NULL
stloc.3 NULL
ldloc.3 NULL
brfalse.s Label2
ldloc.3 NULL
callvirt System.Boolean VRage.Game.Entity.MyEntity::get_Closed()
brtrue.s Label3
ldloc.3 NULL
callvirt virtual VRage.Game.Components.MyPositionComponentBase VRage.Game.Entity.MyEntity::get_PositionComp()
callvirt VRageMath.Vector3D VRage.Game.Components.MyPositionComponentBase::GetPosition()
ldloc.1 NULL
call static VRageMath.Vector3D VRageMath.Vector3D::op_Subtraction(VRageMath.Vector3D value1, VRageMath.Vector3D value2)
stloc.s 5 (VRageMath.Vector3D)
ldloca.s 5 (VRageMath.Vector3D)
call System.Double VRageMath.Vector3D::LengthSquared()
conv.r4 NULL
stloc.s 4 (System.Single)
br.s Label4
ldc.r4 3.402823E+38 [Label2, Label3]
stloc.s 4 (System.Single)
ldarg.0 NULL [Label4]
ldfld System.Single[] Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_distanceEntityKeys
ldloc.2 NULL
ldloc.s 4 (System.Single)
stelem.r4 NULL
ldloc.2 NULL
ldc.i4.1 NULL
add NULL
stloc.2 NULL
ldloc.2 NULL [Label1]
ldloc.0 NULL
ldlen NULL
conv.i4 NULL
blt.s Label5
ldarg.0 NULL
ldfld System.Single[] Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_distanceEntityKeys
ldloc.0 NULL
ldc.i4.0 NULL
ldloc.0 NULL
ldlen NULL
conv.i4 NULL
ldsfld System.FloatComparer System.FloatComparer::Instance
call static System.Void System.Array::Sort(System.Single[] keys, VRage.Game.Entity.MyEntity[] items, System.Int32 index, System.Int32 length, System.Collections.Generic.IComparer`1<System.Single> comparer)
ldloc.0 NULL [Label0]
ret NULL
