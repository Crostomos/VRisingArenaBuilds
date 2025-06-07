using System.Collections.Generic;
using ArenaBuilds.Models.Interfaces;

namespace ArenaBuilds.Data.Db;

public class AbilityToSpellModDb : IDatabase
{
    public static Dictionary<string, List<string>> AbilityToSpellMod = new()
    {
        // Blood
        {
            "BloodRage", [SpellModDb.ApplyFadingSnareMedium, SpellModDb.DispellDebuffs]
        },
        {
            "BloodRite", [SpellModDb.IncreaseMoveSpeedDuringChannelHigh]
        },
        {
            "CarrionSwarm", [
                SpellModDb.ApplyFadingSnareMedium,
                "SpellMod_CarrionSwam_BonusDamage", // nice spell mistake :D
                "SpellMod_CarrionSwam_Explode",
                "SpellMod_CarrionSwam_Leech",
                "SpellMod_CarrionSwam_StunOnHit",
                "SpellMod_CarrionSwam_VampiricCurse"
            ]
        },
        {
            "SanguineCoil", [SpellModDb.AddCharges, SpellModDb.ProjectileRangeAndVelocity]
        },
        {
            "Shadowbolt",
            [
                SpellModDb.BloodConsumeLeechSelfHealBig,
                SpellModDb.CastRate,
                SpellModDb.CooldownMedium,
                SpellModDb.KnockBackOnHitMedium,
                SpellModDb.ProjectileRangeAndVelocity
            ]
        },
        {
            "VeilOfBlood", [SpellModDb.VeilBonusDamageOnPrimary, SpellModDb.VeilBuffAndIllusionDuration]
        },
        // Chaos
        {
            "Aftershock",
            [
                SpellModDb.ChaosConsumeIgniteAgonizingFlames,
                SpellModDb.CooldownMedium,
                SpellModDb.ProjectileIncreaseRangeMedium,
                // "SpellMod_Chaos_Aftershock_BonusDamage",
                // "SpellMod_Chaos_Aftershock_InflictSlowOnProjectile",
                // "SpellMod_Chaos_Aftershock_KnockbackArea"
            ]
        },
        {
            "Barrier", [SpellModDb.IncreaseMoveSpeedDuringChannelLow]
        },
        {
            "PowerSurge", [SpellModDb.DispellDebuffs]
        },
        {
            "RainOfChaos",
            [SpellModDb.ApplyFadingSnareShort, SpellModDb.ChaosConsumeIgniteAgonizingFlames, SpellModDb.CooldownMedium]
        },
        {
            "Void", [SpellModDb.ProjectileIncreaseRangeMedium, SpellModDb.ChaosConsumeIgniteAgonizingFlames]
        },
        {
            "Volley",
            [
                SpellModDb.ChaosConsumeIgniteAgonizingFlames, SpellModDb.CooldownMedium, SpellModDb.KnockBackOnHitLight,
                SpellModDb.ProjectileRangeAndVelocity
            ]
        },
        {
            "VeilOfChaos",
            [
                SpellModDb.ChaosConsumeIgniteAgonizingFlames, SpellModDb.VeilBonusDamageOnPrimary,
                SpellModDb.VeilBuffAndIllusionDuration
            ]
        },
        // Frost
        {
            "ColdSnap",
            [
                SpellModDb.FrostIncreaseFreezeWhenChill, SpellModDb.FrostWeapon,
                SpellModDb.IncreaseMoveSpeedDuringChannelHigh
            ]
        },
        {
            "CrystalLance",
            [
                SpellModDb.CastRate, SpellModDb.FrostIncreaseFreezeWhenChill, SpellModDb.FrostSplinterNovaOnFrosty,
                SpellModDb.ProjectileRangeAndVelocity
            ]
        },
        {
            "FrostBat",
            [
                SpellModDb.CastRate, SpellModDb.FrostShieldOnFrosty, SpellModDb.FrostSplinterNovaOnFrosty,
                SpellModDb.ProjectileRangeAndVelocity
            ]
        },
        {
            "IceNova", [SpellModDb.CooldownMedium, SpellModDb.TargetAoEIncreaseRangeMedium]
        },
        {
            "FrostBarrier", [SpellModDb.FrostConsumeChillIntoFreezeRecast, SpellModDb.IncreaseMoveSpeedDuringChannelLow]
        },
        {
            "VeilOfFrost",
            [
                SpellModDb.FrostConsumeChillIntoFreezeRecast, SpellModDb.VeilBonusDamageOnPrimary,
                SpellModDb.VeilBuffAndIllusionDuration
            ]
        },
        // Illusion
        {
            "Curse", [SpellModDb.ApplyFadingSnareShort, SpellModDb.KnockBackOnHitMedium]
        },
        {
            "MistTrance",
            [
                SpellModDb.IncreaseMoveSpeedDuringChannelHigh, SpellModDb.KnockBackOnHitMedium,
                SpellModDb.TravelBuffIncreaseRangeMedium
            ]
        },
        {
            "PhantomAegis", [SpellModDb.DispellDebuffs, SpellModDb.KnockBackOnHitMedium, SpellModDb.MovementSpeedNormal]
        },
        {
            "SpectralWolf",
            [
                SpellModDb.IllusionConsumeWeakenSpawnWisp, SpellModDb.IllusionWeakenShield,
                SpellModDb.ProjectileRangeAndVelocity
            ]
        },
        {
            "WraithSpear",
            [
                SpellModDb.ApplyFadingSnareMedium, SpellModDb.IllusionConsumeWeakenSpawnWisp,
                SpellModDb.IllusionWeakenShield, SpellModDb.ProjectileRangeAndVelocity
            ]
        },
        {
            "VeilOfIllusion",
            [
                SpellModDb.IllusionWeakenShieldOnAttack, SpellModDb.VeilBonusDamageOnPrimary,
                SpellModDb.VeilBuffAndIllusionDuration
            ]
        },
        // Storm
        {
            "BallLightning", [SpellModDb.ProjectileRangeAndVelocity, SpellModDb.StormConsumeStaticIntoStunExplode]
        },
        {
            "Cyclone",
            [
                SpellModDb.CastRate, SpellModDb.ProjectileRangeAndVelocity, SpellModDb.StormConsumeStaticIntoStun,
                SpellModDb.StormConsumeStaticIntoWeaponCharge
            ]
        },
        {
            "Discharge", [SpellModDb.IncreaseMoveSpeedDuringChannelHigh, SpellModDb.StormGrantWeaponCharge]
        },
        {
            "LightningTendrils", [SpellModDb.CastRate, SpellModDb.ProjectileRangeAndVelocity]
        },
        {
            "PolarityShift",
            [
                SpellModDb.ApplyFadingSnareMedium, SpellModDb.ProjectileRangeAndVelocity,
                SpellModDb.StormConsumeStaticIntoWeaponCharge
            ]
        },
        {
            "VeilOfStorm",
            [
                SpellModDb.StormConsumeStaticIntoStun, SpellModDb.VeilBonusDamageOnPrimary,
                SpellModDb.VeilBuffAndIllusionDuration
            ]
        },
        // unholy
        {
            "CorpseExplosion",
            [SpellModDb.CooldownMedium, SpellModDb.TargetAoEIncreaseRangeMedium, SpellModDb.UnholySkeletonBomb]
        },
        {
            "CorruptedSkull",
            [SpellModDb.KnockBackOnHitMedium, SpellModDb.ProjectileRangeAndVelocity, SpellModDb.UnholySkeletonBomb]
        },
        {
            "Soulburn", [SpellModDb.ApplyFadingSnareLong, SpellModDb.CastRate, SpellModDb.UnholySkeletonBomb]
        },
        {
            "WardOfTheDamned", [SpellModDb.IncreaseMoveSpeedDuringChannelLow, SpellModDb.UnholySkeletonBomb]
        },
        {
            "VeilOfBones", [SpellModDb.VeilBonusDamageOnPrimary, SpellModDb.VeilBuffAndIllusionDuration]
        }
    };


    public void Init()
    {
        foreach (var mod in SpellModDb.Mods)
        {
            var abilityName = mod.Split("_")[1];
            if (SpellSchoolDb.SpellSchools.Contains(abilityName)) // handle format with spell school
            {
                abilityName = mod.Split("_")[2];
            }

            if (AbilityToSpellMod.ContainsKey(abilityName))
            {
                if (!AbilityToSpellMod[abilityName].Contains(mod))
                {
                    AbilityToSpellMod[abilityName].Add(mod);
                }
            }
            else
            {
                AbilityToSpellMod.Add(abilityName, [mod]);
            }
        }

        Plugin.Logger.LogInfo("AbilityToSpellModDatabase initialized.");
    }
}