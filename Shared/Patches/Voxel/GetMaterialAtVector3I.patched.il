// d214d704
newobj System.Void VRage.Voxels.MyStorageData::.ctor()
stloc.0 NULL
ldloc.0 NULL
ldsfld VRageMath.Vector3I VRageMath.Vector3I::One
callvirt System.Void VRage.Voxels.MyStorageData::Resize(VRageMath.Vector3I size3D)
ldarg.0 NULL
ldloc.0 NULL
ldc.i4.3 NULL
ldc.i4.0 NULL
ldarg.1 NULL
ldobj VRageMath.Vector3I
ldarg.1 NULL
ldobj VRageMath.Vector3I
callvirt abstract virtual System.Void VRage.ModAPI.IMyStorage::ReadRange(VRage.Voxels.MyStorageData target, VRage.Voxels.MyStorageDataTypeFlags dataToRead, System.Int32 lodIndex, VRageMath.Vector3I lodVoxelRangeMin, VRageMath.Vector3I lodVoxelRangeMax)
ldloc.0 NULL
ldc.i4.0 NULL
callvirt System.Byte VRage.Voxels.MyStorageData::Material(System.Int32 linearIdx)
stloc.1 NULL
ldloc.1 NULL
ldc.i4 255
bne.un.s Label0
ldnull NULL
ret NULL
call static Sandbox.Definitions.MyDefinitionManager Sandbox.Definitions.MyDefinitionManager::get_Static() [Label0]
ldloc.1 NULL
callvirt VRage.Game.MyVoxelMaterialDefinition Sandbox.Definitions.MyDefinitionManager::GetVoxelMaterialDefinition(System.Byte materialIndex)
ret NULL
