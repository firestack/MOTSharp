using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MOTSharp.JSON
{
    class user 
    {
        [JsonProperty("display_name")]
        public string display_name;
        [JsonProperty("_id")]
        public int id;
        [JsonProperty("created_at")]
        public System.DateTime created_at;
    }
}
