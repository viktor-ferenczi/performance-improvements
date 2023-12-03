// 3ee15483
ldarg.0
call Sandbox.Definitions.DefinitionSet Sandbox.Definitions.MyDefinitionManager::get_m_definitions()
ldfld Sandbox.Definitions.DefinitionDictionary`1<Sandbox.Definitions.MyBlueprintDefinitionBase> Sandbox.Definitions.DefinitionSet::m_blueprintsById
ldarg.1
callvirt virtual System.Boolean System.Collections.Generic.Dictionary`2<VRage.Game.MyDefinitionId, Sandbox.Definitions.MyBlueprintDefinitionBase>::ContainsKey(VRage.Game.MyDefinitionId key)
brtrue.s L0
ldarg.1
call static System.Void Shared.Patches.MyDefinitionManagerPatch::LogWithRateLimit(VRage.Game.MyDefinitionId blueprintId)
ldnull
ret
L0:
ldarg.0
call Sandbox.Definitions.DefinitionSet Sandbox.Definitions.MyDefinitionManager::get_m_definitions()
ldfld Sandbox.Definitions.DefinitionDictionary`1<Sandbox.Definitions.MyBlueprintDefinitionBase> Sandbox.Definitions.DefinitionSet::m_blueprintsById
ldarg.1
callvirt virtual Sandbox.Definitions.MyBlueprintDefinitionBase System.Collections.Generic.Dictionary`2<VRage.Game.MyDefinitionId, Sandbox.Definitions.MyBlueprintDefinitionBase>::get_Item(VRage.Game.MyDefinitionId key)
ret
