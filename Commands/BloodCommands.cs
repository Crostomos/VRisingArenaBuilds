using ArenaBuilds.Data.Db;
using ArenaBuilds.Extensions;
using ArenaBuilds.Helpers;
using ArenaBuilds.Models;
using ArenaBuilds.Models.CommandArguments;
using VampireCommandFramework;

namespace ArenaBuilds.Commands;

internal class BloodCommands
{
    [Command("fill_blood", "fillb", "Draculin Scholar 1 VampireName", "Fill your blood", adminOnly: false)]
    public static void GiveBloodCommand(
        ChatCommandContext ctx,
        BloodModel bloodTypePrimary,
        BloodModel bloodTypeSecondary = null,
        int secondaryBuffIndex = 0,
        PlayerData player = null)
    {
        player ??= new PlayerData(ctx.User, ctx.Event.SenderUserEntity);

        BloodHelper.SetBlood(
            player.CharacterEntity,
            bloodTypePrimary.PrefabName,
            bloodTypeSecondary?.PrefabName ?? "",
            100,
            100,
            secondaryBuffIndex
        );

        var bloodTypeString = bloodTypePrimary.Name;
        if (bloodTypeSecondary != null) bloodTypeString += $"/{bloodTypeSecondary.Name}";
        ctx.Reply(
            $"Fill blood of <color=white>{player.CharacterName}</color> with <color=white>{bloodTypeString}</color>.");
    }

    [Command("list_blood", "listbl", description: "List bloods", adminOnly: false)]
    public static void ListBloodsCommand(ChatCommandContext ctx)
    {
        ctx.Reply($"Bloods :\n{BloodDb.Bloods.ToFormattedList()}");
    }
}