using ProjectM;
using Unity.Entities;
using VampireCommandFramework;

namespace ArenaBuildsMod.Commands;

internal class TestCommands
{
    [Command("test", description: "Test", adminOnly: false)]
    public static void TestCommand(ChatCommandContext ctx)
    {

        var playerCharEntity = ctx.Event.SenderCharacterEntity;
       // Helper.UnlockAll(ctx.Event.SenderUserEntity, ctx.Event.SenderCharacterEntity);
        
        var buffer2 = Core.EntityManager.GetBuffer<ActivePassivesBuffer>(ctx.Event.SenderCharacterEntity);

        for (var i = 0; i < buffer2.Length; ++i)
        {
            var passive = new SpellSchoolPassive
            {
                Passive = Data.Prefabs.SpellPassive_Blood_T01_BloodSpray,
                Tier = SpellSchoolProgressionTier.Tier4
            };
            Plugin.Logger.LogInfo($"count - {i}");
            Core.EntityManager.SetComponentData(buffer2[i].Entity, passive);
        }

        ctx.Reply($"FEUR");
    }
}
