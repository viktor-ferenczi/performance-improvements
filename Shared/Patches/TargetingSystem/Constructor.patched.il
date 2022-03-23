// e9046217
ldarg.0 NULL
ldc.i4.3 NULL
stfld System.Int32 Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_VisibleCachePeriod
ldarg.0 NULL
ldc.i4.2 NULL
stfld Sandbox.Game.Weapons.MyTargetingOption Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_raycastMode
ldarg.0 NULL
newobj System.Void System.Collections.Generic.Dictionary`2<System.Int64, VRage.Game.Entity.MyEntity>::.ctor()
stfld System.Collections.Generic.Dictionary`2<System.Int64, VRage.Game.Entity.MyEntity> Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_raycastIgnoredEntities
ldarg.0 NULL
newobj System.Void System.Collections.Generic.List`1<Sandbox.Engine.Physics.HitInfo>::.ctor()
stfld System.Collections.Generic.List`1<Sandbox.Engine.Physics.HitInfo> Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_hitCollector
ldarg.0 NULL
newobj System.Void Sandbox.Game.Weapons.MyTargetSelectionWorkData::.ctor()
stfld Sandbox.Game.Weapons.MyTargetSelectionWorkData Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_targetSelectionWorkData
ldarg.0 NULL
newobj System.Void VRage.FastResourceLock::.ctor()
stfld VRage.FastResourceLock Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_targetSelectionLock
ldarg.0 NULL
ldc.i4.1 NULL
stfld System.Boolean Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_checkOtherTargets
ldarg.0 NULL
ldc.i4.1 NULL
newarr VRage.Game.Entity.MyEntity
stfld VRage.Game.Entity.MyEntity[] Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_forcedTargetListWrapped
ldarg.0 NULL
newobj System.Void System.Collections.Generic.List`1<VRage.Game.Entity.MyEntity>::.ctor()
stfld System.Collections.Generic.List`1<VRage.Game.Entity.MyEntity> Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_forcedTargetBlocks
ldarg.0 NULL
call System.Void System.Object::.ctor()
ldarg.0 NULL
ldarg.1 NULL
stfld Sandbox.Game.Entities.Interfaces.IMyTargetingReceiver Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_targetReceiver
ldarg.0 NULL
ldc.i4.0 NULL
call System.Void Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::set_IsPotentialTarget(System.Boolean value)
ldarg.0 NULL
ldarg.0 NULL
ldfld Sandbox.Game.Entities.Interfaces.IMyTargetingReceiver Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_targetReceiver
newobj System.Void Sandbox.Game.Weapons.MyPositionPrediction::.ctor(Sandbox.ModAPI.IMyShootOrigin originEntity)
stfld Sandbox.Game.Weapons.MyPositionPrediction Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::TargetPrediction
ldarg.0 NULL
ldarg.2 NULL
stfld Sandbox.Game.Weapons.MyTargetingOption Sandbox.Game.Weapons.MyLargeTurretTargetingSystem::m_raycastMode
ret NULL
