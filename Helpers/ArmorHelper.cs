using System;
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
        var armorParts = new (string item, Func<Armors, string> getPart)[]
        {
            ("Boots", a => a.Boots),
            ("Chest", a => a.Chest),
            ("Cloak", a => a.Cloak),
            ("Gloves", a => a.Gloves),
            ("Head", a => a.Head),
            ("Legs", a => a.Legs),
            ("MagicSource", a => a.MagicSource)
        };

        foreach (var (_, getPart) in armorParts)
        {
            if (UtilsHelper.TryGetPrefabGuid(getPart(armors), out var guid))
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