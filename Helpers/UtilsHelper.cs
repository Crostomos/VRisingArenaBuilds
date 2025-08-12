using System.Reflection;
using ArenaBuilds.Data;
using Il2CppInterop.Runtime;
using ProjectM;
using ProjectM.Network;
using Stunlock.Core;
using Unity.Collections;
using Unity.Entities;

namespace ArenaBuilds.Helpers;

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

    public static NativeArray<Entity> GetEntitiesByComponentType<T1>(
        bool includeAll = false,
        bool includeDisabled = false,
        bool includeSpawn = false,
        bool includePrefab = false,
        bool includeDestroyed = false)
    {
        var options = EntityQueryOptions.Default;
        if (includeAll) options |= EntityQueryOptions.IncludeAll;
        if (includeDisabled) options |= EntityQueryOptions.IncludeDisabled;
        if (includeSpawn) options |= EntityQueryOptions.IncludeSpawnTag;
        if (includePrefab) options |= EntityQueryOptions.IncludePrefab;
        if (includeDestroyed) options |= EntityQueryOptions.IncludeDestroyTag;

        var entityQueryBuilder = new EntityQueryBuilder(Allocator.Temp)
            .AddAll(new ComponentType(Il2CppType.Of<T1>()))
            .WithOptions(options);

        var query = Core.EntityManager.CreateEntityQuery(ref entityQueryBuilder);

        var entities = query.ToEntityArray(Allocator.Temp);
        return entities;
    }

    public static Entity GetUserEntity(Entity character)
    {
        var playerCharacter = Core.EntityManager.GetComponentData<PlayerCharacter>(character);
        return playerCharacter.UserEntity;
    }
}