using ProjectM;
using ProjectM.Network;
using Unity.Entities;

namespace ArenaBuildsMod.Helpers;

internal static class PlayerHelper
{
    public static void UnlockAll(Entity user, Entity character)
    {
        var fromCharacter = new FromCharacter
        {
            User = user,
            Character = character
        };

        var debugEventsSystem = Core.Server.GetExistingSystemManaged<DebugEventsSystem>();
        debugEventsSystem.UnlockAllResearch(fromCharacter);
        debugEventsSystem.UnlockAllVBloods(fromCharacter);
        debugEventsSystem.CompleteAllAchievements(fromCharacter);
    }
}