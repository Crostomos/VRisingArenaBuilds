using System.Collections.Generic;
using ArenaBuildsMod.Models;
using ProjectM;
using ProjectM.Network;
using Unity.Entities;

namespace ArenaBuildsMod.Helpers;

internal static class WeaponHelper
{
    public static void GiveWeapons(User user, Entity character, List<WeaponData> weapons)
    {
        foreach (var weapon in weapons)
        {
            if (IsLegendary(weapon.Name))
            {
                CreateAndGiveLegendaryWeapon(user.Index, weapon);
            }
            else
            {
                CreateAndGiveBasicWeapon(character, weapon);
            }
        }
    }

    private static bool IsLegendary(string prefabName)
    {
        return prefabName.Contains("Legendary") || prefabName.Contains("T08");
    }

    private static void CreateAndGiveBasicWeapon(Entity character, WeaponData weaponData)
    {
        if (UtilsHelper.TryGetPrefabGuid(weaponData.Name, out var weaponGuid))
        {
            InventoryHelper.AddItemToInventory(character, weaponGuid, 1);
        }
    }

    private static void CreateAndGiveLegendaryWeapon(int userIndex, WeaponData weaponData)
    {
        if (UtilsHelper.TryGetPrefabGuid(weaponData.Name, out var weaponGuid))
        {
            var debugEventsSystem = Core.Server.GetExistingSystemManaged<DebugEventsSystem>();
            var legendaryWeaponEvent = new CreateLegendaryWeaponDebugEvent
            {
                WeaponPrefabGuid = weaponGuid,
                Tier = 4,
                InfuseSpellMod = UtilsHelper.GetPrefabGuid(weaponData.InfuseSpellMod) ?? default, 
                // TODO SpellMod1 & SpellMod2
                StatMod1 = UtilsHelper.GetPrefabGuid(weaponData.StatMod1) ?? default,
                StatMod1Power = weaponData.StatMod1Power,
                StatMod2 = UtilsHelper.GetPrefabGuid(weaponData.StatMod2) ?? default,
                StatMod2Power = weaponData.StatMod2Power,
                StatMod3 = UtilsHelper.GetPrefabGuid(weaponData.StatMod3) ?? default,
                StatMod3Power = weaponData.StatMod3Power,
                StatMod4 = UtilsHelper.GetPrefabGuid(weaponData.StatMod4) ?? default,
                StatMod4Power = weaponData.StatMod4Power,
            };

            debugEventsSystem.CreateLegendaryWeaponEvent(userIndex, ref legendaryWeaponEvent);
        }
    }
}