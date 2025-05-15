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
        public static Dictionary<string, BuildData> Builds;


        internal static void LoadData()
        {
            if (!File.Exists(BuildPath))
            {
                Plugin.Logger.LogWarning($"Builds.json not found in {BuildPath}"); // TODO create default builds.json
            }
            else
            {
                var jsonString = File.ReadAllText(BuildPath);
                Builds = JsonSerializer.Deserialize<Dictionary<string, BuildData>>(jsonString)!;
                Plugin.Logger.LogInfo($"Loaded {Builds.Count} builds from Builds.json");
            }
        }

        public static string GetBuildList()
        {
            if (Builds == null || Builds.Count == 0)
            {
                return string.Empty;
            }

            return string.Join("\n- ", Builds.Keys);
        }
    }
}