using System.Collections.Generic;
using System.Linq;
using ArenaBuilds.Models;
using ProjectM;
using ProjectM.Network;
using Stunlock.Core;
using Unity.Entities;
using Exception = System.Exception;

namespace ArenaBuilds.Helpers;

internal static class InventoryHelper
{
    public static void GiveItems(Entity character, List<BuildItemData> items)
    {
        foreach (var item in items)
        {
            if (string.IsNullOrEmpty(item.Name)) continue;
            if (UtilsHelper.TryGetPrefabGuid(item.Name, out var itemGuid))
            {
                AddItemToInventory(character, itemGuid, item.Amount);
            }
            else
            {
                Plugin.Logger.LogWarning($"Item guid not found for {item.Name}.");
            }
        }
    }

    public static Entity AddItemToInventory(Entity recipient, PrefabGUID guid, int amount)
    {
        try
        {
            var inventoryResponse = Core.ServerGameManager.TryAddInventoryItem(recipient, guid, amount);
            return inventoryResponse.NewEntity;
        }
        catch (Exception e)
        {
            Plugin.Logger.LogFatal(e);
        }

        return new Entity();
    }

    public static void EquipEquipment(Entity character, int slot)
    {
        UtilsHelper.CreateEventFromCharacter(character, new EquipItemEvent { SlotIndex = slot });
    }

    public static void ClearInventory(Entity character)
    {
        var equipment = Core.EntityManager.GetComponentData<Equipment>(character);
        List<Entity> equippedEntity =
        [
            equipment.GetEquipmentEntity(EquipmentType.Headgear).GetEntityOnServer(),
            equipment.GetEquipmentEntity(EquipmentType.Chest).GetEntityOnServer(),
            equipment.GetEquipmentEntity(EquipmentType.Legs).GetEntityOnServer(),
            equipment.GetEquipmentEntity(EquipmentType.Footgear).GetEntityOnServer(),
            equipment.GetEquipmentEntity(EquipmentType.Gloves).GetEntityOnServer(),
            equipment.GetEquipmentEntity(EquipmentType.Cloak).GetEntityOnServer(),
            equipment.GetEquipmentEntity(EquipmentType.MagicSource).GetEntityOnServer(),
            equipment.GetEquipmentEntity(EquipmentType.Bag).GetEntityOnServer(),
            equipment.GetEquipmentEntity(EquipmentType.Weapon).GetEntityOnServer()
        ];

        foreach (var entity in equippedEntity.Where(entity => entity != new Entity()))
        {
            InventoryUtilitiesServer.TryUnEquipItem(Core.EntityManager, character, entity);
        }

        InventoryUtilitiesServer.ClearInventory(Core.EntityManager, character);
        // TODO Clear Jewels
    }
}