using System.Collections.Generic;

namespace ArenaBuilds.Models;

public class BuildModel
{
    public BloodData Blood { get; set; }

    public List<BuildItemData> Items { get; set; }

    public Abilities Abilities { get; set; }

    public PassiveSpells PassiveSpells { get; set; }

    public Armors Armors { get; set; }

    public List<WeaponData> Weapons { get; set; }
}