using System;
using System.Collections.Generic;

namespace Server
{
    public abstract class Game
    {
        public List<String> Players { get; set; }

        public Game(String player)
        {
            Players = new List<string> {player};
        }
    }

    public delegate void GameCheck(Game game);
}
