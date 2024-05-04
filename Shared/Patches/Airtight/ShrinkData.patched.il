// 70dfe78b
ldarg.0
ldfld Sandbox.Game.GameSystems.MyOxygenCube Sandbox.Game.GameSystems.MyGridGasSystem::m_cubeRoom
brtrue.s L0
ret
L0:
ldarg.0
call VRageMath.Vector3I Sandbox.Game.GameSystems.MyGridGasSystem::GridMin()
stloc.0
ldarg.0
call VRageMath.Vector3I Sandbox.Game.GameSystems.MyGridGasSystem::GridMax()
stloc.1
ldloc.0
ldarg.0
ldfld VRageMath.Vector3I Sandbox.Game.GameSystems.MyGridGasSystem::m_storedGridMin
call static VRageMath.Vector3I VRageMath.Vector3I::op_Subtraction(VRageMath.Vector3I a, VRageMath.Vector3I b)
ldarg.0
ldfld VRageMath.Vector3I Sandbox.Game.GameSystems.MyGridGasSystem::m_storedGridMax
ldloc.1
call static VRageMath.Vector3I VRageMath.Vector3I::op_Subtraction(VRageMath.Vector3I a, VRageMath.Vector3I b)
stloc.2
ldsfld VRageMath.Vector3I VRageMath.Vector3I::Zero
call static System.Boolean VRageMath.Vector3I::op_Inequality(VRageMath.Vector3I a, VRageMath.Vector3I b)
brtrue.s L1
ldloc.2
ldsfld VRageMath.Vector3I VRageMath.Vector3I::Zero
call static System.Boolean VRageMath.Vector3I::op_Inequality(VRageMath.Vector3I a, VRageMath.Vector3I b)
brfalse L2
L1:
ldarg.0
ldloc.0
stfld VRageMath.Vector3I Sandbox.Game.GameSystems.MyGridGasSystem::m_storedGridMin
ldarg.0
ldloc.1
stfld VRageMath.Vector3I Sandbox.Game.GameSystems.MyGridGasSystem::m_storedGridMax
ldarg.0
ldfld VRage.Collections.MyConcurrentList`1<Sandbox.Game.GameSystems.MyOxygenRoom> Sandbox.Game.GameSystems.MyGridGasSystem::m_rooms
ldc.i4.0
callvirt virtual Sandbox.Game.GameSystems.MyOxygenRoom VRage.Collections.MyConcurrentList`1<Sandbox.Game.GameSystems.MyOxygenRoom>::get_Item(System.Int32 index)
stloc.3
call static System.Collections.Generic.HashSet`1<VRageMath.Vector3I> Shared.Patches.MyGridGasSystemPatch::Get()
stloc.s 4 (System.Collections.Generic.HashSet`1[VRageMath.Vector3I])
ldloc.3
callvirt System.Collections.Generic.HashSet`1<VRageMath.Vector3I> Sandbox.Game.GameSystems.MyOxygenRoom::get_Blocks()
callvirt System.Collections.Generic.Enumerator<VRageMath.Vector3I> System.Collections.Generic.HashSet`1<VRageMath.Vector3I>::GetEnumerator()
stloc.s 5 (System.Collections.Generic.HashSet`1+Enumerator[VRageMath.Vector3I])
[EX_BeginException]
br.s L3
L5:
ldloca.s 5 (System.Collections.Generic.HashSet`1+Enumerator[VRageMath.Vector3I])
call virtual VRageMath.Vector3I System.Collections.Generic.Enumerator<VRageMath.Vector3I>::get_Current()
stloc.s 6 (VRageMath.Vector3I)
ldarg.0
ldloc.s 6 (VRageMath.Vector3I)
call System.Boolean Sandbox.Game.GameSystems.MyGridGasSystem::IsInBounds(VRageMath.Vector3I pos)
brtrue.s L4
ldloc.s 4 (System.Collections.Generic.HashSet`1[VRageMath.Vector3I])
ldloc.s 6 (VRageMath.Vector3I)
callvirt virtual System.Boolean System.Collections.Generic.HashSet`1<VRageMath.Vector3I>::Add(VRageMath.Vector3I item)
pop
L3:
L4:
ldloca.s 5 (System.Collections.Generic.HashSet`1+Enumerator[VRageMath.Vector3I])
call virtual System.Boolean System.Collections.Generic.Enumerator<VRageMath.Vector3I>::MoveNext()
brtrue.s L5
leave.s L6
[EX_BeginFinally]
ldloca.s 5 (System.Collections.Generic.HashSet`1+Enumerator[VRageMath.Vector3I])
constrained. System.Collections.Generic.HashSet`1+Enumerator[VRageMath.Vector3I]
callvirt abstract virtual System.Void System.IDisposable::Dispose()
[EX_EndException]
endfinally
L6:
ldloc.s 4 (System.Collections.Generic.HashSet`1[VRageMath.Vector3I])
callvirt virtual System.Int32 System.Collections.Generic.HashSet`1<VRageMath.Vector3I>::get_Count()
ldc.i4.0
ble.s L7
ldloc.3
callvirt System.Collections.Generic.HashSet`1<VRageMath.Vector3I> Sandbox.Game.GameSystems.MyOxygenRoom::get_Blocks()
ldloc.s 4 (System.Collections.Generic.HashSet`1[VRageMath.Vector3I])
callvirt virtual System.Void System.Collections.Generic.HashSet`1<VRageMath.Vector3I>::ExceptWith(System.Collections.Generic.IEnumerable`1<VRageMath.Vector3I> other)
ldloc.3
ldloc.3
callvirt System.Collections.Generic.HashSet`1<VRageMath.Vector3I> Sandbox.Game.GameSystems.MyOxygenRoom::get_Blocks()
callvirt virtual System.Int32 System.Collections.Generic.HashSet`1<VRageMath.Vector3I>::get_Count()
callvirt System.Void Sandbox.Game.GameSystems.MyOxygenRoom::set_BlockCount(System.Int32 value)
ldloc.3
ldarg.0
ldfld VRageMath.Vector3I Sandbox.Game.GameSystems.MyGridGasSystem::m_storedGridMin
callvirt System.Void Sandbox.Game.GameSystems.MyOxygenRoom::set_StartingPosition(VRageMath.Vector3I value)
L2:
L7:
ldloc.s 4
call static System.Void Shared.Patches.MyGridGasSystemPatch::Return(System.Collections.Generic.HashSet`1<VRageMath.Vector3I> hashSet)
ret
