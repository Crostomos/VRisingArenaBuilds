using ArenaBuilds.Data.Db;
using ArenaBuilds.Extensions;
using ArenaBuilds.Models.CommandArguments;
using VampireCommandFramework;

namespace ArenaBuilds.Commands.Converters;

internal class ArtifactWeaponConverter : CommandArgumentConverter<ArtifactWeaponModel>
{
    public override ArtifactWeaponModel Parse(ICommandContext ctx, string input)
    {
        var variation = 1;
        var lastChar = input[^1];
        if (char.IsDigit(lastChar))
        {
            input = input[..^1];
            variation = int.Parse(lastChar.ToString());
        }

        var weapon =
            WeaponDb.Weapons.EqualsCommandArgument(input) as WeaponModel ??
            (WeaponDb.Weapons.ContainsCommandArgument(input) as WeaponModel ??
             throw ctx.Error($"Unknown weapon <color=white>{input}</color>."));

        weapon.SetArtifactPrefab(variation: variation);

        if (!WeaponDb.ArtifactWeaponPrefabToAbilityMods.TryGetValue(weapon.PrefabName, out var abilityMods))
        {
            throw ctx.Error(
                $"Weapon variation <color=white>{variation}</color> does not exist for <color=white>{weapon.Name}</color>.");
        }

        var artifactWeapon = ArtifactWeaponModel.FromWeaponModel(weapon);
        artifactWeapon.Variation = variation;
        artifactWeapon.SpellMod1 = abilityMods.Mod1;
        artifactWeapon.SpellMod2 = abilityMods.Mod2;

        return artifactWeapon;
    }
}