using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacketLibrary
{
    class Packet
    {
        public Flag Flag { get; set; }
        public Object Data { get; set; }
    }
}
