using ArenaBuildsMod.Models.Interfaces;

namespace ArenaBuildsMod.Models;

public class AbilityData : IAbilityData
{
    public string Name { get; set; }
    
    public JewelData Jewel { get; set; }
}