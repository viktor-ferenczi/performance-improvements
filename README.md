# Space Engineers Performance Improvements Plugin

**Scroll down for:**
- List of features
- Technical details
- Keen bug reports to vote on

For support please [join the SE Mods Discord](https://discord.gg/PYPFPGf3Ca).

Please consider supporting my work on [Patreon](https://www.patreon.com/semods) or one time via [PayPal](https://www.paypal.com/paypalme/vferenczi/).

*Thank you and enjoy!*

## Available for

- [Space Engineers](https://store.steampowered.com/app/244850/Space_Engineers/) with [Plugin Loader](https://steamcommunity.com/sharedfiles/filedetails/?id=2407984968)
- [Torch Server](https://torchapi.net/)
- [Dedicated Server](https://www.spaceengineersgame.com/dedicated-servers/)

## Want to know more?

- [SE Mods Discord](https://discord.gg/PYPFPGf3Ca) FAQ, Troubleshooting, Support, Bug Reports, Discussion
- [Plugin Loader Discord](https://discord.gg/6ETGRU3CzR) Everything about plugins

## Installation

### Client plugin
1. Install [Plugin Loader](https://steamcommunity.com/sharedfiles/filedetails/?id=2407984968) into Space Engineers (add launch option)
2. Enable the **Performance Improvements** plugin from the **Plugins** dialog
3. Apply, restart the game

### Torch Server plugin

Select the **Performance Improvements** plugin from the plugin list inside the Torch GUI.

Please note, that the plugin is using Harmony to patch the game code. Once Keen fixes the issues 
these patches are expected to be removed anyway, so I did not bother using Torch's patching mechanism.

### Dedicated Server plugin
1. Download the latest ZIP from [Releases](https://github.com/viktor-ferenczi/performance-improvements/releases).
2. Open the `Bin64` folder of your Dedicated Server installation.
3. Create a `Plugins` folder if it does not exists.
4. Extract the ZIP file into the `Bin64/Plugins` folder.
5. Right click on all the DLLs extracted and select **Unblock** from the file's **Properties** dialog.
6. Start the Dedicated Server.
7. Add the `PerformanceImprovements.dll` from the `Bin64/Plugins` folder to the Plugins list.

## Credits

*In alphabetical order*

### Patreon

#### Admiral level supporters
- BetaMark
- Bishbash777
- Casinost
- Dorimanx
- wafoxxx

#### Captain level supporters
- DontFollowOrders
- Gabor
- Lazul
- Lotan
- mkaito
- ransomthetoaster

### Developers
- Avaness: client side Plugin Loader
- Bishbash77: testing and keeping Torch alive
- mkaito: testing and design discussions
- zznty: contributed patches

### Testers
- CaveBadgerMan: SG Dimensions, Torch servers
- Dorimanx: contributed patches
- mkaito: testing with his heavy offline world
- Robot10: client side
- Multiple server admins for testing and feedback
- zznty

## Technical details

### Conveyor updates while merging grids (server and offline game)

Disables the `MyConveyorLine.UpdateIsWorking` method while any grid merging operation is in
progress. It considerably reduces the merge time of grids with long conveyor systems. At the
end of `MyCubeGrid.MergeGridInternal` it calls `GridSystems.ConveyorSystem.FlagForRecomputation()`
on the grid to force recalculating all `IsWorking` values to fix any side-effects of the
optimization.

### Update while pasting grids (server and offline game)

Disables updates while pasting grids by setting `MySession.Static.m_updateAllowed` to 
`false` while `MyCubeGrid.PasteBlocksServer` is running. It eliminates a lot of 
unnecessary computations until the paste is done.

This one and the previous fix combined make grid merge and paste operations ~60-70% faster 
in my test world, at least for grids with lots of blocks and conveyor ports. It adds up
on multiplayer servers, especially if NPC are pasted automatically.

Please vote on the [support ticket](https://support.keenswh.com/spaceengineers/pc/topic/22823-performance-unnecessary-updates-during-grid-merge-and-paste-operations)

### EOS P2P UpdateStats (client only)

Eliminates 98% of the ~50% constant CPU core load imposed by the
`VRage.EOS.MyP2PQoSAdapter.UpdateStats` method, even during **offline** games.
It is done by replacing 49 out of 50 calls with a `Thread.Sleep(1)`.
It limits the outer loop's frequency to less than 1000/s and spends
less CPU power on gathering statistics.

It makes the game faster only if you have 4 or less CPU cores, since this
method is called repeatedly in a loop on its own thread. It still helps to
reduce CPU power consumption and cache misses if you have more than 4 cores.

Whether it affects the stability of multiplayer networking or any other EOS
related functionality is yet to be seen.

Please vote on the [support ticket](https://support.keenswh.com/spaceengineers/pc/topic/22802-performance-constant-50-core-load-by-vrage-eos-myp2pqosadapter-updatestats)

### GC.Collect calls (both client and server)

Contributed by: zznty

The game makes explicit calls to `GC.Collect`, which may cause long pauses 
while starting or stopping large worlds. It mostly affects large multiplayer 
servers where worlds are big, but it can shave off a few hundred milliseconds
of world load (and close) time in case of loading offline games as well.

There are also calls elsewhere, for example in `MyPlanetTextureMapProvider` and
`MySimpleProfiler.LogPerformanceTestResults`, which may be invoked during gameplay.
The patched calls are now logged at the DEBUG log level, so we can see them and
measure how much we save by eliminating them. 

Parallel GC should happen later and free up memory anyway. No explicit garbage
collection calls should be needed anymore.

Consider disabling this setting if your PC or server does not have at least 8GB RAM.

TODO: Add a Keen support ticket after gathering profiling data. 

### Mod API call statistics overhead (both client and server)

Contributed by: zznty

It may be a performance hog if many mods are used. This fix disables the
`VRage.Scripting.Rewriters.PerfCountingRewriter.Rewrite` method, so the
API calls are not rewritten, removing the overhead.

I hope it will not be a problem for Keen, as long as only a small minority
of players are using plugins. It is not a bug, it is a feature. Therefore
no support ticket is needed.

Measured 10% lower simulation CPU load in a heavily modded test world after
loading it with this fix enabled.

### Lag on grid group changes (server and offline game)

There is some serious lag on connector lock/unlock and rotor head attach/detach 
due to grid group changes causing massive main thread workload, which could 
easily be deferred to worker threads with minimal consequences.

This fix disables resource updates while grids are being moved between groups
and marks those resources for updating by a worker thread later.

Please vote on the [support ticket](https://support.keenswh.com/spaceengineers/pc/topic/23278-lag-on-connector-lockunlock-and-rotor-head-attachdetach-due-to-grid-group-changes)

### Caching compiled mods and in-game scripts

Compiling all mods and PB scripts on world load is very time consuming and CPU intensive.
It takes a lot of time to load a world which uses many mods and/or in-game scripts. 
It mainly affects large multiplayer servers, but I have also seen 
advanced single player worlds affected by slow world loading.

Please vote on the [support ticket](https://support.keenswh.com/spaceengineers/pc/topic/23906-performance-cache-compiled-mods-and-in-game-scripts)

### MySafeZone caching and optimizations

#### Caching the result of MySafeZone.IsSafe

`MySafeZone.IsSafe` is called very frequently for entities inside safe zones. 
This is quite a bit of overhead in multiplayer worlds with many small grids and
safe zones in it, like the Alehouse Rover PvP one. 

Workaround is to cache the result of MySafeZone.IsSafe for up to 128 simulation 
ticks (~2 seconds). Side effect of the fix is that grid ownership changes are 
reflected in safe zone behavior only up to 2 seconds later (1 second on average).

Please vote on the [support ticket](https://support.keenswh.com/spaceengineers/pc/topic/24146-performance-mysafezone-issafe-is-called-frequently-but-not-cached)

**1.203.022**: While Keen reduced the GC pressure (memory allocations) by using a 
`ReuseCollection`, it is not enough. The main issue is repeating this 
expensive check frequently without caching it. Therefore the caching
implemented by this plugin is still required.

#### Optimized MySafeZone.IsOutside

`MySafeZone.IsOutside()` is implemented in a convoluted way. Replaced it with
an optimized implementation which does not instantiate any new bounding boxes.

Replaced only the `MySafeZone.IsOutside(BoundingBoxD aabb)` override, because
it caused issues with many grids around safe zones. The time spent in the
other two overrides were not significant, either they are used less or they
could be fully optimized by the JIT compiler eliminating the bounding box
objects.

#### Caching the result of MySafeZone.IsActionAllowed

Due to the high call counts of busy servers this method benefits from caching.
The results is cached for 2 seconds, therefore the effect of changes in
Safe Zone  configuration or grid safe-zone containment is delayed by up to
2 seconds, which is acceptable considering the overall performance benefits.

### Reducing frequent memory allocations

Game update 1.202.066 (Automaton) attempted to fix [the slowness](https://support.keenswh.com/spaceengineers/pc/topic/24210-performance-pre-calculate-or-cache-mydefinitionid-tostring-results),
but introduced a [deadlock](https://support.keenswh.com/spaceengineers/pc/topic/27997-servers-deadlocked-on-load) as a result.

So the fix to `MyDefinitionId.ToString` has been put back into this plugin.

### Reducing memory allocations in the turret targeting system

There are large memory allocations in some frequently called routines, 
causing quite a bit of GC pressure:
- `MyLargeTurretTargetingSystem.SortTargetRoots`
- `MyLargeTurretTargetingSystem.UpdateVisibilityCacheCounters` (this fix was disabled since 1.10.4 due to reported crashes)

Please vote on the [support ticket](https://support.keenswh.com/spaceengineers/pc/topic/24145-excessive-memory-allocation-in-mylargeturrettargetingsystem)

### Caching the result of wind turbine atmosphere checks

Since the result of `MyWindTurbine.IsInAtmosphere` does not change often, 
it can safely be cached for a few seconds.

Please vote on the [support ticket](https://support.keenswh.com/spaceengineers/pc/topic/24209-performance-cache-the-result-of-mywindturbine-isinatmosphere)

### Less frequent sync of block counts for limit checking

Suppresses frequent calls to MyPlayerCollection.SendDirtyBlockLimits.

Please vote on the [support ticket](https://support.keenswh.com/spaceengineers/pc/topic/24390-performance-myplayercollection-senddirtyblocklimits-is-called-too-frequently)

### Cache actions allowed by the safe zone

Caches the result of MySafeZone.IsActionAllowed and MySessionComponentSafeZones.IsActionAllowedForSafezone for 2 seconds.

Please vote on the [support ticket](https://support.keenswh.com/spaceengineers/pc/topic/24391-performance-safe-zone-isactionallowed)

### Less frequent update of PB access to blocks

Suppresses frequent calls to MyGridTerminalSystem.UpdateGridBlocksOwnership updating IsAccessibleForProgrammableBlock unnecessarily often.

Please vote on the [support ticket](https://support.keenswh.com/spaceengineers/pc/topic/24389-performance-frequent-update-of-pb-access-rights-to-blocks)

### Less frequent update of block access rights

NOTE: This fix has been disabled in 1.10.1 due to a bug, which has been fixed since. It may be re-enabled once tested carefully.

Caches the result of MyCubeBlock.GetUserRelationToOwner and MyTerminalBlock.HasPlayerAccessReason.

### Fixed Havok thread count in MyPhysics

Keen introduced `MyVRage.Platform.System.OptimalHavokThreadCount`, but it is set to `null`.

The new logic in `MyPhysics.LoadData` falls back to the call it made before: `HkJobThreadPool()`

But inside Havok (C++ code) they apparently changed it to default to a single thread
in this case, so all the physics is running on a single thread (main thread).

### Optimized MyClusterTree.ReorderClusters

Replaced an O(N*M) algorithm with one of better time complexity.

Improves the load time of servers with many grids.
Potentially reduce lag as ships move around.

### Cached MyGridConveyorSystem.Reachable

Cached the result of `Reachable` calls, because they are very numerous
in case of grids with long conveyor networks (capital ships, production bases). 

There is a separate cache per logical grid group.

Cache invalidation conditions:
- block added/removed to/from grid if the block has conveyor ports
- grid split/merge
- grid ownership change
- connector lock/unlock or config change
- grid added/remove to/from logical group

It eliminates most of the lag when players enter/leave cockpits or cryopods.
It also reduces the conveyor overhead while loading large production grids.

It may have a slight impact on simple grids with short conveyor systems
due to the additional overhead of building and using the cache for little
benefit in that case, however this overhead should be negligible.

## Bugs fixed by Keen in 1.202.066 Automaton

### SafeZone m_RemoveEntityPhantomTaskList HashSet corruption due to race condition

Fixed the [HashSet corruption](https://support.keenswh.com/spaceengineers/pc/topic/24149-safezone-m_removeentityphantomtasklist-hashset-corruption-due-to-race-condition) by using a `MyConcurrentHashSet`.

### Redundant evaluation in MyEntity.InScene getter

Fixed the [slow InScene getter](https://support.keenswh.com/spaceengineers/pc/topic/23462-myentity-inscene-is-responsible-for-4-of-main-thread-cpu-load-on-a-large-server) by implementing the suggested fix.

### Havok performance fix

Fixed the [slow implementation](https://support.keenswh.com/spaceengineers/pc/topic/24211-performance-hkshape-comparison-with-boxing-allocation) by implementing the suggested fix.

### Reduced memory allocation in broadcaster scanning

Keen significantly changed `MyDataReceiver.UpdateBroadcastersInRange`, 
it is using a single `MyUtils.ReuseCollection`. I consider this fixed,
but performance testing will be needed to confirm.

Original [support ticket](https://support.keenswh.com/spaceengineers/pc/topic/24388-performance-excess-memory-allocation-in-mydatareceiver-updatebroadcastersinrange)

### Rate limited excessive logging 

Rate limited excessive logging from `MyDefinitionManager.GetBlueprintDefinition`.

For example it caused 11000 of the "No blueprint with Id" messages logged every minute
while players were running Isy's Inventory Manager PB script. In addition to the extra
CPU load it risked running out of disk space if left unchecked.

### Disabled functional blocks in projected grids

Projected functional blocks are updated, which is a waste of time. Also due to bugs
some of them can even function, for example projected welders can weld in creative
mode if they are enabled in the blueprint.

In order to fixe these functional blocks have to be disabled on grids with no physics.
These should only be the projected functional blocks. It happens only once when the
functional block is added to the scene in order to avoid a constant CPU overhead.

Testing `functionalBlock.CubeGrid?.Projector` instead would be more correct,
but it is not set when the projected block is added to the scene. 
Setting NeedsUpdate to NONE does not work either. Solving it differently
would have more overhead.

This fix may have side-effects should a plugin provide physics-less subgrids.
In such a case disable this fix and use the Multigrid Projector plugin
to fix this specific case only for the welders in a different way.

This fix has the visual side-effect of all functional blocks showing up as disabled
in the projection, so the players don't know in advance whether they will be enabled
once welded. The fix does not affect the welded state, only the visual feedback.
This applies only of this plugin is installed on the client side.

## Bugs fixed by Keen in 1.202.023

### Slowness of Physics clusters

Keen reverted the default value of `MyVoxelPhysicsBody.m_staticForCluster` 
to true in the 1.203.023 hotfix update.

## Remarks

Fixes for performance issues or bugs fixed by Keen in the regular public game version
are removed from this page. Check the older versions of this document if you want to
recall them.
