`0.2.10-dev`
- Introduced new commands to grant artifact and legendary weapons.
- Added support for giving jewels with custom modifiers.
- Updated `give_build`:
    - `SpellMod1` and `SpellMod2` are now automatically assigned for artifact weapons based on weapon variation.
- Added new build options :
  - `ClearInventory`: Clears inventory before applying the build.
  - `SecondaryBuffIndex`: Select the secondary blood bonus.
  - `FillBloodPool`: Fills the blood pool.
  - `GiveBloodPotion`: Grants a blood potion.

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