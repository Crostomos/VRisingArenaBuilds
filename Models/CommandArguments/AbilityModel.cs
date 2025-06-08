using System.Collections.Generic;
using ArenaBuilds.Models.Interfaces;

namespace ArenaBuilds.Models.CommandArguments;

internal class AbilityModel(string name, string prefabName, List<string> argNames, string spellSchool) : ICommandArgument
{
    public string Name { get; set; } = name;
    
    public string SpellSchool { get; set; } = spellSchool;

    public string PrefabName { get; set; } = prefabName;

    public List<string> ArgNames { get; set; } = argNames;
}