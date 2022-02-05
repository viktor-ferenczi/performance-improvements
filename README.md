# Space Engineers Performance Improvements Plugin

## Features (performance fixes)

- `MySpinWait` optimization (lower CPU consumption during heavy load)
- Suppressing useless updates during grid merge and paste (about twice as fast for grids with lots of conveyors)
- Reducing CPU load of network statistics updates (saves ~50% constant load on a CPU core)

Please see below for the technical details.

More optimizations are planned.

## Prerequisites

- Space Engineers with [Plugin Loader](https://steamcommunity.com/sharedfiles/filedetails/?id=2407984968) installed or
- [Torch Server](https://torchapi.net/) or
- Dedicated Server

## Installation

### Client plugin

1. Install [Plugin Loader](https://steamcommunity.com/sharedfiles/filedetails/?id=2407984968) into Space Engineers (add launch option)
2. Enable the **Performance Improvements** plugin from the **Plugins** dialog
3. Apply, restart the game

### Torch Server plugin

Select the **Performance Improvements** plugin from the plugin list inside the Torch GUI.

### Dedicated Server plugin

1. Download the latest ZIP from [Releases](https://github.com/viktor-ferenczi/performance-improvements/releases).
2. Open the `Bin64` folder of your Dedicated Server installation.
3. Create a `Plugins` folder if it does not exists.
4. Extract the ZIP file into the `Bin64/Plugins` folder.
5. Right click on all the DLLs extracted and select **Unblock** from the file's **Properties** dialog.
6. Start the Dedicated Server.
7. Add the `PerformanceImprovements.dll` from the `Bin64/Plugins` folder to the Plugins list.

## Technical details of the optimizations

### SPINWAIT

Replaces `MySpinWait.SpinOnce` with more energy efficient code, which consumes less CPU time
while waiting on locks to be released. While it reduces CPU consumption (and some cache misses),
it does not make anything completing any faster (no measurable difference).

### USELESS_UPDATES

Disables the `MyConveyorLine.UpdateIsWorking` method while any grid merging operation is in
progress. It considerably reduces the merge time of grids with long conveyor systems. At the
end of `MyCubeGrid.MergeGridInternal` it calls `GridSystems.ConveyorSystem.FlagForRecomputation()`
on the grid to force recalculating all `IsWorking` values to fix any side-effects of the
optimization.

It also sets `MySession.Static.m_updateAllowed` to `false` while `MyCubeGrid.PasteBlocksServer`
is running. It eliminates the useless computations until the paste is done, making it faster.

These two fixes combined make grid merge and paste operations ~60-70% faster.

### NETWORK_STATISTICS

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