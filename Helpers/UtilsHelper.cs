using System.Reflection;
using Data;
using ProjectM;
using ProjectM.Network;
using ProjectM.Shared;
using Stunlock.Core;
using Unity.Entities;

namespace ArenaBuildsMod.Helpers;

internal static class UtilsHelper
{
    public static bool TryGetPrefabGuid(string name, out PrefabGUID guid)
    {
        var temp = GetPrefabGuid(name);
        if (temp is not null)
        {
            guid = temp.Value;
            return true;
        }

        guid = default;
        return false;
    }

    public static PrefabGUID? GetPrefabGuid(string name)
    {
        var type = typeof(Prefabs);
        var field = type.GetField(name, BindingFlags.Public | BindingFlags.Static);

        return field != null && field.FieldType == typeof(PrefabGUID) ? (PrefabGUID?)field.GetValue(null) : null;
    }

    public static void CreateEventFromCharacter<T>(Entity character, T eventData)
    {
        var entity = Core.EntityManager.CreateEntity(
            ComponentType.ReadWrite<FromCharacter>(),
            ComponentType.ReadWrite<T>()
        );
        var userEntity = GetUserEntity(character);
        Core.EntityManager.SetComponentData(entity,
            new FromCharacter { User = userEntity, Character = character });
        Core.EntityManager.SetComponentData(entity, eventData);
    }

    public static void DestroyTargetEntity(Entity entity)
    {
        if (entity != Entity.Null)
        {
            DestroyUtility.Destroy(Core.EntityManager, entity);
        }
    }

    public static Entity GetUserEntity(Entity character)
    {
        var playerCharacter = Core.EntityManager.GetComponentData<PlayerCharacter>(character);
        return playerCharacter.UserEntity;
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