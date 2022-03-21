### Patch: System.Boolean Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::IsTargetVisible(VRage.Game.Entity.MyEntity target, System.Nullable`1<VRageMath.Vector3D> overridePredictedPos, System.Boolean useVisibilityCache)
### Replacement: static System.Boolean Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::Sandbox.Game.Weapons.MyLargeTurretTargetingSystem.IsTargetVisible_Patch0(Sandbox.Game.Weapons.MyLargeTurretTargetingSystem this, VRage.Game.Entity.MyEntity target, System.Nullable`1<VRageMath.Vector3D> overridePredictedPos, System.Boolean useVisibilityCache)
Local var 0: Sandbox.Game.Weapons.MyLargeTurretTargetingSystem/<>c__DisplayClass55_0
Local var 1: VRageMath.Vector3D
Local var 2: System.Int32
Local var 3: VRageMath.Vector3D
Local var 4: VRageMath.Vector3D
Local var 5: VRageMath.Vector3D
Local var 6: System.Boolean
Local var 7: Sandbox.Game.Weapons.MyLargeTurretTargetingSystem/MyTargetingOption
Local var 8: System.Nullable`1<Sandbox.Engine.Physics.MyPhysics/HitInfo>
Local var 9: System.Nullable`1<Sandbox.Engine.Physics.MyPhysics/HitInfo>
// start original
newobj     System.Void Sandbox.Game.Weapons.<>c__DisplayClass55_0::.ctor()
stloc.0
ldloc.0
ldarg.0
stfld      Sandbox.Game.Weapons.MyLargeTurretTargetingSystem Sandbox.Game.Weapons.<>c__DisplayClass55_0::<>4__this
ldloc.0
ldarg.1
stfld      VRage.Game.Entity.MyEntity Sandbox.Game.Weapons.<>c__DisplayClass55_0::target
ldloc.0
ldfld      VRage.Game.Entity.MyEntity Sandbox.Game.Weapons.<>c__DisplayClass55_0::target
brfalse => Label0
ldloc.0
ldfld      VRage.Game.Entity.MyEntity Sandbox.Game.Weapons.<>c__DisplayClass55_0::target
ldnull
callvirt   VRage.Game.Entity.MyEntity VRage.Game.Entity.MyEntity::GetTopMostParent(System.Type type)
callvirt   VRage.Game.Components.MyPhysicsComponentBase VRage.Game.Entity.MyEntity::get_Physics()
brfalse => Label1
ldarg.0
ldloc.0
ldfld      VRage.Game.Entity.MyEntity Sandbox.Game.Weapons.<>c__DisplayClass55_0::target
call       System.Boolean Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::IsTargetInSafeZone(VRage.Game.Entity.MyEntity target)
brfalse => Label2
Label0
Label1
ldc.i4.0
br =>      Label23
Label2
ldarga.s   2
call       System.Boolean System.Nullable`1<VRageMath.Vector3D>::get_HasValue()
brfalse => Label3
ldarga.s   2
call       VRageMath.Vector3D System.Nullable`1<VRageMath.Vector3D>::get_Value()
stloc.1
br =>      Label4
Label3
ldarg.0
ldfld      Sandbox.Game.Weapons.MyPositionPrediction Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::TargetPrediction
ldfld      System.Boolean Sandbox.Game.Weapons.MyPositionPrediction::UsePrediction
stloc.s    6 (System.Boolean)
ldarg.0
ldfld      Sandbox.Game.Weapons.MyPositionPrediction Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::TargetPrediction
callvirt   System.Boolean Sandbox.Game.Weapons.MyPositionPrediction::get_IsFastPrediction()
brtrue =>  Label5
ldarg.0
ldfld      Sandbox.Game.Weapons.MyPositionPrediction Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::TargetPrediction
ldc.i4.0
stfld      System.Boolean Sandbox.Game.Weapons.MyPositionPrediction::UsePrediction
Label5
ldarg.0
ldfld      Sandbox.Game.Weapons.MyPositionPrediction Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::TargetPrediction
ldloc.0
ldfld      VRage.Game.Entity.MyEntity Sandbox.Game.Weapons.<>c__DisplayClass55_0::target
callvirt   VRageMath.Vector3D Sandbox.Game.Weapons.MyPositionPrediction::GetTargetCoordinates(VRage.ModAPI.IMyEntity currentTarget)
stloc.1
ldarg.0
ldfld      Sandbox.Game.Weapons.MyPositionPrediction Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::TargetPrediction
ldloc.s    6 (System.Boolean)
stfld      System.Boolean Sandbox.Game.Weapons.MyPositionPrediction::UsePrediction
ldarg.0
ldfld      Sandbox.Game.Weapons.MyPositionPrediction Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::TargetPrediction
ldfld      System.Boolean Sandbox.Game.Weapons.MyPositionPrediction::IsLastPredictedCoordinatesInRange
brtrue =>  Label6
ldc.i4.0
br =>      Label24
Label4
Label6
ldarga.s   2
call       System.Boolean System.Nullable`1<VRageMath.Vector3D>::get_HasValue()
brtrue =>  Label7
ldarg.0
ldfld      System.Collections.Concurrent.ConcurrentDictionary`2<VRage.Game.Entity.MyEntity, System.Int32> Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_notVisibleTargets
ldloc.0
ldfld      VRage.Game.Entity.MyEntity Sandbox.Game.Weapons.<>c__DisplayClass55_0::target
ldloca.s   2 (System.Int32)
callvirt   virtual System.Boolean System.Collections.Concurrent.ConcurrentDictionary`2<VRage.Game.Entity.MyEntity, System.Int32>::TryGetValue(VRage.Game.Entity.MyEntity key, System.Int32& value)
brfalse => Label8
ldloc.2
ldc.i4.0
ble =>     Label9
call       static System.Boolean Sandbox.Game.Multiplayer.Sync::get_IsServer()
brfalse => Label10
ldc.i4.0
br =>      Label25
Label7
Label8
Label9
Label10
ldarg.3
brfalse => Label11
ldarg.0
ldfld      System.Collections.Concurrent.ConcurrentDictionary`2<VRage.Game.Entity.MyEntity, System.Int32> Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_visibleTargets
ldloc.0
ldfld      VRage.Game.Entity.MyEntity Sandbox.Game.Weapons.<>c__DisplayClass55_0::target
callvirt   virtual System.Boolean System.Collections.Concurrent.ConcurrentDictionary`2<VRage.Game.Entity.MyEntity, System.Int32>::ContainsKey(VRage.Game.Entity.MyEntity key)
brfalse => Label12
ldc.i4.1
br =>      Label26
Label12
ldarg.0
ldloc.0
ldfld      VRage.Game.Entity.MyEntity Sandbox.Game.Weapons.<>c__DisplayClass55_0::target
ldc.i4.0
ldc.i4.1
newobj     System.Void System.Nullable`1<System.Int32>::.ctor(System.Int32 value)
call       System.Void Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::SetTargetVisible(VRage.Game.Entity.MyEntity target, System.Boolean visible, System.Nullable`1<System.Int32> timeout)
Label11
ldarg.0
ldfld      Sandbox.Game.Entities.Interfaces.IMyTargetingReceiver Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_targetReceiver
callvirt   abstract virtual VRageMath.Vector3D Sandbox.Game.Entities.Interfaces.IMyTargetingReceiver::get_ShootDirection()
stloc.3
ldarg.0
ldfld      Sandbox.Game.Entities.Interfaces.IMyTargetingReceiver Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_targetReceiver
callvirt   abstract virtual VRageMath.Vector3D Sandbox.ModAPI.IMyShootOrigin::get_ShootOrigin()
stloc.s    4 (VRageMath.Vector3D)
ldloc.1
stloc.s    5 (VRageMath.Vector3D)
ldloc.s    5 (VRageMath.Vector3D)
ldloc.s    4 (VRageMath.Vector3D)
call       static VRageMath.Vector3D VRageMath.Vector3D::op_Subtraction(VRageMath.Vector3D value1, VRageMath.Vector3D value2)
call       static VRageMath.Vector3D VRageMath.Vector3D::Normalize(VRageMath.Vector3D value)
ldloc.3
call       static System.Double VRageMath.Vector3D::Dot(VRageMath.Vector3D vector1, VRageMath.Vector3D vector2)
ldsfld     System.Single Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::EXACT_VISIBILITY_TEST_TRESHOLD_ANGLE
conv.r8
ble.un =>  Label13
ldarg.0
ldfld      Sandbox.Game.Entities.Interfaces.IMyTargetingReceiver Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_targetReceiver
callvirt   abstract virtual VRageMath.Vector3D Sandbox.ModAPI.IMyShootOrigin::get_ShootOrigin()
ldloc.3
ldc.r8     0.5
call       static VRageMath.Vector3D VRageMath.Vector3D::op_Multiply(VRageMath.Vector3D value, System.Double scaleFactor)
call       static VRageMath.Vector3D VRageMath.Vector3D::op_Addition(VRageMath.Vector3D value1, VRageMath.Vector3D value2)
stloc.s    4 (VRageMath.Vector3D)
Label13
ldarg.3
brfalse => Label14
ldarg.0
ldfld      Sandbox.Game.Weapons.MyTargetingOption Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_raycastMode
stloc.s    7 (Sandbox.Game.Weapons.MyLargeTurretTargetingSystem+MyTargetingOption)
ldloc.s    7 (Sandbox.Game.Weapons.MyLargeTurretTargetingSystem+MyTargetingOption)
switch =>  Labels15,16,17
Label15
ldloca.s   4 (VRageMath.Vector3D)
ldloca.s   5 (VRageMath.Vector3D)
ldc.i4.s   15
ldloc.0
ldftn      System.Void Sandbox.Game.Weapons.<>c__DisplayClass55_0::<IsTargetVisible>b__0(System.Nullable`1<Sandbox.Engine.Physics.HitInfo> physTarget)
newobj     System.Void System.Action`1<System.Nullable`1<Sandbox.Engine.Physics.HitInfo>>::.ctor(System.Object object, System.IntPtr method)
call       static System.Void Sandbox.Engine.Physics.MyPhysics::CastRayParallel(VRageMath.Vector3D& from, VRageMath.Vector3D& to, System.Int32 raycastFilterLayer, System.Action`1<System.Nullable`1<Sandbox.Engine.Physics.HitInfo>> callback)
br =>      Label18
Label16
ldloca.s   5 (VRageMath.Vector3D)
ldloca.s   4 (VRageMath.Vector3D)
ldc.i4.s   15
ldloc.0
ldftn      System.Void Sandbox.Game.Weapons.<>c__DisplayClass55_0::<IsTargetVisible>b__1(System.Nullable`1<Sandbox.Engine.Physics.HitInfo> physTarget)
newobj     System.Void System.Action`1<System.Nullable`1<Sandbox.Engine.Physics.HitInfo>>::.ctor(System.Object object, System.IntPtr method)
call       static System.Void Sandbox.Engine.Physics.MyPhysics::CastRayParallel(VRageMath.Vector3D& from, VRageMath.Vector3D& to, System.Int32 raycastFilterLayer, System.Action`1<System.Nullable`1<Sandbox.Engine.Physics.HitInfo>> callback)
br =>      Label19
Label17
ldloc.s    4 (VRageMath.Vector3D)
ldloc.s    5 (VRageMath.Vector3D)
ldarg.0
ldfld      System.Collections.Generic.List`1<Sandbox.Engine.Physics.HitInfo> Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_hitCollector
ldc.i4.s   15
call       static System.Void Sandbox.Engine.Physics.MyPhysics::CastRay(VRageMath.Vector3D from, VRageMath.Vector3D to, System.Collections.Generic.List`1<Sandbox.Engine.Physics.HitInfo> toList, System.Int32 raycastFilterLayer)
ldarg.0
ldloc.0
ldfld      VRage.Game.Entity.MyEntity Sandbox.Game.Weapons.<>c__DisplayClass55_0::target
ldarg.0
ldfld      System.Collections.Generic.List`1<Sandbox.Engine.Physics.HitInfo> Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_hitCollector
ldarg.0
ldfld      Sandbox.Game.Entities.Interfaces.IMyTargetingReceiver Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_targetReceiver
castclass  VRage.Game.Entity.MyEntity
call       System.Boolean Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::OnVisibilityRayCastFullCompleted(VRage.Game.Entity.MyEntity target, System.Collections.Generic.List`1<Sandbox.Engine.Physics.HitInfo> physTargets, VRage.Game.Entity.MyEntity ignoredTarget)
pop
Label18
Label19
ldc.i4.1
br =>      Label27
Label14
ldarg.0
ldfld      Sandbox.Game.Weapons.MyTargetingOption Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_raycastMode
stloc.s    7 (Sandbox.Game.Weapons.MyLargeTurretTargetingSystem+MyTargetingOption)
ldloc.s    7 (Sandbox.Game.Weapons.MyLargeTurretTargetingSystem+MyTargetingOption)
switch =>  Labels20,21,22
Label20
ldloc.s    4 (VRageMath.Vector3D)
ldloc.s    5 (VRageMath.Vector3D)
ldc.i4.s   15
call       static System.Nullable`1<Sandbox.Engine.Physics.HitInfo> Sandbox.Engine.Physics.MyPhysics::CastRay(VRageMath.Vector3D from, VRageMath.Vector3D to, System.Int32 raycastFilterLayer)
stloc.s    8 (System.Nullable`1[Sandbox.Engine.Physics.MyPhysics+HitInfo])
ldarg.0
ldloc.0
ldfld      VRage.Game.Entity.MyEntity Sandbox.Game.Weapons.<>c__DisplayClass55_0::target
ldloc.s    8 (System.Nullable`1[Sandbox.Engine.Physics.MyPhysics+HitInfo])
call       System.Boolean Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::OnVisibilityRayCastRegularCompleted(VRage.Game.Entity.MyEntity target, System.Nullable`1<Sandbox.Engine.Physics.HitInfo> physTarget)
br =>      Label28
Label21
ldloc.s    5 (VRageMath.Vector3D)
ldloc.s    4 (VRageMath.Vector3D)
ldc.i4.s   15
call       static System.Nullable`1<Sandbox.Engine.Physics.HitInfo> Sandbox.Engine.Physics.MyPhysics::CastRay(VRageMath.Vector3D from, VRageMath.Vector3D to, System.Int32 raycastFilterLayer)
stloc.s    9 (System.Nullable`1[Sandbox.Engine.Physics.MyPhysics+HitInfo])
ldarg.0
ldloc.0
ldfld      VRage.Game.Entity.MyEntity Sandbox.Game.Weapons.<>c__DisplayClass55_0::target
ldloc.s    9 (System.Nullable`1[Sandbox.Engine.Physics.MyPhysics+HitInfo])
call       System.Boolean Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::OnVisibilityRayCastReverseCompleted(VRage.Game.Entity.MyEntity target, System.Nullable`1<Sandbox.Engine.Physics.HitInfo> physTarget)
br =>      Label29
Label22
ldloc.s    4 (VRageMath.Vector3D)
ldloc.s    5 (VRageMath.Vector3D)
ldarg.0
ldfld      System.Collections.Generic.List`1<Sandbox.Engine.Physics.HitInfo> Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_hitCollector
ldc.i4.s   15
call       static System.Void Sandbox.Engine.Physics.MyPhysics::CastRay(VRageMath.Vector3D from, VRageMath.Vector3D to, System.Collections.Generic.List`1<Sandbox.Engine.Physics.HitInfo> toList, System.Int32 raycastFilterLayer)
ldarg.0
ldloc.0
ldfld      VRage.Game.Entity.MyEntity Sandbox.Game.Weapons.<>c__DisplayClass55_0::target
ldarg.0
ldfld      System.Collections.Generic.List`1<Sandbox.Engine.Physics.HitInfo> Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_hitCollector
ldarg.0
ldfld      Sandbox.Game.Entities.Interfaces.IMyTargetingReceiver Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_targetReceiver
castclass  VRage.Game.Entity.MyEntity
call       System.Boolean Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::OnVisibilityRayCastFullCompleted(VRage.Game.Entity.MyEntity target, System.Collections.Generic.List`1<Sandbox.Engine.Physics.HitInfo> physTargets, VRage.Game.Entity.MyEntity ignoredTarget)
// end original
Label23
Label24
Label25
Label26
Label27
Label28
Label29
ret
DONE