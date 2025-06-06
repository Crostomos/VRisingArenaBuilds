using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using ArenaBuilds.Models.CommandArguments;
using ArenaBuilds.Models.Interfaces;

namespace ArenaBuilds.Data.Db;

public class SpellModDb : IDatabase
{
    public static List<SpellModModel> Mods = [];

    public void Init()
    {
        foreach (var ability in AbilityDb.Abilities)
        {
            AddToList(ability.Name);
        }

        AddToList("Shared");
    }

    private static void AddToList(string name)
    {
        var regex = new Regex($"^SpellMod_{name}_[a-zA-Z]+$", RegexOptions.IgnoreCase);

        var matchingProperties = typeof(Prefabs)
            .GetFields(BindingFlags.Static | BindingFlags.Public)
            .Where(prop => regex.IsMatch(prop.Name) && prop.Name.Count(c => c == '_') == 2)
            .ToList();

        foreach (var prop in matchingProperties)
        {
            var modName = prop.Name.Split("_")[2];
            Mods.Add(new SpellModModel(modName, prop.Name, []));
        }
    }
}