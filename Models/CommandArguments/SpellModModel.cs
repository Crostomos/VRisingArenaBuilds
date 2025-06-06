using System.Collections.Generic;
using ArenaBuilds.Models.Interfaces;

namespace ArenaBuilds.Models.CommandArguments;

public class SpellModModel(string name, string prefabName, List<string> argNames) : ICommandArgument
{
    public string Name { get; set; } = name;

    public string PrefabName { get; set; } = prefabName;

    public List<string> ArgNames { get; set; } = argNames;
}