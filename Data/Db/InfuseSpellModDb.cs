using System.Collections.Generic;
using ArenaBuilds.Models.CommandArguments;
using ArenaBuilds.Models.Interfaces;

namespace ArenaBuilds.Data.Db;

internal class InfuseSpellModDb : IDatabase
{
    public static List<InfuseSpellModModel> Mods =
    [
        new("Blood", "SpellMod_Weapon_BloodInfused", ["blood", "leech"]),
        new("Chaos", "SpellMod_Weapon_ChaosInfused", ["chaos", "ignite"]),
        new("Frost", "SpellMod_Weapon_FrostInfused", ["frost", "chill"]),
        new("Illusion", "SpellMod_Weapon_IllusionInfused", ["illusion", "weaken"]),
        new("Storm", "SpellMod_Weapon_StormInfused", ["storm", "static"]),
        new("Undead", "SpellMod_Weapon_UndeadInfused", ["undead", "unholy", "condemn"]),
    ];

    public void Init()
    {
        Plugin.Logger.LogInfo("InfuseSpellModDatabase initialized.");
    }
}