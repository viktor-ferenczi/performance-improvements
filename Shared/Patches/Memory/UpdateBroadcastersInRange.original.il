// 17abb432
ldarg.0
ldfld System.Collections.Generic.HashSet`1<Sandbox.Game.Entities.MyDataBroadcaster> Sandbox.Game.Entities.MyDataReceiver::m_broadcastersInRange
callvirt virtual System.Void System.Collections.Generic.HashSet`1<Sandbox.Game.Entities.MyDataBroadcaster>::Clear()
ldsfld System.Boolean Sandbox.Engine.Utils.MyFakes::ENABLE_RADIO_HUD
brfalse.s L0
ldarg.0
call System.Boolean Sandbox.Game.Entities.MyDataReceiver::get_Enabled()
brtrue.s L1
L0:
ret
L1:
ldarg.0
call VRage.ModAPI.IMyEntity VRage.Game.Components.MyEntityComponentBase::get_Entity()
callvirt abstract virtual VRage.Game.Components.MyEntityComponentContainer VRage.ModAPI.IMyEntity::get_Components()
ldloca.s 0 (Sandbox.Game.Entities.MyDataBroadcaster)
callvirt System.Boolean VRage.Game.Components.MyComponentContainer::TryGet(Sandbox.Game.Entities.MyDataBroadcaster& component)
brfalse.s L2
ldarg.0
ldfld System.Collections.Generic.HashSet`1<Sandbox.Game.Entities.MyDataBroadcaster> Sandbox.Game.Entities.MyDataReceiver::m_broadcastersInRange
ldloc.0
callvirt virtual System.Boolean System.Collections.Generic.HashSet`1<Sandbox.Game.Entities.MyDataBroadcaster>::Add(Sandbox.Game.Entities.MyDataBroadcaster item)
pop
L2:
ldarg.0
ldarg.0
ldflda System.Collections.Generic.HashSet`1<Sandbox.Game.Entities.MyDataBroadcaster> Sandbox.Game.Entities.MyDataReceiver::m_broadcastersInRange
callvirt abstract virtual System.Void Sandbox.Game.Entities.MyDataReceiver::GetBroadcastersInMyRange(System.Collections.Generic.HashSet`1& broadcastersInRange)
newobj System.Void System.Collections.Generic.HashSet`1<System.Int64>::.ctor()
stloc.1
ldarg.0
ldfld System.Collections.Generic.List`1<Sandbox.Game.Entities.MyDataBroadcaster> Sandbox.Game.Entities.MyDataReceiver::m_lastBroadcastersInRange
callvirt System.Collections.Generic.Enumerator<Sandbox.Game.Entities.MyDataBroadcaster> System.Collections.Generic.List`1<Sandbox.Game.Entities.MyDataBroadcaster>::GetEnumerator()
stloc.2
[EX_BeginException]
br.s L3
L5:
ldloca.s 2 (System.Collections.Generic.List`1+Enumerator[Sandbox.Game.Entities.MyDataBroadcaster])
call virtual Sandbox.Game.Entities.MyDataBroadcaster System.Collections.Generic.Enumerator<Sandbox.Game.Entities.MyDataBroadcaster>::get_Current()
stloc.3
ldloc.3
callvirt VRage.ModAPI.IMyEntity VRage.Game.Components.MyEntityComponentBase::get_Entity()
brfalse.s L4
ldloc.1
ldloc.3
callvirt System.Int64 Sandbox.Game.Entities.MyDataBroadcaster::get_AntennaEntityId()
callvirt virtual System.Boolean System.Collections.Generic.HashSet`1<System.Int64>::Add(System.Int64 item)
pop
L3:
L4:
ldloca.s 2 (System.Collections.Generic.List`1+Enumerator[Sandbox.Game.Entities.MyDataBroadcaster])
call virtual System.Boolean System.Collections.Generic.Enumerator<Sandbox.Game.Entities.MyDataBroadcaster>::MoveNext()
brtrue.s L5
leave.s L6
[EX_BeginFinally]
ldloca.s 2 (System.Collections.Generic.List`1+Enumerator[Sandbox.Game.Entities.MyDataBroadcaster])
constrained. System.Collections.Generic.List`1+Enumerator[Sandbox.Game.Entities.MyDataBroadcaster]
callvirt abstract virtual System.Void System.IDisposable::Dispose()
[EX_EndException]
endfinally
L6:
ldarg.0
ldfld System.Collections.Generic.List`1<Sandbox.Game.Entities.MyDataBroadcaster> Sandbox.Game.Entities.MyDataReceiver::m_lastBroadcastersInRange
callvirt virtual System.Int32 System.Collections.Generic.List`1<Sandbox.Game.Entities.MyDataBroadcaster>::get_Count()
ldc.i4.1
sub
stloc.s 4 (System.Int32)
br.s L7
L12:
ldarg.0
ldfld System.Collections.Generic.List`1<Sandbox.Game.Entities.MyDataBroadcaster> Sandbox.Game.Entities.MyDataReceiver::m_lastBroadcastersInRange
ldloc.s 4 (System.Int32)
callvirt virtual Sandbox.Game.Entities.MyDataBroadcaster System.Collections.Generic.List`1<Sandbox.Game.Entities.MyDataBroadcaster>::get_Item(System.Int32 index)
stloc.s 5 (Sandbox.Game.Entities.MyDataBroadcaster)
ldloc.s 5 (Sandbox.Game.Entities.MyDataBroadcaster)
callvirt VRage.ModAPI.IMyEntity VRage.Game.Components.MyEntityComponentBase::get_Entity()
brfalse.s L8
ldloc.1
ldloc.s 5 (Sandbox.Game.Entities.MyDataBroadcaster)
callvirt VRage.ModAPI.IMyEntity VRage.Game.Components.MyEntityComponentBase::get_Entity()
callvirt abstract virtual System.Int64 VRage.ModAPI.IMyEntity::get_EntityId()
callvirt virtual System.Boolean System.Collections.Generic.HashSet`1<System.Int64>::Contains(System.Int64 item)
brtrue.s L9
ldarg.0
ldfld System.Collections.Generic.List`1<Sandbox.Game.Entities.MyDataBroadcaster> Sandbox.Game.Entities.MyDataReceiver::m_lastBroadcastersInRange
ldloc.s 4 (System.Int32)
call static System.Void System.Collections.Generic.ListExtensions::RemoveAtFast(System.Collections.Generic.List`1<Sandbox.Game.Entities.MyDataBroadcaster> list, System.Int32 index)
ldarg.0
ldfld Sandbox.Game.Entities.BroadcasterChangeInfo Sandbox.Game.Entities.MyDataReceiver::OnBroadcasterLost
dup
brtrue.s L10
pop
br.s L11
L10:
ldloc.s 5 (Sandbox.Game.Entities.MyDataBroadcaster)
callvirt virtual System.Void Sandbox.Game.Entities.BroadcasterChangeInfo::Invoke(Sandbox.Game.Entities.MyDataBroadcaster broadcaster)
L8:
L9:
L11:
ldloc.s 4 (System.Int32)
ldc.i4.1
sub
stloc.s 4 (System.Int32)
L7:
ldloc.s 4 (System.Int32)
ldc.i4.0
bge.s L12
ldarg.0
ldfld System.Collections.Generic.HashSet`1<Sandbox.Game.Entities.MyDataBroadcaster> Sandbox.Game.Entities.MyDataReceiver::m_broadcastersInRange
callvirt System.Collections.Generic.Enumerator<Sandbox.Game.Entities.MyDataBroadcaster> System.Collections.Generic.HashSet`1<Sandbox.Game.Entities.MyDataBroadcaster>::GetEnumerator()
stloc.s 6 (System.Collections.Generic.HashSet`1+Enumerator[Sandbox.Game.Entities.MyDataBroadcaster])
[EX_BeginException]
br.s L13
L18:
ldloca.s 6 (System.Collections.Generic.HashSet`1+Enumerator[Sandbox.Game.Entities.MyDataBroadcaster])
call virtual Sandbox.Game.Entities.MyDataBroadcaster System.Collections.Generic.Enumerator<Sandbox.Game.Entities.MyDataBroadcaster>::get_Current()
stloc.s 7 (Sandbox.Game.Entities.MyDataBroadcaster)
ldloc.s 7 (Sandbox.Game.Entities.MyDataBroadcaster)
callvirt VRage.ModAPI.IMyEntity VRage.Game.Components.MyEntityComponentBase::get_Entity()
brfalse.s L14
ldloc.1
ldloc.s 7 (Sandbox.Game.Entities.MyDataBroadcaster)
callvirt System.Int64 Sandbox.Game.Entities.MyDataBroadcaster::get_AntennaEntityId()
callvirt virtual System.Boolean System.Collections.Generic.HashSet`1<System.Int64>::Contains(System.Int64 item)
brtrue.s L15
ldarg.0
ldfld System.Collections.Generic.List`1<Sandbox.Game.Entities.MyDataBroadcaster> Sandbox.Game.Entities.MyDataReceiver::m_lastBroadcastersInRange
ldloc.s 7 (Sandbox.Game.Entities.MyDataBroadcaster)
callvirt virtual System.Void System.Collections.Generic.List`1<Sandbox.Game.Entities.MyDataBroadcaster>::Add(Sandbox.Game.Entities.MyDataBroadcaster item)
ldarg.0
ldfld Sandbox.Game.Entities.BroadcasterChangeInfo Sandbox.Game.Entities.MyDataReceiver::OnBroadcasterFound
dup
brtrue.s L16
pop
br.s L17
L16:
ldloc.s 7 (Sandbox.Game.Entities.MyDataBroadcaster)
callvirt virtual System.Void Sandbox.Game.Entities.BroadcasterChangeInfo::Invoke(Sandbox.Game.Entities.MyDataBroadcaster broadcaster)
L13:
L14:
L15:
L17:
ldloca.s 6 (System.Collections.Generic.HashSet`1+Enumerator[Sandbox.Game.Entities.MyDataBroadcaster])
call virtual System.Boolean System.Collections.Generic.Enumerator<Sandbox.Game.Entities.MyDataBroadcaster>::MoveNext()
brtrue.s L18
leave.s L19
[EX_BeginFinally]
ldloca.s 6 (System.Collections.Generic.HashSet`1+Enumerator[Sandbox.Game.Entities.MyDataBroadcaster])
constrained. System.Collections.Generic.HashSet`1+Enumerator[Sandbox.Game.Entities.MyDataBroadcaster]
callvirt abstract virtual System.Void System.IDisposable::Dispose()
[EX_EndException]
endfinally
L19:
ret
