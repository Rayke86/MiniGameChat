

using System;
using System.Net.Sockets;
using System.Threading;
using PacketLibrary;

namespace Server
{
    internal class ServerMain
    {
        private static void Main(string[] args)
        {
            new ServerMain();
        }

        public ServerMain()
        {
            TcpListener listener = new TcpListener(System.Net.IPAddress.Any, 1330);
            listener.Start();

            while (true)
            {
                try
                {
                    TcpClient client = listener.AcceptTcpClient();
                    new ClientHandler(client, this);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        public void setChat(ClientHandler client, Packet packet) //string veranderen in Packet
        {
            //stuur chat naar elke client gui
            //TODO list met alle online clients.
        }

        public void setRPSLS(ClientHandler client, Packet packet) //string veranderen in Packet
        {
            RockPaperScissorsLizardSpock data = (RockPaperScissorsLizardSpock) packet.Data;
            //stuur keuze naar client gui
            //TODO list met clients die met elkaar verbonden zijn.
        }

        public void setConnectFour(ClientHandler client, Packet packet) //string veranderen in Packet
        {
            ConnectFour data = (ConnectFour) packet.Data;
            //stuur keuze naar client gui
            //TODO list met clients die met elkaar verbonden zijn.
        }
    }
}