using ArenaBuilds.Helpers;
using VampireCommandFramework;

namespace ArenaBuilds.Commands;

internal class BuildCommands
{
    [Command("give_build", "giveb", description: "Give a full build", adminOnly: false)]
    public static void BuildCommand(ChatCommandContext ctx, string buildName)
    {
        if (BuildManager.Builds.Count == 0)
        {
            BuildManager.LoadData();
        }

        if (BuildManager.Builds.TryGetValue(buildName, out var build))
        {
            InventoryHelper.ClearInventory(ctx.Event.SenderCharacterEntity);

            WeaponHelper.GiveWeapons(ctx.User, ctx.Event.SenderCharacterEntity, build.Weapons);
            InventoryHelper.GiveBloodPotion(
                ctx.Event.SenderCharacterEntity,
                build.Blood.PrimaryType,
                secondaryBloodType: build.Blood.SecondaryType,
                primaryQuality: build.Blood.PrimaryQuality,
                secondaryQuality: build.Blood.SecondaryQuality
            );
            InventoryHelper.GiveItems(ctx.Event.SenderCharacterEntity, build.Items);

            ArmorHelper.EquipArmors(ctx.Event.SenderCharacterEntity, build.Armors);

            BloodHelper.SetBlood(
                ctx.Event.SenderCharacterEntity,
                build.Blood.PrimaryType,
                secondaryBloodType: build.Blood.SecondaryType,
                primaryQuality: build.Blood.PrimaryQuality,
                secondaryQuality: build.Blood.SecondaryQuality,
                amount: 100
            );

            AbilityHelper.EquipAbilities(ctx.Event.SenderCharacterEntity, ctx.User, build.Abilities);
            AbilityHelper.EquipPassiveSpells(ctx.Event.SenderCharacterEntity, build.PassiveSpells);

            ctx.Reply($"Equipped build <color=white>{buildName}</color>.");
        }
        else
        {
            ctx.Reply($"Unknown build <color=white>{buildName}</color>.");
        }
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
        ctx.Reply($"Available builds :\n- {buildList}");
    }

    [Command("clear_build", "clearb", description: "Clear current build", adminOnly: false)]
    public static void ClearBuildCommand(ChatCommandContext ctx)
    {
        InventoryHelper.ClearInventory(ctx.Event.SenderCharacterEntity);
        AbilityHelper.ClearPassiveSpells(ctx.Event.SenderCharacterEntity);
        AbilityHelper.ClearAbilities(ctx.Event.SenderCharacterEntity);

        ctx.Reply("Build cleared.");
    }

    [Command("unlock_all", description: "Unlock All", adminOnly: true)]
    public static void UnlockAllCommand(ChatCommandContext ctx)
    {
        PlayerHelper.UnlockAll(ctx.Event.SenderUserEntity, ctx.Event.SenderCharacterEntity);
        ctx.Reply("Researches, VBloods and Achievements unlocked.");
    }
}