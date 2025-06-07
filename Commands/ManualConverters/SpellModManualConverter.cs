using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ArenaBuilds.Data.Db;
using ArenaBuilds.Extensions;
using ArenaBuilds.Models.CommandArguments;
using VampireCommandFramework;

namespace ArenaBuilds.Commands.ManualConverters;

internal class SpellModManualConverter
{
    public static string GetSpellMod(ChatCommandContext ctx, AbilityModel ability, int spellModIndex)
    {
        if (AbilityToSpellModDb.AbilityToSpellMod.TryGetValue(ability.Name, out var spellMods))
        {
            var mod = spellMods.ElementAtOrDefault(spellModIndex - 1);
            if (mod != null)
            {
                return mod;
            }
            
            throw ctx.Error($"Unknown spell mod index <color=white>{spellModIndex.ToBase36()}</color>.");
        }

        throw ctx.Error($"Ability <color=white>{ability.Name}</color> have no spell mods.");
    }
    
    public static List<string> GetSpellModNameList(ChatCommandContext ctx, AbilityModel ability)
    {
        if (AbilityToSpellModDb.AbilityToSpellMod.TryGetValue(ability.Name, out var spellMods))
        {
            return spellMods
                .Select(s => s.Replace("_", "")
                    .Replace(ability.Name, "")
                    .Replace("SpellMod", "")
                    .Replace("Shared", ""))
                .Select(s => Regex.Replace(s, @"([A-Z])", " $1").Trim())
                .ToList();
        }

        throw ctx.Error($"Ability <color=white>{ability.Name}</color> have no spell mods.");
    }
}