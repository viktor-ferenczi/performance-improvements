### Patch: virtual Havok.HkRigidBody Sandbox.Engine.Physics.MyPhysicsBody::get_RigidBody()
### Replacement: static Havok.HkRigidBody Sandbox.Engine.Physics.MyPhysicsBody::Sandbox.Engine.Physics.MyPhysicsBody.get_RigidBody_Patch0(Sandbox.Engine.Physics.MyPhysicsBody this)
// start original
ldarg.0
call       Sandbox.Engine.Physics.MyWeldInfo Sandbox.Engine.Physics.MyPhysicsBody::get_WeldInfo()
ldfld      Sandbox.Engine.Physics.MyPhysicsBody Sandbox.Engine.Physics.MyWeldInfo::Parent
brtrue =>  Label0
ldarg.0
ldfld      Havok.HkRigidBody Sandbox.Engine.Physics.MyPhysicsBody::m_rigidBody
br =>      Label1
Label0
ldarg.0
call       Sandbox.Engine.Physics.MyWeldInfo Sandbox.Engine.Physics.MyPhysicsBody::get_WeldInfo()
ldfld      Sandbox.Engine.Physics.MyPhysicsBody Sandbox.Engine.Physics.MyWeldInfo::Parent
callvirt   abstract virtual Havok.HkRigidBody VRage.Game.Components.MyPhysicsComponentBase::get_RigidBody()
// end original
Label1
ret
DONE