using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VampireCommandFramework;

namespace ArenaBuilds.Commands.Converters;

public record struct ArtifactWeapon(string PrefabName, string AbilityMod1, string AbilityMod2);

internal class ArtifactWeaponConverter : CommandArgumentConverter<ArtifactWeapon>
{
   
    public static readonly Dictionary<string, (string Mod1, string Mod2)> WeaponPrefabToAbilityMods = new()
    {
        { "Item_Weapon_Axe_Unique_T08_Variation01", ("SpellMod_Axe_Frenzy_Duration", "SpellMod_Axe_Frenzy_Duration") },
        { "Item_Weapon_Claws_Unique_T08_Variation01", ("SpellMod_Claws_VaultSlash_Unholy", "SpellMod_Claws_SkeweringLeap_Unholy") },
        { "Item_Weapon_Crossbow_Unique_T08_Variation01", ("SpellMod_Crossbow_RainOfBolts_BonusArrows", "SpellMod_Crossbow_Snapshot_Fear") },
        { "Item_Weapon_Daggers_Unique_T08_Variation01", ("SpellMod_Daggers_RainOfDaggers_DamagePhantasm", "SpellMod_Daggers_CallDaggers_GainPhantasm") },
        { "Item_Weapon_GreatSword_Unique_T08_Variation01", ("SpellMod_GreatSword_GreatCleaver_ChaosDamage", "SpellMod_GreatSword_LeapAttack_Aftershock") },
        { "Item_Weapon_Longbow_Unique_T08_Variation01", ("SpellMod_Longbow_Multishot_Focus", "SpellMod_Longbow_GuidedArrow_FirstHit") },
        { "Item_Weapon_Mace_Unique_T08_Variation01", ("SpellMod_Mace_CrushingBlow_Shatter", "SpellMod_Mace_Smack_Freeze") },
        { "Item_Weapon_Pistols_Unique_T08_Variation01", ("SpellMod_Pistols_FanTheHammer_Move", "SpellMod_Pistols_ExplosiveShot_DoubleBarrel") },
        { "Item_Weapon_Reaper_Unique_T08_Variation01", ("SpellMod_Reaper_TendonSwing_Consume", "SpellMod_Reaper_HowlingReaper_SpawnSkeleton") },
        { "Item_Weapon_Slashers_Unique_T08_Variation01", ("SpellMod_Slashers_ElusiveStrike_TripleDash", "SpellMod_Slashers_Camouflage_BonusDamage") },
        { "Item_Weapon_Slashers_Unique_T08_Variation02", ("SpellMod_Slashers_ElusiveStrike_BloodFountain", "SpellMod_Slashers_Camouflage_Rupture") },
        { "Item_Weapon_Spear_Unique_T08_Variation01", ("SpellMod_Spear_AThousandSpears_BallLightning", "SpellMod_Spear_Harpoon_DragSelf") },
        { "Item_Weapon_Sword_Unique_T08_Variation01", ("SpellMod_Sword_Whirlwind_ResetDurationOnKill", "SpellMod_Sword_Shockwave_ReduceWhirlwindCDOnHit") },
        { "Item_Weapon_TwinBlades_Unique_T08_Variation01", ("SpellMod_TwinBlades_SweepingStrike_DamageToFrost", "SpellMod_TwinBlades_Javelin_Freeze") },
        { "Item_Weapon_Whip_Unique_T08_Variation01", ("SpellMod_Whip_Dash_FlameWhip", "SpellMod_Whip_Entangle_FlameWhip") },
    };


    public override ArtifactWeapon Parse(ICommandContext ctx, string input)
    {
        input = input.ToLower();

        var variation = 1;
        var lastChar = input[^1];
        if (char.IsDigit(lastChar))
        {
            input = input.Substring(0, input.Length - 1);
            variation = Mathf.Clamp(int.Parse(lastChar.ToString()), 1, 2);
        }

        var match = LegendaryWeaponConverter.BaseWeaponNames.FirstOrDefault(n => n.ToLower().Contains(input));

        if (string.IsNullOrEmpty(match))
        {
            throw ctx.Error($"Unknown weapon <color=white>{input}</color>.");
        }

        var artifactPrefabName = $"Item_Weapon_{match}_Unique_T08_Variation0{variation}";

        if (!WeaponPrefabToAbilityMods.TryGetValue(artifactPrefabName, out var abilityMods))
        {
            throw ctx.Error($"Weapon variation <color=white>{artifactPrefabName}</color> does not exist.");  
        }

        return new ArtifactWeapon(artifactPrefabName, abilityMods.Mod1, abilityMods.Mod2);
    }
}