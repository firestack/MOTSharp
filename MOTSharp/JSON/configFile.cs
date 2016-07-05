using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MOTSharp.JSON
{
    internal class twitchSubDict
    {
        [JsonProperty("username")]
        public string username = String.Empty;

        [JsonProperty("password")]
        public string password = String.Empty;
    }

    class configFile
    {
        [JsonProperty("PGURL")]
        public string pgurl = String.Empty;

        [JsonProperty("twitch")]
        public twitchSubDict twitch = new twitchSubDict();

        [JsonProperty("MOTAPI")]
        public string motapi = String.Empty;
    }
}
