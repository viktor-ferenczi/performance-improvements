// 0a2a4cf1
ldarg.0
call Sandbox.Definitions.DefinitionSet Sandbox.Definitions.MyDefinitionManager::get_m_definitions()
ldfld Sandbox.Definitions.DefinitionDictionary`1<Sandbox.Definitions.MyBlueprintDefinitionBase> Sandbox.Definitions.DefinitionSet::m_blueprintsById
ldarg.1
callvirt virtual System.Boolean System.Collections.Generic.Dictionary`2<VRage.Game.MyDefinitionId, Sandbox.Definitions.MyBlueprintDefinitionBase>::ContainsKey(VRage.Game.MyDefinitionId key)
brtrue.s L0
ldsfld VRage.Utils.MyLog Sandbox.MySandboxGame::Log
ldstr "No blueprint with Id \'{0}\'"
ldarg.1
box VRage.Game.MyDefinitionId
call static System.String System.String::Format(System.String format, System.Object arg0)
callvirt System.Void VRage.Utils.MyLog::WriteLine(System.String msg)
ldnull
ret
L0:
ldarg.0
call Sandbox.Definitions.DefinitionSet Sandbox.Definitions.MyDefinitionManager::get_m_definitions()
ldfld Sandbox.Definitions.DefinitionDictionary`1<Sandbox.Definitions.MyBlueprintDefinitionBase> Sandbox.Definitions.DefinitionSet::m_blueprintsById
ldarg.1
callvirt virtual Sandbox.Definitions.MyBlueprintDefinitionBase System.Collections.Generic.Dictionary`2<VRage.Game.MyDefinitionId, Sandbox.Definitions.MyBlueprintDefinitionBase>::get_Item(VRage.Game.MyDefinitionId key)
ret
