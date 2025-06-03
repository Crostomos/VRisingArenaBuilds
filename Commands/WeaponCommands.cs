using ArenaBuilds.Commands.Converters;
using ArenaBuilds.Extensions;
using ArenaBuilds.Helpers;
using ArenaBuilds.Models;
using VampireCommandFramework;

namespace ArenaBuilds.Commands;

internal class WeaponCommands
{
    [Command("give_legendary", "giveleg", description: "Give a legendary weapon", adminOnly: false)]
    public static void GiveLegendaryWeaponCommand(
        ChatCommandContext ctx,
        LegendaryWeapon weapon,
        InfuseSpellMod infuseSpellMod,
        WeaponStatMod statMod1,
        WeaponStatMod statMod2,
        WeaponStatMod statMod3)
    {
        var weaponData = new WeaponData
        {
            Name = weapon.PrefabName,
            InfuseSpellMod = infuseSpellMod.Name,
            StatMod1 = statMod1.Name,
            StatMod1Power = 1,
            StatMod2 = statMod2.Name,
            StatMod2Power = 1,
            StatMod3 = statMod3.Name,
            StatMod3Power = 1
        };

        WeaponHelper.CreateAndGiveLegendaryWeapon(ctx.User.Index, weaponData);
        ctx.Reply($"Given legendary weapon <color=white>{weapon.PrefabName}</color>.");
    }

    [Command("give_artifact", "giveart", description: "Give an artifact weapon", adminOnly: false)]
    public static void GiveArtifactWeaponCommand(
        ChatCommandContext ctx,
        ArtifactWeapon weapon,
        WeaponStatMod statMod1,
        WeaponStatMod statMod2,
        WeaponStatMod statMod3)
    {
        var weaponData = new WeaponData
        {
            Name = weapon.PrefabName,
            SpellMod1 = weapon.AbilityMod1,
            SpellMod2 = weapon.AbilityMod2,
            StatMod1 = statMod1.Name,
            StatMod1Power = 1,
            StatMod2 = statMod2.Name,
            StatMod2Power = 1,
            StatMod3 = statMod3.Name,
            StatMod3Power = 1
        };

        WeaponHelper.CreateAndGiveArtifactWeapon(ctx.User.Index, ctx.Event.SenderCharacterEntity, weaponData);
        ctx.Reply($"Given artifact weapon <color=white>{weapon.PrefabName}</color>.");
    }

    [Command("list_weapon", "listw", description: "List weapons", adminOnly: false)]
    public static void ListWeaponsCommand(ChatCommandContext ctx)
    {
        ctx.Reply($"Weapons :\n{LegendaryWeaponConverter.BaseWeaponNames.ToFormattedList()}");
    }

    [Command("list_statmod", "listsm", description: "List stats mods", adminOnly: false)]
    public static void ListStatModsCommand(ChatCommandContext ctx)
    {
        ctx.Reply($"Stat Mods :\n{StatModConverter.ShorthandToPrefabName.ToFormattedList(transformValue: value => value[8..])}");
    }
    
    [Command("list_infuse", "listi", description: "List infuses", adminOnly: false)]
    public static void ListInfuseCommand(ChatCommandContext ctx)
    {
        ctx.Reply($"Infuses :\n{InfuseSpellModConverter.ShorthandToPrefabName.Keys.ToFormattedList()}");
    }
}