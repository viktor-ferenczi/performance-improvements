// 60f67776
ldarg.0
call virtual VRage.Game.Components.MyRenderComponentBase VRage.Game.Entity.MyEntity::get_Render()
brfalse.s L0
ldarg.0
call virtual VRage.Game.Components.MyRenderComponentBase VRage.Game.Entity.MyEntity::get_Render()
callvirt VRage.ObjectBuilders.MyPersistentEntityFlags2 VRage.Game.Components.MyRenderComponentBase::get_PersistentFlags()
ldc.i4.s 16
and
ldc.i4.0
cgt
ret
L0:
ldc.i4.0
ret
