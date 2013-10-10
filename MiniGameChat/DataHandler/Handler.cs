using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacketLibrary;

namespace DataHandler
{
    public delegate void IncommingMessageHandler(Packet packet);

    public class Handler
    {
        public event IncommingMessageHandler IncommingMessageHandler;

        public void OnIncommingMessageHandler(Packet packet)
        {
            if (IncommingMessageHandler != null)
                IncommingMessageHandler(packet);
        }
    }

}
