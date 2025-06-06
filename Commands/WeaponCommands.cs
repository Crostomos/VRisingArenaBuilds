using ArenaBuilds.Data.Db;
using ArenaBuilds.Extensions;
using ArenaBuilds.Helpers;
using ArenaBuilds.Models;
using ArenaBuilds.Models.CommandArguments;
using VampireCommandFramework;

namespace ArenaBuilds.Commands;

internal class WeaponCommands
{
    [Command("give_legendary", "giveleg", description: "Give a legendary weapon", adminOnly: false)]
    public static void GiveLegendaryWeaponCommand(
        ChatCommandContext ctx,
        WeaponModel weapon,
        InfuseSpellModModel infuseSpellModModel,
        StatModModel statMod1,
        StatModModel statMod2,
        StatModModel statMod3)
    {
        if (statMod1.Name == statMod2.Name || statMod1.Name == statMod3.Name || statMod2.Name == statMod3.Name)
        {
            throw ctx.Error("Stat mods must be different.");
        }

        var weaponData = new WeaponData
        {
            Name = weapon.PrefabName,
            InfuseSpellMod = infuseSpellModModel.PrefabName,
            StatMod1 = statMod1.PrefabName,
            StatMod1Power = 1,
            StatMod2 = statMod2.PrefabName,
            StatMod2Power = 1,
            StatMod3 = statMod3.PrefabName,
            StatMod3Power = 1
        };

        WeaponHelper.CreateAndGiveLegendaryWeapon(ctx.User.Index, weaponData);
        ctx.Reply($"Legendary weapon <color=white>{weapon.Name}</color> acquired.");
    }

    [Command("give_artifact", "giveart", description: "Give an artifact weapon", adminOnly: false)]
    public static void GiveArtifactWeaponCommand(
        ChatCommandContext ctx,
        ArtifactWeaponModel weapon,
        StatModModel statMod1,
        StatModModel statMod2,
        StatModModel statMod3)
    {
        if (statMod1.Name == statMod2.Name || statMod1.Name == statMod3.Name || statMod2.Name == statMod3.Name)
        {
            throw ctx.Error("Stat mods must be different.");
        }

        var weaponData = new WeaponData
        {
            Name = weapon.PrefabName,
            SpellMod1 = weapon.SpellMod1,
            SpellMod2 = weapon.SpellMod2,
            StatMod1 = statMod1.PrefabName,
            StatMod1Power = 1,
            StatMod2 = statMod2.PrefabName,
            StatMod2Power = 1,
            StatMod3 = statMod3.PrefabName,
            StatMod3Power = 1
        };

        WeaponHelper.CreateAndGiveArtifactWeapon(ctx.User.Index, ctx.Event.SenderCharacterEntity, weaponData);
        ctx.Reply($"Artifact weapon <color=white>{weapon.Name}{weapon.Variation}</color> acquired.");
    }

    [Command("list_weapon", "listw", description: "List weapons", adminOnly: false)]
    public static void ListWeaponsCommand(ChatCommandContext ctx)
    {
        ctx.Reply($"Weapons :\n{WeaponDb.Weapons.ToFormattedList()}");
    }

    [Command("list_statmod", "listsm", description: "List stats mods", adminOnly: false)]
    public static void ListStatModsCommand(ChatCommandContext ctx)
    {
        ctx.Reply($"Stat Mods :\n{StatModDb.Mods.ToFormattedList(showName: true)}");
    }

    [Command("list_infuse", "listi", description: "List infuses", adminOnly: false)]
    public static void ListInfuseCommand(ChatCommandContext ctx)
    {
        ctx.Reply($"Infuses :\n{InfuseSpellModDb.Mods.ToFormattedList()}");
    }
}