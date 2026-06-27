using System;
using Newtonsoft.Json;

namespace SteamGameAdder.Models
{
    public class GameEntry
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("appId")]
        public int AppId { get; set; }

        [JsonProperty("installPath")]
        public string InstallPath { get; set; }

        [JsonProperty("executable")]
        public string Executable { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("addedDate")]
        public string AddedDate { get; set; }

        public GameEntry()
        {
            AddedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public override string ToString() => $"{Name} (App ID: {AppId})";
    }
}
