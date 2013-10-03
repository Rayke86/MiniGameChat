using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using PacketLibrary;

namespace Server
{
    class ClientHandler
    {
        private TcpClient tcpClient;
        private ServerMain serverMain;
        private Thread listenthread;
        public string username { get; set; }

        public ClientHandler(TcpClient tcpClient, ServerMain serverMain)
        {
            this.tcpClient = tcpClient;
            this.serverMain = serverMain;

            this.listenthread = new Thread(new ThreadStart(handler));
        }

        public void handler()
        {
            NetworkStream nwStream = tcpClient.GetStream();
            BinaryFormatter binfor = new BinaryFormatter();

            while (true)
            {
                try
                {
                    Packet packet = binfor.Deserialize(nwStream) as Packet;
                    packetHandler(packet);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    serverMain.removeClient(this);
                }
            }
        }

        public void packetHandler(Packet packet)
        {
            switch (packet.Flag)
            {
                case Flag.Chat:
                    break;
                case Flag.Connect4:
                    break;
                case  Flag.RPSLS:
                    break;
                case Flag.HandshakeRequest:
                    serverMain.handshakeHandler(this, packet);
                    break;
                case Flag.OnlineUserList:
                    break;
            }
        }

        public void send(Packet packet)
        {
            //TODO send the packet back to the client!
        }
    }
}
