# Space Engineers Performance Improvements Plugin

## Features (performance fixes)

- Conveyor system
- Pasting grids over another grids (merge)

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