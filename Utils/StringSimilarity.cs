using System;
using System.Collections.Generic;
using System.Linq;

namespace ArenaBuilds.Utils;

public static class StringSimilarity
{
    private static readonly HashSet<string> IgnoredTokens =
        new(StringComparer.OrdinalIgnoreCase)
        {
            "Of",
            "The"
        };

    public static string FindClosestWithTokens(
        string query,
        IEnumerable<string> candidates)
    {
        ArgumentNullException.ThrowIfNull(query);
        ArgumentNullException.ThrowIfNull(candidates);

        var queryTokens = Tokenize(query)
            .Where(t => !IgnoredTokens.Contains(t))
            .ToArray();

        if (queryTokens.Length == 0)
            return null;

        string bestResult = null;
        var bestScore = double.MinValue;

        foreach (var candidate in candidates)
        {
            if (string.IsNullOrWhiteSpace(candidate))
                continue;

            var candidateTokens = Tokenize(candidate)
                .Where(t => !IgnoredTokens.Contains(t))
                .ToArray();

            var score = CalculateScore(queryTokens, candidateTokens);

            if (!(score > bestScore)) continue;

            bestScore = score;
            bestResult = candidate;
        }

        return bestResult;
    }

    private static double CalculateScore(
        string[] queryTokens,
        string[] candidateTokens)
    {
        var bestScore = double.MinValue;

        foreach (var queryToken in queryTokens)
        foreach (var candidateToken in candidateTokens)
        {
            // Exact token match.
            if (string.Equals(
                    queryToken,
                    candidateToken,
                    StringComparison.OrdinalIgnoreCase))
            {
                bestScore = Math.Max(bestScore, 1000);
                continue;
            }

            var distance = LevenshteinDistance(
                queryToken.ToLowerInvariant(),
                candidateToken.ToLowerInvariant());

            // Don't consider very poor matches.
            if (distance > 4)
                continue;

            // Normalize distance relative to token length.
            var similarity =
                1.0 - (double)distance /
                Math.Max(queryToken.Length, candidateToken.Length);

            // Bonus for longer matching tokens.
            var score =
                similarity * 100 +
                Math.Min(candidateToken.Length, 10);

            bestScore = Math.Max(bestScore, score);
        }

        return bestScore;
    }

    private static string[] Tokenize(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return [];

        var tokens = new List<string>();
        var start = 0;

        for (var i = 1; i < value.Length; i++)
            // PascalCase boundary:
            // PhantomAegis → Phantom / Aegis
            if (char.IsUpper(value[i]))
            {
                tokens.Add(value[start..i]);
                start = i;
            }

        tokens.Add(value[start..]);

        return tokens.ToArray();
    }

    private static int LevenshteinDistance(string source1, string source2) //O(n*m)
    {
        var source1Length = source1.Length;
        var source2Length = source2.Length;

        var matrix = new int[source1Length + 1, source2Length + 1];

        // First calculation, if one entry is empty return full length
        if (source1Length == 0)
            return source2Length;

        if (source2Length == 0)
            return source1Length;

        // Initialization of matrix with row size source1Length and columns size source2Length
        for (var i = 0; i <= source1Length; matrix[i, 0] = i++)
        {
        }

        for (var j = 0; j <= source2Length; matrix[0, j] = j++)
        {
        }

        // Calculate rows and collumns distances
        for (var i = 1; i <= source1Length; i++)
        for (var j = 1; j <= source2Length; j++)
        {
            var cost = source2[j - 1] == source1[i - 1] ? 0 : 1;

            matrix[i, j] = Math.Min(
                Math.Min(matrix[i - 1, j] + 1, matrix[i, j - 1] + 1),
                matrix[i - 1, j - 1] + cost);
        }

        return matrix[source1Length, source2Length];
    }
}