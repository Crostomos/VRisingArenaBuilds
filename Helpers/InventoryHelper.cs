using System;
using System.Collections.Generic;
using ArenaBuildsMod.Models;
using Data;
using ProjectM;
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

    public static void ClearInventory(Entity character)
    {
        InventoryUtilitiesServer.ClearInventory(Core.EntityManager, character);

        var equipment = Core.EntityManager.GetComponentData<Equipment>(character);
        UtilsHelper.DestroyTargetEntity(equipment.ArmorHeadgearSlot.SlotEntity._Entity);
        UtilsHelper.DestroyTargetEntity(equipment.ArmorChestSlot.SlotEntity._Entity);
        UtilsHelper.DestroyTargetEntity(equipment.ArmorChestSlot.SlotEntity._Entity);
        UtilsHelper.DestroyTargetEntity(equipment.ArmorLegsSlot.SlotEntity._Entity);
        UtilsHelper.DestroyTargetEntity(equipment.ArmorFootgearSlot.SlotEntity._Entity);
        UtilsHelper.DestroyTargetEntity(equipment.CloakSlot.SlotEntity._Entity);
        UtilsHelper.DestroyTargetEntity(equipment.WeaponSlot.SlotEntity._Entity);
        UtilsHelper.DestroyTargetEntity(equipment.GrimoireSlot.SlotEntity._Entity);
        UtilsHelper.DestroyTargetEntity(equipment.BagSlot.SlotEntity._Entity);
    }
}