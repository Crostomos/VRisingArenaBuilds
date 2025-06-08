using ArenaBuilds.Data.Db;
using ArenaBuilds.Extensions;
using ArenaBuilds.Models.CommandArguments;
using VampireCommandFramework;

namespace ArenaBuilds.Commands.Converters;

internal class LegendaryWeaponConverter : CommandArgumentConverter<WeaponModel>
{
    public override WeaponModel Parse(ICommandContext ctx, string input)
    {
        var match =
            WeaponDb.Weapons.EqualsCommandArgument(input) as WeaponModel ??
            (WeaponDb.Weapons.ContainsCommandArgument(input) as WeaponModel ??
             throw ctx.Error($"Unknown weapon <color=white>{input}</color>."));

        match.SetLegendaryPrefab();
        return match;
    }
}