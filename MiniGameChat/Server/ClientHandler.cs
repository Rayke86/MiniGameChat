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
            }
        }
    }
}
