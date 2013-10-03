

using System;
using System.Collections.Generic;
using System.Net.Configuration;
using System.Net.Sockets;
using System.Threading;
using PacketLibrary;

namespace Server
{
    internal class ServerMain
    {
        private List<string> onlineUsers;
 
        private static void Main(string[] args)
        {
            new ServerMain();
        }

        public ServerMain()
        {
            onlineUsers = new List<string>();

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
            ChatMessage msg = (ChatMessage) packet.Data;
            if (msg.Receiver == "BROADCAST")
            {
                //TODO send to everyone
            }
            else
            {
                ClientHandler clientHandler = 
            }
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

        public void handshakeHandler(ClientHandler client, Packet packet)
        {
            HandshakeRequest request = (HandshakeRequest) packet.Data;
            Packet rePacket = new Packet();
            rePacket.Flag = Flag.HandshakeResponse;
            HandshakeResponse response = new HandshakeResponse();

            if(onlineUsers.Contains(request.Username))
                response.Response = Response.INVALIDLOGIN;
            else
            {
                response.Response = Response.OK;
                onlineUsers.Add(request.Username);
            }

            rePacket.Data = response;
            client.send(rePacket);
        }

        public void removeClient(ClientHandler client)
        {
            onlineUsers.Remove(client.username);
            //TODO close games of this user.
            //TODO remove user from dictionaries.
        }
    }
}