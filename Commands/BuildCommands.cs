using VampireCommandFramework;

namespace ArenaBuildsMod.Commands;

internal class BuildCommands
{
    [Command("build", description: "Give a full build for arena", adminOnly: false)]
    public static void BuildCommand(ChatCommandContext ctx, string targetBuild)
    {
        if (BuildManager.Builds == null)
        {
            BuildManager.LoadData();
        }

        if (BuildManager.Builds!.TryGetValue(targetBuild, out var build))
        {
            Helper.ClearInventory(ctx.Event.SenderCharacterEntity);
            Helper.EquipBuild(ctx.Event.SenderCharacterEntity, ctx.User, build);
            Helper.GiveBloodPotion(ctx.Event.SenderCharacterEntity, build.Blood.PrimaryType, secondaryBloodType:build.Blood.SecondaryType, primaryQuality: build.Blood.PrimaryQuality, secondaryQuality: build.Blood.SecondaryQuality);
            ctx.Reply($"Equipped build {targetBuild}.");
        }
        else
        {
            ctx.Reply($"Unknown build: {targetBuild}.");
        }
    }
    
    [Command("list", description: "List available builds", adminOnly: false)]
    public static void ListBuildCommand(ChatCommandContext ctx)
    {
        if (BuildManager.Builds == null)
        {
            BuildManager.LoadData();
        }
        
        var buildList = BuildManager.GetBuildList();
        ctx.Reply($"Available builds :\n- {buildList}");
    }
}