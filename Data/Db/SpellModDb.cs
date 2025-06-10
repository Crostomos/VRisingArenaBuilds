using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using ArenaBuilds.Models.Interfaces;

namespace ArenaBuilds.Data.Db;

internal class SpellModDb : IDatabase
{
    public static List<string> Mods = [];

    private static readonly List<string> OutdatedMods =
    [
        "SpellMod_BloodRage_IncreaseArea",
        "SpellMod_BloodRite_BonusDaggers",
        "SpellMod_BloodRite_DaggerBonusDamage",
        "SpellMod_BloodRite_ConsumeLeechHealXTimes",
        "SpellMod_BloodRite_ConsumeLeechReduceCooldownXTimes",
        "SpellMod_Shadowbolt_ForkOnHit",
        "SpellMod_VeilOfBlood_BloodNova",
        "SpellMod_VeilOfBlood_BloodNovaArea",
        "SpellMod_BloodFountain_ConsumeLeechBonusDamage",
        "SpellMod_BloodFountain_IncreaseArea",
        "SpellMod_CorruptedSkull_DetonateSkeleton",
        "SpellMod_VeilOfBones_SpawnSkeleton",
        "SpellMod_DeathKnight_BonusDamageBelowTreshhold",
        "SpellMod_DeathKnight_SkeletonMageOnDeath",
        "SpellMod_Soulburn_ConsumeSkeletonEmpower",
        "SpellMod_Soulburn_ConsumeSkeletonHeal",
        "SpellMod_Soulburn_IncreasedSilenceDuration",
        "SpellMod_Soulburn_SpellMod_Soulburn_ReduceCooldownOnSilence",
        "SpellMod_ChainsOfDeath_Explosion",
        "SpellMod_PhantomAegis_ConsumeWeakenIntoFear",
        "SpellMod_VeilOfIllusion_IllusionProjectileDamage",
        "SpellMod_IceNova_IncreaseRadius",
        "SpellMod_ColdSnap_Immaterial",
        "SpellMod_VeilOfFrost_BonusDamage",
        "SpellMod_FrostCone_FrostWave",
        "SpellMod_Discharge_Immaterial",
        "SpellMod_Discharge_IncreaseStormShieldDuration",
        "SpellMod_Cyclone_IncreaseLifetime",
        "SpellMod_Cyclone_ReducedDamageReduction",
        "SpellMod_VeilOfStorm_RecastIllusionDash",
    ];

    // Shared
    public const string ApplyFadingSnareLong = "SpellMod_Shared_ApplyFadingSnare_Long";
    public const string ApplyFadingSnareMedium = "SpellMod_Shared_ApplyFadingSnare_Medium";
    public const string ApplyFadingSnareShort = "SpellMod_Shared_ApplyFadingSnare_Short";
    public const string DispellDebuffs = "SpellMod_Shared_DispellDebuffs";
    public const string IncreaseMoveSpeedDuringChannelHigh = "SpellMod_Shared_IncreaseMoveSpeedDuringChannel_High";
    public const string AddCharges = "SpellMod_Shared_AddCharges";
    public const string ProjectileRangeAndVelocity = "SpellMod_Shared_Projectile_RangeAndVelocity";
    public const string CastRate = "SpellMod_Shared_CastRate";
    public const string CooldownMedium = "SpellMod_Shared_Cooldown_Medium";
    public const string ProjectileIncreaseRangeMedium = "SpellMod_Shared_Projectile_IncreaseRange_Medium";
    public const string IncreaseMoveSpeedDuringChannelLow = "SpellMod_Shared_IncreaseMoveSpeedDuringChannel_Low";
    public const string KnockBackOnHitLight = "SpellMod_Shared_KnockBackOnHit_Light";
    public const string KnockBackOnHitMedium = "SpellMod_Shared_KnockBackOnHit_Medium";
    public const string TargetAoEIncreaseRangeMedium = "SpellMod_Shared_TargetAoE_IncreaseRange_Medium";
    public const string TravelBuffIncreaseRangeMedium = "SpellMod_Shared_TravelBuff_IncreaseRange_Medium";
    public const string MovementSpeedNormal = "SpellMod_Shared_MovementSpeed_Normal";

    // Blood
    public const string BloodConsumeLeechSelfHealBig = "SpellMod_Shared_Blood_ConsumeLeechSelfHeal_Big";

    // Chaos
    public const string ChaosConsumeIgniteAgonizingFlames = "SpellMod_Shared_Chaos_ConsumeIgniteAgonizingFlames";

    // Frost
    public const string FrostIncreaseFreezeWhenChill = "SpellMod_Shared_Frost_IncreaseFreezeWhenChill";
    public const string FrostWeapon = "SpellMod_Shared_FrostWeapon";
    public const string FrostSplinterNovaOnFrosty = "SpellMod_Shared_Frost_SplinterNovaOnFrosty";
    public const string FrostShieldOnFrosty = "SpellMod_Shared_Frost_ShieldOnFrosty";
    public const string FrostConsumeChillIntoFreezeRecast = "SpellMod_Shared_Frost_ConsumeChillIntoFreeze_Recast";

    // Illusion
    public const string IllusionConsumeWeakenSpawnWisp = "SpellMod_Shared_Illusion_ConsumeWeakenSpawnWisp";
    public const string IllusionWeakenShieldOnAttack = "SpellMod_Shared_Illusion_WeakenShield_OnAttack";
    public const string IllusionWeakenShield = "SpellMod_Shared_Illusion_WeakenShield";

    // Storm
    public const string StormConsumeStaticIntoStun = "SpellMod_Shared_Storm_ConsumeStaticIntoStun";
    public const string StormConsumeStaticIntoWeaponCharge = "SpellMod_Shared_Storm_ConsumeStaticIntoWeaponCharge";
    public const string StormConsumeStaticIntoStunExplode = "SpellMod_Shared_Storm_ConsumeStaticIntoStun_Explode";
    public const string StormGrantWeaponCharge = "SpellMod_Shared_Storm_GrantWeaponCharge";

    // Unholy
    public const string UnholySkeletonBomb = "SpellMod_Shared_Unholy_SkeletonBomb";

    // Veil
    public const string VeilBonusDamageOnPrimary = "SpellMod_Shared_Veil_BonusDamageOnPrimary";
    public const string VeilBuffAndIllusionDuration = "SpellMod_Shared_Veil_BuffAndIllusionDuration";

    public void Init()
    {
        foreach (var ability in AbilityDb.Abilities)
        {
            AddToList(ability.Name, spellSchool: ability.SpellSchool);
        }

        AddToList("Shared");
    }

    private static void AddToList(string name, string spellSchool = "")
    {
        List<string> regexList =
        [
            $"^SpellMod_{name}_[a-zA-Z]+$",
            $"^SpellMod_{spellSchool}_{name}_[a-zA-Z]+$"
        ];

        foreach (var item in regexList)
        {
            var regex = new Regex(item, RegexOptions.IgnoreCase);

            var matchingProperties = typeof(Prefabs)
                .GetFields(BindingFlags.Static | BindingFlags.Public)
                .Where(prop => regex.IsMatch(prop.Name))
                .ToList();

            foreach (var prop in matchingProperties.Where(prop => !OutdatedMods.Contains(prop.Name)))
            {
                Mods.Add(prop.Name);
            }
        }
    }
}