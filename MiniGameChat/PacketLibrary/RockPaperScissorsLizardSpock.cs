using System;

namespace PacketLibrary
{
    [Serializable]
    public class RockPaperScissorsLizardSpock
    {
        public GameSituation Situation { get; set; }
        public Hands YourHand { get; set; }
        public Hands OpponentHand { get; set; }
        public string You { get; set; }
        public string Opponent { get; set; }
    }
}
