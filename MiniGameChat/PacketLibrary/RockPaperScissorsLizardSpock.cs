using System;

namespace PacketLibrary
{
    [Serializable]
    public class RockPaperScissorsLizardSpock
    {
        public GameSituation Situation { get; set; }
        public Hands Hand { get; set; }
    }
}
