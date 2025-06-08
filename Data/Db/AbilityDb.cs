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
            List<string> regexList =
            [
                $"^AB_{spellSchool}_[a-zA-Z]+_AbilityGroup$",
                $"^AB_{spellSchool}_[a-zA-Z]+_Group$"
            ];

            foreach (var item in regexList)
            {
                var regex = new Regex(item, RegexOptions.IgnoreCase);

                var matchingProperties = typeof(Prefabs)
                    .GetFields(BindingFlags.Static | BindingFlags.Public)
                    .Where(prop => regex.IsMatch(prop.Name) && prop.Name.Count(c => c == '_') == 3)
                    .ToList();

                foreach (var prop in matchingProperties)
                {
                    var abilityName = prop.Name.Split("_")[2];
                    Abilities.Add(new AbilityModel(
                        abilityName,
                        prop.Name,
                        [abilityName.Replace(" ", "")],
                        spellSchool));
                }
            }
        }

        // add special cases
        Abilities.Add(new AbilityModel("FrostBarrier", "AB_FrostBarrier_AbilityGroup", ["FrostBarrier"], "Frost"));
        Abilities.Add(new AbilityModel("FrostCone", "AB_FrostCone_AbilityGroup", ["FrostCone"], "Frost"));

        Plugin.Logger.LogInfo("AbilityDatabase initialized.");
    }
}