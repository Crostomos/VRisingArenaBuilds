namespace ArenaBuilds.Extensions;

internal static class IntegerExtensions
{
    private const string Base36Chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    public static string ToBase36(this int value)
    {
        switch (value)
        {
            case < 0:
                Plugin.Logger.LogError("Negative integers are not supported by base36 conversion.");
                return string.Empty;
            case 0:
                return "0";
        }

        var result = "";
        while (value > 0)
        {
            var remainder = value % 36;
            result = Base36Chars[remainder] + result;
            value /= 36;
        }

        return result;
    }
}