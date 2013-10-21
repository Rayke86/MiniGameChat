using System;
using System.Dynamic;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using PacketLibrary;

namespace Server
{
    public class ClientHandler
    {
        private TcpClient tcpClient;
        private ServerMain serverMain;
        private Thread listenThread;
        private NetworkStream nwStream;
        public string Username { get; set; }

        public ClientHandler(TcpClient tcpClient, ServerMain serverMain)
        {
            this.tcpClient = tcpClient;
            this.serverMain = serverMain;

            listenThread = new Thread(new ThreadStart(handler));
            listenThread.Start();
        }

        public void handler()
        {
            nwStream = tcpClient.GetStream();
            BinaryFormatter binfor = new BinaryFormatter();

            while (true)
            {
                try
                {
                    Packet packet = binfor.Deserialize(nwStream) as Packet;
                    packetHandler(packet);
                    NoSuchAgencyHandler.writeHandler(Username, packet);
                }
                catch (Exception)
                {
                }
            }
        }

        public void packetHandler(Packet packet)
        {
            switch (packet.Flag)
            {
                case Flag.Chat:
                    serverMain.SetChat(this, packet);
                    break;
                case Flag.Connect4:
                    ConnectFour c4data = packet.Data as ConnectFour;
                    if (c4data != null)
                    {
                        serverMain.SetConnectFour(this, packet);
                    }
                    break;
                case  Flag.RPSLS:
                    RockPaperScissorsLizardSpock rpsdata = packet.Data as RockPaperScissorsLizardSpock;
                    if (rpsdata != null)
                    {
                        serverMain.SetRPSLS(this, packet);
                    }
                    break;
                case Flag.HandshakeRequest:
                    Console.WriteLine("Handshake Request detected.");
                    serverMain.handshakeHandler(this, packet);
                    break;
                case Flag.RemoveClient:
                    serverMain.RemoveClient(this);
                    break;
                case Flag.GameRequest:
                    serverMain.HandleGameRequest(packet);
                    Console.WriteLine("GameRequest from {0}", Username);
                    break;
                case Flag.GameResponse:
                    Console.WriteLine("GameResponse from {0}", Username);
                    BaseGame g = packet.Data as BaseGame;
                    if (g.Situation == GameSituation.Connect)
                    {
                        if (packet.Data is ConnectFour)
                        {
                            serverMain.CreateGame(packet, Flag.Connect4);
                        }
                        else if (packet.Data is RockPaperScissorsLizardSpock)
                        {
                            serverMain.CreateGame(packet, Flag.RPSLS);
                        }
                    }
                    else if (g.Situation == GameSituation.Disconnect)
                    {
                        serverMain.HandleGameRequest(packet);
                    }
                    break;
            }
        }

        public void send(Packet packet)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(nwStream, packet);
            }
            catch (Exception)
            {
            }
        }

        public void Disconnect()
        {
            try
            {
                nwStream.Close(0);
                tcpClient.Close();
                listenThread.Abort(0);
            }
            catch (Exception)
            {
            }
        }
    }
}
