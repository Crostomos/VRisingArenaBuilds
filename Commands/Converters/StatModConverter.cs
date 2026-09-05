using ArenaBuilds.Data.Db;
using ArenaBuilds.Extensions;
using ArenaBuilds.Models.CommandArguments;
using VampireCommandFramework;

namespace ArenaBuilds.Commands.Converters;

internal class StatModConverter : CommandArgumentConverter<StatModModel>
{
    public override StatModModel Parse(ICommandContext ctx, string input)
    {
        return StatModDb.Mods.ContainsCommandArgument(input) as StatModModel ??
               throw ctx.Error($"Unknown stat mod <color=white>{input}</color>.");
    }
}