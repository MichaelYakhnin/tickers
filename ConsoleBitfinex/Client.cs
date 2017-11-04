
using ExternalServices;
using Quartz;
using Quartz.Impl;
using Quartz.Logging;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using ExternalServices.GdaxWS;
using ExternalServices.BitfinexWS;
using ExternalServices.BitstampWS;

namespace ConsoleBitfinex
{
    class Program
    {

        static void Main(string[] args)
        {

            //BlockchainInfo.Connect("wss://ws.blockchain.info/inv").Wait();


           // GdaxService gdaxService = new GdaxService();
           // BitfinexService bitfinexService = new BitfinexService();
            BitstampService bitstampService = new BitstampService();
        }

       


    }
}

