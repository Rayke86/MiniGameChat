using System;

namespace PacketLibrary
{
    [Serializable]
    public class ChatMessage
    {
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string Message { get; set; }
    }
}
