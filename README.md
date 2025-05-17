# ArenaBuildsMod

**ArenaBuildsMod** is a lightweight, server-side mod for *V Rising* that allows players to instantly equip ready-to-use PvP builds via simple in-game commands.  
It’s designed to streamline arena practice and fast-paced duels without the need to manually gear up.

---

## 🔧 Features

- Instantly equip a complete build with a single command, including:
  - Blood type
  - Fully attributed weapons
  - Armor sets
  - Abilities
  - Jewels
  - Passive spells
- Default builds are based on PVP_Preset
- Builds are fully customizable via the `builds.json` file
  - A list of valid `prefabName` values is available here: [VRising DB](https://vrising.gaming.tools)
  - For a comprehensive list of prefabName values, particularly those related to StatMod and SpellMod, refer to: [VRising Mods Wiki](https://wiki.vrisingmods.com/prefabs/Spell)

---

## 💬 Commands

| Command            | Description                                                  |
|--------------------|--------------------------------------------------------------|
| `.build <class>`   | Equips the specified build with gear, spells, and passives   |
| `.list_build`      | Displays the list of available builds                        |
| `.clear_build`     | Clears your inventory, spells, and passives                  |

---

## 📦 Requirements

- [BepInEx](https://github.com/BepInEx/BepInEx)
- [VampireCommandFramework (VCF)](https://github.com/decaprime/VampireCommandFramework)

---

## 📥 Installation

1. **Install BepInEx**  
   Follow the guide here: [BepInEx Installation Guide](https://wiki.vrisingmods.com/user/bepinex_install.html)  
   ⚠️ *Until BepInEx is updated for v1.1, avoid using the Thunderstore version.* Use the correct testing version listed [here](https://wiki.vrisingmods.com/user/game_update.html).

2. **Install VampireCommandFramework (VCF)**  
   ⚠️ *Also avoid the Thunderstore version until VCF is updated for v1.1.* Download the proper version [here](https://wiki.vrisingmods.com/user/game_update.html).

3. **Download ArenaBuildsMod**  
   Grab the `BuildArena.dll` and `builds.json` files from the [Releases](#) section.

4. **Place the files**  
   - Move `VampireCommandFramework.dll` and `BuildArena.dll` to:  
     `BepInEx/Plugins/`
   - Move `builds.json` to:  
     `BepInEx/config/ArenaBuildsMod/`

---

## 🐞 Known Issues

- Weapon spell modifiers are not currently applied.
- The `.clear_build` command doesn't properly remove equipped items or jewels.

---

## 🚧 Planned Features

- Save and restore your character’s original state before and after using a build.
- Restrict `.build` usage to specific arena zones (can be toggled via config).

---

## 🙌 Credits

- Big thanks to the [V Rising Modding Community](https://vrisingmods.com/) for documentation and open-source tools.
- Special thanks to [Odjit](https://github.com/Odjit) for the **KindredExtract** mod, which was a helpful reference.

---

## 👤 Author

Developed by **Crostomos**

---

## 📄 License

This project is licensed under the **AGPL-3.0** license.
