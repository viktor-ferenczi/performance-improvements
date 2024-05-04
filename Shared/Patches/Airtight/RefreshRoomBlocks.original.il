// 2db5daff
ldarg.1
brfalse.s L0
ldarg.1
callvirt virtual System.Boolean Sandbox.Game.GameSystems.MyOxygenRoom::get_IsAirtight()
brfalse.s L1
ldarg.1
callvirt virtual System.Boolean Sandbox.Game.GameSystems.MyOxygenRoom::get_IsDirty()
brtrue.s L2
L0:
ret
L1:
L2:
ldarg.0
ldfld VRage.Collections.MyConcurrentList`1<Sandbox.Game.GameSystems.MyOxygenRoom> Sandbox.Game.GameSystems.MyGridGasSystem::m_rooms
ldc.i4.0
callvirt virtual Sandbox.Game.GameSystems.MyOxygenRoom VRage.Collections.MyConcurrentList`1<Sandbox.Game.GameSystems.MyOxygenRoom>::get_Item(System.Int32 index)
stloc.0
ldarg.1
callvirt virtual VRageMath.Vector3I Sandbox.Game.GameSystems.MyOxygenRoom::get_StartingPosition()
stloc.1
ldloc.1
stloc.2
ldarg.0
ldfld System.Collections.Generic.Queue`1<VRageMath.Vector3I> Sandbox.Game.GameSystems.MyGridGasSystem::m_blockQueue
callvirt System.Void System.Collections.Generic.Queue`1<VRageMath.Vector3I>::Clear()
ldarg.0
ldfld System.Collections.Generic.Queue`1<VRageMath.Vector3I> Sandbox.Game.GameSystems.MyGridGasSystem::m_blockQueue
ldloc.2
callvirt System.Void System.Collections.Generic.Queue`1<VRageMath.Vector3I>::Enqueue(VRageMath.Vector3I item)
newobj System.Void System.Collections.Generic.HashSet`1<VRageMath.Vector3I>::.ctor()
stloc.3
ldloc.3
ldloc.2
callvirt virtual System.Boolean System.Collections.Generic.HashSet`1<VRageMath.Vector3I>::Add(VRageMath.Vector3I item)
pop
ldc.i4.1
stloc.s 4 (System.Boolean)
br L3
L19:
ldarg.0
ldfld System.Collections.Generic.Queue`1<VRageMath.Vector3I> Sandbox.Game.GameSystems.MyGridGasSystem::m_blockQueue
callvirt VRageMath.Vector3I System.Collections.Generic.Queue`1<VRageMath.Vector3I>::Dequeue()
stloc.2
ldc.i4.0
stloc.s 5 (System.Int32)
br L4
L18:
ldloc.2
ldarg.0
ldfld VRageMath.Vector3I[] Sandbox.Game.GameSystems.MyGridGasSystem::m_neighbours
ldloc.s 5 (System.Int32)
ldelem VRageMath.Vector3I
call static VRageMath.Vector3I VRageMath.Vector3I::op_Addition(VRageMath.Vector3I a, VRageMath.Vector3I b)
stloc.s 6 (VRageMath.Vector3I)
ldloc.3
ldloc.s 6 (VRageMath.Vector3I)
callvirt virtual System.Boolean System.Collections.Generic.HashSet`1<VRageMath.Vector3I>::Contains(VRageMath.Vector3I item)
brtrue L5
ldloc.s 6 (VRageMath.Vector3I)
ldarg.0
ldfld VRageMath.Vector3I Sandbox.Game.GameSystems.MyGridGasSystem::m_storedGridMin
call static VRageMath.Vector3I VRageMath.Vector3I::Min(VRageMath.Vector3I value1, VRageMath.Vector3I value2)
ldarg.0
ldfld VRageMath.Vector3I Sandbox.Game.GameSystems.MyGridGasSystem::m_storedGridMin
call static System.Boolean VRageMath.Vector3I::op_Inequality(VRageMath.Vector3I a, VRageMath.Vector3I b)
brtrue.s L6
ldloc.s 6 (VRageMath.Vector3I)
ldarg.0
ldfld VRageMath.Vector3I Sandbox.Game.GameSystems.MyGridGasSystem::m_storedGridMax
call static VRageMath.Vector3I VRageMath.Vector3I::Max(VRageMath.Vector3I value1, VRageMath.Vector3I value2)
ldarg.0
ldfld VRageMath.Vector3I Sandbox.Game.GameSystems.MyGridGasSystem::m_storedGridMax
call static System.Boolean VRageMath.Vector3I::op_Inequality(VRageMath.Vector3I a, VRageMath.Vector3I b)
brfalse.s L7
L6:
ldc.i4.0
stloc.s 4 (System.Boolean)
br L8
L7:
ldarg.0
ldloc.2
ldloc.s 6 (VRageMath.Vector3I)
call System.Boolean Sandbox.Game.GameSystems.MyGridGasSystem::IsAirtightBetweenPositions(VRageMath.Vector3I startPos, VRageMath.Vector3I endPos)
stloc.s 7 (System.Boolean)
ldloc.s 7 (System.Boolean)
brtrue L9
ldloc.3
ldloc.s 6 (VRageMath.Vector3I)
callvirt virtual System.Boolean System.Collections.Generic.HashSet`1<VRageMath.Vector3I>::Add(VRageMath.Vector3I item)
pop
ldarg.0
call VRage.Game.ModAPI.IMyCubeGrid Sandbox.Game.GameSystems.MyGridGasSystem::get_CubeGrid()
ldloc.s 6 (VRageMath.Vector3I)
callvirt abstract virtual VRage.Game.ModAPI.IMySlimBlock VRage.Game.ModAPI.IMyCubeGrid::GetCubeBlock(VRageMath.Vector3I pos)
stloc.s 8 (VRage.Game.ModAPI.IMySlimBlock)
ldloc.s 8 (VRage.Game.ModAPI.IMySlimBlock)
brfalse.s L10
ldloc.s 8 (VRage.Game.ModAPI.IMySlimBlock)
callvirt abstract virtual VRage.Game.ModAPI.IMyCubeBlock VRage.Game.ModAPI.IMySlimBlock::get_FatBlock()
isinst Sandbox.ModAPI.IMyDoor
stloc.s 10 (Sandbox.ModAPI.IMyDoor)
ldloc.s 10 (Sandbox.ModAPI.IMyDoor)
brfalse.s L11
ldloc.s 10 (Sandbox.ModAPI.IMyDoor)
callvirt abstract virtual Sandbox.ModAPI.Ingame.DoorStatus Sandbox.ModAPI.Ingame.IMyDoor::get_Status()
ldc.i4.1
beq.s L12
ldloc.s 7 (System.Boolean)
brtrue.s L13
L12:
ldarg.0
ldfld System.Collections.Generic.Queue`1<VRageMath.Vector3I> Sandbox.Game.GameSystems.MyGridGasSystem::m_blockQueue
ldloc.s 6 (VRageMath.Vector3I)
callvirt System.Void System.Collections.Generic.Queue`1<VRageMath.Vector3I>::Enqueue(VRageMath.Vector3I item)
br.s L14
L10:
L11:
ldarg.0
ldfld Sandbox.Game.GameSystems.MyOxygenCube Sandbox.Game.GameSystems.MyGridGasSystem::m_cubeRoom
ldloc.s 6 (VRageMath.Vector3I)
ldfld System.Int32 VRageMath.Vector3I::X
ldloc.s 6 (VRageMath.Vector3I)
ldfld System.Int32 VRageMath.Vector3I::Y
ldloc.s 6 (VRageMath.Vector3I)
ldfld System.Int32 VRageMath.Vector3I::Z
callvirt Sandbox.Game.GameSystems.MyOxygenBlock Sandbox.Game.GameSystems.MyOxygenCube::get_Item(System.Int32 x, System.Int32 y, System.Int32 z)
stloc.s 9 (Sandbox.Game.GameSystems.MyOxygenBlock)
ldloc.s 9 (Sandbox.Game.GameSystems.MyOxygenBlock)
brfalse.s L15
ldloc.s 9 (Sandbox.Game.GameSystems.MyOxygenBlock)
callvirt Sandbox.Game.GameSystems.MyOxygenRoom Sandbox.Game.GameSystems.MyOxygenBlock::get_Room()
brtrue.s L16
L15:
ldloc.s 8 (VRage.Game.ModAPI.IMySlimBlock)
brtrue.s L17
L16:
ldarg.0
ldfld System.Collections.Generic.Queue`1<VRageMath.Vector3I> Sandbox.Game.GameSystems.MyGridGasSystem::m_blockQueue
ldloc.s 6 (VRageMath.Vector3I)
callvirt System.Void System.Collections.Generic.Queue`1<VRageMath.Vector3I>::Enqueue(VRageMath.Vector3I item)
L5:
L9:
L13:
L14:
L17:
ldloc.s 5 (System.Int32)
ldc.i4.1
add
stloc.s 5 (System.Int32)
L4:
ldloc.s 5 (System.Int32)
ldarg.0
ldfld VRageMath.Vector3I[] Sandbox.Game.GameSystems.MyGridGasSystem::m_neighbours
ldlen
conv.i4
blt L18
L3:
L8:
ldarg.0
ldfld System.Collections.Generic.Queue`1<VRageMath.Vector3I> Sandbox.Game.GameSystems.MyGridGasSystem::m_blockQueue
callvirt virtual System.Int32 System.Collections.Generic.Queue`1<VRageMath.Vector3I>::get_Count()
ldc.i4.0
bgt L19
ldloc.s 4 (System.Boolean)
brfalse L20
ldnull
stloc.s 11 (Sandbox.Game.GameSystems.MyOxygenRoomLink)
ldloc.0
ldarg.1
bne.un L21
ldarg.0
ldarg.0
ldfld System.Int32 Sandbox.Game.GameSystems.MyGridGasSystem::m_lastRoomIndex
ldc.i4.1
add
stfld System.Int32 Sandbox.Game.GameSystems.MyGridGasSystem::m_lastRoomIndex
ldarg.0
ldfld System.Int32 Sandbox.Game.GameSystems.MyGridGasSystem::m_lastRoomIndex
newobj System.Void Sandbox.Game.GameSystems.MyOxygenRoom::.ctor(System.Int32 index)
starg.s 1
ldarg.0
ldfld VRage.Collections.MyConcurrentList`1<Sandbox.Game.GameSystems.MyOxygenRoom> Sandbox.Game.GameSystems.MyGridGasSystem::m_rooms
ldarg.1
callvirt virtual System.Void VRage.Collections.MyConcurrentList`1<Sandbox.Game.GameSystems.MyOxygenRoom>::Add(Sandbox.Game.GameSystems.MyOxygenRoom value)
ldarg.1
newobj System.Void Sandbox.Game.GameSystems.MyOxygenRoomLink::.ctor(Sandbox.Game.GameSystems.MyOxygenRoom room)
stloc.s 11 (Sandbox.Game.GameSystems.MyOxygenRoomLink)
ldarg.1
ldloc.1
callvirt System.Void Sandbox.Game.GameSystems.MyOxygenRoom::set_StartingPosition(VRageMath.Vector3I value)
ldloc.3
callvirt System.Collections.Generic.Enumerator<VRageMath.Vector3I> System.Collections.Generic.HashSet`1<VRageMath.Vector3I>::GetEnumerator()
stloc.s 12 (System.Collections.Generic.HashSet`1+Enumerator[VRageMath.Vector3I])
[EX_BeginException]
br L22
L24:
ldloca.s 12 (System.Collections.Generic.HashSet`1+Enumerator[VRageMath.Vector3I])
call virtual VRageMath.Vector3I System.Collections.Generic.Enumerator<VRageMath.Vector3I>::get_Current()
stloc.s 13 (VRageMath.Vector3I)
ldarg.0
ldfld Sandbox.Game.GameSystems.MyOxygenCube Sandbox.Game.GameSystems.MyGridGasSystem::m_cubeRoom
ldloc.s 13 (VRageMath.Vector3I)
ldfld System.Int32 VRageMath.Vector3I::X
ldloc.s 13 (VRageMath.Vector3I)
ldfld System.Int32 VRageMath.Vector3I::Y
ldloc.s 13 (VRageMath.Vector3I)
ldfld System.Int32 VRageMath.Vector3I::Z
callvirt Sandbox.Game.GameSystems.MyOxygenBlock Sandbox.Game.GameSystems.MyOxygenCube::get_Item(System.Int32 x, System.Int32 y, System.Int32 z)
callvirt Sandbox.Game.GameSystems.MyOxygenRoom Sandbox.Game.GameSystems.MyOxygenBlock::get_Room()
ldloc.0
bne.un.s L23
ldarg.0
ldfld Sandbox.Game.GameSystems.MyOxygenCube Sandbox.Game.GameSystems.MyGridGasSystem::m_cubeRoom
ldloc.s 13 (VRageMath.Vector3I)
ldfld System.Int32 VRageMath.Vector3I::X
ldloc.s 13 (VRageMath.Vector3I)
ldfld System.Int32 VRageMath.Vector3I::Y
ldloc.s 13 (VRageMath.Vector3I)
ldfld System.Int32 VRageMath.Vector3I::Z
callvirt Sandbox.Game.GameSystems.MyOxygenBlock Sandbox.Game.GameSystems.MyOxygenCube::get_Item(System.Int32 x, System.Int32 y, System.Int32 z)
ldloc.s 11 (Sandbox.Game.GameSystems.MyOxygenRoomLink)
callvirt System.Void Sandbox.Game.GameSystems.MyOxygenBlock::set_RoomLink(Sandbox.Game.GameSystems.MyOxygenRoomLink value)
ldarg.1
dup
callvirt virtual System.Int32 Sandbox.Game.GameSystems.MyOxygenRoom::get_BlockCount()
stloc.s 14 (System.Int32)
ldloc.s 14 (System.Int32)
ldc.i4.1
add
callvirt System.Void Sandbox.Game.GameSystems.MyOxygenRoom::set_BlockCount(System.Int32 value)
ldloc.0
dup
callvirt virtual System.Int32 Sandbox.Game.GameSystems.MyOxygenRoom::get_BlockCount()
stloc.s 14 (System.Int32)
ldloc.s 14 (System.Int32)
ldc.i4.1
sub
callvirt System.Void Sandbox.Game.GameSystems.MyOxygenRoom::set_BlockCount(System.Int32 value)
ldloc.0
callvirt System.Collections.Generic.HashSet`1<VRageMath.Vector3I> Sandbox.Game.GameSystems.MyOxygenRoom::get_Blocks()
ldloc.s 13 (VRageMath.Vector3I)
callvirt virtual System.Boolean System.Collections.Generic.HashSet`1<VRageMath.Vector3I>::Remove(VRageMath.Vector3I item)
pop
L22:
L23:
ldloca.s 12 (System.Collections.Generic.HashSet`1+Enumerator[VRageMath.Vector3I])
call virtual System.Boolean System.Collections.Generic.Enumerator<VRageMath.Vector3I>::MoveNext()
brtrue L24
leave L25
[EX_BeginFinally]
ldloca.s 12 (System.Collections.Generic.HashSet`1+Enumerator[VRageMath.Vector3I])
constrained. System.Collections.Generic.HashSet`1+Enumerator[VRageMath.Vector3I]
callvirt abstract virtual System.Void System.IDisposable::Dispose()
[EX_EndException]
endfinally
L21:
ldarg.1
callvirt Sandbox.Game.GameSystems.MyOxygenRoomLink Sandbox.Game.GameSystems.MyOxygenRoom::get_Link()
stloc.s 11 (Sandbox.Game.GameSystems.MyOxygenRoomLink)
ldarg.1
callvirt virtual System.Int32 Sandbox.Game.GameSystems.MyOxygenRoom::get_BlockCount()
stloc.s 15 (System.Int32)
ldarg.1
ldloc.3
callvirt virtual System.Int32 System.Collections.Generic.HashSet`1<VRageMath.Vector3I>::get_Count()
callvirt System.Void Sandbox.Game.GameSystems.MyOxygenRoom::set_BlockCount(System.Int32 value)
ldloc.3
callvirt System.Collections.Generic.Enumerator<VRageMath.Vector3I> System.Collections.Generic.HashSet`1<VRageMath.Vector3I>::GetEnumerator()
stloc.s 12 (System.Collections.Generic.HashSet`1+Enumerator[VRageMath.Vector3I])
[EX_BeginException]
br.s L26
L28:
ldloca.s 12 (System.Collections.Generic.HashSet`1+Enumerator[VRageMath.Vector3I])
call virtual VRageMath.Vector3I System.Collections.Generic.Enumerator<VRageMath.Vector3I>::get_Current()
stloc.s 16 (VRageMath.Vector3I)
ldarg.0
ldfld Sandbox.Game.GameSystems.MyOxygenCube Sandbox.Game.GameSystems.MyGridGasSystem::m_cubeRoom
ldloc.s 16 (VRageMath.Vector3I)
ldloca.s 17 (Sandbox.Game.GameSystems.MyOxygenBlock)
callvirt System.Boolean Sandbox.Game.GameSystems.MyOxygenCube::TryGetValue(VRageMath.Vector3I key, Sandbox.Game.GameSystems.MyOxygenBlock& value)
brtrue.s L27
newobj System.Void Sandbox.Game.GameSystems.MyOxygenBlock::.ctor()
stloc.s 17 (Sandbox.Game.GameSystems.MyOxygenBlock)
ldarg.0
ldfld Sandbox.Game.GameSystems.MyOxygenCube Sandbox.Game.GameSystems.MyGridGasSystem::m_cubeRoom
ldloc.s 16 (VRageMath.Vector3I)
ldloc.s 17 (Sandbox.Game.GameSystems.MyOxygenBlock)
callvirt System.Void Sandbox.Game.GameSystems.MyOxygenCube::Add(VRageMath.Vector3I key, Sandbox.Game.GameSystems.MyOxygenBlock value)
L27:
ldloc.s 17 (Sandbox.Game.GameSystems.MyOxygenBlock)
ldloc.s 11 (Sandbox.Game.GameSystems.MyOxygenRoomLink)
callvirt System.Void Sandbox.Game.GameSystems.MyOxygenBlock::set_RoomLink(Sandbox.Game.GameSystems.MyOxygenRoomLink value)
L26:
ldloca.s 12 (System.Collections.Generic.HashSet`1+Enumerator[VRageMath.Vector3I])
call virtual System.Boolean System.Collections.Generic.Enumerator<VRageMath.Vector3I>::MoveNext()
brtrue.s L28
leave.s L29
[EX_BeginFinally]
ldloca.s 12 (System.Collections.Generic.HashSet`1+Enumerator[VRageMath.Vector3I])
constrained. System.Collections.Generic.HashSet`1+Enumerator[VRageMath.Vector3I]
callvirt abstract virtual System.Void System.IDisposable::Dispose()
[EX_EndException]
endfinally
L29:
ldloc.s 15 (System.Int32)
ldloc.3
callvirt virtual System.Int32 System.Collections.Generic.HashSet`1<VRageMath.Vector3I>::get_Count()
ble.s L30
ldarg.1
callvirt System.Collections.Generic.HashSet`1<VRageMath.Vector3I> Sandbox.Game.GameSystems.MyOxygenRoom::get_Blocks()
stloc.s 18 (System.Collections.Generic.HashSet`1[VRageMath.Vector3I])
ldloc.s 18 (System.Collections.Generic.HashSet`1[VRageMath.Vector3I])
ldloc.3
callvirt virtual System.Void System.Collections.Generic.HashSet`1<VRageMath.Vector3I>::ExceptWith(System.Collections.Generic.IEnumerable`1<VRageMath.Vector3I> other)
ldarg.1
callvirt virtual System.Single Sandbox.Game.GameSystems.MyOxygenRoom::get_OxygenAmount()
ldloc.s 15 (System.Int32)
conv.r4
div
ldloc.s 18 (System.Collections.Generic.HashSet`1[VRageMath.Vector3I])
callvirt virtual System.Int32 System.Collections.Generic.HashSet`1<VRageMath.Vector3I>::get_Count()
conv.r4
mul
stloc.s 19 (System.Single)
ldarg.0
ldloc.s 18 (System.Collections.Generic.HashSet`1[VRageMath.Vector3I])
ldloc.s 19 (System.Single)
ldloc.s 18 (System.Collections.Generic.HashSet`1[VRageMath.Vector3I])
call static VRageMath.Vector3I System.Collections.Generic.HashSetExtensions::FirstElement(System.Collections.Generic.HashSet`1<VRageMath.Vector3I> hashset)
call Sandbox.Game.GameSystems.MyOxygenRoom Sandbox.Game.GameSystems.MyGridGasSystem::CreateAirtightRoom(System.Collections.Generic.HashSet`1<VRageMath.Vector3I> roomBlocks, System.Single oxygenAmount, VRageMath.Vector3I startingPosition)
pop
ldarg.1
dup
callvirt virtual System.Single Sandbox.Game.GameSystems.MyOxygenRoom::get_OxygenAmount()
ldloc.s 19 (System.Single)
sub
callvirt System.Void Sandbox.Game.GameSystems.MyOxygenRoom::set_OxygenAmount(System.Single value)
L25:
L30:
ldarg.1
ldloc.3
callvirt System.Void Sandbox.Game.GameSystems.MyOxygenRoom::set_Blocks(System.Collections.Generic.HashSet`1<VRageMath.Vector3I> value)
L20:
ldarg.1
ldloc.s 4 (System.Boolean)
callvirt System.Void Sandbox.Game.GameSystems.MyOxygenRoom::set_IsAirtight(System.Boolean value)
ldarg.1
ldc.i4.0
callvirt System.Void Sandbox.Game.GameSystems.MyOxygenRoom::set_IsDirty(System.Boolean value)
ret
