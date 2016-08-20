using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MOT.JSON
{
    class user 
    {
        [JsonProperty("display_name")]
        public string display_name { get; set; }
        [JsonProperty("_id")]
        public int id { get; set; }
        [JsonProperty("created_at")]
        public System.DateTime created_at { get; set; }
    }
}
