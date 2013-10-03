

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
        private Dictionary<string, ClientHandler> onlineUsers;
 
        private static void Main(string[] args)
        {
            new ServerMain();
        }

        public ServerMain()
        {
            onlineUsers = new Dictionary<string, ClientHandler>();

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

        public void setChat(ClientHandler client, Packet packet)
        {
            ChatMessage msg = (ChatMessage) packet.Data;
            if (msg.Receiver == "BROADCAST")
            {
                foreach (ClientHandler clientHandler in onlineUsers.Values)
                {
                    clientHandler.send(packet);
                }
            }
            else
            {
                ClientHandler clientHandler = onlineUsers[msg.Receiver];
                clientHandler.send(packet);
            }
        }

        public void setRPSLS(ClientHandler client, Packet packet)
        {
            RockPaperScissorsLizardSpock data = (RockPaperScissorsLizardSpock) packet.Data;
            //stuur keuze naar client gui
            //TODO list met clients die met elkaar verbonden zijn.
        }

        public void setConnectFour(ClientHandler client, Packet packet)
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

            if(onlineUsers.ContainsKey(client.username))
                response.Response = Response.INVALIDLOGIN;
            else
            {
                response.Response = Response.OK;
                addClient(client);
            }

            rePacket.Data = response;
            client.send(rePacket);
        }

        public void addClient(ClientHandler client)
        {
            onlineUsers.Add(client.username, client);
            sendOnlineList();
        }

        public void removeClient(ClientHandler client)
        {
            onlineUsers.Remove(client.username);
            //TODO close games of this user.
            //TODO remove user from dictionaries.
            sendOnlineList();
        }

        public void sendOnlineList()
        {
            List<string> users = new List<string>(onlineUsers.Keys);

            Packet packet = new Packet();
            packet.Flag = Flag.OnlineUserList;
            packet.Data = users;

            foreach (ClientHandler client in onlineUsers.Values)
            {
                client.send(packet);
            }
        }
    }
}