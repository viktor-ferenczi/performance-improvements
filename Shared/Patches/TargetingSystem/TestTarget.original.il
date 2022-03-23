ldarg.1 NULL
brfalse.s Label0
ldarg.0 NULL
ldfld System.Collections.Concurrent.ConcurrentDictionary`2<VRage.Game.Entity.MyEntity, System.Int32> Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_notVisibleTargets
ldarg.1 NULL
ldloca.s 0 (System.Int32)
callvirt virtual System.Boolean System.Collections.Concurrent.ConcurrentDictionary`2<VRage.Game.Entity.MyEntity, System.Int32>::TryGetValue(VRage.Game.Entity.MyEntity key, System.Int32& value)
brfalse.s Label1
ldloc.0 NULL
ldc.i4.0 NULL
ble.s Label2
ldc.i4.0 NULL [Label0]
ret NULL
ldsfld VRageMath.Vector3D VRageMath.Vector3D::Zero [Label1, Label2]
stloc.1 NULL
ldarg.0 NULL
ldfld Sandbox.Game.Weapons.MyPositionPrediction Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::TargetPrediction
ldfld System.Boolean Sandbox.Game.Weapons.MyPositionPrediction::UsePrediction
stloc.2 NULL
ldarg.0 NULL
ldfld Sandbox.Game.Weapons.MyPositionPrediction Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::TargetPrediction
ldc.i4.0 NULL
stfld System.Boolean Sandbox.Game.Weapons.MyPositionPrediction::UsePrediction
ldarg.0 NULL
ldfld Sandbox.Game.Weapons.MyPositionPrediction Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::TargetPrediction
ldarg.1 NULL
callvirt VRageMath.Vector3D Sandbox.Game.Weapons.MyPositionPrediction::GetTargetCoordinates(VRage.ModAPI.IMyEntity currentTarget)
stloc.1 NULL
ldarg.0 NULL
ldfld Sandbox.Game.Weapons.MyPositionPrediction Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::TargetPrediction
ldloc.2 NULL
stfld System.Boolean Sandbox.Game.Weapons.MyPositionPrediction::UsePrediction
ldarg.0 NULL
ldfld Sandbox.Game.Entities.Interfaces.IMyTargetingReceiver Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_targetReceiver
ldloc.1 NULL
callvirt abstract virtual System.Boolean Sandbox.Game.Entities.Interfaces.IMyTargetingReceiver::IsTargetInView(VRageMath.Vector3D predPos)
brfalse.s Label3
ldarg.0 NULL
ldarg.1 NULL
ldloc.1 NULL
newobj System.Void System.Nullable`1<VRageMath.Vector3D>::.ctor(VRageMath.Vector3D value)
ldc.i4.1 NULL
call System.Boolean Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::IsTargetVisible(VRage.Game.Entity.MyEntity target, System.Nullable`1<VRageMath.Vector3D> overridePredictedPos, System.Boolean useVisibilityCache)
brtrue.s Label4
ldc.i4.0 NULL [Label3]
ret NULL
ldc.i4.1 NULL [Label4]
ret NULL
