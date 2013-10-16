using System;
using System.Collections.Generic;

namespace Server
{
    public abstract class Game
    {
        public List<String> Players { get; set; }
        public ServerMain serverMain;

        public Game(ServerMain serverMain, String player)
        {
            Players = new List<string> {player};
            this.serverMain = serverMain;
        }
    }

    public delegate void GameCheck(Game game);
}
