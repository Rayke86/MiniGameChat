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
using PacketLibrary;

namespace Client
{
    public delegate void IncommingMessageHandler(Packet packet);

    public class Communication
    {
        public TcpClient client;
        public NetworkStream NwStream;
        public string User;
        public Thread thread;

        public event IncommingMessageHandler IncommingMessageHandler;

        public Communication(string ip, string user)
        {
            client = new TcpClient(ip, 1330);
            this.User = user;
            NwStream = client.GetStream();
            thread = new Thread(new ThreadStart(StartRunning));
            thread.Start();
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
            Handshake();
            bool run = true;
            while (run)
            {
                try
                {
                    BinaryFormatter format = new BinaryFormatter();
                    Packet packet = format.Deserialize(client.GetStream()) as Packet;
                    OnIncommingMessageHandler(packet);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
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

        protected virtual void OnIncommingMessageHandler(Packet packet)
        {
            if (packet != null)
                IncommingMessageHandler(packet);
        }

    }
}
   
