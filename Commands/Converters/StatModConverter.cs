using System.Collections.Generic;
using System.Linq;
using VampireCommandFramework;

namespace ArenaBuilds.Commands.Converters;

public record struct WeaponStatMod(string Name);

internal class StatModConverter : CommandArgumentConverter<WeaponStatMod>
{
    public static readonly Dictionary<string, string> ShorthandToPrefabName = new()
    {
        { "AS", "StatMod_AttackSpeed" },
        { "PCC", "StatMod_CriticalStrikePhysical" },
        { "CSP", "StatMod_CriticalStrikePhysicalPower" },
        { "CSS", "StatMod_CriticalStrikeSpellPower" },
        { "SCC", "StatMod_CriticalStrikeSpells" },
        { "MH", "StatMod_MaxHealth" },
        { "MS", "StatMod_MovementSpeed" },
        { "PP", "StatMod_PhysicalPower" },
        { "SCD", "StatMod_SpellCooldownReduction" },
        { "SL", "StatMod_SpellLeech" },
        { "SP", "StatMod_SpellPower" },
        { "TCD", "StatMod_TravelCooldownReduction" },
        { "WCD", "StatMod_WeaponCooldownReduction" },
        { "WSP", "StatMod_WeaponSkillPower" }
    };

    public override WeaponStatMod Parse(ICommandContext ctx, string input)
    {
        input = input.ToUpper();
        var match = ShorthandToPrefabName.Where(kvp => kvp.Key.Equals(input)).Select(kvp => kvp.Value).FirstOrDefault();

        if (string.IsNullOrEmpty(match))
        {
            throw ctx.Error($"Unknown stat mod <color=white>{input}</color>.");
        }

        return new WeaponStatMod(match);
    }
}