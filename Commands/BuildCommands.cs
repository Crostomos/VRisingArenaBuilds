using ArenaBuildsMod.Helpers;
using VampireCommandFramework;

namespace ArenaBuildsMod.Commands;

internal class BuildCommands
{
    [Command("build", description: "Give a full build for arena", adminOnly: false)]
    public static void BuildCommand(ChatCommandContext ctx, string targetBuild)
    {
        if (BuildManager.Builds == null)
        {
            BuildManager.LoadData();
        }

        if (BuildManager.Builds!.TryGetValue(targetBuild, out var build))
        {
            InventoryHelper.ClearInventory(ctx.Event.SenderCharacterEntity);

            ArmorHelper.EquipArmors(ctx.Event.SenderCharacterEntity, build.Armors);
            WeaponHelper.GiveWeapons(ctx.User, ctx.Event.SenderCharacterEntity, build.Weapons);
            InventoryHelper.GiveBloodPotion(
                ctx.Event.SenderCharacterEntity,
                build.Blood.PrimaryType,
                secondaryBloodType: build.Blood.SecondaryType,
                primaryQuality: build.Blood.PrimaryQuality,
                secondaryQuality: build.Blood.SecondaryQuality
            );
            InventoryHelper.GiveItems(ctx.Event.SenderCharacterEntity, build.Items);

            AbilityHelper.EquipAbilities(ctx.Event.SenderCharacterEntity, build.Abilities);
            // TODO EquipJewels
            
            AbilityHelper.EquipPassiveSpells(ctx.Event.SenderCharacterEntity, build.PassiveSpells);

            ctx.Reply($"Equipped build <color=white>{targetBuild}</color>.");
        }
        else
        {
            ctx.Reply($"Unknown build <color=white>{targetBuild}</color>.");
        }
    }

    [Command("list_build", description: "List available builds", adminOnly: false)]
    public static void ListBuildCommand(ChatCommandContext ctx)
    {
        if (BuildManager.Builds == null)
        {
            BuildManager.LoadData();
        }

        var buildList = BuildManager.GetBuildList();
        ctx.Reply($"Available builds :\n- {buildList}");
    }
    
    [Command("clear_build", description: "Clear current build", adminOnly: false)]
    public static void ClearBuildCommand(ChatCommandContext ctx)
    {
        InventoryHelper.ClearInventory(ctx.Event.SenderCharacterEntity);
        AbilityHelper.ClearPassiveSpells(ctx.Event.SenderCharacterEntity);      
        AbilityHelper.ClearAbilities(ctx.Event.SenderCharacterEntity);      
    }
    
    [Command("unlock_all", description: "Unlock All", adminOnly: false)]
    public static void UnlockAllCommand(ChatCommandContext ctx)
    {
        PlayerHelper.UnlockAll(ctx.Event.SenderUserEntity, ctx.Event.SenderCharacterEntity);
    }
}