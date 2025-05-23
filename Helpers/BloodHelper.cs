using ProjectM.Network;
using Unity.Entities;

namespace ArenaBuildsMod.Helpers;

internal static class BloodHelper
{
    public static void SetBlood(
        Entity character,
        string primaryBloodType,
        string secondaryBloodType = "BloodType_None",
        float primaryQuality = 100,
        float secondaryQuality = 100,
        int amount = 100)
    {
        if (UtilsHelper.TryGetPrefabGuid(primaryBloodType, out var primaryBloodTypeGuid))
        {
            if (UtilsHelper.TryGetPrefabGuid(secondaryBloodType, out var secondaryBloodTypeGuid))
            {
                var bloodEvent = new ConsumeBloodAdminEvent
                {
                    Amount = amount,
                    PrimaryQuality = primaryQuality,
                    SecondaryQuality = secondaryQuality,
                    PrimaryType = primaryBloodTypeGuid,
                    SecondaryType = secondaryBloodTypeGuid,
                    SecondaryBuffIndex = 1,
                    ApplyTier4SecondaryBuff = true
                };

                UtilsHelper.CreateEventFromCharacter(character, bloodEvent);
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
}