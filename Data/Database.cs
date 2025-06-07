using System.Collections.Generic;
using ArenaBuilds.Data.Db;
using ArenaBuilds.Models.Interfaces;

namespace ArenaBuilds.Data;

internal static class Database
{
    public static void Initialize()
    {
        var databases = new List<IDatabase>
        {
            new SpellSchoolDb(),
            new AbilityDb(),
            new SpellModDb(),
            new StatModDb(),
            new InfuseSpellModDb(),
            new WeaponDb(),
            new AbilityToSpellModDb()
        };

        foreach (var db in databases)
        {
            db.Init();
        }
        
        Plugin.Logger.LogInfo("All databases initialized.");
    }
}