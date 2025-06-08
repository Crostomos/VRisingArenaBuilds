using ArenaBuilds.Data.Db;
using ArenaBuilds.Extensions;
using ArenaBuilds.Models.CommandArguments;
using VampireCommandFramework;

namespace ArenaBuilds.Commands.Converters;

internal class StatModConverter : CommandArgumentConverter<StatModModel>
{
    public override StatModModel Parse(ICommandContext ctx, string input)
    {
        if (StatModDb.Mods.ContainsCommandArgument(input) is not StatModModel match)
        {
            throw ctx.Error($"Unknown stat mod <color=white>{input}</color>.");
        }

        return match;
    }
}