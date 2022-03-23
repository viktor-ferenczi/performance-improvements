// fc347f5d
ldarg.0 NULL
call Sandbox.Engine.Physics.MyWeldInfo Sandbox.Engine.Physics.MyPhysicsBody::get_WeldInfo()
ldfld Sandbox.Engine.Physics.MyPhysicsBody Sandbox.Engine.Physics.MyWeldInfo::Parent
brtrue.s Label0
ldarg.0 NULL
ldfld Havok.HkRigidBody Sandbox.Engine.Physics.MyPhysicsBody::m_rigidBody
ret NULL
ldarg.0 NULL [Label0]
call Sandbox.Engine.Physics.MyWeldInfo Sandbox.Engine.Physics.MyPhysicsBody::get_WeldInfo()
ldfld Sandbox.Engine.Physics.MyPhysicsBody Sandbox.Engine.Physics.MyWeldInfo::Parent
callvirt abstract virtual Havok.HkRigidBody VRage.Game.Components.MyPhysicsComponentBase::get_RigidBody()
ret NULL
