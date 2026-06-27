using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Win32;
using SteamGameAdder.Models;

namespace SteamGameAdder.Services
{
    public class SteamService
    {
        private string _steamPath;

        public SteamService()
        {
            _steamPath = GetSteamPath();
        }

        public string GetSteamPath()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Valve\Steam"))
                {
                    if (key != null)
                    {
                        object steamPath = key.GetValue("SteamPath");
                        if (steamPath != null)
                            return steamPath.ToString();
                    }
                }
            }
            catch { }

            return @"C:\Program Files (x86)\Steam";
        }

        public void SetSteamPath(string path)
        {
            if (Directory.Exists(path))
                _steamPath = path;
        }

        public bool AddGameToSteam(GameEntry game)
        {
            try
            {
                if (!Directory.Exists(game.InstallPath))
                    return false;

                string exePath = Path.Combine(game.InstallPath, game.Executable);
                if (!File.Exists(exePath))
                    return false;

                string manifestContent = CreateManifest(game);
                string manifestDir = Path.Combine(_steamPath, "steamapps");
                string manifestFile = Path.Combine(manifestDir, $"appmanifest_{game.AppId}.acf");

                Directory.CreateDirectory(manifestDir);
                File.WriteAllText(manifestFile, manifestContent);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: {ex.Message}");
                return false;
            }
        }

        public bool RemoveGameFromSteam(GameEntry game)
        {
            try
            {
                string manifestFile = Path.Combine(_steamPath, "steamapps", 
                    $"appmanifest_{game.AppId}.acf");

                if (File.Exists(manifestFile))
                {
                    File.Delete(manifestFile);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: {ex.Message}");
                return false;
            }
        }

        private string CreateManifest(GameEntry game)
        {
            long timestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
            return $@"\"AppState\"
{{
    \"appid\"\t\t\"{game.AppId}\"
    \"Universe\"\t\t\"1\"
    \"name\"\t\t\"{game.Name}\"
    \"StateFlags\"\t\t\"4\"
    \"LastUpdated\"\t\t\"{timestamp}\"
    \"ContentUpdateNumber\"\t\t\"1\"
    \"StagingContentID\"\t\t\"0\"
    \"SteamID\"\t\t\"0\"
    \"MountedContentID\"\t\t\"0\"
    \"SymlinkContentID\"\t\t\"0\"
    \"AutoUpdateBehavior\"\t\t\"0\"
    \"AllowOtherDownloadsWhileRunning\"\t\t\"0\"
    \"InstalledDepots\"
    {{
        \"{game.AppId * 2}\"
        {{
            \"manifest\"\t\t\"0\"
            \"size\"\t\t\"0\"
        }}
    }}
    \"SharedContentID\"\t\t\"0\"
}}";
        }

        public string GetSteamPathValue() => _steamPath;
    }
}
