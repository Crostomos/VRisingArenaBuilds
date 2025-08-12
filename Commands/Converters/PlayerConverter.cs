using System;
using ArenaBuilds.Helpers;
using ArenaBuilds.Models;
using ProjectM.Network;
using VampireCommandFramework;

namespace ArenaBuilds.Commands.Converters;

internal class PlayerConverter : CommandArgumentConverter<PlayerData>
{
    public override PlayerData Parse(ICommandContext ctx, string input)
    {
        var isLong = ulong.TryParse(input, out var steamId);
        var userEntities = UtilsHelper.GetEntitiesByComponentType<User>(includeDisabled: true);
        foreach (var entity in userEntities)
        {
            var userData = Core.EntityManager.GetComponentData<User>(entity);

            if (!userData.IsConnected)
            {
                continue;
            }

            if (isLong && userData.PlatformId == steamId || string.Equals(userData.CharacterName.ToString(), input,
                    StringComparison.CurrentCultureIgnoreCase))
            {
                return new PlayerData(userData, entity);
            }
        }

        throw ctx.Error($"Unknown player <color=white>{input}</color>.");
    }
}