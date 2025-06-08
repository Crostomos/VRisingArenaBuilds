using ArenaBuilds.Models.Interfaces;

namespace ArenaBuilds.Models;

internal class AbilityData : IAbilityData
{
    public string Name { get; set; } = string.Empty;

    public JewelData Jewel { get; set; } = new();
}