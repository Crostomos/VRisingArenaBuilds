using ArenaBuilds.Data.Db;
using ArenaBuilds.Extensions;
using ArenaBuilds.Models.CommandArguments;
using VampireCommandFramework;

namespace ArenaBuilds.Commands.Converters;

internal class BloodConverter : CommandArgumentConverter<BloodModel>
{
    public override BloodModel Parse(ICommandContext ctx, string input)
    {
        var match =
            BloodDb.Bloods.EqualsCommandArgument(input) as BloodModel ??
            BloodDb.Bloods.SearchCommandArgument(input) as BloodModel ??
            throw ctx.Error($"Unknown blood type <color=white>{input}</color>.");

        return match;
    }
}