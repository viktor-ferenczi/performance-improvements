// 3dcabfed
ldarg.0
call Sandbox.Engine.Physics.MyWeldInfo Sandbox.Engine.Physics.MyPhysicsBody::get_WeldInfo()
ldfld Sandbox.Engine.Physics.MyPhysicsBody Sandbox.Engine.Physics.MyWeldInfo::Parent
dup
brtrue.s L0
pop
ldarg.0
ldfld Havok.HkRigidBody Sandbox.Engine.Physics.MyPhysicsBody::m_rigidBody
ret
L0:
callvirt abstract virtual Havok.HkRigidBody VRage.Game.Components.MyPhysicsComponentBase::get_RigidBody()
ret
