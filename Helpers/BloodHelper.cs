using ArenaBuilds.Data;
using ProjectM;
using ProjectM.Network;
using ProjectM.Shared;
using Stunlock.Core;
using Unity.Entities;
using UnityEngine;

namespace ArenaBuilds.Helpers;

internal static class BloodHelper
{
    public static void SetBlood(
        Entity character,
        string primaryBloodType,
        string secondaryBloodType = "",
        float primaryQuality = 100,
        float secondaryQuality = 100,
        int secondaryBuffIndex = 0,
        int amount = 100)
    {
        if (!TryGetBloodGuids(primaryBloodType, secondaryBloodType, out var primaryGuid, out var secondaryGuid,
                ref secondaryQuality, ref secondaryBuffIndex)) return;

        var bloodEvent = new ConsumeBloodAdminEvent
        {
            Amount = amount,
            PrimaryQuality = Mathf.Clamp(primaryQuality, 0, 100),
            SecondaryQuality = Mathf.Clamp(secondaryQuality, 0, 100),
            PrimaryType = primaryGuid,
            SecondaryType = secondaryGuid,
            SecondaryBuffIndex = (byte)Mathf.Clamp(secondaryBuffIndex, 0, 2),
            ApplyTier4SecondaryBuff = secondaryGuid != new PrefabGUID(0)
        };

        UtilsHelper.CreateEventFromCharacter(character, bloodEvent);
    }

    public static void GiveBloodPotion(
        Entity character,
        string primaryBloodType,
        string secondaryBloodType = "",
        float primaryQuality = 100,
        float secondaryQuality = 100,
        int secondaryBuffIndex = 0)
    {
        if (!TryGetBloodGuids(primaryBloodType, secondaryBloodType, out var primaryGuid, out var secondaryGuid,
                ref secondaryQuality, ref secondaryBuffIndex)) return;

        var entity = InventoryHelper.AddItemToInventory(character, Prefabs.Item_Consumable_PrisonPotion_Bloodwine, 1);
        var blood = new StoredBlood
        {
            BloodQuality = Mathf.Clamp(primaryQuality, 0, 100),
            PrimaryBloodType = primaryGuid,
            SecondaryBlood = new SecondaryBloodData
            {
                Type = secondaryGuid,
                Quality = Mathf.Clamp(secondaryQuality, 0, 100),
                BuffIndex = (byte)Mathf.Clamp(secondaryBuffIndex, 0, 2)
            }
        };

        Core.EntityManager.SetComponentData(entity, blood);
    }

    private static bool TryGetBloodGuids(
        string primaryBloodType,
        string secondaryBloodType,
        out PrefabGUID primaryGuid,
        out PrefabGUID secondaryGuid,
        ref float secondaryQuality,
        ref int secondaryBuffIndex)
    {
        primaryGuid = default;
        secondaryGuid = default;

        if (string.IsNullOrEmpty(primaryBloodType))
            return false;

        if (!UtilsHelper.TryGetPrefabGuid(primaryBloodType, out primaryGuid))
        {
            Plugin.Logger.LogWarning($"Primary Blood type guid not found for {primaryBloodType}.");
            return false;
        }

        if (string.IsNullOrEmpty(secondaryBloodType) ||
            !UtilsHelper.TryGetPrefabGuid(secondaryBloodType, out secondaryGuid))
        {
            secondaryGuid = new PrefabGUID(0);
            secondaryQuality = 0;
            secondaryBuffIndex = 0;
        }

        return true;
    }
}