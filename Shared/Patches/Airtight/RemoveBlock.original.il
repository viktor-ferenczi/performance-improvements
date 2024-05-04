// 241c139e
ldc.i4.0
stloc.0
ldarg.2
ldsfld VRageMath.Vector3I VRageMath.Vector3I::Zero
stobj VRageMath.Vector3I
ldarg.3
ldsfld VRageMath.Vector3I VRageMath.Vector3I::Zero
stobj VRageMath.Vector3I
ldarg.1
stloc.1
ldarg.0
ldfld VRage.Collections.MyConcurrentList`1<Sandbox.Game.GameSystems.MyOxygenRoom> Sandbox.Game.GameSystems.MyGridGasSystem::m_rooms
ldc.i4.0
callvirt virtual Sandbox.Game.GameSystems.MyOxygenRoom VRage.Collections.MyConcurrentList`1<Sandbox.Game.GameSystems.MyOxygenRoom>::get_Item(System.Int32 index)
stloc.2
ldarg.0
ldloca.s 1 (VRageMath.Vector3I)
ldloc.2
call Sandbox.Game.GameSystems.MyOxygenRoom Sandbox.Game.GameSystems.MyGridGasSystem::GetMaxBlockRoom(VRageMath.Vector3I& current, Sandbox.Game.GameSystems.MyOxygenRoom topRoom)
stloc.3
ldloc.3
brtrue.s L0
ldloc.0
ret
L0:
ldc.i4.0
stloc.s 6 (System.Int32)
br L1
L23:
ldloc.1
ldarg.0
ldfld VRageMath.Vector3I[] Sandbox.Game.GameSystems.MyGridGasSystem::m_neighboursForDelete
ldloc.s 6 (System.Int32)
ldelem VRageMath.Vector3I
call static VRageMath.Vector3I VRageMath.Vector3I::op_Addition(VRageMath.Vector3I a, VRageMath.Vector3I b)
stloc.s 7 (VRageMath.Vector3I)
ldarg.0
ldloc.s 7 (VRageMath.Vector3I)
call System.Boolean Sandbox.Game.GameSystems.MyGridGasSystem::IsInBounds(VRageMath.Vector3I pos)
brfalse L2
ldarg.0
ldloca.s 7 (VRageMath.Vector3I)
call Sandbox.Game.GameSystems.MyOxygenRoom Sandbox.Game.GameSystems.MyGridGasSystem::GetOxygenRoomForCubeGridPosition(VRageMath.Vector3I& gridPosition)
stloc.s 8 (Sandbox.Game.GameSystems.MyOxygenRoom)
ldloc.s 8 (Sandbox.Game.GameSystems.MyOxygenRoom)
brfalse L3
ldloc.s 8 (Sandbox.Game.GameSystems.MyOxygenRoom)
ldloc.3
beq L4
ldloc.1
ldloc.s 7 (VRageMath.Vector3I)
call static System.Boolean VRageMath.Vector3I::op_Inequality(VRageMath.Vector3I a, VRageMath.Vector3I b)
brfalse.s L5
ldarg.0
ldloc.1
ldloc.s 7 (VRageMath.Vector3I)
call System.Boolean Sandbox.Game.GameSystems.MyGridGasSystem::IsAirtightBetweenPositions(VRageMath.Vector3I startPos, VRageMath.Vector3I endPos)
brtrue L6
L5:
ldloc.3
callvirt virtual System.Boolean Sandbox.Game.GameSystems.MyOxygenRoom::get_IsAirtight()
brfalse L7
ldloc.s 8 (Sandbox.Game.GameSystems.MyOxygenRoom)
callvirt virtual System.Boolean Sandbox.Game.GameSystems.MyOxygenRoom::get_IsAirtight()
brtrue L8
ldloc.s 8 (Sandbox.Game.GameSystems.MyOxygenRoom)
dup
callvirt virtual System.Int32 Sandbox.Game.GameSystems.MyOxygenRoom::get_BlockCount()
ldloc.3
callvirt virtual System.Int32 Sandbox.Game.GameSystems.MyOxygenRoom::get_BlockCount()
add
callvirt System.Void Sandbox.Game.GameSystems.MyOxygenRoom::set_BlockCount(System.Int32 value)
ldloc.s 8 (Sandbox.Game.GameSystems.MyOxygenRoom)
dup
callvirt virtual System.Single Sandbox.Game.GameSystems.MyOxygenRoom::get_OxygenAmount()
ldloc.3
callvirt virtual System.Single Sandbox.Game.GameSystems.MyOxygenRoom::get_OxygenAmount()
add
callvirt System.Void Sandbox.Game.GameSystems.MyOxygenRoom::set_OxygenAmount(System.Single value)
ldarg.0
ldloc.s 8 (Sandbox.Game.GameSystems.MyOxygenRoom)
ldloc.3
ldloc.s 8 (Sandbox.Game.GameSystems.MyOxygenRoom)
callvirt Sandbox.Game.GameSystems.MyOxygenRoomLink Sandbox.Game.GameSystems.MyOxygenRoom::get_Link()
call System.Void Sandbox.Game.GameSystems.MyGridGasSystem::MergeRooms(Sandbox.Game.GameSystems.MyOxygenRoom target, Sandbox.Game.GameSystems.MyOxygenRoom withRoom, Sandbox.Game.GameSystems.MyOxygenRoomLink link)
ldloc.3
callvirt System.Collections.Generic.HashSet`1<VRageMath.Vector3I> Sandbox.Game.GameSystems.MyOxygenRoom::get_Blocks()
brfalse.s L9
ldloc.s 8 (Sandbox.Game.GameSystems.MyOxygenRoom)
callvirt System.Collections.Generic.HashSet`1<VRageMath.Vector3I> Sandbox.Game.GameSystems.MyOxygenRoom::get_Blocks()
brfalse.s L10
ldloc.s 8 (Sandbox.Game.GameSystems.MyOxygenRoom)
callvirt System.Collections.Generic.HashSet`1<VRageMath.Vector3I> Sandbox.Game.GameSystems.MyOxygenRoom::get_Blocks()
ldloc.3
callvirt System.Collections.Generic.HashSet`1<VRageMath.Vector3I> Sandbox.Game.GameSystems.MyOxygenRoom::get_Blocks()
callvirt virtual System.Void System.Collections.Generic.HashSet`1<VRageMath.Vector3I>::UnionWith(System.Collections.Generic.IEnumerable`1<VRageMath.Vector3I> other)
L9:
L10:
ldloc.3
ldarg.0
call VRage.Game.ModAPI.IMyCubeGrid Sandbox.Game.GameSystems.MyGridGasSystem::get_CubeGrid()
callvirt abstract virtual System.Single VRage.Game.ModAPI.Ingame.IMyCubeGrid::get_GridSize()
callvirt virtual System.Single Sandbox.Game.GameSystems.MyOxygenRoom::OxygenLevel(System.Single gridSize)
ldloc.s 8 (Sandbox.Game.GameSystems.MyOxygenRoom)
callvirt virtual System.Single Sandbox.Game.GameSystems.MyOxygenRoom::get_EnvironmentOxygen()
sub
ldc.r4 0.2
ble.un.s L11
ldc.i4.1
stloc.0
ldarg.2
ldloc.1
stobj VRageMath.Vector3I
ldarg.3
ldloc.s 7 (VRageMath.Vector3I)
stobj VRageMath.Vector3I
L11:
ldloc.3
ldc.i4.0
callvirt System.Void Sandbox.Game.GameSystems.MyOxygenRoom::set_IsAirtight(System.Boolean value)
ldloc.3
ldc.r4 0
callvirt System.Void Sandbox.Game.GameSystems.MyOxygenRoom::set_OxygenAmount(System.Single value)
ldloc.3
ldloc.3
callvirt virtual System.Single Sandbox.Game.GameSystems.MyOxygenRoom::get_EnvironmentOxygen()
ldloc.s 8 (Sandbox.Game.GameSystems.MyOxygenRoom)
callvirt virtual System.Single Sandbox.Game.GameSystems.MyOxygenRoom::get_EnvironmentOxygen()
call static System.Single System.Math::Max(System.Single val1, System.Single val2)
callvirt System.Void Sandbox.Game.GameSystems.MyOxygenRoom::set_EnvironmentOxygen(System.Single value)
ldloc.3
ldarg.0
call System.Int32 Sandbox.Game.GameSystems.MyGridGasSystem::GetTotalGamePlayTimeInMilliseconds()
callvirt System.Void Sandbox.Game.GameSystems.MyOxygenRoom::set_DepressurizationTime(System.Int32 value)
ldloc.3
callvirt Sandbox.Game.GameSystems.MyOxygenRoomLink Sandbox.Game.GameSystems.MyOxygenRoom::get_Link()
ldloc.s 8 (Sandbox.Game.GameSystems.MyOxygenRoom)
callvirt System.Void Sandbox.Game.GameSystems.MyOxygenRoomLink::set_Room(Sandbox.Game.GameSystems.MyOxygenRoom value)
ldloc.s 8 (Sandbox.Game.GameSystems.MyOxygenRoom)
ldloc.3
beq.s L12
ldloc.3
ldloc.2
beq.s L13
ldloc.3
ldc.i4.0
callvirt System.Void Sandbox.Game.GameSystems.MyOxygenRoom::set_BlockCount(System.Int32 value)
ldloc.3
ldnull
callvirt System.Void Sandbox.Game.GameSystems.MyOxygenRoom::set_Blocks(System.Collections.Generic.HashSet`1<VRageMath.Vector3I> value)
ldarg.0
ldfld VRage.Collections.MyConcurrentList`1<Sandbox.Game.GameSystems.MyOxygenRoom> Sandbox.Game.GameSystems.MyGridGasSystem::m_rooms
ldloc.3
callvirt virtual System.Boolean VRage.Collections.MyConcurrentList`1<Sandbox.Game.GameSystems.MyOxygenRoom>::Remove(Sandbox.Game.GameSystems.MyOxygenRoom item)
pop
L12:
L13:
ldloc.s 8 (Sandbox.Game.GameSystems.MyOxygenRoom)
stloc.3
br L14
L7:
L8:
ldloc.3
callvirt virtual System.Boolean Sandbox.Game.GameSystems.MyOxygenRoom::get_IsAirtight()
brtrue L15
ldloc.s 8 (Sandbox.Game.GameSystems.MyOxygenRoom)
callvirt virtual System.Boolean Sandbox.Game.GameSystems.MyOxygenRoom::get_IsAirtight()
brfalse L16
ldloc.3
dup
callvirt virtual System.Int32 Sandbox.Game.GameSystems.MyOxygenRoom::get_BlockCount()
ldloc.s 8 (Sandbox.Game.GameSystems.MyOxygenRoom)
callvirt virtual System.Int32 Sandbox.Game.GameSystems.MyOxygenRoom::get_BlockCount()
add
callvirt System.Void Sandbox.Game.GameSystems.MyOxygenRoom::set_BlockCount(System.Int32 value)
ldloc.3
dup
callvirt virtual System.Single Sandbox.Game.GameSystems.MyOxygenRoom::get_OxygenAmount()
ldloc.s 8 (Sandbox.Game.GameSystems.MyOxygenRoom)
callvirt virtual System.Single Sandbox.Game.GameSystems.MyOxygenRoom::get_OxygenAmount()
add
callvirt System.Void Sandbox.Game.GameSystems.MyOxygenRoom::set_OxygenAmount(System.Single value)
ldarg.0
ldloc.3
ldloc.s 8 (Sandbox.Game.GameSystems.MyOxygenRoom)
ldloc.3
callvirt Sandbox.Game.GameSystems.MyOxygenRoomLink Sandbox.Game.GameSystems.MyOxygenRoom::get_Link()
call System.Void Sandbox.Game.GameSystems.MyGridGasSystem::MergeRooms(Sandbox.Game.GameSystems.MyOxygenRoom target, Sandbox.Game.GameSystems.MyOxygenRoom withRoom, Sandbox.Game.GameSystems.MyOxygenRoomLink link)
ldloc.3
ldloc.3
callvirt virtual System.Single Sandbox.Game.GameSystems.MyOxygenRoom::get_EnvironmentOxygen()
ldloc.s 8 (Sandbox.Game.GameSystems.MyOxygenRoom)
callvirt virtual System.Single Sandbox.Game.GameSystems.MyOxygenRoom::get_EnvironmentOxygen()
call static System.Single System.Math::Max(System.Single val1, System.Single val2)
callvirt System.Void Sandbox.Game.GameSystems.MyOxygenRoom::set_EnvironmentOxygen(System.Single value)
ldloc.s 8 (Sandbox.Game.GameSystems.MyOxygenRoom)
ldarg.0
call VRage.Game.ModAPI.IMyCubeGrid Sandbox.Game.GameSystems.MyGridGasSystem::get_CubeGrid()
callvirt abstract virtual System.Single VRage.Game.ModAPI.Ingame.IMyCubeGrid::get_GridSize()
callvirt virtual System.Single Sandbox.Game.GameSystems.MyOxygenRoom::OxygenLevel(System.Single gridSize)
ldloc.3
callvirt virtual System.Single Sandbox.Game.GameSystems.MyOxygenRoom::get_EnvironmentOxygen()
sub
ldc.r4 0.2
ble.un.s L17
ldc.i4.1
stloc.0
ldarg.2
ldloc.1
stobj VRageMath.Vector3I
ldarg.3
ldloc.s 7 (VRageMath.Vector3I)
stobj VRageMath.Vector3I
L17:
ldloc.s 8 (Sandbox.Game.GameSystems.MyOxygenRoom)
ldc.i4.0
callvirt System.Void Sandbox.Game.GameSystems.MyOxygenRoom::set_IsAirtight(System.Boolean value)
ldloc.s 8 (Sandbox.Game.GameSystems.MyOxygenRoom)
ldc.r4 0
callvirt System.Void Sandbox.Game.GameSystems.MyOxygenRoom::set_OxygenAmount(System.Single value)
ldloc.s 8 (Sandbox.Game.GameSystems.MyOxygenRoom)
ldloc.3
callvirt virtual System.Single Sandbox.Game.GameSystems.MyOxygenRoom::get_EnvironmentOxygen()
ldloc.s 8 (Sandbox.Game.GameSystems.MyOxygenRoom)
callvirt virtual System.Single Sandbox.Game.GameSystems.MyOxygenRoom::get_EnvironmentOxygen()
call static System.Single System.Math::Max(System.Single val1, System.Single val2)
callvirt System.Void Sandbox.Game.GameSystems.MyOxygenRoom::set_EnvironmentOxygen(System.Single value)
ldloc.s 8 (Sandbox.Game.GameSystems.MyOxygenRoom)
ldarg.0
call System.Int32 Sandbox.Game.GameSystems.MyGridGasSystem::GetTotalGamePlayTimeInMilliseconds()
callvirt System.Void Sandbox.Game.GameSystems.MyOxygenRoom::set_DepressurizationTime(System.Int32 value)
ldloc.s 8 (Sandbox.Game.GameSystems.MyOxygenRoom)
callvirt Sandbox.Game.GameSystems.MyOxygenRoomLink Sandbox.Game.GameSystems.MyOxygenRoom::get_Link()
ldloc.3
callvirt System.Void Sandbox.Game.GameSystems.MyOxygenRoomLink::set_Room(Sandbox.Game.GameSystems.MyOxygenRoom value)
ldloc.s 8 (Sandbox.Game.GameSystems.MyOxygenRoom)
ldloc.3
beq L18
ldloc.s 8 (Sandbox.Game.GameSystems.MyOxygenRoom)
ldloc.2
beq L19
ldloc.s 8 (Sandbox.Game.GameSystems.MyOxygenRoom)
ldc.i4.0
callvirt System.Void Sandbox.Game.GameSystems.MyOxygenRoom::set_BlockCount(System.Int32 value)
ldloc.s 8 (Sandbox.Game.GameSystems.MyOxygenRoom)
ldnull
callvirt System.Void Sandbox.Game.GameSystems.MyOxygenRoom::set_Blocks(System.Collections.Generic.HashSet`1<VRageMath.Vector3I> value)
ldarg.0
ldfld VRage.Collections.MyConcurrentList`1<Sandbox.Game.GameSystems.MyOxygenRoom> Sandbox.Game.GameSystems.MyGridGasSystem::m_rooms
ldloc.s 8 (Sandbox.Game.GameSystems.MyOxygenRoom)
callvirt virtual System.Boolean VRage.Collections.MyConcurrentList`1<Sandbox.Game.GameSystems.MyOxygenRoom>::Remove(Sandbox.Game.GameSystems.MyOxygenRoom item)
pop
br.s L20
L15:
L16:
ldloc.3
dup
callvirt virtual System.Int32 Sandbox.Game.GameSystems.MyOxygenRoom::get_BlockCount()
ldloc.s 8 (Sandbox.Game.GameSystems.MyOxygenRoom)
callvirt virtual System.Int32 Sandbox.Game.GameSystems.MyOxygenRoom::get_BlockCount()
add
callvirt System.Void Sandbox.Game.GameSystems.MyOxygenRoom::set_BlockCount(System.Int32 value)
ldloc.3
dup
callvirt virtual System.Single Sandbox.Game.GameSystems.MyOxygenRoom::get_OxygenAmount()
ldloc.s 8 (Sandbox.Game.GameSystems.MyOxygenRoom)
callvirt virtual System.Single Sandbox.Game.GameSystems.MyOxygenRoom::get_OxygenAmount()
add
callvirt System.Void Sandbox.Game.GameSystems.MyOxygenRoom::set_OxygenAmount(System.Single value)
ldarg.0
ldloc.3
ldloc.s 8 (Sandbox.Game.GameSystems.MyOxygenRoom)
ldloc.3
callvirt Sandbox.Game.GameSystems.MyOxygenRoomLink Sandbox.Game.GameSystems.MyOxygenRoom::get_Link()
call System.Void Sandbox.Game.GameSystems.MyGridGasSystem::MergeRooms(Sandbox.Game.GameSystems.MyOxygenRoom target, Sandbox.Game.GameSystems.MyOxygenRoom withRoom, Sandbox.Game.GameSystems.MyOxygenRoomLink link)
ldloc.s 8 (Sandbox.Game.GameSystems.MyOxygenRoom)
callvirt Sandbox.Game.GameSystems.MyOxygenRoomLink Sandbox.Game.GameSystems.MyOxygenRoom::get_Link()
ldloc.3
callvirt System.Void Sandbox.Game.GameSystems.MyOxygenRoomLink::set_Room(Sandbox.Game.GameSystems.MyOxygenRoom value)
ldloc.s 8 (Sandbox.Game.GameSystems.MyOxygenRoom)
ldloc.3
beq.s L21
ldloc.s 8 (Sandbox.Game.GameSystems.MyOxygenRoom)
ldloc.2
beq.s L22
ldloc.s 8 (Sandbox.Game.GameSystems.MyOxygenRoom)
ldc.i4.0
callvirt System.Void Sandbox.Game.GameSystems.MyOxygenRoom::set_BlockCount(System.Int32 value)
ldloc.s 8 (Sandbox.Game.GameSystems.MyOxygenRoom)
ldnull
callvirt System.Void Sandbox.Game.GameSystems.MyOxygenRoom::set_Blocks(System.Collections.Generic.HashSet`1<VRageMath.Vector3I> value)
ldarg.0
ldfld VRage.Collections.MyConcurrentList`1<Sandbox.Game.GameSystems.MyOxygenRoom> Sandbox.Game.GameSystems.MyGridGasSystem::m_rooms
ldloc.s 8 (Sandbox.Game.GameSystems.MyOxygenRoom)
callvirt virtual System.Boolean VRage.Collections.MyConcurrentList`1<Sandbox.Game.GameSystems.MyOxygenRoom>::Remove(Sandbox.Game.GameSystems.MyOxygenRoom item)
pop
L2:
L3:
L4:
L6:
L14:
L18:
L19:
L20:
L21:
L22:
ldloc.s 6 (System.Int32)
ldc.i4.1
add
stloc.s 6 (System.Int32)
L1:
ldloc.s 6 (System.Int32)
ldarg.0
ldfld VRageMath.Vector3I[] Sandbox.Game.GameSystems.MyGridGasSystem::m_neighboursForDelete
ldlen
conv.i4
blt L23
ldloc.1
stloc.s 4 (VRageMath.Vector3I)
ldarg.0
ldfld Sandbox.Game.GameSystems.MyOxygenCube Sandbox.Game.GameSystems.MyGridGasSystem::m_cubeRoom
ldloc.s 4 (VRageMath.Vector3I)
ldfld System.Int32 VRageMath.Vector3I::X
ldloc.s 4 (VRageMath.Vector3I)
ldfld System.Int32 VRageMath.Vector3I::Y
ldloc.s 4 (VRageMath.Vector3I)
ldfld System.Int32 VRageMath.Vector3I::Z
callvirt Sandbox.Game.GameSystems.MyOxygenBlock Sandbox.Game.GameSystems.MyOxygenCube::get_Item(System.Int32 x, System.Int32 y, System.Int32 z)
stloc.s 5 (Sandbox.Game.GameSystems.MyOxygenBlock)
ldloc.s 5 (Sandbox.Game.GameSystems.MyOxygenBlock)
brtrue.s L24
newobj System.Void Sandbox.Game.GameSystems.MyOxygenBlock::.ctor()
stloc.s 5 (Sandbox.Game.GameSystems.MyOxygenBlock)
ldarg.0
ldfld Sandbox.Game.GameSystems.MyOxygenCube Sandbox.Game.GameSystems.MyGridGasSystem::m_cubeRoom
ldloc.s 4 (VRageMath.Vector3I)
ldloc.s 5 (Sandbox.Game.GameSystems.MyOxygenBlock)
callvirt System.Void Sandbox.Game.GameSystems.MyOxygenCube::Add(VRageMath.Vector3I key, Sandbox.Game.GameSystems.MyOxygenBlock value)
L24:
ldloc.s 5 (Sandbox.Game.GameSystems.MyOxygenBlock)
callvirt Sandbox.Game.GameSystems.MyOxygenRoom Sandbox.Game.GameSystems.MyOxygenBlock::get_Room()
brtrue.s L25
ldloc.s 5 (Sandbox.Game.GameSystems.MyOxygenBlock)
ldloc.3
callvirt Sandbox.Game.GameSystems.MyOxygenRoomLink Sandbox.Game.GameSystems.MyOxygenRoom::get_Link()
callvirt System.Void Sandbox.Game.GameSystems.MyOxygenBlock::set_RoomLink(Sandbox.Game.GameSystems.MyOxygenRoomLink value)
ldloc.3
dup
callvirt virtual System.Int32 Sandbox.Game.GameSystems.MyOxygenRoom::get_BlockCount()
stloc.s 9 (System.Int32)
ldloc.s 9 (System.Int32)
ldc.i4.1
add
callvirt System.Void Sandbox.Game.GameSystems.MyOxygenRoom::set_BlockCount(System.Int32 value)
ldloc.3
callvirt System.Collections.Generic.HashSet`1<VRageMath.Vector3I> Sandbox.Game.GameSystems.MyOxygenRoom::get_Blocks()
ldloc.s 4 (VRageMath.Vector3I)
callvirt virtual System.Boolean System.Collections.Generic.HashSet`1<VRageMath.Vector3I>::Add(VRageMath.Vector3I item)
pop
L25:
ldloc.0
ret
