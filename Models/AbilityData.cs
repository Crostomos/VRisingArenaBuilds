using ArenaBuilds.Models.Interfaces;

namespace ArenaBuilds.Models;

public class AbilityData : IAbilityData
{
    public string Name { get; set; }

    public JewelData Jewel { get; set; }
}