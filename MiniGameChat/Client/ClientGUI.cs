using System;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using PacketLibrary;
using System.Collections.Generic;

namespace Client
{
    public partial class ClientGUI : Form
    {
        private string name { get; set; }
        private string ip { get; set; }
        private string broadcast;
        private string handShake { get; set; }
        public Communication Comm;
        public NetworkStream NwStream;
        public List<string> onlineUserList; 
        
        public ClientGUI(string ip, string name)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            this.name = name;
            this.ip = ip;
            this.broadcast = "BROADCAST";

            tabController.TabPages.Add(broadcast);
            tabController.TabPages[0].Name = broadcast;
            tabController.TabPages[0].Controls.Add(new ChatPanel());

            Comm = new Communication(ip, name);

            Comm.IncommingMessageHandler += Comm_IncommingMessageHandler;
        }

        public void addPages(string name)
        {
            int number = tabController.Controls.Count;
            tabController.TabPages.Add(name);
            tabController.TabPages[number].Name = name;
            tabController.TabPages[number].Controls.Add(new ChatPanel());
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            //send text to server
            string receiver = tabController.SelectedTab.Name;
            string text = textChat.Text;

            Packet packet = new Packet();
            packet.Flag = Flag.Chat;
            ChatMessage message = new ChatMessage();
            message.Sender = name;
            message.Message = text;
            message.Receiver = receiver;

            packet.Data = message;

            Comm.OutgoingMessageHandler(packet);

            textChat.Clear();
            textChat.Focus();
        }

        public void Comm_IncommingMessageHandler(Packet packet)
        {
            switch (packet.Flag)
            {
                case Flag.Chat:
                    ChatMessage message = packet.Data as ChatMessage;
                    string sender = message.Sender;
                    string chatMessage = message.Message;
                    if (sender == name)
                        sender = message.Receiver;
                    AddText(chatMessage, sender);
                    break;

                case Flag.OnlineUserList: onlineUserList = packet.Data as List<string>;
                            foreach(string user in onlineUserList)
                            {
                                addPages(user);
                            }
                    break;

                case Flag.Connect4:
                    break;

                case Flag.RPSLS:
                    break;

                case Flag.HandshakeResponse: 
                    HandshakeResponse shake = packet.Data as HandshakeResponse;
                    handShake = shake.Response.ToString();

                    if (shake.Response == Response.INVALIDLOGIN)
                    {                                
                        InvalidNamePopUp ivnp = new InvalidNamePopUp();
                        if (ivnp.ShowDialog() == DialogResult.OK)
                        {
                            name = ivnp.name;
                            ivnp.Dispose();
                            Comm.Handshake();
                        }
                    }

                    AddText("Handshake = " + handShake, broadcast);
                    break;
            }

            
        }

        public void AddText(string text, string sender)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                Console.WriteLine("selected tab = " + tabController.SelectedTab);
                tabController.SelectTab(sender);
                int index = tabController.SelectedTab.TabIndex;
                ChatPanel panel = (ChatPanel) tabController.TabPages[index].Controls[0];
                panel.addChat(text);
            }));
        }

        
    }
}
