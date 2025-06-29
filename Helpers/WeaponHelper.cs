﻿using System.Collections.Generic;
using ArenaBuilds.Data.Db;
using ArenaBuilds.Models;
using ProjectM;
using ProjectM.Network;
using ProjectM.Shared;
using Stunlock.Core;
using Unity.Entities;
using UnityEngine;

namespace ArenaBuilds.Helpers;

internal static class WeaponHelper
{
    public static void GiveWeapons(User user, Entity character, List<WeaponData> weapons)
    {
        foreach (var weaponData in weapons)
        {
            if (IsLegendary(weaponData.Name))
            {
                CreateAndGiveLegendaryWeapon(user.Index, weaponData);
            }
            else if (IsArtifact(weaponData.Name))
            {
                CreateAndGiveArtifactWeapon(user.Index, character, weaponData);
            }
            else
            {
                CreateAndGiveWeapon(character, weaponData);
            }
        }
    }

    public static void CreateAndGiveLegendaryWeapon(int userIndex, WeaponData weaponData)
    {
        if (string.IsNullOrEmpty(weaponData.Name)) return;
        if (!UtilsHelper.TryGetPrefabGuid(weaponData.Name, out var weaponGuid))
        {
            Plugin.Logger.LogWarning($"Weapon guid not found for {weaponData.Name}.");
            return;
        }

        var legendaryWeaponEvent = new CreateLegendaryWeaponDebugEvent
        {
            WeaponPrefabGuid = weaponGuid,
            Tier = 8,
            InfuseSpellMod = UtilsHelper.GetPrefabGuid(weaponData.InfuseSpellMod) ?? default,
            StatMod1 = UtilsHelper.GetPrefabGuid(weaponData.StatMod1) ?? default,
            StatMod1Power = Mathf.Clamp(weaponData.StatMod1Power, 0, 1),
            StatMod2 = UtilsHelper.GetPrefabGuid(weaponData.StatMod2) ?? default,
            StatMod2Power = Mathf.Clamp(weaponData.StatMod2Power, 0, 1),
            StatMod3 = UtilsHelper.GetPrefabGuid(weaponData.StatMod3) ?? default,
            StatMod3Power = Mathf.Clamp(weaponData.StatMod3Power, 0, 1),
            StatMod4 = UtilsHelper.GetPrefabGuid(weaponData.StatMod4) ?? default,
            StatMod4Power = Mathf.Clamp(weaponData.StatMod4Power, 0, 1),
        };

        Core.DebugEventsSystem.CreateLegendaryWeaponEvent(userIndex, ref legendaryWeaponEvent);
    }

    public static void CreateAndGiveArtifactWeapon(int userIndex, Entity character, WeaponData weaponData)
    {
        CreateAndGiveLegendaryWeapon(userIndex, weaponData);
        var weaponEntity = GetCreatedWeapon(character, weaponData);

        if (string.IsNullOrEmpty(weaponData.SpellMod1))
        {
            weaponData.SpellMod1 = GetDefaultSpellMod(weaponData.Name, 1);
        }
        
        if (string.IsNullOrEmpty(weaponData.SpellMod2))
        {
            weaponData.SpellMod2 = GetDefaultSpellMod(weaponData.Name, 2);
        }
        
        if (HasValidSpellMods(weaponData))
        {
            SetAbilityMod(weaponEntity, weaponData);
        }
        else
        {
            Plugin.Logger.LogWarning($"Wrong SpellMod for {weaponData.Name}");
        }
    }
    
    private static bool IsLegendary(string prefabName)
    {
        return prefabName.Contains("Legendary");
    }

    private static bool IsArtifact(string prefabName)
    {
        return prefabName.Contains("Unique_T08");
    }

    private static string GetWeaponNameType(WeaponData weaponData)
    {
        return weaponData.Name.Split('_')[2];
    }
    
    private static string GetDefaultSpellMod(string weaponName, int modIndex)
    {
        if (WeaponDb.ArtifactWeaponPrefabToAbilityMods.TryGetValue(weaponName, out var abilityMods))
        {
            switch (modIndex)
            {
                case 1:
                    return abilityMods.Mod1;
                case 2:
                    return abilityMods.Mod2;
            }
        }

        return string.Empty;
    }

