// 3dcabfed
ldarg.0 NULL
call Sandbox.Engine.Physics.MyWeldInfo Sandbox.Engine.Physics.MyPhysicsBody::get_WeldInfo()
ldfld Sandbox.Engine.Physics.MyPhysicsBody Sandbox.Engine.Physics.MyWeldInfo::Parent
dup NULL
brtrue.s Label0
pop NULL
ldarg.0 NULL
ldfld Havok.HkRigidBody Sandbox.Engine.Physics.MyPhysicsBody::m_rigidBody
ret NULL
callvirt abstract virtual Havok.HkRigidBody VRage.Game.Components.MyPhysicsComponentBase::get_RigidBody() [Label0]
ret NULL
