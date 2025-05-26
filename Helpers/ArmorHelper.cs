using ArenaBuilds.Models;
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
        InventoryHelper.EquipEquipment(character, slot);
    }
}