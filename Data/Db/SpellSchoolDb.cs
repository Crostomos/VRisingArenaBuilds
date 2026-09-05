using System.Collections.Generic;
using ArenaBuilds.Models.Interfaces;

namespace ArenaBuilds.Data.Db;

internal class SpellSchoolDb : IDatabase
{
    public static readonly List<string> SpellSchools =
    [
        "Blood",
        "Chaos",
        "Frost",
        "Illusion",
        "Storm",
        "Unholy",
        "Vampire"
    ];

    public void Init()
    {
        Plugin.Logger.LogInfo("SpellSchoolDatabase initialized.");
    }
}