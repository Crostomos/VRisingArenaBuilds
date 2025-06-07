using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArenaBuilds.Models.Interfaces;

namespace ArenaBuilds.Extensions;

public static class ListExtensions
{
    public static string ToIndexedList(this IEnumerable<string> list)
    {
        var result = new StringBuilder();
        var index = 1;
        foreach (var item in list)
        {
            result.AppendLine($"{index.ToBase36()} : {item}");
            index++;
        }

        return result.ToString();
    }
    
    public static string ToFormattedList(this IEnumerable<string> list)
    {
        var result = new StringBuilder();
        foreach (var item in list)
        {
            result.AppendLine($"- {item}");
        }

        return result.ToString();
    }

    public static string ToFormattedList(this IEnumerable<ICommandArgument> list, bool showName = false)
    {
        var result = new StringBuilder();
        foreach (var item in list)
        {
            if (item.ArgNames.Count == 0) continue;

            var acceptedArgument = item.ArgNames.Count > 1 ? string.Join(" / ", item.ArgNames) : item.ArgNames[0];

            if (showName)
            {
                result.AppendLine($"- {acceptedArgument} : {item.Name}");
            }
            else
            {
                result.AppendLine($"- {acceptedArgument}");
            }
        }

        return result.ToString();
    }

    public static ICommandArgument ContainsCommandArgument(this IEnumerable<ICommandArgument> list, string input)
    {
        return list.FirstOrDefault(s =>
            s.ArgNames.Any(arg => arg.Contains(input, StringComparison.OrdinalIgnoreCase)));
    }

    public static ICommandArgument EqualsCommandArgument(this IEnumerable<ICommandArgument> list, string input)
    {
        return list.FirstOrDefault(s => s.ArgNames.Contains(input, StringComparer.OrdinalIgnoreCase));
    }
}