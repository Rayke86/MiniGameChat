﻿using System;
using System.Collections.Generic;
using PacketLibrary;

namespace Server
{
    public abstract class Game
    {
        public List<String> Players { get; set; }
        public ServerMain serverMain;

        public Game(ServerMain serverMain, String player1, String player2)
        {
            Players = new List<string> {player1, player2};
            this.serverMain = serverMain;
        }
        public abstract void GameCheck();
        public abstract void Set(string player, BaseGame baseGame);
    }
}
