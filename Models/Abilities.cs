namespace ArenaBuilds.Models;

public class Abilities
{
    public AbilityData Travel { get; set; } = new();

    public AbilityData Ability1 { get; set; } = new();

    public AbilityData Ability2 { get; set; } = new();

    public UltimateAbilityData Ultimate { get; set; } = new();
}