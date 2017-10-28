using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExternalServices.GdaxWS
{
    public class GdaxSubscribeTicker
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("product_ids")]
        public string[] ProductIds { get; set; }

        [JsonProperty("channels")]
        public object[] Channels { get; set; }
    }

    public class channels
    {
        public string name { get; set; }
        public string[] product_ids { get; set; }
    }
}
