using System.Collections.Generic;
using Newtonsoft.Json;

namespace SteamGameAdder.Models
{
    public class ConfigData
    {
        [JsonProperty("steamPath")]
        public string SteamPath { get; set; }

        [JsonProperty("games")]
        public List<GameEntry> Games { get; set; }

        public ConfigData()
        {
            Games = new List<GameEntry>();
            SteamPath = @"C:\Program Files (x86)\Steam";
        }
    }
}
