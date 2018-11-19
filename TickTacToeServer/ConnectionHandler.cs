using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using System.Net.Sockets;

namespace TickTacToeServer
{
    public class ConnectionHandler
    {
        private StreamReader playerReader;
        private StreamWriter player1Writer;
        private StreamWriter player2Writer;
        private Game game;
        private ILogger logger;

        public ConnectionHandler(Game currentGame, StreamReader reader, StreamWriter player1, StreamWriter player2)
        {
            game = currentGame;
            playerReader = reader;
            player1Writer = player1;
            player2Writer = player2;
            logger = Log.Logger;
            logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.Console().WriteTo.File("logs\\debuglogs.txt", rollingInterval: RollingInterval.Day).CreateLogger();
        }

        public void listen()
        {
            while(true)
            {
                string message = playerReader.ReadLine();
                try
                {
                    GridPoint incommingPoint = JsonConvert.DeserializeObject<GridPoint>(message);
                    game.MakeMove(incommingPoint.Ownership(), incommingPoint);
                }
                catch (ArgumentException ae)
                {
                    Log.Error(ae, $"Move invalid! Error Message: {ae.Message}");
                }
                catch(Exception ex)
                {
                    Log.Error(ex, $"Error Reading Message! Error Message: {ex.Message}");
                }
            }
        }
    }

    public class Session
    {
        private ConnectionHandler p1;
        private ConnectionHandler p2;

        public Session(TcpClient player1, TcpClient player2)
        {
            Player firstPlayer = new Player();
            Player secondPlayer = new Player();
            Grid gameGrid = new Grid();
            Game newGame = new Game(firstPlayer, secondPlayer, gameGrid);
            p1 = new ConnectionHandler(newGame, new StreamReader(player1.GetStream()), new StreamWriter(player1.GetStream()), new StreamWriter(player2.GetStream()));
            p2 = new ConnectionHandler(newGame, new StreamReader(player2.GetStream()), new StreamWriter(player2.GetStream()), new StreamWriter(player2.GetStream()));
            Task.Run(() => { p1.listen(); });
            Task.Run(() => { p2.listen(); });
        }
    }
}
