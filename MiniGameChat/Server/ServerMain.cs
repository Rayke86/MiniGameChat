using System;
using System.Collections.Generic;
using System.Net.Sockets;
using PacketLibrary;

namespace Server
{
    public class ServerMain
    {
        private Dictionary<string, ClientHandler> onlineUsers;
        private Dictionary<string, List<Game>> currentGames; // fill with games when user chooses one.
 
        private static void Main(string[] args)
        {
            new ServerMain();
        }

        public ServerMain()
        {
            onlineUsers = new Dictionary<string, ClientHandler>();
            currentGames = new Dictionary<string, List<Game>>();

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
                    Console.WriteLine("disconnected? I dunnooooooo");
                }
            }
        }

        public void SetChat(ClientHandler client, Packet packet)
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
                client.send(packet);
            }
        }

        public void SetRPSLS(ClientHandler client, Packet packet)
        {
            RockPaperScissorsLizardSpock data = packet.Data as RockPaperScissorsLizardSpock;
            if (data != null)
            {
                foreach (Game currentGame in currentGames[client.Username])
                {
                    if (currentGame.Players.Contains(data.Opponent))
                    {
                        if (currentGame is ServerRPSLS)
                        {
                            ((ServerRPSLS) currentGame).Set(client.Username, data);
                        }
                    }
                }
            }
        }

        public void SetConnectFour(ClientHandler client, Packet packet)
        {
            ConnectFour data = packet.Data as ConnectFour;
            if (data != null)
            {
                foreach (Game currentGame in currentGames[client.Username])
                {
                    if (currentGame.Players.Contains(data.Opponent))
                    {
                        if (currentGame is ServerConnectFour)
                        {
                            ((ServerConnectFour) currentGame).Set(client.Username, data);
                        }
                    }
                }
            }
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
                AddClient(client);
                SendOnlineList(client);
                Console.WriteLine("Client accepted.");
            }

            rePacket.Data = response;
            client.send(rePacket);
        }

        public void AddClient(ClientHandler client)
        {
            onlineUsers.Add(client.Username, client);
            Packet packet = new Packet();
            packet.Flag = Flag.AddClient;
            packet.Data = client.Username;
            foreach (ClientHandler clientHandler in onlineUsers.Values)
            {
                if (client != clientHandler)
                {
                    clientHandler.send(packet);
                }
            }
        }

        public void RemoveClient(ClientHandler client)
        {
            onlineUsers.Remove(client.Username);
            //TODO close games of this user.
            //TODO remove user from dictionaries.

            Packet packet = new Packet();
            packet.Flag = Flag.RemoveClient;
            packet.Data = client.Username;
            foreach (ClientHandler clientHandler in onlineUsers.Values)
            {
                clientHandler.send(packet);
            }
            client.Disconnect();
        }

        public void SendOnlineList(ClientHandler client)
        {
            List<string> users = new List<string>(onlineUsers.Keys);
            Packet packet = new Packet();
            packet.Flag = Flag.OnlineUserList;
            packet.Data = users;
            client.send(packet);
        }

        public void SendResolvedGameSituation(string user, Packet packet)
        {
            onlineUsers[user].send(packet);
        }

        public void CreateGame(Packet packet, Flag flag)
        {
            switch (flag)
            {
                case Flag.Connect4:
                    ConnectFour c4data = packet.Data as ConnectFour;
                    if (c4data != null)
                    {
                        ServerConnectFour c4game = new ServerConnectFour(this, c4data.You, c4data.Opponent);
                        if (currentGames.ContainsKey(c4data.You))
                        {
                            currentGames[c4data.You].Add(c4game);
                        }
                        else
                        {
                            currentGames.Add(c4data.You, new List<Game>(){c4game});
                        }
                        if (currentGames.ContainsKey(c4data.Opponent))
                        {
                            currentGames[c4data.Opponent].Add(c4game);
                        }
                        else
                        {
                            currentGames.Add(c4data.Opponent, new List<Game>() { c4game });
                        }
                        Random r = new Random();
                        int starting = r.Next(0, 2);
                        for (int i = 0; i < c4game.Players.Count; i++)
                        {
                            c4data.ItIsYourTurn = starting == c4game.Players.IndexOf(c4game.Players[i]);
                            c4data.You = c4game.Players[i];
                            c4data.Opponent = c4game.Players[(i + 1)%2];
                            packet.Data = c4data;
                            onlineUsers[c4game.Players[i]].send(packet);
                        }
                    }
                    break;
                case Flag.RPSLS:
                    RockPaperScissorsLizardSpock rpslsData = packet.Data as RockPaperScissorsLizardSpock;
                    if (rpslsData != null)
                    {
                        ServerRPSLS rpslsGame = new ServerRPSLS(this, rpslsData.You, rpslsData.Opponent);
                        if (currentGames.ContainsKey(rpslsData.You))
                        {
                            currentGames[rpslsData.You].Add(rpslsGame);
                        }
                        else
                        {
                            currentGames.Add(rpslsData.You, new List<Game>() { rpslsGame });
                        }
                        if (currentGames.ContainsKey(rpslsData.Opponent))
                        {
                            currentGames[rpslsData.Opponent].Add(rpslsGame);
                        }
                        else
                        {
                            currentGames.Add(rpslsData.Opponent, new List<Game>() { rpslsGame });
                        }
                        foreach (string player in rpslsGame.Players)
                        {
                            onlineUsers[player].send(packet);
                        }
                        for (int i = 0; i < rpslsGame.Players.Count; i++)
                        {
                            rpslsData.You = rpslsGame.Players[i];
                            rpslsData.Opponent = rpslsGame.Players[(i + 1) % 2];
                            packet.Data = rpslsData;
                            onlineUsers[rpslsGame.Players[i]].send(packet);
                        }
                    }
                    break;
            }
        }

        public void RemoveGame(Game game)
        {
            foreach (string player in game.Players)
            {
                currentGames[player].Remove(game);
            }
        }

        public void RemoveGames(String player)
        {
            foreach (Game game in currentGames[player])
            {
                RemoveGame(game);
            }
        }

        public void HandleGameRequest(Packet packet)
        {
            BaseGame g = packet.Data as BaseGame;
            if (g != null)
                onlineUsers[g.Opponent].send(packet);
        }
    }
}