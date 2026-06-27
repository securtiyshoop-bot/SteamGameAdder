using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using SteamGameAdder.Models;

namespace SteamGameAdder.Services
{
    public class ConfigService
    {
        private readonly string _configPath = "games.json";
        private ConfigData _config;

        public ConfigService()
        {
            LoadConfig();
        }

        public void LoadConfig()
        {
            try
            {
                if (File.Exists(_configPath))
                {
                    string json = File.ReadAllText(_configPath);
                    _config = JsonConvert.DeserializeObject<ConfigData>(json);
                }
                else
                {
                    _config = new ConfigData();
                    SaveConfig();
                }
            }
            catch
            {
                _config = new ConfigData();
            }
        }

        public void SaveConfig()
        {
            try
            {
                string json = JsonConvert.SerializeObject(_config, Formatting.Indented);
                File.WriteAllText(_configPath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Kayıt hatası: {ex.Message}");
            }
        }

        public void AddGame(GameEntry game)
        {
            _config.Games.Add(game);
            SaveConfig();
        }

        public void RemoveGame(GameEntry game)
        {
            _config.Games.Remove(game);
            SaveConfig();
        }

        public List<GameEntry> GetAllGames() => _config.Games;

        public string GetSteamPath() => _config.SteamPath;

        public void SetSteamPath(string path)
        {
            _config.SteamPath = path;
            SaveConfig();
        }
    }
}
