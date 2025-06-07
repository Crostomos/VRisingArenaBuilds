using System.Collections.Generic;
using System.Linq;
using ArenaBuilds.Extensions;
using VampireCommandFramework;

namespace ArenaBuilds.Commands.ManualConverters;

internal class IndexesManualConverter
{
    public static List<int> ToIndexesList(ChatCommandContext ctx, string input, int length, bool ignoreZero = true)
    {
        var fixedInput = input;
        if (ignoreZero)
        {
            fixedInput = input.Replace("0", "");
        }

        // add '0' at the end
        fixedInput = fixedInput.PadRight(length, '0');

        // index unique check
        var listWithoutZero = fixedInput.Select(c => c.ToString()).Where(i => i != "0").ToList();
        if (listWithoutZero.Distinct().Count() != listWithoutZero.Count)
        {
            throw ctx.Error($"Indexes must be different (<color=white>{input}</color>).");
        }

        // fix list size
        if (fixedInput.Length > length)
        {
            ctx.Reply($"Only {length} indexes are required. Keeping <color=white>{fixedInput[..length]}</color>.");
            fixedInput = fixedInput.Substring(0, length);
        }

        return fixedInput.Select(c => c.ToString().FromBase36()).ToList();
    }
}