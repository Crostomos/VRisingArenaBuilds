﻿using ArenaBuilds.Models;
using ProjectM;
using Stunlock.Core;
using Unity.Entities;

namespace ArenaBuilds.Helpers;

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
            armors.Cloak,
            armors.Bag
        };

        foreach (var armor in armorList)
        {
            if (string.IsNullOrEmpty(armor)) continue;
            if (UtilsHelper.TryGetPrefabGuid(armor, out var guid))
            {
                GiveAndEquip(character, guid);
            }
            else
            {
                Plugin.Logger.LogWarning($"Armor guid not found for {armor}.");
            }
        }
    }

    private static void GiveAndEquip(Entity character, PrefabGUID guid)
    {
        var itemEntity = InventoryHelper.AddItemToInventory(character, guid, 1);
        var slot = InventoryUtilities.GetItemSlot(Core.EntityManager, character, guid, itemEntity);
        InventoryHelper.EquipEquipment(character, slot);
    }
}