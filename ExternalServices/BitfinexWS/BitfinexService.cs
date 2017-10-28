using System;
using System.Collections.Generic;
using System.Text;

namespace ExternalServices.BitfinexWS
{
    public class BitfinexService
    {
        WebSocketApi ws;

        public BitfinexService()
        {
            string uri = "wss://api.bitfinex.com/ws";
            //websocket bitfinex
            //BitfinexOrderSend bitstampOrderSend = new BitfinexOrderSend
            //{
            //    Event = "subscribe",
            //    Channel = "book",
            //    Pair = "BTCUSD",
            //    Prec = "P0",
            //    Freq = "F0"
            //};
            //BitfinexOrdersrWS bitfinexOrdersrWS = new BitfinexOrdersrWS(bitstampOrderSend);

            BitfinexSubscribeTicker asset = new BitfinexSubscribeTicker
            {
                channel = "ticker",
                Event = "subscribe",
                symbol = "tBTCUSD"
            };
            ws = new WebSocketApi();
            ws.TickerEvent += GetRatesFromService;
            ws.WebSocketSubscribe(uri, asset);
        }
        public void GetRatesFromService(byte[] buffer)
        {
            var resultJson = (new UTF8Encoding()).GetString(buffer);
           
            Console.WriteLine(resultJson);
        }
    }
}
