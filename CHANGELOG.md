`0.1.13-dev`
- Added `ClearInventory` option (clear inventory before giving build if true).
- Added `SeconDaryBuffIndex` option to chose secondary blood bonus.
- Added `FillBloodPool` option.
- Added `GiveBloodPotion` option.

`0.1.12`
- Fixed nullable errors during JSON deserialization.
- Resolved JSON loading failure caused by trailing comma.
- Added log if `prefabName` does not exist.
- Fixed blood pool with only one type.
- Added automatic generation of empty `build.json` template if the file does not exist.

`0.1.11`
- Renamed the `build` command to `give_build` to prevent conflict with the KindredSchematics mod.
- Also added a shorthand for commands.

`0.1.10`
- Initial release.