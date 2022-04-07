// 2ca50d4a
ldarg.0
ldc.i4.0
stfld System.Boolean Sandbox.Game.Entities.Cube.MyRefinery::m_queueNeedsRebuild
ldarg.0
ldc.i4.0
call System.Void Sandbox.Game.Entities.Cube.MyProductionBlock::ClearQueue(System.Boolean sendEvent)
ldarg.0
ldfld System.Collections.Generic.List`1<System.Collections.Generic.KeyValuePair`2<System.Int32, Sandbox.Definitions.MyBlueprintDefinitionBase>> Sandbox.Game.Entities.Cube.MyRefinery::m_tmpSortedBlueprints
callvirt virtual System.Void System.Collections.Generic.List`1<System.Collections.Generic.KeyValuePair`2<System.Int32, Sandbox.Definitions.MyBlueprintDefinitionBase>>::Clear()
ldarg.0
call Sandbox.Game.MyInventory Sandbox.Game.Entities.Cube.MyProductionBlock::get_InputInventory()
callvirt abstract virtual System.Collections.Generic.List`1<VRage.Game.Entity.MyPhysicalInventoryItem> VRage.Game.Entity.MyInventoryBase::GetItems()
callvirt VRage.Game.Entity.MyPhysicalInventoryItem[] System.Collections.Generic.List`1<VRage.Game.Entity.MyPhysicalInventoryItem>::ToArray()
stloc.0
ldarg.0
ldfld Sandbox.Definitions.MyRefineryDefinition Sandbox.Game.Entities.Cube.MyRefinery::m_refineryDef
ldfld System.Collections.Generic.List`1<Sandbox.Definitions.MyBlueprintClassDefinition> Sandbox.Definitions.MyProductionBlockDefinition::BlueprintClasses
stloc.s 16 (System.Collections.Generic.List`1[Sandbox.Definitions.MyBlueprintClassDefinition])
ldc.i4.0
stloc.3
br L0
L13:
ldc.i4.0
stloc.s 4 (System.Int32)
br L1
L12:
ldloc.s 16 (System.Collections.Generic.List`1[Sandbox.Definitions.MyBlueprintClassDefinition])
ldloc.s 4 (System.Int32)
callvirt virtual Sandbox.Definitions.MyBlueprintClassDefinition System.Collections.Generic.List`1<Sandbox.Definitions.MyBlueprintClassDefinition>::get_Item(System.Int32 index)
callvirt virtual System.Collections.Generic.IEnumerator`1<Sandbox.Definitions.MyBlueprintDefinitionBase> Sandbox.Definitions.MyBlueprintClassDefinition::GetEnumerator()
stloc.s 5 (System.Collections.Generic.IEnumerator`1[Sandbox.Definitions.MyBlueprintDefinitionBase])
[EX_BeginException]
br L2
L9:
ldloc.s 5 (System.Collections.Generic.IEnumerator`1[Sandbox.Definitions.MyBlueprintDefinitionBase])
callvirt abstract virtual Sandbox.Definitions.MyBlueprintDefinitionBase System.Collections.Generic.IEnumerator`1<Sandbox.Definitions.MyBlueprintDefinitionBase>::get_Current()
stloc.s 6 (Sandbox.Definitions.MyBlueprintDefinitionBase)
ldloc.s 6 (Sandbox.Definitions.MyBlueprintDefinitionBase)
ldfld Sandbox.Definitions.Item[] Sandbox.Definitions.MyBlueprintDefinitionBase::Prerequisites
stloc.s 17 (Sandbox.Definitions.MyBlueprintDefinitionBase+Item[])
ldc.i4.0
stloc.s 7 (System.Boolean)
ldloca.s 8 (VRage.Game.MyDefinitionId)
ldloc.0
ldloc.3
ldelema VRage.Game.Entity.MyPhysicalInventoryItem
ldfld VRage.Game.MyObjectBuilder_PhysicalObject VRage.Game.Entity.MyPhysicalInventoryItem::Content
callvirt VRage.ObjectBuilders.MyObjectBuilderType VRage.ObjectBuilders.MyObjectBuilder_Base::get_TypeId()
ldloc.0
ldloc.3
ldelema VRage.Game.Entity.MyPhysicalInventoryItem
ldfld VRage.Game.MyObjectBuilder_PhysicalObject VRage.Game.Entity.MyPhysicalInventoryItem::Content
callvirt VRage.Utils.MyStringHash VRage.ObjectBuilders.MyObjectBuilder_Base::get_SubtypeId()
call System.Void VRage.Game.MyDefinitionId::.ctor(VRage.ObjectBuilders.MyObjectBuilderType type, VRage.Utils.MyStringHash subtypeId)
ldc.i4.0
stloc.s 9 (System.Int32)
br.s L3
L6:
ldloc.s 17 (Sandbox.Definitions.MyBlueprintDefinitionBase+Item[])
ldloc.s 9 (System.Int32)
ldelema Sandbox.Definitions.MyBlueprintDefinitionBase+Item
ldflda VRage.Game.MyDefinitionId Sandbox.Definitions.Item::Id
ldloc.s 8 (VRage.Game.MyDefinitionId)
call virtual System.Boolean VRage.Game.MyDefinitionId::Equals(VRage.Game.MyDefinitionId other)
brfalse.s L4
ldc.i4.1
stloc.s 7 (System.Boolean)
br.s L5
L4:
ldloc.s 9 (System.Int32)
ldc.i4.1
add
stloc.s 9 (System.Int32)
L3:
ldloc.s 9 (System.Int32)
ldloc.s 6 (Sandbox.Definitions.MyBlueprintDefinitionBase)
ldfld Sandbox.Definitions.Item[] Sandbox.Definitions.MyBlueprintDefinitionBase::Prerequisites
ldlen
conv.i4
blt.s L6
L5:
ldloc.s 7 (System.Boolean)
brfalse.s L7
ldarg.0
ldfld System.Collections.Generic.List`1<System.Collections.Generic.KeyValuePair`2<System.Int32, Sandbox.Definitions.MyBlueprintDefinitionBase>> Sandbox.Game.Entities.Cube.MyRefinery::m_tmpSortedBlueprints
ldloc.3
ldloc.s 6 (Sandbox.Definitions.MyBlueprintDefinitionBase)
newobj System.Void System.Collections.Generic.KeyValuePair`2<System.Int32, Sandbox.Definitions.MyBlueprintDefinitionBase>::.ctor(System.Int32 key, Sandbox.Definitions.MyBlueprintDefinitionBase value)
callvirt virtual System.Void System.Collections.Generic.List`1<System.Collections.Generic.KeyValuePair`2<System.Int32, Sandbox.Definitions.MyBlueprintDefinitionBase>>::Add(System.Collections.Generic.KeyValuePair`2<System.Int32, Sandbox.Definitions.MyBlueprintDefinitionBase> item)
leave.s L8
L2:
L7:
ldloc.s 5 (System.Collections.Generic.IEnumerator`1[Sandbox.Definitions.MyBlueprintDefinitionBase])
callvirt abstract virtual System.Boolean System.Collections.IEnumerator::MoveNext()
brtrue L9
leave.s L10
[EX_BeginFinally]
ldloc.s 5 (System.Collections.Generic.IEnumerator`1[Sandbox.Definitions.MyBlueprintDefinitionBase])
brfalse.s L11
ldloc.s 5 (System.Collections.Generic.IEnumerator`1[Sandbox.Definitions.MyBlueprintDefinitionBase])
callvirt abstract virtual System.Void System.IDisposable::Dispose()
L11:
[EX_EndException]
endfinally
L8:
L10:
ldloc.s 4 (System.Int32)
ldc.i4.1
add
stloc.s 4 (System.Int32)
L1:
ldloc.s 4 (System.Int32)
ldloc.s 16 (System.Collections.Generic.List`1[Sandbox.Definitions.MyBlueprintClassDefinition])
callvirt virtual System.Int32 System.Collections.Generic.List`1<Sandbox.Definitions.MyBlueprintClassDefinition>::get_Count()
blt L12
ldloc.3
ldc.i4.1
add
stloc.3
L0:
ldloc.3
ldloc.0
ldlen
conv.i4
blt L13
ldc.i4.0
stloc.s 10 (System.Int32)
br L14
L22:
ldarg.0
ldfld System.Collections.Generic.List`1<System.Collections.Generic.KeyValuePair`2<System.Int32, Sandbox.Definitions.MyBlueprintDefinitionBase>> Sandbox.Game.Entities.Cube.MyRefinery::m_tmpSortedBlueprints
ldloc.s 10 (System.Int32)
callvirt virtual System.Collections.Generic.KeyValuePair`2<System.Int32, Sandbox.Definitions.MyBlueprintDefinitionBase> System.Collections.Generic.List`1<System.Collections.Generic.KeyValuePair`2<System.Int32, Sandbox.Definitions.MyBlueprintDefinitionBase>>::get_Item(System.Int32 index)
stloc.s 12 (System.Collections.Generic.KeyValuePair`2[System.Int32,Sandbox.Definitions.MyBlueprintDefinitionBase])
ldloca.s 12 (System.Collections.Generic.KeyValuePair`2[System.Int32,Sandbox.Definitions.MyBlueprintDefinitionBase])
call Sandbox.Definitions.MyBlueprintDefinitionBase System.Collections.Generic.KeyValuePair`2<System.Int32, Sandbox.Definitions.MyBlueprintDefinitionBase>::get_Value()
stloc.s 11 (Sandbox.Definitions.MyBlueprintDefinitionBase)
ldsfld VRage.MyFixedPoint VRage.MyFixedPoint::MaxValue
stloc.1
ldloc.s 11 (Sandbox.Definitions.MyBlueprintDefinitionBase)
ldfld Sandbox.Definitions.Item[] Sandbox.Definitions.MyBlueprintDefinitionBase::Prerequisites
stloc.s 13 (Sandbox.Definitions.MyBlueprintDefinitionBase+Item[])
ldc.i4.0
stloc.s 14 (System.Int32)
br.s L15
L18:
ldloc.s 13 (Sandbox.Definitions.MyBlueprintDefinitionBase+Item[])
ldloc.s 14 (System.Int32)
ldelem Sandbox.Definitions.MyBlueprintDefinitionBase+Item
stloc.s 15 (Sandbox.Definitions.MyBlueprintDefinitionBase+Item)
ldloc.0
ldloc.s 10 (System.Int32)
ldelema VRage.Game.Entity.MyPhysicalInventoryItem
ldfld VRage.MyFixedPoint VRage.Game.Entity.MyPhysicalInventoryItem::Amount
stloc.2
ldloc.2
ldc.i4.0
call static VRage.MyFixedPoint VRage.MyFixedPoint::op_Implicit(System.Int32 i)
call static System.Boolean VRage.MyFixedPoint::op_Equality(VRage.MyFixedPoint a, VRage.MyFixedPoint b)
brfalse.s L16
ldc.i4.0
call static VRage.MyFixedPoint VRage.MyFixedPoint::op_Implicit(System.Int32 i)
stloc.1
br.s L17
L16:
ldloc.2
ldc.r4 1
ldloc.s 15 (Sandbox.Definitions.MyBlueprintDefinitionBase+Item)
ldfld VRage.MyFixedPoint Sandbox.Definitions.Item::Amount
call static System.Single VRage.MyFixedPoint::op_Explicit(VRage.MyFixedPoint fp)
conv.r4
div
call static VRage.MyFixedPoint VRage.MyFixedPoint::op_Multiply(VRage.MyFixedPoint a, System.Single b)
ldloc.1
call static VRage.MyFixedPoint VRage.MyFixedPoint::Min(VRage.MyFixedPoint a, VRage.MyFixedPoint b)
stloc.1
ldloc.s 14 (System.Int32)
ldc.i4.1
add
stloc.s 14 (System.Int32)
L15:
ldloc.s 14 (System.Int32)
ldloc.s 13 (Sandbox.Definitions.MyBlueprintDefinitionBase+Item[])
ldlen
conv.i4
blt.s L18
L17:
ldloc.s 11 (Sandbox.Definitions.MyBlueprintDefinitionBase)
ldfld System.Boolean Sandbox.Definitions.MyBlueprintDefinitionBase::Atomic
brfalse.s L19
ldloc.1
call static VRage.MyFixedPoint VRage.MyFixedPoint::Floor(VRage.MyFixedPoint a)
stloc.1
L19:
ldloc.1
ldc.i4.0
call static VRage.MyFixedPoint VRage.MyFixedPoint::op_Implicit(System.Int32 i)
call static System.Boolean VRage.MyFixedPoint::op_GreaterThan(VRage.MyFixedPoint a, VRage.MyFixedPoint b)
brfalse.s L20
ldloc.1
ldsfld VRage.MyFixedPoint VRage.MyFixedPoint::MaxValue
call static System.Boolean VRage.MyFixedPoint::op_Inequality(VRage.MyFixedPoint a, VRage.MyFixedPoint b)
brfalse.s L21
ldarg.0
ldc.i4.m1
ldloc.s 11 (Sandbox.Definitions.MyBlueprintDefinitionBase)
ldloc.1
call System.Void Sandbox.Game.Entities.Cube.MyProductionBlock::InsertQueueItemRequest(System.Int32 idx, Sandbox.Definitions.MyBlueprintDefinitionBase blueprint, VRage.MyFixedPoint amount)
L20:
L21:
ldloc.s 10 (System.Int32)
ldc.i4.1
add
stloc.s 10 (System.Int32)
L14:
ldloc.s 10 (System.Int32)
ldarg.0
ldfld System.Collections.Generic.List`1<System.Collections.Generic.KeyValuePair`2<System.Int32, Sandbox.Definitions.MyBlueprintDefinitionBase>> Sandbox.Game.Entities.Cube.MyRefinery::m_tmpSortedBlueprints
callvirt virtual System.Int32 System.Collections.Generic.List`1<System.Collections.Generic.KeyValuePair`2<System.Int32, Sandbox.Definitions.MyBlueprintDefinitionBase>>::get_Count()
blt L22
ldarg.0
ldfld System.Collections.Generic.List`1<System.Collections.Generic.KeyValuePair`2<System.Int32, Sandbox.Definitions.MyBlueprintDefinitionBase>> Sandbox.Game.Entities.Cube.MyRefinery::m_tmpSortedBlueprints
callvirt virtual System.Void System.Collections.Generic.List`1<System.Collections.Generic.KeyValuePair`2<System.Int32, Sandbox.Definitions.MyBlueprintDefinitionBase>>::Clear()
ret
