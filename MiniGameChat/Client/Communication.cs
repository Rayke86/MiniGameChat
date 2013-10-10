using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public delegate void IncommingMessageHandler(object sender, EventArgs e);

    public class Communication
    {
        public event IncommingMessageHandler Imh;

        public Communication(string ip)
        {
            TcpClient client = new TcpClient(ip, 1330);
            //bool done = false;
            //while (!done)
            //{
            //    Console.Write("Enter a message to send to server: ");
            //    string message = Console.ReadLine();

            //    SendMessage(client, message);

            //    string response = ReadResponse(client);
            //    Console.WriteLine("Response: " + response);
            //    done = response.Equals("BYE");
            //}
        }

        private static void SendMessage(TcpClient client, string message)
        {
            //make sure the other end decodes with the same format!
            if (message == null)
                message = "test";

            byte[] bytes = Encoding.Unicode.GetBytes(message);

            client.GetStream().Write(bytes, 0, bytes.Length);
        }


        private static string ReadResponse(TcpClient client)
        {
            byte[] buffer = new byte[256];
            int totalRead = 0;

            //read bytes until stream indicates there are no more:
            do
            {
                //blocks until client sends message:	
                int read = client.GetStream().Read
                    (buffer, totalRead, buffer.Length - totalRead);

                totalRead += read;
            } while (client.GetStream().DataAvailable);

            return Encoding.Unicode.GetString(buffer, 0, totalRead);

        }


    }
}
   
