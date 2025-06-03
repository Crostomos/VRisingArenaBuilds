using System;
using System.Collections.Generic;
using System.Text;

namespace ArenaBuilds.Extensions;

public static class DictionaryExtensions
{
    public static string ToFormattedList(
        this Dictionary<string, string> dict,
        Func<string, string> transformKey = null,
        Func<string, string> transformValue = null)
    {
        var result = new StringBuilder();
        foreach (var kvp in dict)
        {
            var key = transformKey != null ? transformKey(kvp.Key) : kvp.Key;
            var value = transformValue != null ? transformValue(kvp.Value) : kvp.Value;
            result.AppendLine($"- {key} : {value}");
        }

        return result.ToString();
    }
}