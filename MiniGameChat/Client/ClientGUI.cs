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
        public string Tab;
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
            this.Text = name;
            tabController.TabPages.Add(broadcast);
            tabController.TabPages[0].Name = broadcast;
            tabController.TabPages[0].Controls.Add(new ChatPanel());

            Comm = new Communication(ip, name);

            Comm.IncommingMessageHandler += Comm_IncommingMessageHandler;

            buttonRpsls.Text = "Rock - Paper - Scissors -" + Environment.NewLine +"Lizard - Spock";
        }

        public void addPages(string name)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                int number = tabController.Controls.Count;
                tabController.TabPages.Add(name);
                tabController.TabPages[number].Name = name;
                tabController.TabPages[number].Controls.Add(new ChatPanel());
            }));
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            Send();
        }

        public void Send()
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
                    {
                        Tab = message.Receiver;
                        AddText(chatMessage, Tab,true);
                    }
                    else
                        AddText(chatMessage, sender,false);
                    break;

                case Flag.OnlineUserList: 
                    onlineUserList = packet.Data as List<string>;
                    foreach(string user in onlineUserList)
                    {
                        if(user != name)
                        addPages(user);
                    }
                    break;

                case Flag.AddClient:
                    string newUser = packet.Data as string;
                    addPages(newUser);
                    break;

                case Flag.Connect4:
                    break;

                case Flag.RPSLS:
                    RockPaperScissorsLizardSpock rpsls = packet.Data as RockPaperScissorsLizardSpock;
                    GameSituation game = rpsls.Situation;
                    Hands myhand = rpsls.YourHand;
                    Hands opponenthand = rpsls.OpponentHand;
                    labelSituation2.Text = "Other player chose " + opponenthand;
                    labelSituation.Text = "You chose " + myhand;
                    NewGame newGame = new NewGame();
                    if (newGame.ShowDialog() == DialogResult.OK)
                    {
                        StartNewRpsls();
                    }
                    else
                        panelGame1.Controls.Clear();
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
                            Comm.User = name;
                            ivnp.Dispose();
                            Comm.Handshake();
                        }
                    }

                    AddText("Handshake = " + handShake, broadcast,false);
                    break;
            }            
        }

        public void AddText(string text, string sender,bool isMe)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                if (isMe)
                {
                    tabController.SelectTab(sender);
                    int index = tabController.SelectedTab.TabIndex;
                    ChatPanel panel = (ChatPanel)tabController.TabPages[index].Controls[0];
                    panel.addChat(name + " : " + text);
                }
                else
                {
                    tabController.SelectTab(sender);
                    int index = tabController.SelectedTab.TabIndex;
                    ChatPanel panel = (ChatPanel)tabController.TabPages[index].Controls[0];
                    panel.addChat(sender + " : " + text);
                }
            }));
        }

        private void ClientGUI_FormClosed(object sender, FormClosedEventArgs e)
        {
            Packet packet = new Packet();
            packet.Flag = Flag.RemoveClient;
            packet.Data = name;
            Comm.OutgoingMessageHandler(packet);
            Comm.CloseConnection();
        }

        private void buttonRpsls_Click(object sender, EventArgs e)
        {
            StartNewRpsls();
        }

        public void StartNewRpsls()
        {
            if (tabController.SelectedTab.Name != broadcast)
            {
                panelGame1.Controls.Clear();
                RPSLS rpsls = new RPSLS();
                rpsls.RPSLSChoice += rpsls_RPSLSChoice;
                panelGame1.Controls.Add(rpsls);
                labelSituation.Text = "...starting game";
            }
        }

        public void rpsls_RPSLSChoice(Packet packet)
        {
            RockPaperScissorsLizardSpock rpsls = packet.Data as RockPaperScissorsLizardSpock;
            Hands hand = rpsls.YourHand;
            labelSituation.Text = "You chose " + hand.ToString();
            labelSituation2.Text = "...waiting for other player";
            Comm.OutgoingMessageHandler(packet);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panelGame1.Controls.Clear();
        }

        private void textChat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                Send();
                e.Handled = true;
            }
        }

        
    }
}
