
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.WebSockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExternalServices
{
    public class WebSocketApi
    {
        private static object consoleLock = new object();
        private const int sendChunkSize = 256;
        private const int receiveChunkSize = 1024;
        private const bool verbose = true;
        private static readonly TimeSpan delay = TimeSpan.FromMilliseconds(30000);
        protected static string cmd;

        public delegate void TickerDataHandler(byte[] buffer);

        public event TickerDataHandler TickerEvent;

        public void WebSocketSubscribe(string uri, Object asset)
        {

            string _cmd = JsonConvert.SerializeObject(asset);
            cmd = _cmd;
            Connect(uri).Wait(); ;
        }

        public  async Task Connect(string uri)
        {
            ClientWebSocket webSocket = null;
            
            try
            {
                webSocket = new ClientWebSocket();
                await webSocket.ConnectAsync(new Uri(uri), CancellationToken.None);
                await Task.WhenAll(Receive(webSocket), Send(webSocket));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex);
            }
            finally
            {
                if (webSocket != null)
                    webSocket.Dispose();
                Console.WriteLine();

                lock (consoleLock)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("WebSocket closed.");
                    Console.ResetColor();
                }
            }
        }
        static UTF8Encoding encoder = new UTF8Encoding();

        private  async Task Send(ClientWebSocket webSocket)
        {
            
            byte[] buffer = encoder.GetBytes(cmd);
            await webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);

            while (webSocket.State == WebSocketState.Open)
            {
                LogStatus(false, buffer, buffer.Length);
                await Task.Delay(delay);
            }
        }

        private  async Task Receive(ClientWebSocket webSocket)
        {
            byte[] buffer = new byte[receiveChunkSize];
            while (webSocket.State == WebSocketState.Open)
            {
                var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                }
                else
                {
                    //LogStatus(true, buffer, result.Count);
                    TickerEvent?.Invoke(buffer);
                    
                }
            }
        }

        private  void LogStatus(bool receiving, byte[] buffer, int length)
        {
           
            
            lock (consoleLock)
            {
                Console.ForegroundColor = receiving ? ConsoleColor.Green : ConsoleColor.Gray;
                //Console.WriteLine("{0} ", receiving ? "Received" : "Sent");

                if (verbose)
                {
                    
                    
                    //Console.WriteLine(resultJson);
                    //using (var ms = new MemoryStream(buffer))
                    //using (var streamReader = new StreamReader(ms))
                    //using (var jsonReader = new JsonTextReader(streamReader))
                    //{
                    //    var token = JToken.ReadFrom(jsonReader);
                        
                    //    Console.WriteLine(token);
                    //    //foreach (var item in arr.Children())
                    //    //{
                    //    //    Console.WriteLine(item);
                    //    //}
                    //}

                }


            Console.ResetColor();
            }
        }
    }
}
