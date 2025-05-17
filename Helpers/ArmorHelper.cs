using System;
using ArenaBuildsMod.Models;
using ProjectM;
using Stunlock.Core;
using Unity.Entities;

namespace ArenaBuildsMod.Helpers;

internal static class ArmorHelper
{
    public static void EquipArmors(Entity character, Armors armors)
    {
        var armorList = new[]
        {
            armors.Boots,
            armors.Chest,
            armors.Gloves,
            armors.Legs,
            armors.MagicSource,
            armors.Head,
            armors.Cloak
        };

        for (var i = 0; i < armorList.Length; i++)
        {
            if (UtilsHelper.TryGetPrefabGuid(armorList[i], out var guid))
            {
                GiveAndEquip(character, guid);
            }
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