using ProjectM.Network;
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
        if (string.IsNullOrEmpty(primaryBloodType)) return;
        if (UtilsHelper.TryGetPrefabGuid(primaryBloodType, out var primaryBloodTypeGuid))
        {
            if (string.IsNullOrEmpty(primaryBloodType) ||
                !UtilsHelper.TryGetPrefabGuid(secondaryBloodType, out var secondaryBloodTypeGuid))
            {
                secondaryBloodTypeGuid = new PrefabGUID(0);
                secondaryQuality = 0;
                secondaryBuffIndex = 0;
            }

            var bloodEvent = new ConsumeBloodAdminEvent
            {
                Amount = amount,
                PrimaryQuality = Mathf.Clamp(primaryQuality, 0, 100),
                SecondaryQuality = Mathf.Clamp(secondaryQuality, 0, 100),
                PrimaryType = primaryBloodTypeGuid,
                SecondaryType = secondaryBloodTypeGuid,
                SecondaryBuffIndex = (byte)Mathf.Clamp(secondaryBuffIndex, 0, 2),
                ApplyTier4SecondaryBuff = secondaryBloodTypeGuid != new PrefabGUID(0)
            };

            UtilsHelper.CreateEventFromCharacter(character, bloodEvent);
        }
        else
        {
            Plugin.Logger.LogWarning($"Primary Blood type guid not found for {primaryBloodType}.");
        }
    }
}