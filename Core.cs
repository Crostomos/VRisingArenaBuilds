using System;
using ProjectM.Scripting;
using Unity.Entities;

namespace ArenaBuildsMod;

internal class Core
{
    public static World Server { get; } = GetWorld("Server") ??
                                           throw new Exception(
                                               "There is no Server world (yet). Did you install a server mod on the client?");
    
    public static ServerGameManager ServerGameManager => Server.GetExistingSystemManaged<ServerScriptMapper>()._ServerGameManager;

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


