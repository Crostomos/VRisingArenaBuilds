using System.Collections.Generic;
using ArenaBuilds.Models.Interfaces;

namespace ArenaBuilds.Models.CommandArguments;

public class WeaponModel(string name, string basePrefabName, List<string> argNames) : ICommandArgument
{
    public string Name { get; set; } = name;

    public string BasePrefabName { get; set; } = basePrefabName;

    public string PrefabName { get; set; } = $"Item_Weapon_{basePrefabName}_T05_Iron";

    public List<string> ArgNames { get; set; } = argNames;

    public void SetLegendaryPrefab()
    {
        PrefabName = $"Item_Weapon_{BasePrefabName}_Legendary_T08";
    }

    public void SetArtifactPrefab(int variation = 1)
    {
        PrefabName = $"Item_Weapon_{BasePrefabName}_Unique_T08_Variation0{variation}";
    }
}