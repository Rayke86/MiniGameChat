using System;
using System.Collections.Generic;
using System.Net.Sockets;
using PacketLibrary;

namespace Server
{
    internal class ServerMain
    {
        private Dictionary<string, ClientHandler> onlineUsers;
        private Dictionary<string, Game> currentGames; // fill with games when user chooses one.
 
        private static void Main(string[] args)
        {
            new ServerMain();
        }

        public ServerMain()
        {
            onlineUsers = new Dictionary<string, ClientHandler>();
            currentGames = new Dictionary<string, Game>();

            TcpListener listener = new TcpListener(System.Net.IPAddress.Any, 1330);
            listener.Start();

            while (true)
            {
                Console.WriteLine("Listening...");
                try
                {
                    TcpClient tcpClient = listener.AcceptTcpClient();
                    ClientHandler clientHandler = new ClientHandler(tcpClient, this);
                    Console.WriteLine("Client connected.");
                }
                catch (Exception)
                {
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
                Console.WriteLine("{0} BROADCAST :: '{1}'", msg.Sender, msg.Message);
            }
            else
            {
                ClientHandler clientHandler = onlineUsers[msg.Receiver];
                clientHandler.send(packet);
                Console.WriteLine("{0} to {1} :: '{2}'", msg.Sender, msg.Receiver, msg.Message);
            }
            client.send(packet);
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
            client.Username = request.Username;
            Packet rePacket = new Packet();
            rePacket.Flag = Flag.HandshakeResponse;
            HandshakeResponse response = new HandshakeResponse();

            if (onlineUsers.ContainsKey(client.Username))
            {
                response.Response = Response.INVALIDLOGIN;
                Console.WriteLine("Invalid login received");
            }
            else
            {
                response.Response = Response.OK;
                addClient(client);
                Console.WriteLine("Client accepted.");
            }

            rePacket.Data = response;
            client.send(rePacket);
            sendOnlineList();
        }

        public void addClient(ClientHandler client)
        {
            onlineUsers.Add(client.Username, client);
        }

        public void removeClient(ClientHandler client)
        {
            onlineUsers.Remove(client.Username);
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