using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using ArenaBuilds.Models.CommandArguments;
using ArenaBuilds.Models.Interfaces;

namespace ArenaBuilds.Data.Db;

internal class AbilityDb : IDatabase
{
    public static List<AbilityModel> Abilities = [];

    public void Init()
    {
        foreach (var spellSchool in SpellSchoolDb.SpellSchools)
        {
            var regex = new Regex($"^AB_{spellSchool}_[a-zA-Z]+_AbilityGroup$", RegexOptions.IgnoreCase);

            var matchingProperties = typeof(Prefabs)
                .GetFields(BindingFlags.Static | BindingFlags.Public)
                .Where(prop => regex.IsMatch(prop.Name) && prop.Name.Count(c => c == '_') == 3)
                .ToList();

            foreach (var prop in matchingProperties)
            {
                var abilityName = prop.Name.Split("_")[2];
                Abilities.Add(new AbilityModel(abilityName, prop.Name, [abilityName.Replace(" ", "")]));
            }
        }

        Plugin.Logger.LogInfo("AbilityDatabase initialized.");
    }
}