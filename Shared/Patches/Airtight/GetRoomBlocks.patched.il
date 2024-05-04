// afe05d28
ldarg.0
ldfld System.Collections.Generic.Queue`1<VRageMath.Vector3I> Sandbox.Game.GameSystems.MyGridGasSystem::m_blockQueue
callvirt System.Void System.Collections.Generic.Queue`1<VRageMath.Vector3I>::Clear()
ldarg.0
ldfld System.Collections.Generic.Queue`1<VRageMath.Vector3I> Sandbox.Game.GameSystems.MyGridGasSystem::m_blockQueue
ldarg.1
callvirt System.Void System.Collections.Generic.Queue`1<VRageMath.Vector3I>::Enqueue(VRageMath.Vector3I item)
ldarg.0
ldfld System.Collections.Generic.HashSet`1<VRageMath.Vector3I> Sandbox.Game.GameSystems.MyGridGasSystem::m_visitedBlocks
callvirt virtual System.Void System.Collections.Generic.HashSet`1<VRageMath.Vector3I>::Clear()
ldarg.0
ldfld System.Collections.Generic.HashSet`1<VRageMath.Vector3I> Sandbox.Game.GameSystems.MyGridGasSystem::m_visitedBlocks
ldarg.1
callvirt virtual System.Boolean System.Collections.Generic.HashSet`1<VRageMath.Vector3I>::Add(VRageMath.Vector3I item)
pop
call static System.Collections.Generic.HashSet`1<VRageMath.Vector3I> Shared.Patches.MyGridGasSystemPatch::Get()
stloc.0
ldarg.1
stloc.1
ldloc.0
ldloc.1
callvirt virtual System.Boolean System.Collections.Generic.HashSet`1<VRageMath.Vector3I>::Add(VRageMath.Vector3I item)
pop
ldarg.2
brfalse L0
ldarg.0
ldfld Sandbox.Game.GameSystems.MyOxygenCube Sandbox.Game.GameSystems.MyGridGasSystem::m_cubeRoom
ldloc.1
ldloca.s 2 (Sandbox.Game.GameSystems.MyOxygenBlock)
callvirt System.Boolean Sandbox.Game.GameSystems.MyOxygenCube::TryGetValue(VRageMath.Vector3I key, Sandbox.Game.GameSystems.MyOxygenBlock& value)
brtrue.s L1
newobj System.Void Sandbox.Game.GameSystems.MyOxygenBlock::.ctor()
stloc.2
ldarg.0
ldfld Sandbox.Game.GameSystems.MyOxygenCube Sandbox.Game.GameSystems.MyGridGasSystem::m_cubeRoom
ldloc.1
ldloc.2
callvirt System.Void Sandbox.Game.GameSystems.MyOxygenCube::Add(VRageMath.Vector3I key, Sandbox.Game.GameSystems.MyOxygenBlock value)
L1:
ldloc.2
ldarg.2
callvirt Sandbox.Game.GameSystems.MyOxygenRoomLink Sandbox.Game.GameSystems.MyOxygenRoom::get_Link()
callvirt System.Void Sandbox.Game.GameSystems.MyOxygenBlock::set_RoomLink(Sandbox.Game.GameSystems.MyOxygenRoomLink value)
br L2
L11:
ldarg.0
ldfld System.Collections.Generic.Queue`1<VRageMath.Vector3I> Sandbox.Game.GameSystems.MyGridGasSystem::m_blockQueue
callvirt VRageMath.Vector3I System.Collections.Generic.Queue`1<VRageMath.Vector3I>::Dequeue()
stloc.3
ldc.i4.0
stloc.s 4 (System.Int32)
br L3
L10:
ldloc.3
ldarg.0
ldfld VRageMath.Vector3I[] Sandbox.Game.GameSystems.MyGridGasSystem::m_neighbours
ldloc.s 4 (System.Int32)
ldelem VRageMath.Vector3I
call static VRageMath.Vector3I VRageMath.Vector3I::op_Addition(VRageMath.Vector3I a, VRageMath.Vector3I b)
stloc.s 5 (VRageMath.Vector3I)
ldloc.s 5 (VRageMath.Vector3I)
ldarg.0
ldfld VRageMath.Vector3I Sandbox.Game.GameSystems.MyGridGasSystem::m_storedGridMin
call static VRageMath.Vector3I VRageMath.Vector3I::Min(VRageMath.Vector3I value1, VRageMath.Vector3I value2)
ldarg.0
ldfld VRageMath.Vector3I Sandbox.Game.GameSystems.MyGridGasSystem::m_storedGridMin
call static System.Boolean VRageMath.Vector3I::op_Inequality(VRageMath.Vector3I a, VRageMath.Vector3I b)
brtrue L4
ldloc.s 5 (VRageMath.Vector3I)
ldarg.0
ldfld VRageMath.Vector3I Sandbox.Game.GameSystems.MyGridGasSystem::m_storedGridMax
call static VRageMath.Vector3I VRageMath.Vector3I::Max(VRageMath.Vector3I value1, VRageMath.Vector3I value2)
ldarg.0
ldfld VRageMath.Vector3I Sandbox.Game.GameSystems.MyGridGasSystem::m_storedGridMax
call static System.Boolean VRageMath.Vector3I::op_Inequality(VRageMath.Vector3I a, VRageMath.Vector3I b)
brtrue.s L5
ldarg.0
ldfld System.Collections.Generic.HashSet`1<VRageMath.Vector3I> Sandbox.Game.GameSystems.MyGridGasSystem::m_visitedBlocks
ldloc.s 5 (VRageMath.Vector3I)
callvirt virtual System.Boolean System.Collections.Generic.HashSet`1<VRageMath.Vector3I>::Contains(VRageMath.Vector3I item)
brtrue.s L6
ldarg.0
ldloc.3
ldloc.s 5 (VRageMath.Vector3I)
call System.Boolean Sandbox.Game.GameSystems.MyGridGasSystem::IsAirtightBetweenPositions(VRageMath.Vector3I startPos, VRageMath.Vector3I endPos)
brtrue.s L7
ldarg.0
ldfld System.Collections.Generic.HashSet`1<VRageMath.Vector3I> Sandbox.Game.GameSystems.MyGridGasSystem::m_visitedBlocks
ldloc.s 5 (VRageMath.Vector3I)
callvirt virtual System.Boolean System.Collections.Generic.HashSet`1<VRageMath.Vector3I>::Add(VRageMath.Vector3I item)
pop
ldarg.0
ldfld System.Collections.Generic.Queue`1<VRageMath.Vector3I> Sandbox.Game.GameSystems.MyGridGasSystem::m_blockQueue
ldloc.s 5 (VRageMath.Vector3I)
callvirt System.Void System.Collections.Generic.Queue`1<VRageMath.Vector3I>::Enqueue(VRageMath.Vector3I item)
ldloc.s 5 (VRageMath.Vector3I)
stloc.s 6 (VRageMath.Vector3I)
ldloc.0
ldloc.s 6 (VRageMath.Vector3I)
callvirt virtual System.Boolean System.Collections.Generic.HashSet`1<VRageMath.Vector3I>::Add(VRageMath.Vector3I item)
pop
ldarg.2
brfalse.s L8
ldarg.0
ldfld Sandbox.Game.GameSystems.MyOxygenCube Sandbox.Game.GameSystems.MyGridGasSystem::m_cubeRoom
ldloc.s 6 (VRageMath.Vector3I)
ldloca.s 7 (Sandbox.Game.GameSystems.MyOxygenBlock)
callvirt System.Boolean Sandbox.Game.GameSystems.MyOxygenCube::TryGetValue(VRageMath.Vector3I key, Sandbox.Game.GameSystems.MyOxygenBlock& value)
brtrue.s L9
newobj System.Void Sandbox.Game.GameSystems.MyOxygenBlock::.ctor()
stloc.s 7 (Sandbox.Game.GameSystems.MyOxygenBlock)
ldarg.0
ldfld Sandbox.Game.GameSystems.MyOxygenCube Sandbox.Game.GameSystems.MyGridGasSystem::m_cubeRoom
ldloc.s 6 (VRageMath.Vector3I)
ldloc.s 7 (Sandbox.Game.GameSystems.MyOxygenBlock)
callvirt System.Void Sandbox.Game.GameSystems.MyOxygenCube::Add(VRageMath.Vector3I key, Sandbox.Game.GameSystems.MyOxygenBlock value)
L9:
ldloc.s 7 (Sandbox.Game.GameSystems.MyOxygenBlock)
ldarg.2
callvirt Sandbox.Game.GameSystems.MyOxygenRoomLink Sandbox.Game.GameSystems.MyOxygenRoom::get_Link()
callvirt System.Void Sandbox.Game.GameSystems.MyOxygenBlock::set_RoomLink(Sandbox.Game.GameSystems.MyOxygenRoomLink value)
L4:
L5:
L6:
L7:
L8:
ldloc.s 4 (System.Int32)
ldc.i4.1
add
stloc.s 4 (System.Int32)
L3:
ldloc.s 4 (System.Int32)
ldarg.0
ldfld VRageMath.Vector3I[] Sandbox.Game.GameSystems.MyGridGasSystem::m_neighbours
ldlen
conv.i4
blt L10
L0:
L2:
ldarg.0
ldfld System.Collections.Generic.Queue`1<VRageMath.Vector3I> Sandbox.Game.GameSystems.MyGridGasSystem::m_blockQueue
callvirt virtual System.Int32 System.Collections.Generic.Queue`1<VRageMath.Vector3I>::get_Count()
ldc.i4.0
bgt L11
ldloc.0
ret
