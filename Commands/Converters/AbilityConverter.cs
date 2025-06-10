using ArenaBuilds.Data.Db;
using ArenaBuilds.Extensions;
using ArenaBuilds.Models.CommandArguments;
using VampireCommandFramework;

namespace ArenaBuilds.Commands.Converters;

internal class AbilityConverter : CommandArgumentConverter<AbilityModel>
{
    public override AbilityModel Parse(ICommandContext ctx, string input)
    {
        var match =
            AbilityDb.Abilities.EqualsCommandArgument(input) as AbilityModel ??
            (AbilityDb.Abilities.ContainsCommandArgument(input) as AbilityModel ??
             throw ctx.Error($"Unknown ability <color=white>{input}</color>."));

        return match;
    }
}