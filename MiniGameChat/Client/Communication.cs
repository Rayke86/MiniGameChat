using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting.Channels;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using DataHandler;
using PacketLibrary;

namespace Client
{
    public class Communication
    {
        public TcpClient client;
        public NetworkStream NwStream;
        private Handler handler;
        public string User;

        public Communication(string ip, string user)
        {
            client = new TcpClient(ip, 1330);
            this.User = user;
            NwStream = client.GetStream();
            handler = new Handler();
            Thread thread = new Thread(new ThreadStart(StartRunning));
            thread.Start();

            Handshake();
        }

        public void Handshake()
        {
            Packet packet = new Packet();

            packet.Flag = Flag.HandshakeRequest;
            HandshakeRequest request = new HandshakeRequest();
            request.Username = User;
            packet.Data = request;
            OutgoingMessageHandler(packet);
        }

        public void StartRunning()
        {
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
                Console.WriteLine(e.StackTrace);
            }
        }


    }
}
   
