using System.Collections.Generic;

namespace ArenaBuilds.Models.Interfaces;

public interface ICommandArgument
{
    public string Name { get; set; }

    public string PrefabName { get; set; }

    public List<string> ArgNames { get; set; }
}