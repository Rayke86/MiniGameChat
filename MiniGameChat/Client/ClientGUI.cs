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
         
            buttonConnect4.Enabled = false;
                buttonRpsls.Enabled = false;
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

                case Flag.GameRequest: 
                    if(packet.Data is ConnectFour)
                    {
                        ConnectFour con4 = packet.Data as ConnectFour;
                        opponent = con4.You;
                        switch (con4.Situation)
                        {
                            case GameSituation.Connect : 
                            newGame = new NewGame();
                            newGame.setLabel(opponent + " verzoekt om te spelen");
                            if(newGame.ShowDialog() == DialogResult.OK)
                            {
                                packet = new Packet();
                                packet.Flag = Flag.GameResponse;
                                con4.Situation = GameSituation.Connect;
                                con4.You = name;
                                con4.Opponent = opponent;
                                packet.Data = con4;
                                Comm.OutgoingMessageHandler(packet);
                                //tabController.SelectTab(opponent);
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
                        }
                    }

                    //if (packet.Data is RPSLS)
                    //{
                    //    RockPaperScissorsLizardSpock rpsls = packet.Data as RockPaperScissorsLizardSpock;
                    //    opponent = rpsls.You;
                    //    switch (rpsls.Situation)
                    //    {
                    //        case GameSituation.Connect:
                    //            newGame = new NewGame();
                    //            newGame.setLabel(opponent + " verzoekt om te spelen");
                    //            if (newGame.ShowDialog() == DialogResult.OK)
                    //            {
                    //                packet = new Packet();
                    //                packet.Flag = Flag.GameResponse;
                    //                rpsls.Situation = GameSituation.Connect;
                    //                rpsls.You = name;
                    //                rpsls.Opponent = opponent;
                    //                packet.Data = rpsls;
                    //                Comm.OutgoingMessageHandler(packet);
                    //                tabController.SelectTab(opponent);
                    //                StartNewRpsls(opponent,false);
                    //            }
                    //            else
                    //            {
                    //                Packet DRpacket = new Packet(); // DR = Denied Request
                    //                rpsls.You = name;
                    //                rpsls.Opponent = opponent;
                    //                rpsls.Situation = GameSituation.Disconnect;
                    //                DRpacket.Data = rpsls;
                    //                Comm.OutgoingMessageHandler(DRpacket);
                    //            }
                    //            break;
                    //    }
                    //}
                    break;

                case Flag.GameResponse : 
                    if(packet.Data is ConnectFour)
                    {
                        ConnectFour con4 = packet.Data as ConnectFour;

                        switch (con4.Situation)
                        {
                            case GameSituation.Connect :
                                                                
                                StartNewConnect4(con4.Opponent, con4.ItIsYourTurn);

                                connect4.start(con4.ItIsYourTurn);
                                break;

                            case GameSituation.Disconnect:
                                Console.WriteLine("disconnect");

                                labelSituation.Text = "Game afgewezen";
                                labelSituation2.Text = "";
                                break;
                        }
                    }

                    //if (packet.Data is RPSLS)
                    //{
                    //    RockPaperScissorsLizardSpock rpsls = packet.Data as RockPaperScissorsLizardSpock;
                    //    switch (rpsls.Situation)
                    //    {
                    //        case GameSituation.Connect:
                    //            tabController.SelectTab(opponent);
                    //            StartNewRpsls(opponent,false);                                
                    //            break;

                    //        case GameSituation.Disconnect:
                    //            labelSituation.Text = "Game afgewezen";
                    //            break;
                    //    }
                    //}
                    break;

                case Flag.Connect4:
                    ConnectFour connect_four = packet.Data as ConnectFour;
                    opponent = connect_four.You;

                    switch (connect_four.Situation)
                    {                        
                        case GameSituation.Disconnect:
                            if(openGames.ContainsKey(opponent))
                                openGames.Remove(opponent);
                            buttonConnect4.Enabled = true;
                            panelGame1.Controls.Clear();
                            break;

                        case GameSituation.Win:
                            labelSituation.Text = "You WON!!!";
                            labelSituation2.Text = "";
                            break;

                        case GameSituation.Loss:
                            labelSituation.Text = "You Lost...";
                            labelSituation2.Text = "";
                            newGame = new NewGame();
                            if (newGame.ShowDialog() == DialogResult.OK)
                            {
                                SendConnect4Request();
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
                            break;

                        case GameSituation.Normal:                            
                            this.Invoke(new MethodInvoker(() =>
                            {
                                labelSituation.Text = "Playing...";
                                labelSituation2.Text = "Your Move";
                                if (connect_four.SetPlayedBy != name)
                                    connect4.Play(packet);
                            }));
                            break;
                    }
                    break;

                //case Flag.RPSLS:
                //    RockPaperScissorsLizardSpock ropasilisp = packet.Data as RockPaperScissorsLizardSpock;
                //    GameSituation game = ropasilisp.Situation;
                //    Hands myhand = ropasilisp.YourHand;
                //    Hands opponenthand = ropasilisp.OpponentHand;
                //    opponent = ropasilisp.You;

                //    labelSituation2.Text = "Other player chose " + opponenthand;
                //    labelSituation.Text = "You chose " + myhand;
                //    switch (game)
                //    {
                //        case GameSituation.Disconnect:
                //            {
                //                if (openGames.ContainsKey(opponent))
                //                    openGames.Remove(opponent);
                //                panelGame1.Controls.Clear();
                //            }
                //            buttonRpsls.Enabled = true;
                //            panelGame1.Controls.Clear();
                //            break;

                //        case GameSituation.Tie:
                //            labelSituation2.Text = "It's a Tie";
                //            labelSituation.Text = "";
                //            newGame = new NewGame();
                //            if (newGame.ShowDialog() == DialogResult.OK)
                //            {
                //                StartNewRpsls(opponent);
                //            }
                //            else
                //            {
                //                if (openGames.ContainsKey(opponent))
                //                    openGames.Remove(opponent);
                //                panelGame1.Controls.Clear();
                //            }
                //            break;

                //        case GameSituation.Loss:
                //            labelSituation2.Text = "You lost...";
                //            labelSituation.Text = "";
                //            newGame = new NewGame();
                //            if (newGame.ShowDialog() == DialogResult.OK)
                //            {
                //                StartNewRpsls(opponent);
                //            }
                //            else
                //            {
                //                if (openGames.ContainsKey(opponent))
                //                    openGames.Remove(opponent);
                //                panelGame1.Controls.Clear();
                //            }
                //            break;

                //        case GameSituation.Win:
                //            labelSituation2.Text = "YOU WON!!!";
                //            labelSituation.Text = "";
                //            newGame = new NewGame();
                //            if (newGame.ShowDialog() == DialogResult.OK)
                //            {
                //                StartNewRpsls(opponent);
                //            }
                //            else
                //            {
                //                if (openGames.ContainsKey(opponent))
                //                    openGames.Remove(opponent);
                //                panelGame1.Controls.Clear();
                //            }
                //            break;
                //    }

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

        private void buttonConnect4_Click(object sender, EventArgs e)
        {
            if (tabController.SelectedTab.Name != broadcast)
            {
                SendConnect4Request();
            }
            buttonRpsls.Enabled = false;
        }

        public void SendConnect4Request()
        {
            string opp = tabController.SelectedTab.Name;
            ConnectFour con4Request = new ConnectFour(name, opp, GameSituation.Connect);
            Packet packet = new Packet();
            packet.Flag = Flag.GameRequest;
            packet.Data = con4Request;
            Comm.OutgoingMessageHandler(packet);
        }

        private void buttonRpsls_Click(object sender, EventArgs e)
        {
            if (tabController.SelectedTab.Name != broadcast)
            {
                string opp = tabController.SelectedTab.Name;
                RockPaperScissorsLizardSpock rpsls = new RockPaperScissorsLizardSpock(name, opp, GameSituation.Connect);
                Packet packet = new Packet();
                packet.Flag = Flag.GameRequest;
                packet.Data = rpsls;
                Comm.OutgoingMessageHandler(packet);
            }
            buttonConnect4.Enabled = false;
        }

        //public void StartNewRpsls(string opp)
        //{
        //    this.Invoke(new MethodInvoker(() =>
        //    {
        //        tabController.SelectTab(opp);
        //        panelGame1.Controls.Clear();
        //        RPSLS rpsls = new RPSLS(name, opp);
        //        rpsls.RPSLSChoice += rpsls_RPSLSChoice;

        //        panelGame1.Controls.Add(rpsls);
        //        labelSituation.Text = "...starting game";
        //        if (openGames.ContainsKey(opp))
        //            openGames.Remove(opp);
        //        openGames.Add(opp, "rpsls");
        //        buttonRpsls.Enabled = false;
        //    }));
        //}

        public void StartNewConnect4(string opp,bool turn)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                panelGame1.Controls.Clear();
                connect4 = new Connect4(name, opp);                
                connect4.connect4SChoice += connect4_connect4Choice;
                                                
                panelGame1.Controls.Add(connect4);
                labelSituation.Text = "...starting game";
                if(turn)
                    labelSituation.Text = "It's your turn";
                else
                    labelSituation.Text = "It's not your turn";

                if (openGames.ContainsKey(opp))
                    openGames.Remove(opp);

                openGames.Add(opp, "connect4");
                buttonConnect4.Enabled = false;
                }));
        }

        public void connect4_connect4Choice(Packet packet)
        {
            labelSituation2.Text = "It's not your turn";
            labelSituation.Text = "";
            Comm.OutgoingMessageHandler(packet);
        }

        //public void rpsls_RPSLSChoice(Packet packet)
        //{
        //    RockPaperScissorsLizardSpock rpsls = packet.Data as RockPaperScissorsLizardSpock;
        //    Hands hand = rpsls.YourHand;
        //    labelSituation.Text = "You chose " + hand.ToString();
        //    labelSituation2.Text = "...waiting for other player";
        //    Comm.OutgoingMessageHandler(packet);
        //}        

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

        private void buttonForfeit_Click(object sender, EventArgs e)
        {
            panelGame1.Controls.Clear();
            if (openGames.ContainsKey(tabController.SelectedTab.Name))
            {
                openGames.Remove(tabController.SelectedTab.Name);
            }
            labelSituation.Text = "";
            labelSituation2.Text = "";

            buttonRpsls.Enabled = true;
            buttonConnect4.Enabled = true;
        }

        
    }
}
