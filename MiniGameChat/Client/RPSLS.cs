using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PacketLibrary;

namespace Client
{
    public delegate void RPSLSChoice(Packet packet);

    public partial class RPSLS : UserControl
    {
        public event RPSLSChoice RPSLSChoice;
        public string name;
        public string opponent;

        public RPSLS(string name, string opponent)
        {
            InitializeComponent();
            this.name = name;
            this.opponent = opponent;

            buttonLizard.BackgroundImage = getImage("lizard2");
            buttonPaper.BackgroundImage = getImage("paper2");
            buttonSiccors.BackgroundImage = getImage("scissor2");
            buttonSpock.BackgroundImage = getImage("spock2");
            buttonRock.BackgroundImage = getImage("rock2");
            labelSmash.Image = getImage("smash");
            labelCut.Image = getImage("cuts");
            labelpoisons.Image = getImage("poisons");
            labelCovers.Image = getImage("covers");
            labelCrushes.Image = getImage("crushes");
            labelCenter.Image = getImage("center2");
        }

        public Image getImage(string img)
        {            
            Image image = null;

            try
            {
                image = Image.FromFile("../../Images/" + img + ".png");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return image;
        }

        private void buttonRock_Click(object sender, EventArgs e)
        {
            Packet packet = new Packet();
            packet.Flag = Flag.RPSLS;
            RockPaperScissorsLizardSpock rpsls = new RockPaperScissorsLizardSpock(name, opponent, GameSituation.Normal);
            rpsls.YourHand = Hands.Rock;
            packet.Data = rpsls;
            OnRPSLSChoice(packet);

            buttonLizard.Enabled = false;
            buttonPaper.Enabled = false;
            buttonSpock.Enabled = false;
            buttonSiccors.Enabled = false;
        }

        private void buttonLizard_Click(object sender, EventArgs e)
        {
            Packet packet = new Packet();
            packet.Flag = Flag.RPSLS;
            RockPaperScissorsLizardSpock rpsls = new RockPaperScissorsLizardSpock(name, opponent, GameSituation.Normal);
            rpsls.YourHand = Hands.Lizard;
            packet.Data = rpsls;
            OnRPSLSChoice(packet);

            buttonRock.Enabled = false;
            buttonPaper.Enabled = false;
            buttonSpock.Enabled = false;
            buttonSiccors.Enabled = false;
        }

        private void buttonPaper_Click(object sender, EventArgs e)
        {
            Packet packet = new Packet();
            packet.Flag = Flag.RPSLS;
            RockPaperScissorsLizardSpock rpsls = new RockPaperScissorsLizardSpock(name, opponent, GameSituation.Normal);
            rpsls.YourHand = Hands.Paper;
            packet.Data = rpsls;
            OnRPSLSChoice(packet);

            buttonLizard.Enabled = false;
            buttonRock.Enabled = false;
            buttonSpock.Enabled = false;
            buttonSiccors.Enabled = false;
        }

        private void buttonSpock_Click(object sender, EventArgs e)
        {
            Packet packet = new Packet();
            packet.Flag = Flag.RPSLS;
            RockPaperScissorsLizardSpock rpsls = new RockPaperScissorsLizardSpock(name, opponent, GameSituation.Normal);
            rpsls.YourHand = Hands.Spock;
            packet.Data = rpsls;
            OnRPSLSChoice(packet);

            buttonLizard.Enabled = false;
            buttonPaper.Enabled = false;
            buttonRock.Enabled = false;
            buttonSiccors.Enabled = false;
        }

        private void buttonSiccors_Click(object sender, EventArgs e)
        {
            Packet packet = new Packet();
            packet.Flag = Flag.RPSLS;
            RockPaperScissorsLizardSpock rpsls = new RockPaperScissorsLizardSpock(name, opponent, GameSituation.Normal);
            rpsls.YourHand = Hands.Scissors;
            packet.Data = rpsls;
            OnRPSLSChoice(packet);

            buttonLizard.Enabled = false;
            buttonPaper.Enabled = false;
            buttonSpock.Enabled = false;
            buttonRock.Enabled = false;
        }

        public void sendRequest(Packet packet)
        {
            OnRPSLSChoice(packet);
        }

        protected virtual void OnRPSLSChoice(Packet packet)
        {
            if (packet != null)
                RPSLSChoice(packet);
        }
    }
}
