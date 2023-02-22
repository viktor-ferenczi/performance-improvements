// aae6096a
ldarg.0
call virtual VRage.Game.Components.MyRenderComponentBase VRage.Game.Entity.MyEntity::get_Render()
stloc.0
ldloc.0
dup
brfalse.s L0
ldc.i4.s 16
and
ldc.i4.0
cgt
ret
L0:
pop
ldc.i4.0
ret
