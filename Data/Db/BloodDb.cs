using System.Collections.Generic;
using ArenaBuilds.Models.CommandArguments;
using ArenaBuilds.Models.Interfaces;

namespace ArenaBuilds.Data.Db;

public class BloodDb : IDatabase
{
    public static readonly List<BloodModel> Bloods =
    [
        new("Brute", "BloodType_Brute", ["Brute"]),
        new("Corruption", "BloodType_Corruption", ["Corruption"]),
        new("Creature", "BloodType_Creature", ["Creature"]),
        // new("DraculaTheImmortal", "BloodType_DraculaTheImmortal", ["DraculaTheImmortal"]),
        new("Draculin", "BloodType_Draculin", ["Draculin"]),
        // new("GateBoss", "BloodType_GateBoss", ["GateBoss"]),
        new("Mutant", "BloodType_Mutant", ["Mutant"]),
        // new("None", "BloodType_None", ["None"]),
        new("Rogue", "BloodType_Rogue", ["Rogue"]),
        new("Scholar", "BloodType_Scholar", ["Scholar"]),
        // new("VBlood", "BloodType_VBlood", ["VBlood"]),
        new("Warrior", "BloodType_Warrior", ["Warrior"]),
        new("Worker", "BloodType_Worker", ["Worker"])
    ];

    public void Init()
    {
        Plugin.Logger.LogInfo("BloodsDatabase initialized.");
    }
}