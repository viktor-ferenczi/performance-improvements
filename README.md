# Space Engineers Performance Improvements Plugin

**Scroll down for:**
- List of features
- Technical details
- Keen bug reports to vote on

For support please [join the SE Mods Discord](https://discord.gg/PYPFPGf3Ca).

Please consider [supporting my work on Patreon](https://www.patreon.com/semods).

Thank you!

## Available for
- [Space Engineers](https://store.steampowered.com/app/244850/Space_Engineers/) with [Plugin Loader](https://steamcommunity.com/sharedfiles/filedetails/?id=2407984968)
- [Torch Server](https://torchapi.net/)
- [Dedicated Server](https://www.spaceengineersgame.com/dedicated-servers/)

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

### Patreon

#### Admiral level supporters
- BetaMark
- Casinost
- wafoxxx

#### Captain level supporters
- Lotan
- ransomthetoaster
- Lazul
- mkaito
- DontFollowOrders
- Gabor

### Developers
- Avaness for the client side Plugin Loader
- Bishbash77 for keeping Torch alive + Torch contributors
- zznty
- mkaito

### Testers
- CaveBadgerMan (SG Dimensions, Torch servers)
- Robot10 (client side)
- mkaito (testing with his heavy offline world)
- Multiple server admins for discussion and feedback 

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

### Caching the result of MySafeZone.IsSafe

`MySafeZone.IsSafe` is called very frequently for entities inside safe zones. 
This is quite a bit of overhead in multiplayer worlds with many small grids and
safe zones in it, like the Alehouse Rover PvP one. 

Workaround is to cache the result of MySafeZone.IsSafe for up to 128 simulation 
ticks (~2 seconds). Side effect of the fix is that grid ownership changes are 
reflected in safe zone behavior only up to 2 seconds later (1 second on average).

Please vote on the [support ticket](https://support.keenswh.com/spaceengineers/pc/topic/24146-performance-mysafezone-issafe-is-called-frequently-but-not-cached)

Also fixes a related [race condition bug](https://support.keenswh.com/spaceengineers/pc/topic/24149-safezone-m_removeentityphantomtasklist-hashset-corruption-due-to-race-condition)

### Reducing memory allocations in the turret targeting system

There are large memory allocations in some frequently called routines, 
causing quite a bit of GC pressure:
- `MyLargeTurretTargetingSystem.SortTargetRoots`
- `MyLargeTurretTargetingSystem.UpdateVisibilityCacheCounters`

Please vote on the [support ticket](https://support.keenswh.com/spaceengineers/pc/topic/24145-excessive-memory-allocation-in-mylargeturrettargetingsystem)

### Caching the result of wind turbine atmosphere checks

Since the result of `MyWindTurbine.IsInAtmosphere` does not change often, 
it can safely be cached for a few seconds.

Please vote on the [support ticket](https://support.keenswh.com/spaceengineers/pc/topic/24209-performance-cache-the-result-of-mywindturbine-isinatmosphere)

### Reducing frequent memory allocations

`MyDefinitionId.ToString` is called frequently, it also allocates memory. 
There are only 1000-1500 distinct definition IDs to format (depending on mods),
so these are cacheable without expiration.

StringBuilder pooling was contributed by: zznty

Please vote on the [support ticket](https://support.keenswh.com/spaceengineers/pc/topic/24210-performance-pre-calculate-or-cache-mydefinitionid-tostring-results)

### Havok performance fix

Removed boxing allocation from the Havok.HkShape.HandleEqualityComparer.Equals method.
Also simplified the logic by not checking y for null, because it does not happen.

Please vote on the [support ticket](https://support.keenswh.com/spaceengineers/pc/topic/24211-performance-hkshape-comparison-with-boxing-allocation)

### Reduced memory allocation in broadcaster scanning

Reduces memory allocations in MyDataReceiver.UpdateBroadcastersInRange (needs restart).

Please vote on the [support ticket](https://support.keenswh.com/spaceengineers/pc/topic/24388-performance-excess-memory-allocation-in-mydatareceiver-updatebroadcastersinrange)

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

TBD: Bug ticket

## Bugs fixed by Keen

Fixes for these bugs and performance issues have been removed from the plugin:
* [Crash: NullReferenceException in OnEndShoot on client side on grinding an active (shooting) turret](https://support.keenswh.com/spaceengineers/pc/topic/24387-crash-nullreferenceexception-in-onendshoot-on-client-side-on-grinding-an-active-shooting-turret)
