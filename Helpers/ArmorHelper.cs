using ArenaBuildsMod.Models;
using ProjectM;
using ProjectM.Network;
using Stunlock.Core;
using Unity.Entities;

namespace ArenaBuildsMod.Helpers;

internal static class ArmorHelper
{
    public static void EquipArmors(Entity character, Armors armors)
    {
        if (UtilsHelper.TryGetPrefabGuid(armors.Boots, out var bootsGuid))
        {
            GiveAndEquip(character, bootsGuid);
        }
        
        if (UtilsHelper.TryGetPrefabGuid(armors.Chest, out var chestGuid))
        {
            GiveAndEquip(character, chestGuid);
        }
        
        if (UtilsHelper.TryGetPrefabGuid(armors.Cloak, out var cloakGuid))
        {
            GiveAndEquip(character, cloakGuid);
        }
        
        if (UtilsHelper.TryGetPrefabGuid(armors.Gloves, out var glovesGuid))
        {
            GiveAndEquip(character, glovesGuid);
        }
        
        if (UtilsHelper.TryGetPrefabGuid(armors.Head, out var headGuid))
        {
            GiveAndEquip(character, headGuid);
        }
        
        if (UtilsHelper.TryGetPrefabGuid(armors.Legs, out var legsGuid))
        {
            GiveAndEquip(character, legsGuid);
        }
        
        if (UtilsHelper.TryGetPrefabGuid(armors.MagicSource, out var magicSourceGuid))
        {
            GiveAndEquip(character, magicSourceGuid);
        }
    }

    private static void GiveAndEquip(Entity character, PrefabGUID guid)
    {
        var itemEntity = InventoryHelper.AddItemToInventory(character, guid, 1);
        var slot = InventoryUtilities.GetItemSlot(Core.EntityManager, character, guid, itemEntity);
        EquipEquipment(character, slot);
    }

    private static void EquipEquipment(Entity character, int slot)
    {
        var entity = Core.EntityManager.CreateEntity(
            ComponentType.ReadWrite<FromCharacter>(),
            ComponentType.ReadWrite<EquipItemEvent>()
            );
        var userEntity = UtilsHelper.GetUserEntity(character);
        Core.EntityManager.SetComponentData(entity, new FromCharacter { User = userEntity, Character = character });
        Core.EntityManager.SetComponentData(entity, new EquipItemEvent { SlotIndex = slot });
    }
}