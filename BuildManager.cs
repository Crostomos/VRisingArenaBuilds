using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using ArenaBuildsMod.Models;

namespace ArenaBuildsMod
{
    public static class BuildManager
    {
        private static readonly string FileDirectory = Path.Combine("BepInEx", "config", MyPluginInfo.PLUGIN_NAME);
        private const string BuildFile = "builds.json";
        private static readonly string BuildPath = Path.Combine(FileDirectory, BuildFile);
        public static Dictionary<string, BuildModel> Builds { get; private set; } = new(StringComparer.OrdinalIgnoreCase);


        internal static void LoadData()
        {
            if (!File.Exists(BuildPath))
            {
                Plugin.Logger.LogInfo($"Builds.json not found in {BuildPath}");
                CreateEmptyBuildsFile();
            }

            var jsonString = File.ReadAllText(BuildPath);
            var tempDict = JsonSerializer.Deserialize<Dictionary<string, BuildModel>>(jsonString);
            Builds = new Dictionary<string, BuildModel>(tempDict, StringComparer.OrdinalIgnoreCase);
            Plugin.Logger.LogInfo($"Loaded {Builds.Count} builds from Builds.json");
        }

        public static void CreateEmptyBuildsFile()
        {
            if (!Directory.Exists(FileDirectory))
            {
                Directory.CreateDirectory(FileDirectory);
            }

            File.WriteAllText(BuildPath, "{}");
            Plugin.Logger.LogInfo($"Created empty Builds.json at {BuildPath}");
        }

        public static string GetBuildList()
        {
            if (Builds == null || Builds.Count == 0)
            {
                return string.Empty;
            }

            return string.Join("\n- ", Builds.Keys);
        }

        public static void LogInfoJson()
        {
            foreach (var kvp in Builds)
            {
                Plugin.Logger.LogInfo($"Clé : {kvp.Key}");
                var json = JsonSerializer.Serialize(kvp.Value, new JsonSerializerOptions
                {
                    WriteIndented = true
                });
                Plugin.Logger.LogInfo(json);
            }
        }
    }
}