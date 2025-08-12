using ArenaBuilds.Helpers;
using ArenaBuilds.Models;
using VampireCommandFramework;

namespace ArenaBuilds.Commands;

internal class BuildCommands
{
    [Command("give_build", "giveb", usage:"tank VampireName", description: "Give a full build", adminOnly: false)]
    public static void BuildCommand(ChatCommandContext ctx, string buildName, PlayerData player = null)
    {
        if (BuildManager.Builds.Count == 0)
        {
            BuildManager.LoadData();
        }

        if (!BuildManager.Builds.TryGetValue(buildName, out var build))
        {
            throw ctx.Error($"Unknown build <color=white>{buildName}</color>.");
        }

        if (player == null)
        {
            player = new PlayerData(ctx.User, ctx.Event.SenderUserEntity);
        }

        if (build.Settings.ClearInventory)
        {
            InventoryHelper.ClearInventory(player.CharacterEntity);
        }

        WeaponHelper.GiveWeapons(player.User, player.CharacterEntity, build.Weapons);

        if (build.Blood.GiveBloodPotion)
        {
            BloodHelper.GiveBloodPotion(
                player.CharacterEntity,
                build.Blood.PrimaryType,
                secondaryBloodType: build.Blood.SecondaryType,
                primaryQuality: build.Blood.PrimaryQuality,
                secondaryQuality: build.Blood.SecondaryQuality,
                secondaryBuffIndex: build.Blood.SecondaryBuffIndex
            );
        }

        InventoryHelper.GiveItems(player.CharacterEntity, build.Items);

        ArmorHelper.EquipArmors(player.CharacterEntity, build.Armors);

        if (build.Blood.FillBloodPool)
        {
            BloodHelper.SetBlood(
                player.CharacterEntity,
                build.Blood.PrimaryType,
                secondaryBloodType: build.Blood.SecondaryType,
                primaryQuality: build.Blood.PrimaryQuality,
                secondaryQuality: build.Blood.SecondaryQuality,
                secondaryBuffIndex: build.Blood.SecondaryBuffIndex,
                amount: 100
            );
        }

        AbilityHelper.EquipAbilities(player.CharacterEntity, player.User, build.Abilities);
        AbilityHelper.EquipPassiveSpells(player.CharacterEntity, build.PassiveSpells);

        ctx.Reply($"Equipped build <color=white>{buildName}</color> to <color=white>{player.CharacterName}</color>.");
    }

    [Command("list_build", "listb", description: "List available builds", adminOnly: false)]
    public static void ListBuildCommand(ChatCommandContext ctx)
    {
        if (BuildManager.Builds == null)
        {
            BuildManager.LoadData();
        }

        if (BuildManager.Builds!.Count == 0)
        {
            ctx.Reply($"No builds available.");
            return;
        }

        var buildList = BuildManager.GetBuildList();
        ctx.Reply($"Available builds :\n{buildList}");
    }

    [Command("clear_build", "clearb", description: "Clear current build", adminOnly: false)]
    public static void ClearBuildCommand(ChatCommandContext ctx, PlayerData player = null)
    {
        if (player == null)
        {
            player = new PlayerData(ctx.User, ctx.Event.SenderUserEntity);
        }

        InventoryHelper.ClearInventory(player.CharacterEntity);
        AbilityHelper.ClearPassiveSpells(player.CharacterEntity);
        AbilityHelper.ClearAbilities(player.CharacterEntity);

        ctx.Reply("Build cleared.");
    }

    [Command("unlock_all", description: "Unlock All", adminOnly: true)]
    public static void UnlockAllCommand(ChatCommandContext ctx, PlayerData player = null)
    {
        if (player == null)
        {
            player = new PlayerData(ctx.User, ctx.Event.SenderUserEntity);
        }

        var userEntity = UtilsHelper.GetUserEntity(player.CharacterEntity);

        PlayerHelper.UnlockAll(userEntity, player.CharacterEntity);
        ctx.Reply($"Researches, VBloods and Achievements unlocked for <color=white>{player.CharacterName}</color>.");
    }
}