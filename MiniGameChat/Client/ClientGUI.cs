using System;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using DataHandler;
using PacketLibrary;

namespace Client
{
    public partial class ClientGUI : Form
    {
        private string name { get; set; }
        private string ip { get; set; }
        public Communication Comm;
        public Handler handler;
        public NetworkStream NwStream;
        
        public ClientGUI(string ip, string name)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            this.name = name;
            this.ip = ip;

            handler = new Handler();

            Comm = new Communication(ip, name);

            handler.IncommingMessageHandler += IncommingMessageHandler;
            
            tabController.TabPages.Add("BROADCAST");
            tabController.TabPages[0].Name = "BROADCAST";
            tabController.TabPages[0].Controls.Add(new ChatPanel());

            addPages("Johannes");
            addPages("Ray");

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

        public void IncommingMessageHandler(Packet packet)
        {
            switch (packet.Flag)
            {
                case Flag.Chat:
                    ChatMessage message = packet.Data as ChatMessage;
                    string sender = message.Sender;
                    string chatMessage = message.Message;
 
                    tabController.SelectTab(sender);
                    AddText(chatMessage);

                    break;

                case Flag.Connect4:
                    break;

                case Flag.RPSLS:
                    break;
                case Flag.HandshakeResponse: 
                    HandshakeResponse shake = packet.Data as HandshakeResponse;

                    tabController.SelectTab("BROADCAST");
                    AddText(shake.Response.ToString());
                    break;
            }
        }

        public void AddText(string text)
        {
            int index = tabController.SelectedTab.TabIndex;
            ChatPanel panel = (ChatPanel)tabController.TabPages[index].Controls[0];
            panel.addChat(text);
        }

        
    }
}
