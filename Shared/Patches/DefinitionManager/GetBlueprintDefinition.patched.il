// 23057342
ldarg.0
call Sandbox.Definitions.DefinitionSet Sandbox.Definitions.MyDefinitionManager::get_m_definitions()
ldfld Sandbox.Definitions.DefinitionDictionary`1<Sandbox.Definitions.MyBlueprintDefinitionBase> Sandbox.Definitions.DefinitionSet::m_blueprintsById
ldarg.1
call VRage.Game.MyDefinitionBase System.Collections.Generic.Dictionary`2<VRage.Game.MyDefinitionId, VRage.Game.MyDefinitionBase>::GetValueOrDefault(VRage.Game.MyDefinitionId key)
ret
