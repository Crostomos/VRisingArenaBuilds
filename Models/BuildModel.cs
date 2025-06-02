using System.Collections.Generic;

namespace ArenaBuilds.Models;

public class BuildModel
{
    public BloodData Blood { get; set; } = new();

    public List<BuildItemData> Items { get; set; } = [];

    public Abilities Abilities { get; set; } = new();

    public PassiveSpells PassiveSpells { get; set; } = new();

    public Armors Armors { get; set; } = new();

    public List<WeaponData> Weapons { get; set; } = [];
}