using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using DataHandler;
using PacketLibrary;

namespace Client
{
    public class Communication
    {
        public TcpClient client;
        public NetworkStream NwStream;
        public Handler handler;

        public Communication(string ip)
        {
            client = new TcpClient(ip, 1330);

            NwStream = client.GetStream();

            bool run = true;
            while (run)
            {
                try
                {
                    BinaryFormatter format = new BinaryFormatter();
                    Packet packet = format.Deserialize(client.GetStream()) as Packet;
                    handler.OnIncommingMessageHandler(packet);
                }
                catch (Exception e)
                {
                    if (e is IOException || e is SerializationException)
                    {
                        run = false;
                    }
                }
            }
        }

        public void OutgoingMessageHandler(Packet packet)
        {
            BinaryFormatter binfor = new BinaryFormatter();

            try
            {
                binfor.Serialize(NwStream, packet);
            }
            catch (Exception e)
            {

            }
        }


    }
}
   
