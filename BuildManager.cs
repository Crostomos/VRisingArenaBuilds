﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using ArenaBuilds.Extensions;
using ArenaBuilds.Models;

namespace ArenaBuilds;

internal static class BuildManager
{
    private static readonly string FileDirectory = Path.Combine("BepInEx", "config", MyPluginInfo.PLUGIN_NAME);
    private const string BuildFile = "builds.json";
    private static readonly string BuildPath = Path.Combine(FileDirectory, BuildFile);
    public static Dictionary<string, BuildModel> Builds { get; private set; } = new(StringComparer.OrdinalIgnoreCase);


    public static void LoadData()
    {
        if (!File.Exists(BuildPath))
        {
            Plugin.Logger.LogInfo($"Builds.json not found in {BuildPath}");
            CreateEmptyBuildsFile();
        }

        var jsonString = File.ReadAllText(BuildPath);
        var tempDict = JsonSerializer.Deserialize<Dictionary<string, BuildModel>>(jsonString,
            new JsonSerializerOptions
            {
                AllowTrailingCommas = true
            });
        Builds = new Dictionary<string, BuildModel>(tempDict, StringComparer.OrdinalIgnoreCase);
        Plugin.Logger.LogInfo($"Loaded {Builds.Count} builds from Builds.json");
    }

    private static void CreateEmptyBuildsFile()
    {
        if (!Directory.Exists(FileDirectory))
        {
            Directory.CreateDirectory(FileDirectory);
        }

        var emptyBuilds = new Dictionary<string, BuildModel> { { "EmptyDefault", new BuildModel() } };
        var json = JsonSerializer.Serialize(emptyBuilds, new JsonSerializerOptions
        {
            WriteIndented = true,
        });
        File.WriteAllText(BuildPath, json);
        Plugin.Logger.LogInfo($"Created empty Builds.json at {BuildPath}");
    }

    public static string GetBuildList()
    {
        if (Builds == null || Builds.Count == 0)
        {
            return string.Empty;
        }

        return Builds.Keys.ToFormattedList();
    }
}