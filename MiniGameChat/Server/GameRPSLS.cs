using System;
using System.Collections.Generic;
using PacketLibrary;

namespace Server
{
    class GameRPSLS : Game
    {
        private List<Dictionary<string, Hands>> Rounds;
        private Dictionary<string, Hands> ChosenHands;

        public GameRPSLS(string player) : base(player)
        {
            ChosenHands = new Dictionary<string, Hands>();
            Rounds = new List<Dictionary<string, Hands>>();
        }

        public override void GameWinLossCheck()
        {
            throw new NotImplementedException();
        }
    }
}