    private static bool HasValidSpellMods(WeaponData weaponData)
    {
        var weaponType = GetWeaponNameType(weaponData);
        return weaponData.SpellMod1.Contains(weaponType) && weaponData.SpellMod2.Contains(weaponType);
    }

    private static void CreateAndGiveWeapon(Entity character, WeaponData weaponData)
    {
        if (string.IsNullOrEmpty(weaponData.Name)) return;
        if (UtilsHelper.TryGetPrefabGuid(weaponData.Name, out var weaponGuid))
        {
            InventoryHelper.AddItemToInventory(character, weaponGuid, 1);
        }
        else
        {
            Plugin.Logger.LogWarning($"Weapon guid not found for {weaponData.Name}.");
        }
    }

    private static SpellMod CreateSpellMod(string statMod, float statPower)
    {
        return new SpellMod
        {
            Id = UtilsHelper.GetPrefabGuid(statMod) ?? default,
            Power = statPower
        };
    }

    private static void SetAbilityMod(Entity weapon, WeaponData weaponData)
    {
        if (weapon == new Entity()) return;

        var spellModSetComponent = Core.EntityManager.GetComponentData<LegendaryItemSpellModSetComponent>(weapon);

        var newAbilityMod1 = new SpellModSet
        {
            Mod0 = CreateSpellMod(weaponData.SpellMod1, 1),
            Count = 1
        };

        var newAbilityMod2 = new SpellModSet
        {
            Mod0 = CreateSpellMod(weaponData.SpellMod2, 1),
            Count = 1
        };

        spellModSetComponent.AbilityMods0 = newAbilityMod1;
        spellModSetComponent.AbilityMods1 = newAbilityMod2;

        Core.EntityManager.SetComponentData(weapon, spellModSetComponent);
    }

    private static Entity GetCreatedWeapon(Entity character, WeaponData weaponData)
    {
        if (!UtilsHelper.TryGetPrefabGuid(weaponData.Name, out var weaponGuid) || 
            !InventoryUtilities.TryGetInventory(Core.EntityManager, character, out var inventory)) 
            return new Entity();

        var statMod1Guid = UtilsHelper.GetPrefabGuid(weaponData.StatMod1);
        var statMod2Guid = UtilsHelper.GetPrefabGuid(weaponData.StatMod2);
        var statMod3Guid = UtilsHelper.GetPrefabGuid(weaponData.StatMod3);
        var statMod4Guid = UtilsHelper.GetPrefabGuid(weaponData.StatMod4);
        var infuseSpellGuid = UtilsHelper.GetPrefabGuid(weaponData.InfuseSpellMod);
        var spellMod1Guid = UtilsHelper.GetPrefabGuid(weaponData.SpellMod1);
        var spellMod2Guid = UtilsHelper.GetPrefabGuid(weaponData.SpellMod2);

        foreach (var item in inventory)
        {
            var itemEntity = item.ItemEntity._Entity;
            
            if (itemEntity == default ||
                !Core.EntityManager.HasComponent<PrefabGUID>(itemEntity) ||
                !Core.EntityManager.HasComponent<LegendaryItemSpellModSetComponent>(itemEntity))
                continue;

            var prefabGuid = Core.EntityManager.GetComponentData<PrefabGUID>(itemEntity);
            if (prefabGuid != weaponGuid) continue;

            var spellModSet = Core.EntityManager.GetComponentData<LegendaryItemSpellModSetComponent>(itemEntity);
            var mods = spellModSet.StatMods;

            if ((statMod1Guid != null && mods.Mod0.Id != statMod1Guid) ||
                (statMod2Guid != null && mods.Mod1.Id != statMod2Guid) ||
                (statMod3Guid != null && mods.Mod2.Id != statMod3Guid) ||
                (statMod4Guid != null && mods.Mod3.Id != statMod4Guid) ||
                (infuseSpellGuid != null && spellModSet.AbilityMods0.Mod0.Id != infuseSpellGuid) ||
                (spellMod1Guid != null && spellModSet.AbilityMods0.Mod0.Id == spellMod1Guid) ||
                (spellMod2Guid != null && spellModSet.AbilityMods1.Mod0.Id == spellMod2Guid))
                continue;

            return itemEntity;
        }

        return new Entity();
    }
}