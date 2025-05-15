using ArenaBuildsMod.Models;
using Stunlock.Core;
using Unity.Entities;

namespace ArenaBuildsMod.Helpers;

internal static class  AbilityHelper
{

    public static void EquipAbilities(Entity character, Abilities abilities)
    {
        if (UtilsHelper.TryGetPrefabGuid(abilities.Travel, out var travelGuid))
        {
            EquipAbility(character, travelGuid, AbilitySlot.Travel);
        }
        
        if (UtilsHelper.TryGetPrefabGuid(abilities.Ability1, out var ability1Guid))
        {
            EquipAbility(character, ability1Guid, AbilitySlot.Ability1);
        }
        
        if (UtilsHelper.TryGetPrefabGuid(abilities.Ability2, out var ability2Guid))
        {
            EquipAbility(character, ability2Guid, AbilitySlot.Ability2);
        }
        
        if (UtilsHelper.TryGetPrefabGuid(abilities.Ultimate, out var ultimateGuid))
        {
            EquipAbility(character, ultimateGuid, AbilitySlot.Ultimate);
        }
    }
    
    
    private static void EquipAbility(Entity character, PrefabGUID guid, AbilitySlot slot)
    {
        var buffEntity = Entity.Null;
        Core.ServerGameManager.ModifyAbilityGroupOnSlot(buffEntity, character, (int)slot, guid); // TODO fix didn't change spell when press J
    }
    
    private static void EquipSpellPassive(Entity character, int slot)
    {
        // TODO
        // var entity = Core.EntityManager.CreateEntity(ComponentType.ReadWrite<FromCharacter>(),
        //     ComponentType.ReadWrite<UnlockSpellSchoolPassive>());
        // var userEntity = GetUserEntity(character);
        // Core.EntityManager.SetComponentData<FromCharacter>(entity,
        //     new() { User = userEntity, Character = character });
        // Core.EntityManager.SetComponentData<SpellModPrefabGuidSettings>(entity, new()
        // {
        //
        // });
    }
}