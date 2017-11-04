using Newtonsoft.Json.Linq;
using PusherClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExternalServices.BitstampWS
{
    public class BitstampService
    {
        static Pusher _pusher = null;
        static Channel _chatChannel = null;
        static PresenceChannel _presenceChannel = null;


        public  BitstampService()
        {
            _pusher = new Pusher("de504dc5763aeef9ff52");
            _pusher.ConnectionStateChanged += _pusher_ConnectionStateChanged;
            _pusher.Error += _pusher_Error;

            // Setup private channel
            _chatChannel = _pusher.Subscribe("live_orders");
            _chatChannel.Subscribed += _chatChannel_Subscribed;

            // Inline binding!
            _chatChannel.BindAll((string ch, dynamic data) =>
            {
                try
                {
                    //var obj = JObject.Parse(data);
                    Console.WriteLine(data["price"]);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                
                Console.WriteLine(data);
            });

            

            _pusher.Connect();

            string line;
            do
            {
                line = Console.ReadLine();

                if (line == "quit")
                    break;
                //else
                //    _chatChannel.Trigger("client-my-event", new { message = line, name = _name });

            } while (line != null);

            _pusher.Disconnect();
        }

        static void GetData(dynamic data)
        {
            Console.WriteLine(data.message);
        }


        static void _pusher_Error(object sender, PusherException error)
        {
            Console.WriteLine("Pusher Error: " + error.ToString());
        }

        static void _pusher_ConnectionStateChanged(object sender, ConnectionState state)
        {
            Console.WriteLine("Connection state: " + state.ToString());
        }
        static void _chatChannel_Subscribed(object sender)
        {
            Console.WriteLine("Hi ! Type 'quit' to exit, otherwise type anything to chat!");

        }
    }
}
