// e9046217
ldarg.0
ldc.i4.3
stfld System.Int32 Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_VisibleCachePeriod
ldarg.0
ldc.i4.2
stfld Sandbox.Game.Weapons.MyTargetingOption Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_raycastMode
ldarg.0
newobj System.Void System.Collections.Concurrent.ConcurrentDictionary`2<System.Int64, VRage.Game.Entity.MyEntity>::.ctor()
stfld System.Collections.Concurrent.ConcurrentDictionary`2<System.Int64, VRage.Game.Entity.MyEntity> Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_raycastIgnoredEntities
ldarg.0
newobj System.Void System.Collections.Generic.List`1<Sandbox.Engine.Physics.HitInfo>::.ctor()
stfld System.Collections.Generic.List`1<Sandbox.Engine.Physics.HitInfo> Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_hitCollector
ldarg.0
newobj System.Void Sandbox.Game.Weapons.MyTargetSelectionWorkData::.ctor()
stfld Sandbox.Game.Weapons.MyTargetSelectionWorkData Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_targetSelectionWorkData
ldarg.0
newobj System.Void VRage.FastResourceLock::.ctor()
stfld VRage.FastResourceLock Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_targetSelectionLock
ldarg.0
ldc.i4.1
stfld System.Boolean Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_checkOtherTargets
ldarg.0
ldc.i4.1
newarr VRage.Game.Entity.MyEntity
stfld VRage.Game.Entity.MyEntity[] Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_forcedTargetListWrapped
ldarg.0
newobj System.Void System.Collections.Generic.List`1<VRage.Game.Entity.MyEntity>::.ctor()
stfld System.Collections.Generic.List`1<VRage.Game.Entity.MyEntity> Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_forcedTargetBlocks
ldarg.0
call System.Void System.Object::.ctor()
ldarg.0
ldarg.1
stfld Sandbox.Game.Entities.Interfaces.IMyTargetingReceiver Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_targetReceiver
ldarg.0
ldc.i4.0
call System.Void Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::set_IsPotentialTarget(System.Boolean value)
ldarg.0
ldarg.0
ldfld Sandbox.Game.Entities.Interfaces.IMyTargetingReceiver Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_targetReceiver
newobj System.Void Sandbox.Game.Weapons.MyPositionPrediction::.ctor(Sandbox.ModAPI.IMyShootOrigin originEntity)
stfld Sandbox.Game.Weapons.MyPositionPrediction Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::TargetPrediction
ldarg.0
ldarg.2
stfld Sandbox.Game.Weapons.MyTargetingOption Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_raycastMode
ret
