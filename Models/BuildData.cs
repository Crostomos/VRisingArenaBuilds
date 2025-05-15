using System.Collections.Generic;

namespace ArenaBuildsMod.Models;

public class BuildData
{
    public BloodData Blood { get; set; }
    
    public List<Items> Items { get; set; }
    
    public List<Abilities> Abilities { get; set; }
}