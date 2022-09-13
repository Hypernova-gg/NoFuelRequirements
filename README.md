<h1 align="center"> No Fuel Requirements <sup>v1.0.0.0</sup> </h1> <br>

<p align="center">
  Making it so furnaces and such ignore the fuel requirement to function. Built with Harmony for RUST.
</p>

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->
## Table of Contents

- [Configuration](#configuration)
- [Commands](#commands)
- [Feedback](#feedback)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

## Configuration

This mod creates a configuration file in *./HarmonyMods_Data/NoFuelRequirements/Configuration.json*

By default it'll look something like...
```json
{
  "Modification enabled (true/false)": true,
  "Affected prefabs (prefab shortname)": [
    "furnace",
    "furnace.large",
    "refinery_small_deployed",
    "campfire",
    "bbq.deployed",
    "skull_fire_pit"
  ]
}
```
This should all be pretty self-explanatory, but in case it's not...
* **"Modification enabled (true/false)"** => Enables or disables the entire mod
* **"Affected prefabs (prefab shortname)"** => A list of prefabs that the mod will affect. Note that these prefabs need to be able to cook stuff, this mod does not affect anything like electric stuff, mini's, trains, etc. that require fuel. Just stuff that cooks or smelts.

## Commands

There's only one command included in this mod so far, and that's the reloading of configuration. This is done by doing;
```
nofuelrequirements.reload
```

Changes will be propagated immediately. 

**Note:** Due to the nature of the modification enabler booleans ("Modification enabled (true/false)"), changing these will require a restart of the server. This is because the booleans indicate whether or not to change the source code, which can't change while the server is running. Changing the prefabs list and then reloading using the command should be fine.

## Feedback

Feel free to send me feedback on Discord by adding **Airathias#0001** or [file an issue](https://github.com/Hypernova-gg/NoFuelRequirements/issues/new). Feature requests are always welcome, though due to limited time for sideprojects like these few will be implemented, if any.
