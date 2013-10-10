using System;

namespace PacketLibrary
{
    [Serializable]
    public class Packet
    {
        public Flag Flag { get; set; }
        public Object Data { get; set; }
    }
}
