using System.Collections.Generic;
using ArenaBuilds.Models.CommandArguments;
using ArenaBuilds.Models.Interfaces;

namespace ArenaBuilds.Data.Db;

internal class StatModDb : IDatabase
{
    public static List<StatModModel> Mods =
    [
        new("Attack Speed", "StatMod_AttackSpeed", ["AS"]),
        new("Physical Critical Chance", "StatMod_CriticalStrikePhysical", ["PCC"]),
        new("Physical Critical Damage", "StatMod_CriticalStrikePhysicalPower", ["PCD"]),
        new("Spell Critical Chance", "StatMod_CriticalStrikeSpells", ["SCC"]),
        new("Spell Critical Damage", "StatMod_CriticalStrikeSpellPower", ["SCD"]),
        new("Max Health", "StatMod_MaxHealth", ["MH"]),
        new("Movement Speed", "StatMod_MovementSpeed", ["MS"]),
        new("Physical Power", "StatMod_PhysicalPower", ["PP"]),
        new("Spell Cooldown Reduction", "StatMod_SpellCooldownReduction", ["SCR"]),
        new("Spell Leech", "StatMod_SpellLeech", ["SL"]),
        new("Spell Power", "StatMod_SpellPower", ["SP"]),
        new("Travel Cooldown Reduction", "StatMod_TravelCooldownReduction", ["TCR"]),
        new("Weapon Cooldown Reduction", "StatMod_WeaponCooldownReduction", ["WCR"]),
        new("Weapon Power", "StatMod_WeaponSkillPower", ["WP"]),
    ];

    public void Init()
    {
        Plugin.Logger.LogInfo("StatModDatabase initialized.");
    }
}