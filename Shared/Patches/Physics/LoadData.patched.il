// 06ea6ea2
call static System.Boolean Sandbox.Game.Multiplayer.Sync::get_IsServer()
brtrue.s L0
ldc.i4 25002
br.s L1
L0:
ldc.i4 25001
L1:
call static System.Void Havok.HkVDB::set_Port(System.Int32 value)
ldc.i4 -668493307
ldc.i4.0
call static System.Void Havok.HkBaseSystem::EnableAssert(System.Int32 assertId, System.Boolean enable)
ldc.i4 952495168
ldc.i4.0
call static System.Void Havok.HkBaseSystem::EnableAssert(System.Int32 assertId, System.Boolean enable)
ldc.i4 1501626980
ldc.i4.0
call static System.Void Havok.HkBaseSystem::EnableAssert(System.Int32 assertId, System.Boolean enable)
ldc.i4 -258736554
ldc.i4.0
call static System.Void Havok.HkBaseSystem::EnableAssert(System.Int32 assertId, System.Boolean enable)
ldc.i4 524771844
ldc.i4.0
call static System.Void Havok.HkBaseSystem::EnableAssert(System.Int32 assertId, System.Boolean enable)
ldc.i4 1081361407
ldc.i4.0
call static System.Void Havok.HkBaseSystem::EnableAssert(System.Int32 assertId, System.Boolean enable)
ldc.i4 -1383504214
ldc.i4.0
call static System.Void Havok.HkBaseSystem::EnableAssert(System.Int32 assertId, System.Boolean enable)
ldc.i4 -265005969
ldc.i4.0
call static System.Void Havok.HkBaseSystem::EnableAssert(System.Int32 assertId, System.Boolean enable)
ldc.i4 1976984315
ldc.i4.0
call static System.Void Havok.HkBaseSystem::EnableAssert(System.Int32 assertId, System.Boolean enable)
ldc.i4 -252450131
ldc.i4.0
call static System.Void Havok.HkBaseSystem::EnableAssert(System.Int32 assertId, System.Boolean enable)
ldc.i4 -1400416854
ldc.i4.0
call static System.Void Havok.HkBaseSystem::EnableAssert(System.Int32 assertId, System.Boolean enable)
call static System.Threading.Thread System.Threading.Thread::get_CurrentThread()
callvirt System.Int32 System.Threading.Thread::get_ManagedThreadId()
stsfld System.Int32 Sandbox.Engine.Physics.MyPhysics::ThreadId
call static Sandbox.Game.World.MySession Sandbox.Game.World.MySession::get_Static()
ldfld System.Nullable`1<VRageMath.BoundingBoxD> Sandbox.Game.World.MySession::WorldBoundaries
ldsfld System.Boolean Sandbox.Engine.Utils.MyFakes::MP_SYNC_CLUSTERTREE
brfalse.s L2
call static System.Boolean Sandbox.Game.Multiplayer.Sync::get_IsServer()
ldc.i4.0
ceq
br.s L3
L2:
ldc.i4.0
L3:
newobj System.Void VRageMath.Spatial.MyClusterTree::.ctor(System.Nullable`1<VRageMath.BoundingBoxD> singleCluster, System.Boolean forcedClusters)
stsfld VRageMath.Spatial.MyClusterTree Sandbox.Engine.Physics.MyPhysics::Clusters
ldsfld VRageMath.Spatial.MyClusterTree Sandbox.Engine.Physics.MyPhysics::Clusters
dup
ldfld System.Func`3<System.Int32, VRageMath.BoundingBoxD, System.Object> VRageMath.Spatial.MyClusterTree::OnClusterCreated
ldarg.0
ldftn Havok.HkWorld Sandbox.Engine.Physics.MyPhysics::OnClusterCreated(System.Int32 clusterId, VRageMath.BoundingBoxD bbox)
newobj System.Void System.Func`3<System.Int32, VRageMath.BoundingBoxD, System.Object>::.ctor(System.Object object, System.IntPtr method)
call static System.Delegate System.Delegate::Combine(System.Delegate a, System.Delegate b)
castclass System.Func`3[System.Int32,VRageMath.BoundingBoxD,System.Object]
stfld System.Func`3<System.Int32, VRageMath.BoundingBoxD, System.Object> VRageMath.Spatial.MyClusterTree::OnClusterCreated
ldsfld VRageMath.Spatial.MyClusterTree Sandbox.Engine.Physics.MyPhysics::Clusters
dup
ldfld System.Action`2<System.Object, System.Int32> VRageMath.Spatial.MyClusterTree::OnClusterRemoved
ldarg.0
ldftn System.Void Sandbox.Engine.Physics.MyPhysics::OnClusterRemoved(System.Object world, System.Int32 clusterId)
newobj System.Void System.Action`2<System.Object, System.Int32>::.ctor(System.Object object, System.IntPtr method)
call static System.Delegate System.Delegate::Combine(System.Delegate a, System.Delegate b)
castclass System.Action`2[System.Object,System.Int32]
stfld System.Action`2<System.Object, System.Int32> VRageMath.Spatial.MyClusterTree::OnClusterRemoved
ldsfld VRageMath.Spatial.MyClusterTree Sandbox.Engine.Physics.MyPhysics::Clusters
dup
ldfld System.Action`1<System.Object> VRageMath.Spatial.MyClusterTree::OnFinishBatch
ldarg.0
ldftn System.Void Sandbox.Engine.Physics.MyPhysics::OnFinishBatch(System.Object world)
newobj System.Void System.Action`1<System.Object>::.ctor(System.Object object, System.IntPtr method)
call static System.Delegate System.Delegate::Combine(System.Delegate a, System.Delegate b)
castclass System.Action`1[System.Object]
stfld System.Action`1<System.Object> VRageMath.Spatial.MyClusterTree::OnFinishBatch
ldsfld VRageMath.Spatial.MyClusterTree Sandbox.Engine.Physics.MyPhysics::Clusters
dup
ldfld System.Action VRageMath.Spatial.MyClusterTree::OnClustersReordered
ldarg.0
ldftn System.Void Sandbox.Engine.Physics.MyPhysics::Tree_OnClustersReordered()
newobj System.Void System.Action::.ctor(System.Object object, System.IntPtr method)
call static System.Delegate System.Delegate::Combine(System.Delegate a, System.Delegate b)
castclass System.Action
stfld System.Action VRageMath.Spatial.MyClusterTree::OnClustersReordered
ldsfld VRageMath.Spatial.MyClusterTree Sandbox.Engine.Physics.MyPhysics::Clusters
dup
ldfld System.Func`2<System.Int64, System.Boolean> VRageMath.Spatial.MyClusterTree::GetEntityReplicableExistsById
ldarg.0
ldftn System.Boolean Sandbox.Engine.Physics.MyPhysics::GetEntityReplicableExistsById(System.Int64 entityId)
newobj System.Void System.Func`2<System.Int64, System.Boolean>::.ctor(System.Object object, System.IntPtr method)
call static System.Delegate System.Delegate::Combine(System.Delegate a, System.Delegate b)
castclass System.Func`2[System.Int64,System.Boolean]
stfld System.Func`2<System.Int64, System.Boolean> VRageMath.Spatial.MyClusterTree::GetEntityReplicableExistsById
ldsfld System.Boolean Sandbox.Engine.Platform.Game::IsDedicated
brfalse.s L4
call static Sandbox.Game.World.MySession Sandbox.Game.World.MySession::get_Static()
ldfld VRage.Game.MyObjectBuilder_SessionSettings Sandbox.Game.World.MySession::Settings
ldfld System.Boolean VRage.Game.MyObjectBuilder_SessionSettings::EnableSelectivePhysicsUpdates
brfalse.s L5
ldarg.0
ldsfld VRageMath.Spatial.MyClusterTree Sandbox.Engine.Physics.MyPhysics::Clusters
newobj System.Void Sandbox.Engine.Physics.MyWorldObserver::.ctor(VRageMath.Spatial.MyClusterTree clusterTree)
stfld Sandbox.Engine.Physics.MyWorldObserver Sandbox.Engine.Physics.MyPhysics::m_worldObserver
L4:
L5:
newobj System.Void System.Collections.Concurrent.ConcurrentQueue`1<Sandbox.Engine.Physics.ForceInfo>::.ctor()
call static System.Void Sandbox.Engine.Physics.MyPhysics::set_QueuedForces(System.Collections.Concurrent.ConcurrentQueue`1<Sandbox.Engine.Physics.ForceInfo> value)
ldsfld System.Boolean Sandbox.Engine.Utils.MyFakes::ENABLE_HAVOK_MULTITHREADING
brfalse.s L6
call static VRage.IVRagePlatform VRage.MyVRage::get_Platform()
callvirt abstract virtual VRage.IVRageSystem VRage.IVRagePlatform::get_System()
callvirt abstract virtual System.Nullable`1<System.Int32> VRage.IVRageSystem::get_OptimalHavokThreadCount()
stloc.0
ldc.i4 16
stloc.0
ldloca.s 0 (System.Nullable`1[System.Int32])
call System.Boolean System.Nullable`1<System.Int32>::get_HasValue()
brfalse.s L7
ldloca.s 0 (System.Nullable`1[System.Int32])
call System.Int32 System.Nullable`1<System.Int32>::get_Value()
newobj System.Void Havok.HkJobThreadPool::.ctor(System.Int32 threadCount)
stsfld Havok.HkJobThreadPool Sandbox.Engine.Physics.MyPhysics::m_threadPool
br.s L8
L7:
newobj System.Void Havok.HkJobThreadPool::.ctor()
stsfld Havok.HkJobThreadPool Sandbox.Engine.Physics.MyPhysics::m_threadPool
L8:
ldsfld Havok.HkJobThreadPool Sandbox.Engine.Physics.MyPhysics::m_threadPool
callvirt System.Int32 Havok.HkJobThreadPool::get_ThreadCount()
ldc.i4.1
add
newobj System.Void Havok.HkJobQueue::.ctor(System.Int32 threadCount)
stsfld Havok.HkJobQueue Sandbox.Engine.Physics.MyPhysics::m_jobQueue
L6:
ldc.i4.s 32
call static System.Void Havok.HkCylinderShape::SetNumberOfVirtualSideSegments(System.Int32 number)
ldarg.0
call System.Void Sandbox.Engine.Physics.MyPhysics::InitStepOptimizer()
ret
