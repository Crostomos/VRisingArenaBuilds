using System;
using ProjectM;
using ProjectM.Gameplay.Systems;
using ProjectM.Scripting;
using Unity.Entities;

namespace ArenaBuilds;

internal class Core
{
    public static World Server => GetWorld("Server") ?? throw new Exception("There is no Server world.");

    public static PrefabCollectionSystem PrefabCollectionSystem => Server.GetExistingSystemManaged<PrefabCollectionSystem>();

    public static ActivateVBloodAbilitySystem ActivateVBloodAbilitySystem => Server.GetExistingSystemManaged<ActivateVBloodAbilitySystem>();

    public static ServerGameManager ServerGameManager => Server.GetExistingSystemManaged<ServerScriptMapper>()._ServerGameManager;

    public static DebugEventsSystem DebugEventsSystem => Server.GetExistingSystemManaged<DebugEventsSystem>();

    public static EntityManager EntityManager => Server.EntityManager;

    private static World GetWorld(string name)
    {
        foreach (var world in World.s_AllWorlds)
        {
            if (world.Name == name)
            {
                return world;
            }
        }

        return null;
    }
}