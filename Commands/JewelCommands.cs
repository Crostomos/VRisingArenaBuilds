using ArenaBuilds.Commands.ManualConverters;
using ArenaBuilds.Extensions;
using ArenaBuilds.Helpers;
using ArenaBuilds.Models;
using ArenaBuilds.Models.CommandArguments;
using VampireCommandFramework;

namespace ArenaBuilds.Commands;

internal class JewelCommands
{
    [Command("give_jewel", "givej", usage:"rite 124", description: "Give a custom jewel", adminOnly: false)]
    public static void GiveJewelCommand(
        ChatCommandContext ctx,
        AbilityModel ability,
        string spellModsIndexes)
    {
        var indexes = IndexesManualConverter.ToIndexesList(ctx, spellModsIndexes, 4);
        var spellMod1Index = indexes[0];
        var spellMod2Index = indexes[1];
        var spellMod3Index = indexes[2];
        var spellMod4Index = indexes[3];

        var spellMod1 = SpellModManualConverter.GetSpellMod(ctx, ability, spellMod1Index);
        var spellMod2 = spellMod2Index != 0
            ? SpellModManualConverter.GetSpellMod(ctx, ability, spellMod2Index)
            : string.Empty;
        var spellMod3 = spellMod3Index != 0
            ? SpellModManualConverter.GetSpellMod(ctx, ability, spellMod3Index)
            : string.Empty;
        var spellMod4 = spellMod4Index != 0
            ? SpellModManualConverter.GetSpellMod(ctx, ability, spellMod4Index)
            : string.Empty;

        var jewelData = new JewelData
        {
            SpellMod1 = spellMod1,
            SpellMod1Power = 1,
            SpellMod2 = spellMod2,
            SpellMod2Power = 1,
            SpellMod3 = spellMod3,
            SpellMod3Power = 1,
            SpellMod4 = spellMod4,
            SpellMod4Power = 1
        };
        if (UtilsHelper.TryGetPrefabGuid(ability.PrefabName, out var guid))
        {
            JewelHelper.CreateAndEquip(ctx.User, guid, jewelData);
            ctx.Reply($"Jewel for <color=white>{ability.Name}</color> equipped.");
        }
        else
        {
            throw ctx.Error($"Unknown ability {ability.Name}.");
        }
    }


    [Command("list_spellmod", "listsp", usage:"foutain", description: "List spell mod for abiltiy", adminOnly: false)]
    public static void ListSpellModCommand(ChatCommandContext ctx, AbilityModel ability)
    {
        ctx.Reply(
            $"Spell mods for <color=white>{ability.Name}</color> :\n{SpellModManualConverter.GetSpellModNameList(ctx, ability).ToIndexedList()}");
    }
}