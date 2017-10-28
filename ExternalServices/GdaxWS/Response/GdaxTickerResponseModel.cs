using System;
using System.Collections.Generic;
using System.Text;

namespace ExternalServices.GdaxWS.Response
{
    
    public class Channel
    {
        public string name { get; set; }
        public List<string> product_ids { get; set; }
    }

    public class GdaxTickerResponseModel
    {
        public string type { get; set; }
        public List<Channel> channels { get; set; }
    }
}
