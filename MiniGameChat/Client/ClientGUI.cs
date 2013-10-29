using System;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using PacketLibrary;
using System.Collections.Generic;
using System.Drawing;

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
        public Panel panelGame1;
        private Dictionary<string, string> openGames;
        
        public ClientGUI(string ip, string name)
        {            
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            panelGame1 = new Panel();
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

            this.BackColor = Color.Gray;

            buttonConnect4.Enabled = false;
            buttonRpsls.Enabled = false;

        }

        public Image getImage(string img)
        {
            Image image = null;

            try
            {
                image = Image.FromFile("../../Images/" + img + ".gif");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return image;
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

        public void addGamePages(string opp, string game)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                int number = tabControlGame.Controls.Count;
                tabControlGame.TabPages.Add(opp);
                tabControlGame.TabPages[number].Name = opp;
                switch (game)
                {
                    case "rpsls": RPSLS rpsls = new RPSLS(name, opp);
                        rpsls.BackColor = Color.Gray;
                        tabControlGame.TabPages[number].Controls.Add(rpsls);
                        tabControlGame.TabPages[number].BackColor = Color.Gray;
                        rpsls.RPSLSChoice += rpsls_RPSLSChoice;
                        break;

                    case "connect4": connect4 = new Connect4(name, opp);
                        connect4.BackColor = Color.Gray;
                        tabControlGame.TabPages[number].Controls.Add(connect4);
                        tabControlGame.TabPages[number].BackColor = Color.Gray;
                        connect4.connect4SChoice += connect4_connect4Choice;
                        break;
                }
                tabControlGame.Visible = true;
            }));
        }

        public void removePages(string name)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                int number = tabController.Controls.Count;
                tabController.TabPages.RemoveByKey(name);
            }));
        }

        public void removeGamePages(string tab)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                if(tab != null)
                tabControlGame.TabPages.RemoveByKey(tab);

                if (tabControlGame.TabPages.Count == 0)
                {
                    tabControlGame.Visible = false;
                }
                else
                {
                    tabControlGame.Visible = true;
                }

                buttonConnect4.Enabled = true;
                buttonRpsls.Enabled = true;
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

                case Flag.RemoveClient :
                    string oldUser = packet.Data as string;
                    removePages(oldUser);
                    break;
                    

                case Flag.GameRequest: 
                    if(packet.Data is ConnectFour)
                    {
                        ConnectFour con4 = packet.Data as ConnectFour;
                        opponent = con4.You;
                        EndGameLabel("");
                        switch (con4.Situation)
                        {
                            case GameSituation.Connect : 
                            newGame = new NewGame();
                            newGame.setLabel(opponent + " requests to play " + Environment.NewLine + " Connect 4");
                            if(newGame.ShowDialog() == DialogResult.OK)
                            {
                                packet = new Packet();
                                packet.Flag = Flag.GameResponse;
                                con4.Situation = GameSituation.Connect;
                                con4.You = name;
                                con4.Opponent = opponent;
                                packet.Data = con4;
                                Comm.OutgoingMessageHandler(packet);
                            }
                            else
                            {
                                Packet DRpacket = new Packet();
                                con4.You = name;
                                con4.Opponent = opponent;
                                con4.Situation = GameSituation.Disconnect;
                                DRpacket.Data = con4;
                                Comm.OutgoingMessageHandler(DRpacket);
                            }
                            break;
                        }
                    }

                    if (packet.Data is RockPaperScissorsLizardSpock)
                    {
                        RockPaperScissorsLizardSpock rpsls = packet.Data as RockPaperScissorsLizardSpock;
                        opponent = rpsls.You;
                        EndGameLabel("");
                        switch (rpsls.Situation)
                        {
                            case GameSituation.Connect:
                                newGame = new NewGame();
                                newGame.setLabel(opponent + " requests to play" + Environment.NewLine + "Rock-Paper-Scissors-Lizard-Spock");
                                if (newGame.ShowDialog() == DialogResult.OK)
                                {
                                    packet = new Packet();
                                    packet.Flag = Flag.GameResponse;
                                    rpsls.Situation = GameSituation.Connect;
                                    rpsls.You = name;
                                    rpsls.Opponent = opponent;
                                    packet.Data = rpsls;
                                    Comm.OutgoingMessageHandler(packet);
                                }
                                else
                                {
                                    Packet DRpacket = new Packet();
                                    rpsls.You = name;
                                    rpsls.Opponent = opponent;
                                    rpsls.Situation = GameSituation.Disconnect;
                                    DRpacket.Data = rpsls;
                                    Comm.OutgoingMessageHandler(DRpacket);
                                }
                                break;
                        }
                    }
                    break;

                case Flag.GameResponse : 
                    if(packet.Data is ConnectFour)
                    {
                        ConnectFour con4 = packet.Data as ConnectFour;
                        EndGameLabel("");
                        switch (con4.Situation)
                        {
                            case GameSituation.Connect :                                                                
                                StartNewConnect4(con4.Opponent, con4.ItIsYourTurn);
                                connect4.start(con4.ItIsYourTurn);
                                break;

                            case GameSituation.Disconnect:
                                Console.WriteLine("disconnect");

                                EndGameLabel("Game denied");
                                break;
                        }
                    }

                    if (packet.Data is RockPaperScissorsLizardSpock)
                    {
                        RockPaperScissorsLizardSpock rpsls = packet.Data as RockPaperScissorsLizardSpock;
                        EndGameLabel("");
                        switch (rpsls.Situation)
                        {
                            case GameSituation.Connect:
                                StartNewRpsls(rpsls.Opponent);
                                break;

                            case GameSituation.Disconnect:
                                EndGameLabel("Game denied");
                                break;
                        }
                    }
                    break;

                case Flag.Connect4:
                    ConnectFour connect_four = packet.Data as ConnectFour;
                    opponent = connect_four.Opponent;
                    EndGameLabel("Playing...");
                    switch (connect_four.Situation)
                    {                        
                        case GameSituation.Disconnect:
                            if(openGames.ContainsKey(opponent))
                                openGames.Remove(opponent);
                            EndGameLabel("Disconnected");
                            buttonConnect4.Enabled = true;
                            removeGamePages(opponent);
                            break;

                        case GameSituation.Win:
                            removeGamePages(opponent);
                            EndGameLabel("You WON!!!");
                            break;

                        case GameSituation.Loss:
                            playingConnect4(connect_four, packet);
                            EndGameLabel("You Lost...");
                            removeGamePages(opponent);
                            newGame = new NewGame();
                            if (newGame.ShowDialog() == DialogResult.OK)
                            {
                                SendConnect4Request(opponent);
                            }
                            else
                            {
                                if (openGames.ContainsKey(opponent))
                                    openGames.Remove(opponent);                                
                            }
                            break;
                        case GameSituation.Tie:
                            EndGameLabel("It's a Tie");
                            
                            if (openGames.ContainsKey(opponent))
                                openGames.Remove(opponent);

                            removeGamePages(opponent);

                            buttonConnect4.Enabled = true;
                            buttonRpsls.Enabled = true;
                            break;

                        case GameSituation.Normal:
                            playingConnect4(connect_four, packet);
                            break;
                    }
                    break;

                case Flag.RPSLS:
                    RockPaperScissorsLizardSpock ropasilisp = packet.Data as RockPaperScissorsLizardSpock;
                    GameSituation game = ropasilisp.Situation;
                    Hands myhand = ropasilisp.YourHand;
                    Hands opponenthand = ropasilisp.OpponentHand;
                    opponent = ropasilisp.Opponent;
                        
                switch (game)
                    {
                        case GameSituation.Disconnect:
                            {
                                if (openGames.ContainsKey(opponent))
                                    openGames.Remove(opponent);

                                removeGamePages(opponent);
                            }
                            buttonRpsls.Enabled = true;
                            break;

                        case GameSituation.Tie:
                            removeGamePages(opponent);
                            EndGameLabel("It's a Tie");
                            
                            if (openGames.ContainsKey(opponent))
                                openGames.Remove(opponent);                            

                            buttonConnect4.Enabled = true;
                            buttonRpsls.Enabled = true;
                                                  
                            break;

                        case GameSituation.Loss:
                            removeGamePages(opponent);
                            EndGameLabel("You Lost...");                            
                            newGame = new NewGame();
                            if (newGame.ShowDialog() == DialogResult.OK)
                            {
                                sendRpslsRequest(opponent);
                            }
                            else
                            {
                                if (openGames.ContainsKey(opponent))
                                    openGames.Remove(opponent);
                            }
                            break;

                        case GameSituation.Win:
                            removeGamePages(opponent);
                            EndGameLabel("You WON!!!");
                             if (openGames.ContainsKey(opponent))
                                openGames.Remove(opponent);
                             
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

        public void EndGameLabel(string situation)
        {
            this.Invoke(new MethodInvoker(() =>
                            {
                                labelSituation2.Text = situation;
                                labelSituation.Text = "";
                            }));
        }

        public void playingConnect4(ConnectFour connect_four, Packet packet)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                labelSituation.Text = "Playing...";

                if (connect_four.SetPlayedBy != name)
                {
                    tabControlGame.SelectTab(connect_four.Opponent);
                    connect4.Play(packet);
                    labelSituation2.Text = "Your Move";
                }
                else
                    labelSituation2.Text = "Opponent's  Move";

            }));
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

        private void clearPanel(string opp)
        {
            removeGamePages(opp);
            
            this.Invoke(new MethodInvoker(() =>
            {
                if(panelGame1 != null)
                    panelGame1.Controls.Clear();

                EndGameLabel("");
                buttonConnect4.Enabled = true;
                buttonRpsls.Enabled = true;
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
                string opp = tabController.SelectedTab.Name;
                SendConnect4Request(opp);
            }
            buttonRpsls.Enabled = false;
        }

        public void SendConnect4Request(string opp)
        {
            
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
                sendRpslsRequest(opp);
            }
            buttonConnect4.Enabled = false;
        }

        public void sendRpslsRequest(string opp)
        {
            RockPaperScissorsLizardSpock rpsls = new RockPaperScissorsLizardSpock(name, opp, GameSituation.Connect);
            Packet packet = new Packet();
            packet.Flag = Flag.GameRequest;
            packet.Data = rpsls;
            Comm.OutgoingMessageHandler(packet);
        }

        public void StartNewRpsls(string opp)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                tabController.SelectTab(opp);

                if (tabControlGame.TabPages.ContainsKey(opp))
                    removeGamePages(opp);
                //RPSLS rpsls = new RPSLS(name, opp);              

                addGamePages(opp, "rpsls");
                labelSituation.Text = "...starting game";
                labelSituation2.Text = "";

                if (openGames.ContainsKey(opp))
                    openGames.Remove(opp);

                openGames.Add(opp, "rpsls");
                buttonRpsls.Enabled = false;
            }));
        }

        public void StartNewConnect4(string opp,bool turn)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                tabController.SelectTab(opp);
                removeGamePages(opponent);              

                addGamePages(opp,"connect4");

                labelSituation.Text = "...starting game";
                labelSituation2.Text = "";
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
            ConnectFour con4 = packet.Data as ConnectFour;
            if (con4.Situation != GameSituation.Win)
            {
                labelSituation2.Text = "It's not your turn";
                labelSituation.Text = "";
            }
            else
            {
                removeGamePages(con4.Opponent);
                if (openGames.ContainsKey(con4.Opponent))
                {
                    openGames.Remove(con4.Opponent);
                }
                buttonRpsls.Enabled = true;
                buttonConnect4.Enabled = true;

                EndGameLabel("");
            }
            
            Comm.OutgoingMessageHandler(packet);
        }

        public void rpsls_RPSLSChoice(Packet packet)
        {
            RockPaperScissorsLizardSpock rpsls = packet.Data as RockPaperScissorsLizardSpock;
            if (rpsls.Situation != GameSituation.Win)
            {
                Hands hand = rpsls.YourHand;
                labelSituation.Text = "You chose " + hand.ToString();
                labelSituation2.Text = "...waiting for other player";
            }
            else
            {
                removeGamePages(rpsls.Opponent);
                if (openGames.ContainsKey(rpsls.Opponent))
                {
                    openGames.Remove(rpsls.Opponent);
                }
                buttonRpsls.Enabled = true;
                buttonConnect4.Enabled = true;
                
                EndGameLabel("");
            }
            Comm.OutgoingMessageHandler(packet);
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
            else if (tabController.SelectedTab.Name == broadcast)
            {
                buttonRpsls.Enabled = false;
                buttonConnect4.Enabled = false;
            }
            else
            {
                buttonRpsls.Enabled = true;
                buttonConnect4.Enabled = true;
            }
        }        

        private void ClientGUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            Packet packet = new Packet();
            packet.Flag = Flag.RemoveClient;

            Comm.OutgoingMessageHandler(packet);
        }

        
    }
}
