using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExternalServices
{
    public class BitfinexSubscribeTicker
    {
        [JsonProperty("event")]
        public string Event { get; set; }
        public string channel { get; set; }
        public string symbol { get; set; }
    }
}
