using System.Collections.Generic;

namespace ArenaBuilds.Models.CommandArguments;

public class ArtifactWeaponModel(string name, string basePrefabName, List<string> argNames)
    : WeaponModel(name, basePrefabName, argNames)
{
    public string SpellMod1 { get; set; } = string.Empty;

    public string SpellMod2 { get; set; } = string.Empty;

    public int Variation { get; set; } = 1;

    public static ArtifactWeaponModel FromWeaponModel(WeaponModel weapon)
    {
        return new ArtifactWeaponModel(weapon.Name, weapon.BasePrefabName, weapon.ArgNames)
        {
            PrefabName = weapon.PrefabName
        };
    }
}