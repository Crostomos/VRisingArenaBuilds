using System;
using System.Collections.Generic;
using ArenaBuildsMod.Models;
using Data;
using ProjectM;
using ProjectM.Network;
using Stunlock.Core;
using Unity.Entities;
using UnityEngine;

namespace ArenaBuildsMod.Helpers;

internal static class InventoryHelper
{
    public static void GiveItems(Entity character, List<BuildItemData> items)
    {
        foreach (var item in items)
        {
            if (UtilsHelper.TryGetPrefabGuid(item.Name, out var itemGuid))
            {
                AddItemToInventory(character, itemGuid, item.Amount);
            }
        }
    }

    public static void GiveBloodPotion(Entity character, string primaryBloodType,
        string secondaryBloodType = "BloodType_None", float primaryQuality = 100, float secondaryQuality = 100)
    {
        if (UtilsHelper.TryGetPrefabGuid(primaryBloodType, out var primaryBloodTypeGuid))
        {
            if (UtilsHelper.TryGetPrefabGuid(secondaryBloodType, out var secondaryBloodTypeGuid))
            {
                primaryQuality = Mathf.Clamp(primaryQuality, 0, 100);
                secondaryQuality = Mathf.Clamp(secondaryQuality, 0, 100);

                var entity = AddItemToInventory(character, Prefabs.Item_Consumable_PrisonPotion_Bloodwine, 1);
                var blood = new StoredBlood
                {
                    BloodQuality = primaryQuality,
                    PrimaryBloodType = primaryBloodTypeGuid,
                    SecondaryBlood = new()
                    {
                        Type = secondaryBloodTypeGuid,
                        Quality = secondaryQuality,
                        BuffIndex = 0
                    }
                };

                Core.EntityManager.SetComponentData(entity, blood);
            }
            else
            {
                Plugin.Logger.LogWarning($"Secondary Blood type guid not found for {secondaryBloodType}.");
            }
        }
        else
        {
            Plugin.Logger.LogWarning($"Primary Blood type guid not found for {primaryBloodType}.");
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

    public static void UnEquipEquipment(Entity character, EquipmentType equipmentType)
    {
       
        UtilsHelper.CreateEventFromCharacter(character, new UnequipItemEvent
        {
            EquipmentType = equipmentType
        });
    }

    public static void ClearInventory(Entity character)
    {
        var equipment = Core.EntityManager.GetComponentData<Equipment>(character);

        UtilsHelper.DestroyTargetEntity(equipment.GetEquipmentEntity(EquipmentType.Headgear).GetEntityOnServer());
        UtilsHelper.DestroyTargetEntity(equipment.GetEquipmentEntity(EquipmentType.Chest).GetEntityOnServer());
        UtilsHelper.DestroyTargetEntity(equipment.GetEquipmentEntity(EquipmentType.Legs).GetEntityOnServer());
        UtilsHelper.DestroyTargetEntity(equipment.GetEquipmentEntity(EquipmentType.Footgear).GetEntityOnServer());
        UtilsHelper.DestroyTargetEntity(equipment.GetEquipmentEntity(EquipmentType.Gloves).GetEntityOnServer());
        UtilsHelper.DestroyTargetEntity(equipment.GetEquipmentEntity(EquipmentType.Cloak).GetEntityOnServer());
        UtilsHelper.DestroyTargetEntity(equipment.GetEquipmentEntity(EquipmentType.MagicSource).GetEntityOnServer());
        UtilsHelper.DestroyTargetEntity(equipment.GetEquipmentEntity(EquipmentType.Bag).GetEntityOnServer());

        InventoryUtilitiesServer.ClearInventory(Core.EntityManager, character);
        // TODO Clear Jewels
    }
}