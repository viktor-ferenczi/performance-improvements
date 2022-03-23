// 60f67776
ldarg.0 NULL
call virtual VRage.Game.Components.MyRenderComponentBase VRage.Game.Entity.MyEntity::get_Render()
brfalse.s Label0
ldarg.0 NULL
call virtual VRage.Game.Components.MyRenderComponentBase VRage.Game.Entity.MyEntity::get_Render()
callvirt VRage.ObjectBuilders.MyPersistentEntityFlags2 VRage.Game.Components.MyRenderComponentBase::get_PersistentFlags()
ldc.i4.s 16
and NULL
ldc.i4.0 NULL
cgt NULL
ret NULL
ldc.i4.0 NULL [Label0]
ret NULL
