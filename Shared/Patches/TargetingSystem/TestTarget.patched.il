// 32f612ca
ldarg.1 NULL
brfalse.s Label0
ldarg.0 NULL
ldfld Sandbox.Game.Entities.Interfaces.IMyTargetingReceiver Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_targetReceiver
ldarg.1 NULL
call static System.Boolean Shared.Patches.MyLargeTurretTargetingSystemPatch::IsTargetCachedAsNotVisible(Sandbox.Game.Entities.Interfaces.IMyTargetingReceiver targetReceiver, VRage.Game.Entity.MyEntity target)
brfalse Label2
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
