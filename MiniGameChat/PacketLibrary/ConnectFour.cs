using System;

namespace PacketLibrary
{
    [Serializable]
    public class ConnectFour
    {
        public GameSituation Situation { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
