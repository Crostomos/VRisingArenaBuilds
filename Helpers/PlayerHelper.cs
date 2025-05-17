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

        Core.DebugEventsSystem.UnlockAllResearch(fromCharacter);
        Core.DebugEventsSystem.UnlockAllVBloods(fromCharacter);
        Core.DebugEventsSystem.CompleteAllAchievements(fromCharacter);
    }
}