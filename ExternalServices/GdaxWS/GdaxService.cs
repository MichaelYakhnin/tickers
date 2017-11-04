using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ExternalServices.GdaxWS.Response;

namespace ExternalServices.GdaxWS
{
    public class GdaxService
    {
        WebSocketApi ws;

        public GdaxService()
        {
            string uri = "wss://ws-feed.gdax.com";
            GdaxSubscribeTicker gdaxSubscribeTicker = new GdaxSubscribeTicker
            {
                Type = "subscribe",
                ProductIds = new string[] {
                    "BTC-USD"
                },
                Channels = new object[]
                {
                 
                  new channels
                  {
                      name = "ticker",
                      product_ids = new string[]
                      {
                          "BTC-USD"
                      }
                  }

                }
            };

            ws = new WebSocketApi();
            ws.TickerEvent += GetRatesFromService;
            ws.WebSocketSubscribe(uri, gdaxSubscribeTicker);
            
        }
        public void GetRatesFromService(byte[] buffer)
        {
            var resultJson = (new UTF8Encoding()).GetString(buffer);
            if (!resultJson.Contains("[") )
            {
                string[] separatingChars = { "[", ",", "]", "\0" };

                try
                {

                    string[] arr = resultJson.Split(separatingChars, System.StringSplitOptions.RemoveEmptyEntries);
                    string withoutChars = resultJson.Replace("\0", "");
                    var obj = JObject.Parse(withoutChars);
                    Console.WriteLine($"Ask: {obj["best_ask"]},Bid: {obj["best_bid"]}");
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }

            }

            //GdaxTickerResponseModel gdaxTickerResponseModel = JsonConvert.DeserializeObject<GdaxTickerResponseModel>(resultJson);
            Console.WriteLine(resultJson);
        }
    }
}
