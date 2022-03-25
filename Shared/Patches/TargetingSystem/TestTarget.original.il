// 5061cc1b
ldarg.1
brfalse.s L0
ldarg.0
ldfld System.Collections.Concurrent.ConcurrentDictionary`2<VRage.Game.Entity.MyEntity, System.Int32> Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_notVisibleTargets
ldarg.1
ldloca.s 0 (System.Int32)
callvirt virtual System.Boolean System.Collections.Concurrent.ConcurrentDictionary`2<VRage.Game.Entity.MyEntity, System.Int32>::TryGetValue(VRage.Game.Entity.MyEntity key, System.Int32& value)
brfalse.s L1
ldloc.0
ldc.i4.0
ble.s L2
L0:
ldc.i4.0
ret
L1:
L2:
ldsfld VRageMath.Vector3D VRageMath.Vector3D::Zero
stloc.1
ldarg.0
ldfld Sandbox.Game.Weapons.MyPositionPrediction Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::TargetPrediction
ldfld System.Boolean Sandbox.Game.Weapons.MyPositionPrediction::UsePrediction
stloc.2
ldarg.0
ldfld Sandbox.Game.Weapons.MyPositionPrediction Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::TargetPrediction
ldc.i4.0
stfld System.Boolean Sandbox.Game.Weapons.MyPositionPrediction::UsePrediction
ldarg.0
ldfld Sandbox.Game.Weapons.MyPositionPrediction Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::TargetPrediction
ldarg.1
callvirt VRageMath.Vector3D Sandbox.Game.Weapons.MyPositionPrediction::GetTargetCoordinates(VRage.ModAPI.IMyEntity currentTarget)
stloc.1
ldarg.0
ldfld Sandbox.Game.Weapons.MyPositionPrediction Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::TargetPrediction
ldloc.2
stfld System.Boolean Sandbox.Game.Weapons.MyPositionPrediction::UsePrediction
ldarg.0
ldfld Sandbox.Game.Entities.Interfaces.IMyTargetingReceiver Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_targetReceiver
ldloc.1
callvirt abstract virtual System.Boolean Sandbox.Game.Entities.Interfaces.IMyTargetingReceiver::IsTargetInView(VRageMath.Vector3D predPos)
brfalse.s L3
ldarg.0
ldarg.1
ldloc.1
newobj System.Void System.Nullable`1<VRageMath.Vector3D>::.ctor(VRageMath.Vector3D value)
ldc.i4.1
call System.Boolean Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::IsTargetVisible(VRage.Game.Entity.MyEntity target, System.Nullable`1<VRageMath.Vector3D> overridePredictedPos, System.Boolean useVisibilityCache)
brtrue.s L4
L3:
ldc.i4.0
ret
L4:
ldc.i4.1
ret
