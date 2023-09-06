// a3ee3358
ldarg.0
ldfld VRage.FastResourceLock VRageMath.Spatial.MyClusterTree::m_clustersReorderLock
call static VRage.MyExclusiveLock VRage.FastResourceLockExtensions::AcquireExclusiveUsing(VRage.FastResourceLock lockObject)
stloc.0
[EX_BeginException]
ldarga.s 1
ldsfld VRageMath.Vector3 VRageMath.Spatial.MyClusterTree::IdealClusterSize
ldc.r4 2
call static VRageMath.Vector3 VRageMath.Vector3::op_Division(VRageMath.Vector3 value, System.Single divider)
call VRageMath.BoundingBoxD VRageMath.BoundingBoxD::GetInflated(VRageMath.Vector3 size)
stloc.1
ldloca.s 1 (VRageMath.BoundingBoxD)
ldsfld VRageMath.Vector3 VRageMath.Spatial.MyClusterTree::IdealClusterSize
call static VRageMath.Vector3D VRageMath.Vector3D::op_Implicit(VRageMath.Vector3 v)
call System.Void VRageMath.BoundingBoxD::InflateToMinimum(VRageMath.Vector3D minimumSize)
ldarg.0
ldfld VRageMath.MyDynamicAABBTreeD VRageMath.Spatial.MyClusterTree::m_clusterTree
ldloca.s 1 (VRageMath.BoundingBoxD)
call static System.Collections.Generic.List`1<VRageMath.Spatial.MyCluster> VRageMath.Spatial.MyClusterTree::get_m_resultList()
ldc.i4.0
ldc.i4.1
callvirt System.Void VRageMath.MyDynamicAABBTreeD::OverlapAllBoundingBox(VRageMath.BoundingBoxD& bbox, System.Collections.Generic.List`1<VRageMath.Spatial.MyCluster> elementsList, System.UInt32 requiredFlags, System.Boolean clear)
newobj System.Void System.Collections.Generic.HashSet`1<VRageMath.Spatial.MyObjectData>::.ctor()
stloc.2
ldc.i4.0
stloc.3
br L0
L16:
ldloc.2
callvirt virtual System.Void System.Collections.Generic.HashSet`1<VRageMath.Spatial.MyObjectData>::Clear()
ldarg.2
ldc.i4.m1
conv.i8
beq.s L1
ldloc.2
ldarg.0
ldfld System.Collections.Generic.Dictionary`2<System.UInt64, VRageMath.Spatial.MyObjectData> VRageMath.Spatial.MyClusterTree::m_objectsData
ldarg.2
callvirt virtual VRageMath.Spatial.MyObjectData System.Collections.Generic.Dictionary`2<System.UInt64, VRageMath.Spatial.MyObjectData>::get_Item(System.UInt64 key)
callvirt virtual System.Boolean System.Collections.Generic.HashSet`1<VRageMath.Spatial.MyObjectData>::Add(VRageMath.Spatial.MyObjectData item)
pop
L1:
L9:
L3:
L4:
L6:
L5:
L8:
L2:
L7:
nop
call static System.Collections.Generic.List`1<VRageMath.Spatial.MyCluster> VRageMath.Spatial.MyClusterTree::get_m_resultList()
ldarg.0
ldfld System.Collections.Generic.Dictionary`2<System.UInt64, VRageMath.Spatial.MyObjectData> VRageMath.Spatial.MyClusterTree::m_objectsData
ldloc.2
ldloca.s 1
call static System.Void Shared.Patches.MyClusterTreePatch::OptimizedImplementation(System.Collections.Generic.List`1<VRageMath.Spatial.MyCluster> resultList, System.Object objectsDataAsObject, System.Object sourceAsObject, VRageMath.BoundingBoxD& inflated1)
L10:
call static System.Collections.Generic.List`1<VRageMath.Spatial.MyCluster> VRageMath.Spatial.MyClusterTree::get_m_resultList()
callvirt virtual System.Int32 System.Collections.Generic.List`1<VRageMath.Spatial.MyCluster>::get_Count()
ldarg.0
ldfld VRageMath.MyDynamicAABBTreeD VRageMath.Spatial.MyClusterTree::m_clusterTree
ldloca.s 1 (VRageMath.BoundingBoxD)
call static System.Collections.Generic.List`1<VRageMath.Spatial.MyCluster> VRageMath.Spatial.MyClusterTree::get_m_resultList()
ldc.i4.0
ldc.i4.1
callvirt System.Void VRageMath.MyDynamicAABBTreeD::OverlapAllBoundingBox(VRageMath.BoundingBoxD& bbox, System.Collections.Generic.List`1<VRageMath.Spatial.MyCluster> elementsList, System.UInt32 requiredFlags, System.Boolean clear)
call static System.Collections.Generic.List`1<VRageMath.Spatial.MyCluster> VRageMath.Spatial.MyClusterTree::get_m_resultList()
callvirt virtual System.Int32 System.Collections.Generic.List`1<VRageMath.Spatial.MyCluster>::get_Count()
ceq
stloc.3
ldarg.0
ldfld VRageMath.MyDynamicAABBTreeD VRageMath.Spatial.MyClusterTree::m_staticTree
ldloca.s 1 (VRageMath.BoundingBoxD)
call static System.Collections.Generic.List`1<System.UInt64> VRageMath.Spatial.MyClusterTree::get_m_objectDataResultList()
ldc.i4.0
ldc.i4.1
callvirt System.Void VRageMath.MyDynamicAABBTreeD::OverlapAllBoundingBox(VRageMath.BoundingBoxD& bbox, System.Collections.Generic.List`1<System.UInt64> elementsList, System.UInt32 requiredFlags, System.Boolean clear)
call static System.Collections.Generic.List`1<System.UInt64> VRageMath.Spatial.MyClusterTree::get_m_objectDataResultList()
callvirt System.Collections.Generic.Enumerator<System.UInt64> System.Collections.Generic.List`1<System.UInt64>::GetEnumerator()
stloc.s 18 (System.Collections.Generic.List`1+Enumerator[System.UInt64])
[EX_BeginException]
br.s L11
L14:
ldloca.s 18 (System.Collections.Generic.List`1+Enumerator[System.UInt64])
call virtual System.UInt64 System.Collections.Generic.Enumerator<System.UInt64>::get_Current()
stloc.s 19 (System.UInt64)
ldarg.0
ldfld System.Collections.Generic.Dictionary`2<System.UInt64, VRageMath.Spatial.MyObjectData> VRageMath.Spatial.MyClusterTree::m_objectsData
ldloc.s 19 (System.UInt64)
callvirt virtual VRageMath.Spatial.MyObjectData System.Collections.Generic.Dictionary`2<System.UInt64, VRageMath.Spatial.MyObjectData>::get_Item(System.UInt64 key)
ldfld VRageMath.Spatial.MyCluster VRageMath.Spatial.MyObjectData::Cluster
brfalse.s L12
call static System.Collections.Generic.List`1<VRageMath.Spatial.MyCluster> VRageMath.Spatial.MyClusterTree::get_m_resultList()
ldarg.0
ldfld System.Collections.Generic.Dictionary`2<System.UInt64, VRageMath.Spatial.MyObjectData> VRageMath.Spatial.MyClusterTree::m_objectsData
ldloc.s 19 (System.UInt64)
callvirt virtual VRageMath.Spatial.MyObjectData System.Collections.Generic.Dictionary`2<System.UInt64, VRageMath.Spatial.MyObjectData>::get_Item(System.UInt64 key)
ldfld VRageMath.Spatial.MyCluster VRageMath.Spatial.MyObjectData::Cluster
callvirt virtual System.Boolean System.Collections.Generic.List`1<VRageMath.Spatial.MyCluster>::Contains(VRageMath.Spatial.MyCluster item)
brtrue.s L13
ldloca.s 1 (VRageMath.BoundingBoxD)
ldarg.0
ldfld System.Collections.Generic.Dictionary`2<System.UInt64, VRageMath.Spatial.MyObjectData> VRageMath.Spatial.MyClusterTree::m_objectsData
ldloc.s 19 (System.UInt64)
callvirt virtual VRageMath.Spatial.MyObjectData System.Collections.Generic.Dictionary`2<System.UInt64, VRageMath.Spatial.MyObjectData>::get_Item(System.UInt64 key)
ldflda VRageMath.BoundingBoxD VRageMath.Spatial.MyObjectData::AABB
ldsfld VRageMath.Vector3 VRageMath.Spatial.MyClusterTree::IdealClusterSize
ldc.r4 2
call static VRageMath.Vector3 VRageMath.Vector3::op_Division(VRageMath.Vector3 value, System.Single divider)
call VRageMath.BoundingBoxD VRageMath.BoundingBoxD::GetInflated(VRageMath.Vector3 size)
call VRageMath.BoundingBoxD VRageMath.BoundingBoxD::Include(VRageMath.BoundingBoxD box)
pop
ldc.i4.0
stloc.3
L11:
L12:
L13:
ldloca.s 18 (System.Collections.Generic.List`1+Enumerator[System.UInt64])
call virtual System.Boolean System.Collections.Generic.Enumerator<System.UInt64>::MoveNext()
brtrue.s L14
leave.s L15
[EX_BeginFinally]
ldloca.s 18 (System.Collections.Generic.List`1+Enumerator[System.UInt64])
constrained. System.Collections.Generic.List`1+Enumerator[System.UInt64]
callvirt abstract virtual System.Void System.IDisposable::Dispose()
[EX_EndException]
endfinally
L0:
L15:
ldloc.3
brfalse L16
ldarg.0
ldfld VRageMath.MyDynamicAABBTreeD VRageMath.Spatial.MyClusterTree::m_staticTree
ldloca.s 1 (VRageMath.BoundingBoxD)
call static System.Collections.Generic.List`1<System.UInt64> VRageMath.Spatial.MyClusterTree::get_m_objectDataResultList()
ldc.i4.0
ldc.i4.1
callvirt System.Void VRageMath.MyDynamicAABBTreeD::OverlapAllBoundingBox(VRageMath.BoundingBoxD& bbox, System.Collections.Generic.List`1<System.UInt64> elementsList, System.UInt32 requiredFlags, System.Boolean clear)
call static System.Collections.Generic.List`1<System.UInt64> VRageMath.Spatial.MyClusterTree::get_m_objectDataResultList()
callvirt System.Collections.Generic.Enumerator<System.UInt64> System.Collections.Generic.List`1<System.UInt64>::GetEnumerator()
stloc.s 18 (System.Collections.Generic.List`1+Enumerator[System.UInt64])
[EX_BeginException]
br.s L17
L18:
ldloca.s 18 (System.Collections.Generic.List`1+Enumerator[System.UInt64])
call virtual System.UInt64 System.Collections.Generic.Enumerator<System.UInt64>::get_Current()
stloc.s 20 (System.UInt64)
ldloc.2
ldarg.0
ldfld System.Collections.Generic.Dictionary`2<System.UInt64, VRageMath.Spatial.MyObjectData> VRageMath.Spatial.MyClusterTree::m_objectsData
ldloc.s 20 (System.UInt64)
callvirt virtual VRageMath.Spatial.MyObjectData System.Collections.Generic.Dictionary`2<System.UInt64, VRageMath.Spatial.MyObjectData>::get_Item(System.UInt64 key)
callvirt virtual System.Boolean System.Collections.Generic.HashSet`1<VRageMath.Spatial.MyObjectData>::Add(VRageMath.Spatial.MyObjectData item)
pop
L17:
ldloca.s 18 (System.Collections.Generic.List`1+Enumerator[System.UInt64])
call virtual System.Boolean System.Collections.Generic.Enumerator<System.UInt64>::MoveNext()
brtrue.s L18
leave.s L19
[EX_BeginFinally]
ldloca.s 18 (System.Collections.Generic.List`1+Enumerator[System.UInt64])
constrained. System.Collections.Generic.List`1+Enumerator[System.UInt64]
callvirt abstract virtual System.Void System.IDisposable::Dispose()
[EX_EndException]
endfinally
L19:
ldc.i4.8
stloc.s 4 (System.Int32)
ldc.i4.1
stloc.s 5 (System.Boolean)
newobj System.Void System.Collections.Generic.Stack`1<VRageMath.Spatial.MyClusterDescription>::.ctor()
stloc.s 6 (System.Collections.Generic.Stack`1[VRageMath.Spatial.MyClusterTree+MyClusterDescription])
newobj System.Void System.Collections.Generic.List`1<VRageMath.Spatial.MyClusterDescription>::.ctor()
stloc.s 7 (System.Collections.Generic.List`1[VRageMath.Spatial.MyClusterTree+MyClusterDescription])
ldnull
stloc.s 8 (System.Collections.Generic.List`1[VRageMath.Spatial.MyClusterTree+MyObjectData])
ldsfld VRageMath.Vector3 VRageMath.Spatial.MyClusterTree::IdealClusterSize
stloc.s 9 (VRageMath.Vector3)
br L20
L65:
ldloc.s 6 (System.Collections.Generic.Stack`1[VRageMath.Spatial.MyClusterTree+MyClusterDescription])
callvirt System.Void System.Collections.Generic.Stack`1<VRageMath.Spatial.MyClusterDescription>::Clear()
ldloc.s 7 (System.Collections.Generic.List`1[VRageMath.Spatial.MyClusterTree+MyClusterDescription])
callvirt virtual System.Void System.Collections.Generic.List`1<VRageMath.Spatial.MyClusterDescription>::Clear()
ldloca.s 22 (VRageMath.Spatial.MyClusterTree+MyClusterDescription)
initobj VRageMath.Spatial.MyClusterTree+MyClusterDescription
ldloca.s 22 (VRageMath.Spatial.MyClusterTree+MyClusterDescription)
ldloc.1
stfld VRageMath.BoundingBoxD VRageMath.Spatial.MyClusterDescription::AABB
ldloca.s 22 (VRageMath.Spatial.MyClusterTree+MyClusterDescription)
ldloc.2
ldsfld System.Func`2<VRageMath.Spatial.MyObjectData, System.Boolean> VRageMath.Spatial.<>c::<>9__65_3
dup
brtrue.s L21
pop
ldsfld VRageMath.Spatial.<>c VRageMath.Spatial.<>c::<>9
ldftn System.Boolean VRageMath.Spatial.<>c::<ReorderClusters>b__65_3(VRageMath.Spatial.MyObjectData x)
newobj System.Void System.Func`2<VRageMath.Spatial.MyObjectData, System.Boolean>::.ctor(System.Object object, System.IntPtr method)
dup
stsfld System.Func`2<VRageMath.Spatial.MyObjectData, System.Boolean> VRageMath.Spatial.<>c::<>9__65_3
L21:
call static System.Collections.Generic.IEnumerable`1<VRageMath.Spatial.MyObjectData> System.Linq.Enumerable::Where(System.Collections.Generic.IEnumerable`1<VRageMath.Spatial.MyObjectData> source, System.Func`2<VRageMath.Spatial.MyObjectData, System.Boolean> predicate)
call static System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData> System.Linq.Enumerable::ToList(System.Collections.Generic.IEnumerable`1<VRageMath.Spatial.MyObjectData> source)
stfld System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData> VRageMath.Spatial.MyClusterDescription::DynamicObjects
ldloca.s 22 (VRageMath.Spatial.MyClusterTree+MyClusterDescription)
ldloc.2
ldsfld System.Func`2<VRageMath.Spatial.MyObjectData, System.Boolean> VRageMath.Spatial.<>c::<>9__65_4
dup
brtrue.s L22
pop
ldsfld VRageMath.Spatial.<>c VRageMath.Spatial.<>c::<>9
ldftn System.Boolean VRageMath.Spatial.<>c::<ReorderClusters>b__65_4(VRageMath.Spatial.MyObjectData x)
newobj System.Void System.Func`2<VRageMath.Spatial.MyObjectData, System.Boolean>::.ctor(System.Object object, System.IntPtr method)
dup
stsfld System.Func`2<VRageMath.Spatial.MyObjectData, System.Boolean> VRageMath.Spatial.<>c::<>9__65_4
L22:
call static System.Collections.Generic.IEnumerable`1<VRageMath.Spatial.MyObjectData> System.Linq.Enumerable::Where(System.Collections.Generic.IEnumerable`1<VRageMath.Spatial.MyObjectData> source, System.Func`2<VRageMath.Spatial.MyObjectData, System.Boolean> predicate)
call static System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData> System.Linq.Enumerable::ToList(System.Collections.Generic.IEnumerable`1<VRageMath.Spatial.MyObjectData> source)
stfld System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData> VRageMath.Spatial.MyClusterDescription::StaticObjects
ldloc.s 22 (VRageMath.Spatial.MyClusterTree+MyClusterDescription)
stloc.s 21 (VRageMath.Spatial.MyClusterTree+MyClusterDescription)
ldloc.s 6 (System.Collections.Generic.Stack`1[VRageMath.Spatial.MyClusterTree+MyClusterDescription])
ldloc.s 21 (VRageMath.Spatial.MyClusterTree+MyClusterDescription)
callvirt System.Void System.Collections.Generic.Stack`1<VRageMath.Spatial.MyClusterDescription>::Push(VRageMath.Spatial.MyClusterDescription item)
ldloc.s 21 (VRageMath.Spatial.MyClusterTree+MyClusterDescription)
ldfld System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData> VRageMath.Spatial.MyClusterDescription::StaticObjects
ldsfld System.Func`2<VRageMath.Spatial.MyObjectData, System.Boolean> VRageMath.Spatial.<>c::<>9__65_2
dup
brtrue.s L23
pop
ldsfld VRageMath.Spatial.<>c VRageMath.Spatial.<>c::<>9
ldftn System.Boolean VRageMath.Spatial.<>c::<ReorderClusters>b__65_2(VRageMath.Spatial.MyObjectData x)
newobj System.Void System.Func`2<VRageMath.Spatial.MyObjectData, System.Boolean>::.ctor(System.Object object, System.IntPtr method)
dup
stsfld System.Func`2<VRageMath.Spatial.MyObjectData, System.Boolean> VRageMath.Spatial.<>c::<>9__65_2
L23:
call static System.Collections.Generic.IEnumerable`1<VRageMath.Spatial.MyObjectData> System.Linq.Enumerable::Where(System.Collections.Generic.IEnumerable`1<VRageMath.Spatial.MyObjectData> source, System.Func`2<VRageMath.Spatial.MyObjectData, System.Boolean> predicate)
call static System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData> System.Linq.Enumerable::ToList(System.Collections.Generic.IEnumerable`1<VRageMath.Spatial.MyObjectData> source)
stloc.s 8 (System.Collections.Generic.List`1[VRageMath.Spatial.MyClusterTree+MyObjectData])
ldloc.s 21 (VRageMath.Spatial.MyClusterTree+MyClusterDescription)
ldfld System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData> VRageMath.Spatial.MyClusterDescription::StaticObjects
callvirt virtual System.Int32 System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData>::get_Count()
pop
br L24
L58:
ldloc.s 6 (System.Collections.Generic.Stack`1[VRageMath.Spatial.MyClusterTree+MyClusterDescription])
callvirt VRageMath.Spatial.MyClusterDescription System.Collections.Generic.Stack`1<VRageMath.Spatial.MyClusterDescription>::Pop()
stloc.s 23 (VRageMath.Spatial.MyClusterTree+MyClusterDescription)
ldloc.s 23 (VRageMath.Spatial.MyClusterTree+MyClusterDescription)
ldfld System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData> VRageMath.Spatial.MyClusterDescription::DynamicObjects
callvirt virtual System.Int32 System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData>::get_Count()
brfalse L25
call static VRageMath.BoundingBoxD VRageMath.BoundingBoxD::CreateInvalid()
stloc.s 24 (VRageMath.BoundingBoxD)
ldc.i4.0
stloc.s 29 (System.Int32)
br.s L26
L27:
ldloc.s 23 (VRageMath.Spatial.MyClusterTree+MyClusterDescription)
ldfld System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData> VRageMath.Spatial.MyClusterDescription::DynamicObjects
ldloc.s 29 (System.Int32)
callvirt virtual VRageMath.Spatial.MyObjectData System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData>::get_Item(System.Int32 index)
ldflda VRageMath.BoundingBoxD VRageMath.Spatial.MyObjectData::AABB
ldloc.s 9 (VRageMath.Vector3)
ldc.r4 2
call static VRageMath.Vector3 VRageMath.Vector3::op_Division(VRageMath.Vector3 value, System.Single divider)
call VRageMath.BoundingBoxD VRageMath.BoundingBoxD::GetInflated(VRageMath.Vector3 size)
stloc.s 30 (VRageMath.BoundingBoxD)
ldloca.s 24 (VRageMath.BoundingBoxD)
ldloc.s 30 (VRageMath.BoundingBoxD)
call VRageMath.BoundingBoxD VRageMath.BoundingBoxD::Include(VRageMath.BoundingBoxD box)
pop
ldloc.s 29 (System.Int32)
ldc.i4.1
add
stloc.s 29 (System.Int32)
L26:
ldloc.s 29 (System.Int32)
ldloc.s 23 (VRageMath.Spatial.MyClusterTree+MyClusterDescription)
ldfld System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData> VRageMath.Spatial.MyClusterDescription::DynamicObjects
callvirt virtual System.Int32 System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData>::get_Count()
blt.s L27
ldloc.s 24 (VRageMath.BoundingBoxD)
stloc.s 25 (VRageMath.BoundingBoxD)
ldloca.s 24 (VRageMath.BoundingBoxD)
call VRageMath.Vector3D VRageMath.BoundingBoxD::get_Size()
stloc.s 31 (VRageMath.Vector3D)
ldloca.s 31 (VRageMath.Vector3D)
call System.Int32 VRageMath.Vector3D::AbsMaxComponent()
stloc.s 26 (System.Int32)
ldloc.s 26 (System.Int32)
switch L28, L29, L30
br.s L31
L28:
ldloc.s 23 (VRageMath.Spatial.MyClusterTree+MyClusterDescription)
ldfld System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData> VRageMath.Spatial.MyClusterDescription::DynamicObjects
ldsfld VRageMath.Spatial.AABBComparerX VRageMath.Spatial.AABBComparerX::Static
callvirt System.Void System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData>::Sort(System.Collections.Generic.IComparer`1<VRageMath.Spatial.MyObjectData> comparer)
br.s L32
L29:
ldloc.s 23 (VRageMath.Spatial.MyClusterTree+MyClusterDescription)
ldfld System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData> VRageMath.Spatial.MyClusterDescription::DynamicObjects
ldsfld VRageMath.Spatial.AABBComparerY VRageMath.Spatial.AABBComparerY::Static
callvirt System.Void System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData>::Sort(System.Collections.Generic.IComparer`1<VRageMath.Spatial.MyObjectData> comparer)
br.s L33
L30:
ldloc.s 23 (VRageMath.Spatial.MyClusterTree+MyClusterDescription)
ldfld System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData> VRageMath.Spatial.MyClusterDescription::DynamicObjects
ldsfld VRageMath.Spatial.AABBComparerZ VRageMath.Spatial.AABBComparerZ::Static
callvirt System.Void System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData>::Sort(System.Collections.Generic.IComparer`1<VRageMath.Spatial.MyObjectData> comparer)
L31:
L32:
L33:
ldc.i4.0
stloc.s 27 (System.Boolean)
ldloca.s 24 (VRageMath.BoundingBoxD)
call VRageMath.Vector3D VRageMath.BoundingBoxD::get_Size()
stloc.s 31 (VRageMath.Vector3D)
ldloca.s 31 (VRageMath.Vector3D)
call System.Double VRageMath.Vector3D::AbsMax()
ldsflda VRageMath.Vector3 VRageMath.Spatial.MyClusterTree::MaximumForSplit
call System.Single VRageMath.Vector3::AbsMax()
conv.r8
blt.un L34
call static VRageMath.BoundingBoxD VRageMath.BoundingBoxD::CreateInvalid()
stloc.s 32 (VRageMath.BoundingBoxD)
ldc.r8 -1.79769313486232E+308
stloc.s 33 (System.Double)
ldc.i4.1
stloc.s 34 (System.Int32)
br L35
L40:
ldloc.s 23 (VRageMath.Spatial.MyClusterTree+MyClusterDescription)
ldfld System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData> VRageMath.Spatial.MyClusterDescription::DynamicObjects
ldloc.s 34 (System.Int32)
ldc.i4.1
sub
callvirt virtual VRageMath.Spatial.MyObjectData System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData>::get_Item(System.Int32 index)
ldloc.s 23 (VRageMath.Spatial.MyClusterTree+MyClusterDescription)
ldfld System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData> VRageMath.Spatial.MyClusterDescription::DynamicObjects
ldloc.s 34 (System.Int32)
callvirt virtual VRageMath.Spatial.MyObjectData System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData>::get_Item(System.Int32 index)
stloc.s 35 (VRageMath.Spatial.MyClusterTree+MyObjectData)
ldflda VRageMath.BoundingBoxD VRageMath.Spatial.MyObjectData::AABB
ldloc.s 9 (VRageMath.Vector3)
ldc.r4 2
call static VRageMath.Vector3 VRageMath.Vector3::op_Division(VRageMath.Vector3 value, System.Single divider)
call VRageMath.BoundingBoxD VRageMath.BoundingBoxD::GetInflated(VRageMath.Vector3 size)
stloc.s 36 (VRageMath.BoundingBoxD)
ldloc.s 35 (VRageMath.Spatial.MyClusterTree+MyObjectData)
ldflda VRageMath.BoundingBoxD VRageMath.Spatial.MyObjectData::AABB
ldloc.s 9 (VRageMath.Vector3)
ldc.r4 2
call static VRageMath.Vector3 VRageMath.Vector3::op_Division(VRageMath.Vector3 value, System.Single divider)
call VRageMath.BoundingBoxD VRageMath.BoundingBoxD::GetInflated(VRageMath.Vector3 size)
stloc.s 37 (VRageMath.BoundingBoxD)
ldloca.s 36 (VRageMath.BoundingBoxD)
ldflda VRageMath.Vector3D VRageMath.BoundingBoxD::Max
ldloc.s 26 (System.Int32)
call System.Double VRageMath.Vector3D::GetDim(System.Int32 i)
stloc.s 38 (System.Double)
ldloc.s 38 (System.Double)
ldloc.s 33 (System.Double)
ble.un.s L36
ldloc.s 38 (System.Double)
stloc.s 33 (System.Double)
L36:
ldloca.s 32 (VRageMath.BoundingBoxD)
ldloc.s 36 (VRageMath.BoundingBoxD)
call VRageMath.BoundingBoxD VRageMath.BoundingBoxD::Include(VRageMath.BoundingBoxD box)
pop
ldloca.s 37 (VRageMath.BoundingBoxD)
ldflda VRageMath.Vector3D VRageMath.BoundingBoxD::Min
ldloc.s 26 (System.Int32)
call System.Double VRageMath.Vector3D::GetDim(System.Int32 i)
stloc.s 39 (System.Double)
ldloca.s 36 (VRageMath.BoundingBoxD)
ldflda VRageMath.Vector3D VRageMath.BoundingBoxD::Max
ldloc.s 26 (System.Int32)
call System.Double VRageMath.Vector3D::GetDim(System.Int32 i)
stloc.s 40 (System.Double)
ldloc.s 39 (System.Double)
ldloc.s 40 (System.Double)
sub
ldc.r8 0
ble.un.s L37
ldloc.s 33 (System.Double)
ldloc.s 39 (System.Double)
bgt.un.s L38
ldc.i4.1
stloc.s 27 (System.Boolean)
ldloc.s 32 (VRageMath.BoundingBoxD)
stloc.s 25 (VRageMath.BoundingBoxD)
br.s L39
L37:
L38:
ldloc.s 34 (System.Int32)
ldc.i4.1
add
stloc.s 34 (System.Int32)
L35:
ldloc.s 34 (System.Int32)
ldloc.s 23 (VRageMath.Spatial.MyClusterTree+MyClusterDescription)
ldfld System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData> VRageMath.Spatial.MyClusterDescription::DynamicObjects
callvirt virtual System.Int32 System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData>::get_Count()
blt L40
L34:
L39:
ldloca.s 25 (VRageMath.BoundingBoxD)
ldloc.s 9 (VRageMath.Vector3)
call static VRageMath.Vector3D VRageMath.Vector3D::op_Implicit(VRageMath.Vector3 v)
call System.Void VRageMath.BoundingBoxD::InflateToMinimum(VRageMath.Vector3D minimumSize)
ldloca.s 22 (VRageMath.Spatial.MyClusterTree+MyClusterDescription)
initobj VRageMath.Spatial.MyClusterTree+MyClusterDescription
ldloca.s 22 (VRageMath.Spatial.MyClusterTree+MyClusterDescription)
ldloc.s 25 (VRageMath.BoundingBoxD)
stfld VRageMath.BoundingBoxD VRageMath.Spatial.MyClusterDescription::AABB
ldloca.s 22 (VRageMath.Spatial.MyClusterTree+MyClusterDescription)
newobj System.Void System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData>::.ctor()
stfld System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData> VRageMath.Spatial.MyClusterDescription::DynamicObjects
ldloca.s 22 (VRageMath.Spatial.MyClusterTree+MyClusterDescription)
newobj System.Void System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData>::.ctor()
stfld System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData> VRageMath.Spatial.MyClusterDescription::StaticObjects
ldloc.s 22 (VRageMath.Spatial.MyClusterTree+MyClusterDescription)
stloc.s 28 (VRageMath.Spatial.MyClusterTree+MyClusterDescription)
ldloc.s 23 (VRageMath.Spatial.MyClusterTree+MyClusterDescription)
ldfld System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData> VRageMath.Spatial.MyClusterDescription::DynamicObjects
call static System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData> System.Linq.Enumerable::ToList(System.Collections.Generic.IEnumerable`1<VRageMath.Spatial.MyObjectData> source)
callvirt System.Collections.Generic.Enumerator<VRageMath.Spatial.MyObjectData> System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData>::GetEnumerator()
stloc.s 41 (System.Collections.Generic.List`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyObjectData])
[EX_BeginException]
br.s L41
L43:
ldloca.s 41 (System.Collections.Generic.List`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyObjectData])
call virtual VRageMath.Spatial.MyObjectData System.Collections.Generic.Enumerator<VRageMath.Spatial.MyObjectData>::get_Current()
stloc.s 42 (VRageMath.Spatial.MyClusterTree+MyObjectData)
ldloca.s 25 (VRageMath.BoundingBoxD)
ldloc.s 42 (VRageMath.Spatial.MyClusterTree+MyObjectData)
ldfld VRageMath.BoundingBoxD VRageMath.Spatial.MyObjectData::AABB
call VRageMath.ContainmentType VRageMath.BoundingBoxD::Contains(VRageMath.BoundingBoxD box)
ldc.i4.1
bne.un.s L42
ldloc.s 28 (VRageMath.Spatial.MyClusterTree+MyClusterDescription)
ldfld System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData> VRageMath.Spatial.MyClusterDescription::DynamicObjects
ldloc.s 42 (VRageMath.Spatial.MyClusterTree+MyObjectData)
callvirt virtual System.Void System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData>::Add(VRageMath.Spatial.MyObjectData item)
ldloc.s 23 (VRageMath.Spatial.MyClusterTree+MyClusterDescription)
ldfld System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData> VRageMath.Spatial.MyClusterDescription::DynamicObjects
ldloc.s 42 (VRageMath.Spatial.MyClusterTree+MyObjectData)
callvirt virtual System.Boolean System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData>::Remove(VRageMath.Spatial.MyObjectData item)
pop
L41:
L42:
ldloca.s 41 (System.Collections.Generic.List`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyObjectData])
call virtual System.Boolean System.Collections.Generic.Enumerator<VRageMath.Spatial.MyObjectData>::MoveNext()
brtrue.s L43
leave.s L44
[EX_BeginFinally]
ldloca.s 41 (System.Collections.Generic.List`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyObjectData])
constrained. System.Collections.Generic.List`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyObjectData]
callvirt abstract virtual System.Void System.IDisposable::Dispose()
[EX_EndException]
endfinally
L44:
ldloc.s 23 (VRageMath.Spatial.MyClusterTree+MyClusterDescription)
ldfld System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData> VRageMath.Spatial.MyClusterDescription::StaticObjects
call static System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData> System.Linq.Enumerable::ToList(System.Collections.Generic.IEnumerable`1<VRageMath.Spatial.MyObjectData> source)
callvirt System.Collections.Generic.Enumerator<VRageMath.Spatial.MyObjectData> System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData>::GetEnumerator()
stloc.s 41 (System.Collections.Generic.List`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyObjectData])
[EX_BeginException]
br.s L45
L48:
ldloca.s 41 (System.Collections.Generic.List`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyObjectData])
call virtual VRageMath.Spatial.MyObjectData System.Collections.Generic.Enumerator<VRageMath.Spatial.MyObjectData>::get_Current()
stloc.s 43 (VRageMath.Spatial.MyClusterTree+MyObjectData)
ldloca.s 25 (VRageMath.BoundingBoxD)
ldloc.s 43 (VRageMath.Spatial.MyClusterTree+MyObjectData)
ldfld VRageMath.BoundingBoxD VRageMath.Spatial.MyObjectData::AABB
call VRageMath.ContainmentType VRageMath.BoundingBoxD::Contains(VRageMath.BoundingBoxD box)
stloc.s 44 (VRageMath.ContainmentType)
ldloc.s 44 (VRageMath.ContainmentType)
ldc.i4.1
beq.s L46
ldloc.s 44 (VRageMath.ContainmentType)
ldc.i4.2
bne.un.s L47
L46:
ldloc.s 28 (VRageMath.Spatial.MyClusterTree+MyClusterDescription)
ldfld System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData> VRageMath.Spatial.MyClusterDescription::StaticObjects
ldloc.s 43 (VRageMath.Spatial.MyClusterTree+MyObjectData)
callvirt virtual System.Void System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData>::Add(VRageMath.Spatial.MyObjectData item)
ldloc.s 23 (VRageMath.Spatial.MyClusterTree+MyClusterDescription)
ldfld System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData> VRageMath.Spatial.MyClusterDescription::StaticObjects
ldloc.s 43 (VRageMath.Spatial.MyClusterTree+MyObjectData)
callvirt virtual System.Boolean System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData>::Remove(VRageMath.Spatial.MyObjectData item)
pop
L45:
L47:
ldloca.s 41 (System.Collections.Generic.List`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyObjectData])
call virtual System.Boolean System.Collections.Generic.Enumerator<VRageMath.Spatial.MyObjectData>::MoveNext()
brtrue.s L48
leave.s L49
[EX_BeginFinally]
ldloca.s 41 (System.Collections.Generic.List`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyObjectData])
constrained. System.Collections.Generic.List`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyObjectData]
callvirt abstract virtual System.Void System.IDisposable::Dispose()
[EX_EndException]
endfinally
L49:
ldloc.s 23 (VRageMath.Spatial.MyClusterTree+MyClusterDescription)
ldfld System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData> VRageMath.Spatial.MyClusterDescription::DynamicObjects
callvirt virtual System.Int32 System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData>::get_Count()
ldc.i4.0
ble L50
call static VRageMath.BoundingBoxD VRageMath.BoundingBoxD::CreateInvalid()
stloc.s 45 (VRageMath.BoundingBoxD)
ldloc.s 23 (VRageMath.Spatial.MyClusterTree+MyClusterDescription)
ldfld System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData> VRageMath.Spatial.MyClusterDescription::DynamicObjects
callvirt System.Collections.Generic.Enumerator<VRageMath.Spatial.MyObjectData> System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData>::GetEnumerator()
stloc.s 41 (System.Collections.Generic.List`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyObjectData])
[EX_BeginException]
br.s L51
L52:
ldloca.s 41 (System.Collections.Generic.List`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyObjectData])
call virtual VRageMath.Spatial.MyObjectData System.Collections.Generic.Enumerator<VRageMath.Spatial.MyObjectData>::get_Current()
stloc.s 47 (VRageMath.Spatial.MyClusterTree+MyObjectData)
ldloca.s 45 (VRageMath.BoundingBoxD)
ldloc.s 47 (VRageMath.Spatial.MyClusterTree+MyObjectData)
ldflda VRageMath.BoundingBoxD VRageMath.Spatial.MyObjectData::AABB
ldloc.s 9 (VRageMath.Vector3)
ldc.r4 2
call static VRageMath.Vector3 VRageMath.Vector3::op_Division(VRageMath.Vector3 value, System.Single divider)
call VRageMath.BoundingBoxD VRageMath.BoundingBoxD::GetInflated(VRageMath.Vector3 size)
call VRageMath.BoundingBoxD VRageMath.BoundingBoxD::Include(VRageMath.BoundingBoxD box)
pop
L51:
ldloca.s 41 (System.Collections.Generic.List`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyObjectData])
call virtual System.Boolean System.Collections.Generic.Enumerator<VRageMath.Spatial.MyObjectData>::MoveNext()
brtrue.s L52
leave.s L53
[EX_BeginFinally]
ldloca.s 41 (System.Collections.Generic.List`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyObjectData])
constrained. System.Collections.Generic.List`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyObjectData]
callvirt abstract virtual System.Void System.IDisposable::Dispose()
[EX_EndException]
endfinally
L53:
ldloca.s 45 (VRageMath.BoundingBoxD)
ldloc.s 9 (VRageMath.Vector3)
call static VRageMath.Vector3D VRageMath.Vector3D::op_Implicit(VRageMath.Vector3 v)
call System.Void VRageMath.BoundingBoxD::InflateToMinimum(VRageMath.Vector3D minimumSize)
ldloca.s 22 (VRageMath.Spatial.MyClusterTree+MyClusterDescription)
initobj VRageMath.Spatial.MyClusterTree+MyClusterDescription
ldloca.s 22 (VRageMath.Spatial.MyClusterTree+MyClusterDescription)
ldloc.s 45 (VRageMath.BoundingBoxD)
stfld VRageMath.BoundingBoxD VRageMath.Spatial.MyClusterDescription::AABB
ldloca.s 22 (VRageMath.Spatial.MyClusterTree+MyClusterDescription)
ldloc.s 23 (VRageMath.Spatial.MyClusterTree+MyClusterDescription)
ldfld System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData> VRageMath.Spatial.MyClusterDescription::DynamicObjects
call static System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData> System.Linq.Enumerable::ToList(System.Collections.Generic.IEnumerable`1<VRageMath.Spatial.MyObjectData> source)
stfld System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData> VRageMath.Spatial.MyClusterDescription::DynamicObjects
ldloca.s 22 (VRageMath.Spatial.MyClusterTree+MyClusterDescription)
ldloc.s 23 (VRageMath.Spatial.MyClusterTree+MyClusterDescription)
ldfld System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData> VRageMath.Spatial.MyClusterDescription::StaticObjects
call static System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData> System.Linq.Enumerable::ToList(System.Collections.Generic.IEnumerable`1<VRageMath.Spatial.MyObjectData> source)
stfld System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData> VRageMath.Spatial.MyClusterDescription::StaticObjects
ldloc.s 22 (VRageMath.Spatial.MyClusterTree+MyClusterDescription)
stloc.s 46 (VRageMath.Spatial.MyClusterTree+MyClusterDescription)
ldloca.s 46 (VRageMath.Spatial.MyClusterTree+MyClusterDescription)
ldflda VRageMath.BoundingBoxD VRageMath.Spatial.MyClusterDescription::AABB
call VRageMath.Vector3D VRageMath.BoundingBoxD::get_Size()
stloc.s 31 (VRageMath.Vector3D)
ldloca.s 31 (VRageMath.Vector3D)
call System.Double VRageMath.Vector3D::AbsMax()
ldc.r4 2
ldloca.s 9 (VRageMath.Vector3)
call System.Single VRageMath.Vector3::AbsMax()
mul
conv.r8
ble.un.s L54
ldloc.s 6 (System.Collections.Generic.Stack`1[VRageMath.Spatial.MyClusterTree+MyClusterDescription])
ldloc.s 46 (VRageMath.Spatial.MyClusterTree+MyClusterDescription)
callvirt System.Void System.Collections.Generic.Stack`1<VRageMath.Spatial.MyClusterDescription>::Push(VRageMath.Spatial.MyClusterDescription item)
br.s L55
L54:
ldloc.s 7 (System.Collections.Generic.List`1[VRageMath.Spatial.MyClusterTree+MyClusterDescription])
ldloc.s 46 (VRageMath.Spatial.MyClusterTree+MyClusterDescription)
callvirt virtual System.Void System.Collections.Generic.List`1<VRageMath.Spatial.MyClusterDescription>::Add(VRageMath.Spatial.MyClusterDescription item)
L50:
L55:
ldloca.s 28 (VRageMath.Spatial.MyClusterTree+MyClusterDescription)
ldflda VRageMath.BoundingBoxD VRageMath.Spatial.MyClusterDescription::AABB
call VRageMath.Vector3D VRageMath.BoundingBoxD::get_Size()
stloc.s 31 (VRageMath.Vector3D)
ldloca.s 31 (VRageMath.Vector3D)
call System.Double VRageMath.Vector3D::AbsMax()
ldc.r4 2
ldloca.s 9 (VRageMath.Vector3)
call System.Single VRageMath.Vector3::AbsMax()
mul
conv.r8
cgt
ldloc.s 27 (System.Boolean)
and
brfalse.s L56
ldloc.s 6 (System.Collections.Generic.Stack`1[VRageMath.Spatial.MyClusterTree+MyClusterDescription])
ldloc.s 28 (VRageMath.Spatial.MyClusterTree+MyClusterDescription)
callvirt System.Void System.Collections.Generic.Stack`1<VRageMath.Spatial.MyClusterDescription>::Push(VRageMath.Spatial.MyClusterDescription item)
br.s L57
L56:
ldloc.s 7 (System.Collections.Generic.List`1[VRageMath.Spatial.MyClusterTree+MyClusterDescription])
ldloc.s 28 (VRageMath.Spatial.MyClusterTree+MyClusterDescription)
callvirt virtual System.Void System.Collections.Generic.List`1<VRageMath.Spatial.MyClusterDescription>::Add(VRageMath.Spatial.MyClusterDescription item)
L24:
L25:
L57:
ldloc.s 6 (System.Collections.Generic.Stack`1[VRageMath.Spatial.MyClusterTree+MyClusterDescription])
callvirt virtual System.Int32 System.Collections.Generic.Stack`1<VRageMath.Spatial.MyClusterDescription>::get_Count()
ldc.i4.0
bgt L58
ldc.i4.0
stloc.s 5 (System.Boolean)
ldloc.s 7 (System.Collections.Generic.List`1[VRageMath.Spatial.MyClusterTree+MyClusterDescription])
callvirt System.Collections.Generic.Enumerator<VRageMath.Spatial.MyClusterDescription> System.Collections.Generic.List`1<VRageMath.Spatial.MyClusterDescription>::GetEnumerator()
stloc.s 48 (System.Collections.Generic.List`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyClusterDescription])
[EX_BeginException]
br.s L59
L62:
ldloca.s 48 (System.Collections.Generic.List`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyClusterDescription])
call virtual VRageMath.Spatial.MyClusterDescription System.Collections.Generic.Enumerator<VRageMath.Spatial.MyClusterDescription>::get_Current()
stloc.s 49 (VRageMath.Spatial.MyClusterTree+MyClusterDescription)
ldloc.s 49 (VRageMath.Spatial.MyClusterTree+MyClusterDescription)
ldfld VRageMath.BoundingBoxD VRageMath.Spatial.MyClusterDescription::AABB
stloc.s 50 (VRageMath.BoundingBoxD)
ldloca.s 50 (VRageMath.BoundingBoxD)
call VRageMath.Vector3D VRageMath.BoundingBoxD::get_Size()
stloc.s 31 (VRageMath.Vector3D)
ldloca.s 31 (VRageMath.Vector3D)
call System.Double VRageMath.Vector3D::AbsMax()
ldsfld System.Single VRageMath.Spatial.MyClusterTree::MaximumClusterSize
conv.r8
ble.un.s L60
ldc.i4.1
stloc.s 5 (System.Boolean)
ldloc.s 9 (VRageMath.Vector3)
ldc.r4 2
call static VRageMath.Vector3 VRageMath.Vector3::op_Division(VRageMath.Vector3 value, System.Single divider)
stloc.s 9 (VRageMath.Vector3)
leave.s L61
L59:
L60:
ldloca.s 48 (System.Collections.Generic.List`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyClusterDescription])
call virtual System.Boolean System.Collections.Generic.Enumerator<VRageMath.Spatial.MyClusterDescription>::MoveNext()
brtrue.s L62
leave.s L63
[EX_BeginFinally]
ldloca.s 48 (System.Collections.Generic.List`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyClusterDescription])
constrained. System.Collections.Generic.List`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyClusterDescription]
callvirt abstract virtual System.Void System.IDisposable::Dispose()
[EX_EndException]
endfinally
L61:
L63:
ldloc.s 5 (System.Boolean)
brfalse.s L64
ldloc.s 4 (System.Int32)
ldc.i4.1
sub
stloc.s 4 (System.Int32)
L20:
L64:
ldloc.s 4 (System.Int32)
ldc.i4.0
cgt
ldloc.s 5 (System.Boolean)
and
brtrue L65
newobj System.Void System.Collections.Generic.HashSet`1<VRageMath.Spatial.MyCluster>::.ctor()
stloc.s 10 (System.Collections.Generic.HashSet`1[VRageMath.Spatial.MyClusterTree+MyCluster])
newobj System.Void System.Collections.Generic.HashSet`1<VRageMath.Spatial.MyCluster>::.ctor()
stloc.s 11 (System.Collections.Generic.HashSet`1[VRageMath.Spatial.MyClusterTree+MyCluster])
ldloc.s 8 (System.Collections.Generic.List`1[VRageMath.Spatial.MyClusterTree+MyObjectData])
callvirt System.Collections.Generic.Enumerator<VRageMath.Spatial.MyObjectData> System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData>::GetEnumerator()
stloc.s 41 (System.Collections.Generic.List`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyObjectData])
[EX_BeginException]
br.s L66
L68:
ldloca.s 41 (System.Collections.Generic.List`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyObjectData])
call virtual VRageMath.Spatial.MyObjectData System.Collections.Generic.Enumerator<VRageMath.Spatial.MyObjectData>::get_Current()
stloc.s 51 (VRageMath.Spatial.MyClusterTree+MyObjectData)
ldloc.s 51 (VRageMath.Spatial.MyClusterTree+MyObjectData)
ldfld VRageMath.Spatial.MyCluster VRageMath.Spatial.MyObjectData::Cluster
brfalse.s L67
ldloc.s 10 (System.Collections.Generic.HashSet`1[VRageMath.Spatial.MyClusterTree+MyCluster])
ldloc.s 51 (VRageMath.Spatial.MyClusterTree+MyObjectData)
ldfld VRageMath.Spatial.MyCluster VRageMath.Spatial.MyObjectData::Cluster
callvirt virtual System.Boolean System.Collections.Generic.HashSet`1<VRageMath.Spatial.MyCluster>::Add(VRageMath.Spatial.MyCluster item)
pop
ldarg.0
ldloc.s 51 (VRageMath.Spatial.MyClusterTree+MyObjectData)
ldc.i4.1
ldc.i4.0
call System.Void VRageMath.Spatial.MyClusterTree::RemoveObjectFromCluster(VRageMath.Spatial.MyObjectData objectData, System.Boolean batch, System.Boolean fireEvent)
L66:
L67:
ldloca.s 41 (System.Collections.Generic.List`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyObjectData])
call virtual System.Boolean System.Collections.Generic.Enumerator<VRageMath.Spatial.MyObjectData>::MoveNext()
brtrue.s L68
leave.s L69
[EX_BeginFinally]
ldloca.s 41 (System.Collections.Generic.List`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyObjectData])
constrained. System.Collections.Generic.List`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyObjectData]
callvirt abstract virtual System.Void System.IDisposable::Dispose()
[EX_EndException]
endfinally
L69:
ldloc.s 8 (System.Collections.Generic.List`1[VRageMath.Spatial.MyClusterTree+MyObjectData])
callvirt System.Collections.Generic.Enumerator<VRageMath.Spatial.MyObjectData> System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData>::GetEnumerator()
stloc.s 41 (System.Collections.Generic.List`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyObjectData])
[EX_BeginException]
br.s L70
L72:
ldloca.s 41 (System.Collections.Generic.List`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyObjectData])
call virtual VRageMath.Spatial.MyObjectData System.Collections.Generic.Enumerator<VRageMath.Spatial.MyObjectData>::get_Current()
stloc.s 52 (VRageMath.Spatial.MyClusterTree+MyObjectData)
ldloc.s 52 (VRageMath.Spatial.MyClusterTree+MyObjectData)
ldfld VRageMath.Spatial.MyCluster VRageMath.Spatial.MyObjectData::Cluster
brfalse.s L71
ldloc.s 52 (VRageMath.Spatial.MyClusterTree+MyObjectData)
ldfld VRageMath.Spatial.IMyActivationHandler VRageMath.Spatial.MyObjectData::ActivationHandler
ldloc.s 52 (VRageMath.Spatial.MyClusterTree+MyObjectData)
ldfld VRageMath.Spatial.MyCluster VRageMath.Spatial.MyObjectData::Cluster
ldfld System.Object VRageMath.Spatial.MyCluster::UserData
callvirt abstract virtual System.Void VRageMath.Spatial.IMyActivationHandler::FinishRemoveBatch(System.Object userData)
ldloc.s 52 (VRageMath.Spatial.MyClusterTree+MyObjectData)
ldnull
stfld VRageMath.Spatial.MyCluster VRageMath.Spatial.MyObjectData::Cluster
L70:
L71:
ldloca.s 41 (System.Collections.Generic.List`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyObjectData])
call virtual System.Boolean System.Collections.Generic.Enumerator<VRageMath.Spatial.MyObjectData>::MoveNext()
brtrue.s L72
leave.s L73
[EX_BeginFinally]
ldloca.s 41 (System.Collections.Generic.List`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyObjectData])
constrained. System.Collections.Generic.List`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyObjectData]
callvirt abstract virtual System.Void System.IDisposable::Dispose()
[EX_EndException]
endfinally
L73:
ldc.i4.0
stloc.s 12 (System.Int32)
ldloc.s 7 (System.Collections.Generic.List`1[VRageMath.Spatial.MyClusterTree+MyClusterDescription])
callvirt System.Collections.Generic.Enumerator<VRageMath.Spatial.MyClusterDescription> System.Collections.Generic.List`1<VRageMath.Spatial.MyClusterDescription>::GetEnumerator()
stloc.s 48 (System.Collections.Generic.List`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyClusterDescription])
[EX_BeginException]
br L74
L98:
ldloca.s 48 (System.Collections.Generic.List`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyClusterDescription])
call virtual VRageMath.Spatial.MyClusterDescription System.Collections.Generic.Enumerator<VRageMath.Spatial.MyClusterDescription>::get_Current()
stloc.s 53 (VRageMath.Spatial.MyClusterTree+MyClusterDescription)
ldloc.s 53 (VRageMath.Spatial.MyClusterTree+MyClusterDescription)
ldfld VRageMath.BoundingBoxD VRageMath.Spatial.MyClusterDescription::AABB
stloc.s 54 (VRageMath.BoundingBoxD)
ldarg.0
ldloca.s 54 (VRageMath.BoundingBoxD)
call VRageMath.Spatial.MyCluster VRageMath.Spatial.MyClusterTree::CreateCluster(VRageMath.BoundingBoxD& clusterBB)
stloc.s 55 (VRageMath.Spatial.MyClusterTree+MyCluster)
ldloc.s 53 (VRageMath.Spatial.MyClusterTree+MyClusterDescription)
ldfld System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData> VRageMath.Spatial.MyClusterDescription::DynamicObjects
callvirt System.Collections.Generic.Enumerator<VRageMath.Spatial.MyObjectData> System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData>::GetEnumerator()
stloc.s 41 (System.Collections.Generic.List`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyObjectData])
[EX_BeginException]
br.s L75
L77:
ldloca.s 41 (System.Collections.Generic.List`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyObjectData])
call virtual VRageMath.Spatial.MyObjectData System.Collections.Generic.Enumerator<VRageMath.Spatial.MyObjectData>::get_Current()
stloc.s 56 (VRageMath.Spatial.MyClusterTree+MyObjectData)
ldloc.s 56 (VRageMath.Spatial.MyClusterTree+MyObjectData)
ldfld VRageMath.Spatial.MyCluster VRageMath.Spatial.MyObjectData::Cluster
brfalse.s L76
ldloc.s 10 (System.Collections.Generic.HashSet`1[VRageMath.Spatial.MyClusterTree+MyCluster])
ldloc.s 56 (VRageMath.Spatial.MyClusterTree+MyObjectData)
ldfld VRageMath.Spatial.MyCluster VRageMath.Spatial.MyObjectData::Cluster
callvirt virtual System.Boolean System.Collections.Generic.HashSet`1<VRageMath.Spatial.MyCluster>::Add(VRageMath.Spatial.MyCluster item)
pop
ldarg.0
ldloc.s 56 (VRageMath.Spatial.MyClusterTree+MyObjectData)
ldc.i4.1
ldc.i4.0
call System.Void VRageMath.Spatial.MyClusterTree::RemoveObjectFromCluster(VRageMath.Spatial.MyObjectData objectData, System.Boolean batch, System.Boolean fireEvent)
L75:
L76:
ldloca.s 41 (System.Collections.Generic.List`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyObjectData])
call virtual System.Boolean System.Collections.Generic.Enumerator<VRageMath.Spatial.MyObjectData>::MoveNext()
brtrue.s L77
leave.s L78
[EX_BeginFinally]
ldloca.s 41 (System.Collections.Generic.List`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyObjectData])
constrained. System.Collections.Generic.List`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyObjectData]
callvirt abstract virtual System.Void System.IDisposable::Dispose()
[EX_EndException]
endfinally
L78:
ldloc.s 53 (VRageMath.Spatial.MyClusterTree+MyClusterDescription)
ldfld System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData> VRageMath.Spatial.MyClusterDescription::DynamicObjects
callvirt System.Collections.Generic.Enumerator<VRageMath.Spatial.MyObjectData> System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData>::GetEnumerator()
stloc.s 41 (System.Collections.Generic.List`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyObjectData])
[EX_BeginException]
br.s L79
L81:
ldloca.s 41 (System.Collections.Generic.List`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyObjectData])
call virtual VRageMath.Spatial.MyObjectData System.Collections.Generic.Enumerator<VRageMath.Spatial.MyObjectData>::get_Current()
stloc.s 57 (VRageMath.Spatial.MyClusterTree+MyObjectData)
ldloc.s 57 (VRageMath.Spatial.MyClusterTree+MyObjectData)
ldfld VRageMath.Spatial.MyCluster VRageMath.Spatial.MyObjectData::Cluster
brfalse.s L80
ldloc.s 57 (VRageMath.Spatial.MyClusterTree+MyObjectData)
ldfld VRageMath.Spatial.IMyActivationHandler VRageMath.Spatial.MyObjectData::ActivationHandler
ldloc.s 57 (VRageMath.Spatial.MyClusterTree+MyObjectData)
ldfld VRageMath.Spatial.MyCluster VRageMath.Spatial.MyObjectData::Cluster
ldfld System.Object VRageMath.Spatial.MyCluster::UserData
callvirt abstract virtual System.Void VRageMath.Spatial.IMyActivationHandler::FinishRemoveBatch(System.Object userData)
ldloc.s 57 (VRageMath.Spatial.MyClusterTree+MyObjectData)
ldnull
stfld VRageMath.Spatial.MyCluster VRageMath.Spatial.MyObjectData::Cluster
L79:
L80:
ldloca.s 41 (System.Collections.Generic.List`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyObjectData])
call virtual System.Boolean System.Collections.Generic.Enumerator<VRageMath.Spatial.MyObjectData>::MoveNext()
brtrue.s L81
leave.s L82
[EX_BeginFinally]
ldloca.s 41 (System.Collections.Generic.List`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyObjectData])
constrained. System.Collections.Generic.List`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyObjectData]
callvirt abstract virtual System.Void System.IDisposable::Dispose()
[EX_EndException]
endfinally
L82:
ldloc.s 10 (System.Collections.Generic.HashSet`1[VRageMath.Spatial.MyClusterTree+MyCluster])
callvirt System.Collections.Generic.Enumerator<VRageMath.Spatial.MyCluster> System.Collections.Generic.HashSet`1<VRageMath.Spatial.MyCluster>::GetEnumerator()
stloc.s 58 (System.Collections.Generic.HashSet`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyCluster])
[EX_BeginException]
br.s L83
L85:
ldloca.s 58 (System.Collections.Generic.HashSet`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyCluster])
call virtual VRageMath.Spatial.MyCluster System.Collections.Generic.Enumerator<VRageMath.Spatial.MyCluster>::get_Current()
stloc.s 59 (VRageMath.Spatial.MyClusterTree+MyCluster)
ldarg.0
ldfld System.Action`1<System.Object> VRageMath.Spatial.MyClusterTree::OnFinishBatch
brfalse.s L84
ldarg.0
ldfld System.Action`1<System.Object> VRageMath.Spatial.MyClusterTree::OnFinishBatch
ldloc.s 59 (VRageMath.Spatial.MyClusterTree+MyCluster)
ldfld System.Object VRageMath.Spatial.MyCluster::UserData
callvirt virtual System.Void System.Action`1<System.Object>::Invoke(System.Object obj)
L83:
L84:
ldloca.s 58 (System.Collections.Generic.HashSet`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyCluster])
call virtual System.Boolean System.Collections.Generic.Enumerator<VRageMath.Spatial.MyCluster>::MoveNext()
brtrue.s L85
leave.s L86
[EX_BeginFinally]
ldloca.s 58 (System.Collections.Generic.HashSet`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyCluster])
constrained. System.Collections.Generic.HashSet`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyCluster]
callvirt abstract virtual System.Void System.IDisposable::Dispose()
[EX_EndException]
endfinally
L86:
ldloc.s 53 (VRageMath.Spatial.MyClusterTree+MyClusterDescription)
ldfld System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData> VRageMath.Spatial.MyClusterDescription::DynamicObjects
callvirt System.Collections.Generic.Enumerator<VRageMath.Spatial.MyObjectData> System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData>::GetEnumerator()
stloc.s 41 (System.Collections.Generic.List`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyObjectData])
[EX_BeginException]
br.s L87
L90:
ldloca.s 41 (System.Collections.Generic.List`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyObjectData])
call virtual VRageMath.Spatial.MyObjectData System.Collections.Generic.Enumerator<VRageMath.Spatial.MyObjectData>::get_Current()
stloc.s 60 (VRageMath.Spatial.MyClusterTree+MyObjectData)
ldarg.0
ldloc.s 55 (VRageMath.Spatial.MyClusterTree+MyCluster)
ldloc.s 60 (VRageMath.Spatial.MyClusterTree+MyObjectData)
ldfld System.UInt64 VRageMath.Spatial.MyObjectData::Id
ldloc.s 60 (VRageMath.Spatial.MyClusterTree+MyObjectData)
ldfld System.Int64 VRageMath.Spatial.MyObjectData::EntityId
ldc.i4.1
ldc.i4.0
call System.UInt64 VRageMath.Spatial.MyClusterTree::AddObjectToCluster(VRageMath.Spatial.MyCluster cluster, System.UInt64 objectId, System.Int64 entityId, System.Boolean batch, System.Boolean fireEvent)
pop
ldarg.0
ldfld System.Action`2<System.Int64, System.Int32> VRageMath.Spatial.MyClusterTree::EntityAdded
dup
brtrue.s L88
pop
br.s L89
L88:
ldloc.s 60 (VRageMath.Spatial.MyClusterTree+MyObjectData)
ldfld System.Int64 VRageMath.Spatial.MyObjectData::EntityId
ldloc.s 55 (VRageMath.Spatial.MyClusterTree+MyCluster)
ldfld System.Int32 VRageMath.Spatial.MyCluster::ClusterId
callvirt virtual System.Void System.Action`2<System.Int64, System.Int32>::Invoke(System.Int64 arg1, System.Int32 arg2)
L87:
L89:
ldloca.s 41 (System.Collections.Generic.List`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyObjectData])
call virtual System.Boolean System.Collections.Generic.Enumerator<VRageMath.Spatial.MyObjectData>::MoveNext()
brtrue.s L90
leave.s L91
[EX_BeginFinally]
ldloca.s 41 (System.Collections.Generic.List`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyObjectData])
constrained. System.Collections.Generic.List`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyObjectData]
callvirt abstract virtual System.Void System.IDisposable::Dispose()
[EX_EndException]
endfinally
L91:
ldloc.s 53 (VRageMath.Spatial.MyClusterTree+MyClusterDescription)
ldfld System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData> VRageMath.Spatial.MyClusterDescription::StaticObjects
callvirt System.Collections.Generic.Enumerator<VRageMath.Spatial.MyObjectData> System.Collections.Generic.List`1<VRageMath.Spatial.MyObjectData>::GetEnumerator()
stloc.s 41 (System.Collections.Generic.List`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyObjectData])
[EX_BeginException]
br.s L92
L96:
ldloca.s 41 (System.Collections.Generic.List`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyObjectData])
call virtual VRageMath.Spatial.MyObjectData System.Collections.Generic.Enumerator<VRageMath.Spatial.MyObjectData>::get_Current()
stloc.s 61 (VRageMath.Spatial.MyClusterTree+MyObjectData)
ldloc.s 55 (VRageMath.Spatial.MyClusterTree+MyCluster)
ldflda VRageMath.BoundingBoxD VRageMath.Spatial.MyCluster::AABB
ldloc.s 61 (VRageMath.Spatial.MyClusterTree+MyObjectData)
ldfld VRageMath.BoundingBoxD VRageMath.Spatial.MyObjectData::AABB
call VRageMath.ContainmentType VRageMath.BoundingBoxD::Contains(VRageMath.BoundingBoxD box)
brfalse.s L93
ldarg.0
ldloc.s 55 (VRageMath.Spatial.MyClusterTree+MyCluster)
ldloc.s 61 (VRageMath.Spatial.MyClusterTree+MyObjectData)
ldfld System.UInt64 VRageMath.Spatial.MyObjectData::Id
ldloc.s 61 (VRageMath.Spatial.MyClusterTree+MyObjectData)
ldfld System.Int64 VRageMath.Spatial.MyObjectData::EntityId
ldc.i4.1
ldc.i4.0
call System.UInt64 VRageMath.Spatial.MyClusterTree::AddObjectToCluster(VRageMath.Spatial.MyCluster cluster, System.UInt64 objectId, System.Int64 entityId, System.Boolean batch, System.Boolean fireEvent)
pop
ldarg.0
ldfld System.Action`2<System.Int64, System.Int32> VRageMath.Spatial.MyClusterTree::EntityAdded
dup
brtrue.s L94
pop
br.s L95
L94:
ldloc.s 61 (VRageMath.Spatial.MyClusterTree+MyObjectData)
ldfld System.Int64 VRageMath.Spatial.MyObjectData::EntityId
ldloc.s 55 (VRageMath.Spatial.MyClusterTree+MyCluster)
ldfld System.Int32 VRageMath.Spatial.MyCluster::ClusterId
callvirt virtual System.Void System.Action`2<System.Int64, System.Int32>::Invoke(System.Int64 arg1, System.Int32 arg2)
L95:
ldloc.s 12 (System.Int32)
ldc.i4.1
add
stloc.s 12 (System.Int32)
L92:
L93:
ldloca.s 41 (System.Collections.Generic.List`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyObjectData])
call virtual System.Boolean System.Collections.Generic.Enumerator<VRageMath.Spatial.MyObjectData>::MoveNext()
brtrue.s L96
leave.s L97
[EX_BeginFinally]
ldloca.s 41 (System.Collections.Generic.List`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyObjectData])
constrained. System.Collections.Generic.List`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyObjectData]
callvirt abstract virtual System.Void System.IDisposable::Dispose()
[EX_EndException]
endfinally
L97:
ldloc.s 11 (System.Collections.Generic.HashSet`1[VRageMath.Spatial.MyClusterTree+MyCluster])
ldloc.s 55 (VRageMath.Spatial.MyClusterTree+MyCluster)
callvirt virtual System.Boolean System.Collections.Generic.HashSet`1<VRageMath.Spatial.MyCluster>::Add(VRageMath.Spatial.MyCluster item)
pop
L74:
ldloca.s 48 (System.Collections.Generic.List`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyClusterDescription])
call virtual System.Boolean System.Collections.Generic.Enumerator<VRageMath.Spatial.MyClusterDescription>::MoveNext()
brtrue L98
leave.s L99
[EX_BeginFinally]
ldloca.s 48 (System.Collections.Generic.List`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyClusterDescription])
constrained. System.Collections.Generic.List`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyClusterDescription]
callvirt abstract virtual System.Void System.IDisposable::Dispose()
[EX_EndException]
endfinally
L99:
ldloc.s 10 (System.Collections.Generic.HashSet`1[VRageMath.Spatial.MyClusterTree+MyCluster])
callvirt System.Collections.Generic.Enumerator<VRageMath.Spatial.MyCluster> System.Collections.Generic.HashSet`1<VRageMath.Spatial.MyCluster>::GetEnumerator()
stloc.s 58 (System.Collections.Generic.HashSet`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyCluster])
[EX_BeginException]
br.s L100
L101:
ldloca.s 58 (System.Collections.Generic.HashSet`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyCluster])
call virtual VRageMath.Spatial.MyCluster System.Collections.Generic.Enumerator<VRageMath.Spatial.MyCluster>::get_Current()
stloc.s 62 (VRageMath.Spatial.MyClusterTree+MyCluster)
ldarg.0
ldloc.s 62 (VRageMath.Spatial.MyClusterTree+MyCluster)
call System.Void VRageMath.Spatial.MyClusterTree::RemoveCluster(VRageMath.Spatial.MyCluster cluster)
L100:
ldloca.s 58 (System.Collections.Generic.HashSet`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyCluster])
call virtual System.Boolean System.Collections.Generic.Enumerator<VRageMath.Spatial.MyCluster>::MoveNext()
brtrue.s L101
leave.s L102
[EX_BeginFinally]
ldloca.s 58 (System.Collections.Generic.HashSet`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyCluster])
constrained. System.Collections.Generic.HashSet`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyCluster]
callvirt abstract virtual System.Void System.IDisposable::Dispose()
[EX_EndException]
endfinally
L102:
ldloc.s 11 (System.Collections.Generic.HashSet`1[VRageMath.Spatial.MyClusterTree+MyCluster])
callvirt System.Collections.Generic.Enumerator<VRageMath.Spatial.MyCluster> System.Collections.Generic.HashSet`1<VRageMath.Spatial.MyCluster>::GetEnumerator()
stloc.s 58 (System.Collections.Generic.HashSet`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyCluster])
[EX_BeginException]
br L103
L109:
ldloca.s 58 (System.Collections.Generic.HashSet`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyCluster])
call virtual VRageMath.Spatial.MyCluster System.Collections.Generic.Enumerator<VRageMath.Spatial.MyCluster>::get_Current()
stloc.s 63 (VRageMath.Spatial.MyClusterTree+MyCluster)
ldarg.0
ldfld System.Action`1<System.Object> VRageMath.Spatial.MyClusterTree::OnFinishBatch
brfalse.s L104
ldarg.0
ldfld System.Action`1<System.Object> VRageMath.Spatial.MyClusterTree::OnFinishBatch
ldloc.s 63 (VRageMath.Spatial.MyClusterTree+MyCluster)
ldfld System.Object VRageMath.Spatial.MyCluster::UserData
callvirt virtual System.Void System.Action`1<System.Object>::Invoke(System.Object obj)
L104:
ldloc.s 63 (VRageMath.Spatial.MyClusterTree+MyCluster)
ldfld System.Collections.Generic.HashSet`1<System.UInt64> VRageMath.Spatial.MyCluster::Objects
callvirt System.Collections.Generic.Enumerator<System.UInt64> System.Collections.Generic.HashSet`1<System.UInt64>::GetEnumerator()
stloc.s 64 (System.Collections.Generic.HashSet`1+Enumerator[System.UInt64])
[EX_BeginException]
br.s L105
L107:
ldloca.s 64 (System.Collections.Generic.HashSet`1+Enumerator[System.UInt64])
call virtual System.UInt64 System.Collections.Generic.Enumerator<System.UInt64>::get_Current()
stloc.s 65 (System.UInt64)
ldarg.0
ldfld System.Collections.Generic.Dictionary`2<System.UInt64, VRageMath.Spatial.MyObjectData> VRageMath.Spatial.MyClusterTree::m_objectsData
ldloc.s 65 (System.UInt64)
callvirt virtual VRageMath.Spatial.MyObjectData System.Collections.Generic.Dictionary`2<System.UInt64, VRageMath.Spatial.MyObjectData>::get_Item(System.UInt64 key)
ldfld VRageMath.Spatial.IMyActivationHandler VRageMath.Spatial.MyObjectData::ActivationHandler
brfalse.s L106
ldarg.0
ldfld System.Collections.Generic.Dictionary`2<System.UInt64, VRageMath.Spatial.MyObjectData> VRageMath.Spatial.MyClusterTree::m_objectsData
ldloc.s 65 (System.UInt64)
callvirt virtual VRageMath.Spatial.MyObjectData System.Collections.Generic.Dictionary`2<System.UInt64, VRageMath.Spatial.MyObjectData>::get_Item(System.UInt64 key)
ldfld VRageMath.Spatial.IMyActivationHandler VRageMath.Spatial.MyObjectData::ActivationHandler
callvirt abstract virtual System.Void VRageMath.Spatial.IMyActivationHandler::FinishAddBatch()
L105:
L106:
ldloca.s 64 (System.Collections.Generic.HashSet`1+Enumerator[System.UInt64])
call virtual System.Boolean System.Collections.Generic.Enumerator<System.UInt64>::MoveNext()
brtrue.s L107
leave.s L108
[EX_BeginFinally]
ldloca.s 64 (System.Collections.Generic.HashSet`1+Enumerator[System.UInt64])
constrained. System.Collections.Generic.HashSet`1+Enumerator[System.UInt64]
callvirt abstract virtual System.Void System.IDisposable::Dispose()
[EX_EndException]
endfinally
L103:
L108:
ldloca.s 58 (System.Collections.Generic.HashSet`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyCluster])
call virtual System.Boolean System.Collections.Generic.Enumerator<VRageMath.Spatial.MyCluster>::MoveNext()
brtrue L109
leave.s L110
[EX_BeginFinally]
ldloca.s 58 (System.Collections.Generic.HashSet`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyCluster])
constrained. System.Collections.Generic.HashSet`1+Enumerator[VRageMath.Spatial.MyClusterTree+MyCluster]
callvirt abstract virtual System.Void System.IDisposable::Dispose()
[EX_EndException]
endfinally
L110:
ldarg.0
ldfld System.Action VRageMath.Spatial.MyClusterTree::OnClustersReordered
brfalse.s L111
ldarg.0
ldfld System.Action VRageMath.Spatial.MyClusterTree::OnClustersReordered
callvirt virtual System.Void System.Action::Invoke()
L111:
leave.s L112
[EX_BeginFinally]
ldloca.s 0 (VRage.FastResourceLockExtensions+MyExclusiveLock)
constrained. VRage.FastResourceLockExtensions+MyExclusiveLock
callvirt abstract virtual System.Void System.IDisposable::Dispose()
[EX_EndException]
endfinally
L112:
call static System.Collections.Generic.List`1<VRageMath.Spatial.MyCluster> VRageMath.Spatial.MyClusterTree::get_m_resultList()
callvirt virtual System.Void System.Collections.Generic.List`1<VRageMath.Spatial.MyCluster>::Clear()
ret
