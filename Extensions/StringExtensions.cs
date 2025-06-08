namespace ArenaBuilds.Extensions;

internal static class StringExtensions
{
    public static int FromBase36(this string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return 0;
        }

        input = input.ToUpper();
        var result = 0;

        foreach (var c in input)
        {
            int value;
            switch (c)
            {
                case >= '0' and <= '9':
                    value = c - '0';
                    break;
                case >= 'A' and <= 'Z':
                    value = c - 'A' + 10; // A–Z => 10–35
                    break;
                default:
                    Plugin.Logger.LogError($"Invalid character '{c}' in base36 string.");
                    return 0;
            }

            result = result * 36 + value;
        }

        return result;
    }
}