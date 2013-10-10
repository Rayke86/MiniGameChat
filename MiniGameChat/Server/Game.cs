using System;

namespace Server
{
    public class Game
    {
        public String Player1 { get; set; }
        public String Player2 { get; set; }

        public Game(String player)
        {
            Player1 = player;
        }
    }
}
