using System.Collections.Generic;
using System.Linq;
using VampireCommandFramework;

namespace ArenaBuilds.Commands.Converters;

public record struct InfuseSpellMod(string ShortHand, string Name);

internal class InfuseSpellModConverter : CommandArgumentConverter<InfuseSpellMod>
{
    public static readonly Dictionary<string, string> ShorthandToPrefabName = new()
    {
        { "blood", "SpellMod_Weapon_BloodInfused" },
        { "chaos", "SpellMod_Weapon_ChaosInfused" },
        { "frost", "SpellMod_Weapon_FrostInfused" },
        { "illusion", "SpellMod_Weapon_IllusionInfused" },
        { "storm", "SpellMod_Weapon_StormInfused" },
        { "undead", "SpellMod_Weapon_UndeadInfused" },
    };

    public override InfuseSpellMod Parse(ICommandContext ctx, string input)
    {
        input = input.ToLower();
        var match = ShorthandToPrefabName.Where(kvp => kvp.Key.Equals(input)).Select(kvp => kvp.Value).FirstOrDefault();

        if (string.IsNullOrEmpty(match))
        {
            throw ctx.Error($"Unknown infuse spell mod <color=white>{input}</color>.");
        }

        return new InfuseSpellMod(input, match);
    }
}