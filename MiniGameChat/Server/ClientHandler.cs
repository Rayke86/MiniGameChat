using System;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using PacketLibrary;

namespace Server
{
    class ClientHandler
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
                    serverMain.setChat(this, packet);
                    break;
                case Flag.Connect4:
                    serverMain.setConnectFour(this, packet);
                    break;
                case  Flag.RPSLS:
                    serverMain.setRPSLS(this, packet);
                    break;
                case Flag.HandshakeRequest:
                    Console.WriteLine("Handshake Request detected.");
                    serverMain.handshakeHandler(this, packet);
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
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
