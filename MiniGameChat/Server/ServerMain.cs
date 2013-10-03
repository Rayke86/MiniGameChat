

using System;
using System.Net.Sockets;
using System.Threading;

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

        public void setChat(ClientHandler client, PacketLibrary.Flag packet) //string veranderen in Packet
        {
            //stuur chat naar client gui
        }

        public void setRPSLS(ClientHandler client, string packet) //string veranderen in Packet
        {
            //stuur keuze naar client gui
        }

        public void setConnectFour(ClientHandler client, string packet) //string veranderen in Packet
        {
            //stuur keuze naar client gui
        }
    }
}