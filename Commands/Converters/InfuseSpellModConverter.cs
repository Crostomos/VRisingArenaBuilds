using ArenaBuilds.Data.Db;
using ArenaBuilds.Extensions;
using ArenaBuilds.Models.CommandArguments;
using VampireCommandFramework;

namespace ArenaBuilds.Commands.Converters;

internal class InfuseSpellModConverter : CommandArgumentConverter<InfuseSpellModModel>
{
    public override InfuseSpellModModel Parse(ICommandContext ctx, string input)
    {
        if (InfuseSpellModDb.Mods.ContainsCommandArgument(input) is not InfuseSpellModModel match)
        {
            throw ctx.Error($"Unknown infuse spell mod <color=white>{input}</color>.");
        }

        return match;
    }
}