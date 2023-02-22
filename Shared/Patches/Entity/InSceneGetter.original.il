// 097917ea
ldarg.0
call virtual VRage.Game.Components.MyRenderComponentBase VRage.Game.Entity.MyEntity::get_Render()
stloc.0
ldloc.0
brfalse.s L0
ldloc.0
callvirt VRage.ObjectBuilders.MyPersistentEntityFlags2 VRage.Game.Components.MyRenderComponentBase::get_PersistentFlags()
ldc.i4.s 16
and
ldc.i4.0
cgt
ret
L0:
ldc.i4.0
ret
