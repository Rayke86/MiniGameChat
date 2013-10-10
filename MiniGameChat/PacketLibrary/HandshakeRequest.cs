using System;

namespace PacketLibrary
{
    [Serializable]
    public class HandshakeRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
