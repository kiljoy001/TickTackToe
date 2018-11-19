using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TickTacToeAPI
{
    public class Api
    {
        public delegate void MessageHandler(string message);
        public MessageHandler OnMessageHandler;

        private TcpClient client;
        private StreamReader reader;
        private StreamWriter writer;

        public static Api Create()
        {
            return Task.Run(() => 
            {
                Api connection = new Api();
                connection.listen();
                return connection;
            }).Result;
        }

        private Api()
        {
            client = new TcpClient("localhost", 9999);
            reader = new StreamReader(client.GetStream());
            writer = new StreamWriter(client.GetStream());
        }

        public void move(GridPoint location)
        {
            string convertedobject = JsonConvert.SerializeObject(location);
            writer.WriteLine(convertedobject);
            writer.Flush();
        }

        private void listen()
        {
            while(true)
            {
                string msg = reader.ReadLine();
                OnMessageHandler(msg);
            }
        }
    }
}
