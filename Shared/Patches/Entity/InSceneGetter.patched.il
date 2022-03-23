// 861ca619
ldarg.0 NULL
call virtual VRage.Game.Components.MyRenderComponentBase VRage.Game.Entity.MyEntity::get_Render()
dup NULL
brfalse.s Label0
callvirt VRage.ObjectBuilders.MyPersistentEntityFlags2 VRage.Game.Components.MyRenderComponentBase::get_PersistentFlags()
ldc.i4.s 16
and NULL
ldc.i4.0 NULL
cgt NULL
ret NULL
pop NULL [Label0]
ldc.i4.0 NULL
ret NULL
