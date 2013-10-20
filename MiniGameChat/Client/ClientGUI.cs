﻿using System;
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
        private string opponent {get; set; }
        private string ip { get; set; }
        private string broadcast;
        private string handShake { get; set; }
        public string Tab;
        public Communication Comm;
        public NetworkStream NwStream;
        public List<string> onlineUserList;
        public Connect4 connect4;
        private Dictionary<string, string> openGames;
        
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

            openGames = new Dictionary<string, string>();

            Comm.IncommingMessageHandler += Comm_IncommingMessageHandler;

            buttonRpsls.Text = "Rock - Paper - Scissors -" + Environment.NewLine +"Lizard - Spock";
            panelGame1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;            
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
            NewGame newGame;

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
                    ConnectFour con4 = packet.Data as ConnectFour;
                    opponent = con4.You;

                    switch (con4.Situation)
                    {
                        case GameSituation.Connect : 
                            newGame = new NewGame();
                            newGame.setLabel(opponent + " verzoekt om te spelen");
                            if (newGame.ShowDialog() == DialogResult.OK)
                            {
                                tabController.SelectTab(opponent);
                                StartNewConnect4(opponent);
                            }
                            else
                            {
                                Packet DRpacket = new Packet(); // DR = Denied Request
                                con4.You = name;
                                con4.Opponent = opponent;
                                con4.Situation = GameSituation.Disconnect;
                                DRpacket.Data = con4;
                                Comm.OutgoingMessageHandler(DRpacket);
                            }
                            break;
                        case GameSituation.Disconnect:
                            if(openGames.ContainsKey(opponent))
                                openGames.Remove(opponent);
                            buttonConnect4.Enabled = true;
                            panelGame1.Controls.Clear();
                            break;

                        case GameSituation.Win:
                            labelSituation.Text = "You WON!!!";
                            labelSituation2.Text = "";
                            newGame = new NewGame();
                            if (newGame.ShowDialog() == DialogResult.OK)
                            {
                                StartNewConnect4(opponent);
                            }
                            else
                            {
                                if (openGames.ContainsKey(opponent))
                                    openGames.Remove(opponent);
                                panelGame1.Controls.Clear();
                            }
                            break;
                        case GameSituation.Loss:
                            labelSituation.Text = "You Lost...";
                            labelSituation2.Text = "";
                            newGame = new NewGame();
                            if (newGame.ShowDialog() == DialogResult.OK)
                            {
                                StartNewConnect4(opponent);
                            }
                            else
                            {
                                if (openGames.ContainsKey(opponent))
                                    openGames.Remove(opponent);
                                panelGame1.Controls.Clear();
                            }
                            break;
                        case GameSituation.Tie:
                            labelSituation.Text = "It's a Tie";
                            labelSituation2.Text = ""; 
                            newGame = new NewGame();
                            if (newGame.ShowDialog() == DialogResult.OK)
                            {
                                StartNewConnect4(opponent);
                            }
                            else
                            {
                                if (openGames.ContainsKey(opponent))
                                    openGames.Remove(opponent);
                                panelGame1.Controls.Clear();
                            }
                            break;

                        case GameSituation.Normal:
                            labelSituation.Text = "Playing...";
                            labelSituation2.Text = "Your Move";
                            connect4.Play(packet);
                            break;
                    }
                    break;

                case Flag.RPSLS:
                    RockPaperScissorsLizardSpock rpsls = packet.Data as RockPaperScissorsLizardSpock;
                    GameSituation game = rpsls.Situation;
                    Hands myhand = rpsls.YourHand;
                    Hands opponenthand = rpsls.OpponentHand;
                    opponent = rpsls.You;

                    labelSituation2.Text = "Other player chose " + opponenthand;
                    labelSituation.Text = "You chose " + myhand;
                    switch(game)
                    {
                        case GameSituation.Connect: 
                            newGame = new NewGame();
                            newGame.setLabel(opponent + " verzoekt om te spelen");
                            if (newGame.ShowDialog() == DialogResult.OK)
                            {
                                tabController.SelectTab(opponent);
                                StartNewRpsls(opponent);
                            }
                            else
                            {
                                Packet DRpacket = new Packet(); // DR = Denied Request
                                rpsls.You = name;
                                rpsls.Opponent = opponent;
                                rpsls.Situation = GameSituation.Disconnect;
                                DRpacket.Data = rpsls;
                                Comm.OutgoingMessageHandler(DRpacket);
                            }
                            break;

                        case GameSituation.Disconnect:
                            {
                                if (openGames.ContainsKey(opponent))
                                    openGames.Remove(opponent);
                                panelGame1.Controls.Clear();
                            }
                            buttonRpsls.Enabled = true;
                            panelGame1.Controls.Clear();
                            break;

                        case GameSituation.Tie: 
                            labelSituation2.Text = "It's a Tie";
                            labelSituation.Text = "";
                            newGame = new NewGame();
                            if (newGame.ShowDialog() == DialogResult.OK)
                            {
                                StartNewRpsls(opponent);
                            }
                            else
                            {
                                if (openGames.ContainsKey(opponent))
                                    openGames.Remove(opponent);
                                panelGame1.Controls.Clear();
                            } 
                            break;

                        case GameSituation.Loss: 
                            labelSituation2.Text = "You lost...";
                            labelSituation.Text = "";
                            newGame = new NewGame();
                            if (newGame.ShowDialog() == DialogResult.OK)
                            {
                                 StartNewRpsls(opponent);
                            }
                            else
                            {
                                if (openGames.ContainsKey(opponent))
                                    openGames.Remove(opponent);
                                panelGame1.Controls.Clear();
                            } 
                            break;

                        case GameSituation.Win: 
                            labelSituation2.Text = "YOU WON!!!";
                            labelSituation.Text = "";
                            newGame = new NewGame();
                            if (newGame.ShowDialog() == DialogResult.OK)
                            {
                                StartNewRpsls(opponent);
                            }
                            else
                            {
                                if (openGames.ContainsKey(opponent))
                                    openGames.Remove(opponent);
                                panelGame1.Controls.Clear();
                            }
                            break;
                    }
                    
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
            if (tabController.SelectedTab.Name != broadcast)
            {
                string opponent = tabController.SelectedTab.Name;
                if (opponent != null)
                    StartNewRpsls(opponent);
            }
            buttonConnect4.Enabled = true;
        }

        public void StartNewRpsls(string opponent)
        {
            panelGame1.Controls.Clear();
            RPSLS rpsls = new RPSLS(name, opponent);            
            rpsls.RPSLSChoice += rpsls_RPSLSChoice;

            Packet packet = new Packet();
            packet.Flag = Flag.RPSLS;
            
            RockPaperScissorsLizardSpock rpslsRequest = new RockPaperScissorsLizardSpock(name, opponent, GameSituation.Connect);
            packet.Data = rpslsRequest;
            Comm.OutgoingMessageHandler(packet); 

            panelGame1.Controls.Add(rpsls);
            labelSituation.Text = "...starting game";
            if (openGames.ContainsKey(opponent))
                openGames.Remove(opponent);
            openGames.Add(opponent, "rpsls");
            buttonRpsls.Enabled = false;
       
        }

        public void StartNewConnect4(string opponent)
        {
            panelGame1.Controls.Clear();
            connect4 = new Connect4(name,opponent);
            connect4.connect4SChoice += connect4_connect4Choice;

            Packet packet = new Packet();
            packet.Flag = Flag.Connect4;

            ConnectFour connectFourRequest = new ConnectFour(name, opponent,GameSituation.Connect);
            packet.Data = connectFourRequest;

            panelGame1.Controls.Add(connect4);
            labelSituation.Text = "...starting game";
            if (openGames.ContainsKey(opponent))
                openGames.Remove(opponent);
            openGames.Add(opponent, "connect4");
            buttonConnect4.Enabled = false;
        }

        public void connect4_connect4Choice(Packet packet)
        {
            Comm.OutgoingMessageHandler(packet);
        }

        public void rpsls_RPSLSChoice(Packet packet)
        {
            RockPaperScissorsLizardSpock rpsls = packet.Data as RockPaperScissorsLizardSpock;
            Hands hand = rpsls.YourHand;
            labelSituation.Text = "You chose " + hand.ToString();
            labelSituation2.Text = "...waiting for other player";
            Comm.OutgoingMessageHandler(packet);
        }

        private void buttonConnect4_Click(object sender, EventArgs e)
        {
            if (tabController.SelectedTab.Name != broadcast)
            {
                string opponent = tabController.SelectedTab.Name;
                StartNewConnect4(opponent);
            }
            buttonRpsls.Enabled = true;
        }

        private void textChat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                Send();
                e.Handled = true;
            }
        }

        private void tabController_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (openGames.ContainsKey(tabController.SelectedTab.Name))
            {
                string value = openGames[tabController.SelectedTab.Name];
	    
                switch(value)
                {
                    case "rpsls":
                        buttonRpsls.Enabled = false;
                        buttonConnect4.Enabled = true;
                        break;
                    case "connect4": 
                        buttonRpsls.Enabled = true;
                        buttonConnect4.Enabled = false;
                        break;
                }
            }
            else
            {
                buttonRpsls.Enabled = true;
                buttonConnect4.Enabled = true;
            }

        }

        
    }
}
