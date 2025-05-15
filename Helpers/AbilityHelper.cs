using ArenaBuildsMod.Models;
using ProjectM;
using Stunlock.Core;
using Unity.Entities;

namespace ArenaBuildsMod.Helpers;

internal static class AbilityHelper
{
    public static void EquipAbilities(Entity character, Abilities abilities)
    {
        var abilityMappings = new (string ability, AbilitySlot slot)[]
        {
            (abilities.Travel, AbilitySlot.Travel),
            (abilities.Ability1, AbilitySlot.Ability1),
            (abilities.Ability2, AbilitySlot.Ability2),
            (abilities.Ultimate, AbilitySlot.Ultimate)
        };

        foreach (var (ability, slot) in abilityMappings)
        {
            if (UtilsHelper.TryGetPrefabGuid(ability, out var guid))
            {
                EquipAbility(character, guid, slot);
            }
        }
    }


    public static void EquipPassiveSpells(Entity character, PassiveSpells passiveSpells)
    {
        var spells = new[] 
        {
            passiveSpells.PassiveSpell1,
            passiveSpells.PassiveSpell2,
            passiveSpells.PassiveSpell3,
            passiveSpells.PassiveSpell4,
            passiveSpells.PassiveSpell5
        };

        for (var i = 0; i < spells.Length; i++)
        {
            if (UtilsHelper.TryGetPrefabGuid(spells[i], out var spellGuid))
            {
                EquipSpellPassive(character, spellGuid, i);
            }
        }
    }


    private static void EquipAbility(Entity character, PrefabGUID guid, AbilitySlot slot)
    {
        var buffEntity = Entity.Null;
        Core.ServerGameManager.ModifyAbilityGroupOnSlot(buffEntity, character, (int)slot,
            guid); // TODO fix didn't change spell when press J
    }

    private static void EquipSpellPassive(Entity character, PrefabGUID guid, int slot)
    {
        var buffer = Core.EntityManager.GetBuffer<ActivePassivesBuffer>(character);
        buffer[slot] = new ActivePassivesBuffer
        {
            PrefabGuid = guid
        };
    }

    public static void ClearPassiveSpell(Entity character)
    {
        var buffer = Core.EntityManager.GetBuffer<ActivePassivesBuffer>(character);
        for (var i = 0; i < buffer.Length; ++i)
        {
            buffer[i] = new ActivePassivesBuffer();
        }
    }
}