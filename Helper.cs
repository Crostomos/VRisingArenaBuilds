using System.Linq;
using System.Reflection;
using ArenaBuildsMod.Models;
using Data;
using ProjectM;
using ProjectM.Network;
using ProjectM.Shared;
using Stunlock.Core;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using Exception = System.Exception;

namespace ArenaBuildsMod;

internal static class Helper
{
    public static void EquipBuild(Entity playerCharEntity, User user, BuildData build)
    {
        foreach (var item in build.Items)
        {
            if (TryGetPrefabGuid(item.Name, out var guid))
            {
                var itemEntity = AddItemToInventory(playerCharEntity, guid, item.Amount);


                    var debugEventsSystem = Core.Server.GetExistingSystemManaged<DebugEventsSystem>();

                    var legendaryWeaponEvent = new CreateLegendaryWeaponDebugEvent
                    {
                        WeaponPrefabGuid = guid,
                        Tier = 4,
                        InfuseSpellMod = Prefabs.SpellMod_Axe_XStrike_MultiThrow,
                        StatMod1 = Prefabs.StatMod_MaxHealth,
                        StatMod1Power = 1,
                        // StatMod2 = default,
                        // StatMod2Power = 0,
                        // StatMod3 = default,
                        // StatMod3Power = 0,
                        // StatMod4 = default,
                        // StatMod4Power = 0
                    };
                
                    debugEventsSystem.CreateLegendaryWeaponEvent(user.Index, ref legendaryWeaponEvent);
                

                var slot = InventoryUtilities.GetItemSlot(Core.EntityManager, playerCharEntity, guid, itemEntity);
                EquipEquipment(playerCharEntity, slot);
            }
            else
            {
                Plugin.Logger.LogWarning($"Item guid not found for {item.Name}");
            }
        }

        foreach (var ability in build.Abilities)
        {
            if (TryGetPrefabGuid(ability.Name, out var guid))
            {
                EquipAbility(playerCharEntity, guid, ability.Slot);
                Plugin.Logger.LogInfo($"Ability {ability.Name} ({guid}) has been equipped");
            }
            else
            {
                Plugin.Logger.LogWarning($"Ability guid not found for {ability.Name}");
            }
        }
    }


    private static Entity AddItemToInventory(Entity recipient, PrefabGUID guid, int amount)
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

    private static void EquipEquipment(Entity playerCharEntity, int slot)
    {
        var entity = Core.EntityManager.CreateEntity(ComponentType.ReadWrite<FromCharacter>(),
            ComponentType.ReadWrite<EquipItemEvent>());
        var userEntity = GetUserEntity(playerCharEntity);
        Core.EntityManager.SetComponentData<FromCharacter>(entity,
            new() { User = userEntity, Character = playerCharEntity });
        Core.EntityManager.SetComponentData<EquipItemEvent>(entity, new() { SlotIndex = slot });
    }

    private static void EquipAbility(Entity playerCharEntity, PrefabGUID guid, AbilitySlot slot)
    {
        var buffEntity = Entity.Null;
        Core.ServerGameManager.ModifyAbilityGroupOnSlot(buffEntity, playerCharEntity, (int)slot, guid);
    }

    private static void EquipSpellPassive(Entity playerCharEntity, int slot)
    {
        // TODO
        // var entity = Core.EntityManager.CreateEntity(ComponentType.ReadWrite<FromCharacter>(),
        //     ComponentType.ReadWrite<UnlockSpellSchoolPassive>());
        // var userEntity = GetUserEntity(playerCharEntity);
        // Core.EntityManager.SetComponentData<FromCharacter>(entity,
        //     new() { User = userEntity, Character = playerCharEntity });
        // Core.EntityManager.SetComponentData<SpellModPrefabGuidSettings>(entity, new()
        // {
        //
        // });
    }

    public static void ClearInventory(Entity localCharacterEntity)
    {
        InventoryUtilitiesServer.ClearInventory(Core.EntityManager, localCharacterEntity);

        var equipment = Core.EntityManager.GetComponentData<Equipment>(localCharacterEntity);
        DestroyTargetEntity(equipment.ArmorHeadgearSlot.SlotEntity._Entity);
        DestroyTargetEntity(equipment.ArmorChestSlot.SlotEntity._Entity);
        DestroyTargetEntity(equipment.ArmorGlovesSlot.SlotEntity._Entity);
        DestroyTargetEntity(equipment.ArmorLegsSlot.SlotEntity._Entity);
        DestroyTargetEntity(equipment.ArmorFootgearSlot.SlotEntity._Entity);
        DestroyTargetEntity(equipment.CloakSlot.SlotEntity._Entity);
        DestroyTargetEntity(equipment.WeaponSlot.SlotEntity._Entity);
        DestroyTargetEntity(equipment.BagSlot.SlotEntity._Entity);
    }

    public static void GiveBloodPotion(Entity playerCharEntity, string primaryBloodType,
        string secondaryBloodType = "BloodType_None", float primaryQuality = 100, float secondaryQuality = 100)
    {
        if (TryGetPrefabGuid(primaryBloodType, out var primaryBloodTypeGuid))
        {
            if (TryGetPrefabGuid(secondaryBloodType, out var secondaryBloodTypeGuid))
            {
                primaryQuality = Mathf.Clamp(primaryQuality, 0, 100);
                secondaryQuality = Mathf.Clamp(secondaryQuality, 0, 100);

                var entity = AddItemToInventory(playerCharEntity, Prefabs.Item_Consumable_PrisonPotion_Bloodwine, 1);
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
                Plugin.Logger.LogWarning($"Secondary Blood type guid not found for {secondaryBloodType}");
            }
        }
        else
        {
            Plugin.Logger.LogWarning($"Primary Blood type guid not found for {primaryBloodType}");
        }
    }

    private static bool TryGetPrefabGuid(string name, out PrefabGUID guid)
    {
        guid = default;

        var type = typeof(Prefabs);
        var field = type.GetField(name, BindingFlags.Public | BindingFlags.Static);

        if (field != null && field.FieldType == typeof(PrefabGUID))
        {
            guid = (PrefabGUID)field.GetValue(null)!;
            return true;
        }

        return false;
    }

    private static void DestroyTargetEntity(Entity entity)
    {
        if (entity != Entity.Null)
        {
            DestroyUtility.Destroy(Core.EntityManager, entity);
            ;
        }
    }

    public static void UnlockAll(Entity user, Entity character)
    {
        var fromCharacter = new FromCharacter()
        {
            User = user,
            Character = character
        };

        var debugEventsSystem = Core.Server.GetExistingSystemManaged<DebugEventsSystem>();
        debugEventsSystem.UnlockAllResearch(fromCharacter);
        debugEventsSystem.UnlockAllVBloods(fromCharacter);
        debugEventsSystem.CompleteAllAchievements(fromCharacter);
    }

    public static Entity GetUserEntity(Entity player)
    {
        var playerchar = Core.EntityManager.GetComponentData<PlayerCharacter>(player);
        return playerchar.UserEntity;
    }

    public static void ShowComponentsFromEntity(Entity entity)
    {
        var components = Core.EntityManager.GetComponentTypes(entity);

        foreach (var comp in components)
        {
            Plugin.Logger.LogInfo($"Composant: {comp.GetManagedType().Name}");
        }
    }
}