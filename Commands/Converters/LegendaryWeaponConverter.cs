using System.Collections.Generic;
using System.Linq;
using VampireCommandFramework;

namespace ArenaBuilds.Commands.Converters;

public record struct LegendaryWeapon(string PrefabName);

internal class LegendaryWeaponConverter : CommandArgumentConverter<LegendaryWeapon>
{
    public static readonly List<string> BaseWeaponNames =
    [
        "Sword",
        "Axe",
        "Claws",
        "Crossbow",
        "Daggers",
        "GreatSword",
        "LongBow",
        "Mace",
        "Pistols",
        "Reaper",
        "Slashers",
        "Spear",
        "TwinBlades",
        "Whip"
    ];

    public override LegendaryWeapon Parse(ICommandContext ctx, string input)
    {
        input = input.ToLower();

        var match = BaseWeaponNames.FirstOrDefault(n => n.ToLower().Equals(input));

        if (string.IsNullOrEmpty(match))
        {
            match = BaseWeaponNames.FirstOrDefault(n => n.ToLower().Contains(input));

            if (string.IsNullOrEmpty(match))
            {
                throw ctx.Error($"Unknown weapon <color=white>{input}</color>.");
            }
        }

        var legendaryPrefabName = $"Item_Weapon_{match}_Legendary_T08";

        return new LegendaryWeapon(legendaryPrefabName);
    }
}