using System.Collections.Generic;

namespace ArenaBuilds.Models.Interfaces;

internal interface ICommandArgument
{
    public string Name { get; set; }

    public string PrefabName { get; set; }

    public List<string> ArgNames { get; set; }
}