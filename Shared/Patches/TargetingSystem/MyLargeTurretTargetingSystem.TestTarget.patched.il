### Patch: System.Boolean Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::TestTarget(VRage.Game.Entity.MyEntity target)
### Replacement: static System.Boolean Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::Sandbox.Game.Weapons.MyLargeTurretTargetingSystem.TestTarget_Patch0(Sandbox.Game.Weapons.MyLargeTurretTargetingSystem this, VRage.Game.Entity.MyEntity target)
Local var 0: System.Int32
Local var 1: VRageMath.Vector3D
Local var 2: System.Boolean
// start original
ldarg.1
brfalse => Label0
ldarg.0
ldfld      Sandbox.Game.Entities.Interfaces.IMyTargetingReceiver Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_targetReceiver
ldarg.1
call       static System.Boolean Shared.Patches.MyLargeTurretTargetingSystemPatch::IsTargetCachedAsNotVisible(Sandbox.Game.Entities.Interfaces.IMyTargetingReceiver targetReceiver, VRage.Game.Entity.MyEntity target)
brfalse => Label2
Label0
ldc.i4.0
br =>      Label5
Label1
Label2
ldsfld     VRageMath.Vector3D VRageMath.Vector3D::Zero
stloc.1
ldarg.0
ldfld      Sandbox.Game.Weapons.MyPositionPrediction Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::TargetPrediction
ldfld      System.Boolean Sandbox.Game.Weapons.MyPositionPrediction::UsePrediction
stloc.2
ldarg.0
ldfld      Sandbox.Game.Weapons.MyPositionPrediction Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::TargetPrediction
ldc.i4.0
stfld      System.Boolean Sandbox.Game.Weapons.MyPositionPrediction::UsePrediction
ldarg.0
ldfld      Sandbox.Game.Weapons.MyPositionPrediction Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::TargetPrediction
ldarg.1
callvirt   VRageMath.Vector3D Sandbox.Game.Weapons.MyPositionPrediction::GetTargetCoordinates(VRage.ModAPI.IMyEntity currentTarget)
stloc.1
ldarg.0
ldfld      Sandbox.Game.Weapons.MyPositionPrediction Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::TargetPrediction
ldloc.2
stfld      System.Boolean Sandbox.Game.Weapons.MyPositionPrediction::UsePrediction
ldarg.0
ldfld      Sandbox.Game.Entities.Interfaces.IMyTargetingReceiver Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_targetReceiver
ldloc.1
callvirt   abstract virtual System.Boolean Sandbox.Game.Entities.Interfaces.IMyTargetingReceiver::IsTargetInView(VRageMath.Vector3D predPos)
brfalse => Label3
ldarg.0
ldarg.1
ldloc.1
newobj     System.Void System.Nullable`1<VRageMath.Vector3D>::.ctor(VRageMath.Vector3D value)
ldc.i4.1
call       System.Boolean Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::IsTargetVisible(VRage.Game.Entity.MyEntity target, System.Nullable`1<VRageMath.Vector3D> overridePredictedPos, System.Boolean useVisibilityCache)
brtrue =>  Label4
Label3
ldc.i4.0
br =>      Label6
Label4
ldc.i4.1
// end original
Label5
Label6
ret
DONE