# Space Engineers Performance Improvements Plugin

**Scroll down for:**
- List of features
- Their technical details
- Keen bug tickets to vote on

More performance fixes and optimizations are planned.

## Prerequisites
- [Space Engineers](https://store.steampowered.com/app/244850/Space_Engineers/) with [Plugin Loader](https://steamcommunity.com/sharedfiles/filedetails/?id=2407984968) or
- [Torch Server](https://torchapi.net/) or
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

### Developers
- Avaness for the client side Plugin Loader
- Bishbash77 for keeping Torch alive + Torch contributors
- Z__ (zznty)
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

Contributed by: `Z__` (zznty)

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

It may be a performance hog if many mods are used. This fix disables the
`VRage.Scripting.Rewriters.PerfCountingRewriter.Rewrite` method, so the
API calls are not rewritten, removing the overhead.

I hope it will not be a problem for Keen, as long as only a small minority
of players are using plugins. It is not a bug, it is a feature. Therefore
no support ticket is needed.

Measured 10% lower simulation CPU load in a heavily modded test world after
loading it with this fix enabled.

TODO: Profiling on big multiplayer server worlds.

### Thruster grid updates (server and offline game)

Initial solution was contributed by: `mkaito`

When changing thrust values, such as adjusting override or just pressing WASD,
a lot of time is spent recalculating power and fuel state of all thrusters on
the grid every tick.

This fix allows the recalculation to happen only if there is a relevant change
in the control thrust or when the control thrust is reset back to zero.

The fix has been verified both in space and gravity. It was tested that this fix
does not affect the dampener function.

Not tested yet:
- Autopilot
- NPC ships

Please vote on the [support ticket](https://support.keenswh.com/spaceengineers/pc/topic/22874-grids-with-hydrogen-thrusters-decrease-simulation-speed-after-warfare-2-update-but-not-before)

### Lag on grid group changes (server and offline game)

There is some serious lag on connector lock/unlock and rotor head attach/detach 
due to grid group changes causing massive main thread workload, which could 
easily be deferred to worker threads with minimal consequences.

This fix disables resource updates while grids are being moved between groups
and marks those resources for updating by a worker thread later.

Please vote on the [support ticket](https://support.keenswh.com/spaceengineers/pc/topic/23278-lag-on-connector-lockunlock-and-rotor-head-attachdetach-due-to-grid-group-changes)