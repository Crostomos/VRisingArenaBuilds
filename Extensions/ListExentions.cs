using System.Collections.Generic;
using System.Text;

namespace ArenaBuilds.Extensions;

public static class ListExtensions
{
    public static string ToFormattedList(this ICollection<string> list)
    {
        var result = new StringBuilder();
        foreach (var item in list)
        {
            result.AppendLine($"- {item}");
        }

        return result.ToString();
    }
}