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
    public delegate void connect4SChoice(Packet packet);

    public partial class Connect4 : UserControl
    {
        public List<PictureBox> boxesColumn1;
        public List<PictureBox> boxesColumn2;
        public List<PictureBox> boxesColumn3;
        public List<PictureBox> boxesColumn4;
        public List<PictureBox> boxesColumn5;
        public List<PictureBox> boxesColumn6;
        public List<PictureBox> boxesColumn7;
        public List<PictureBox> boxesColumn8;
        public List<PictureBox> occupiedBoxes;
        public Panel ChoiseP;
        public int X,Y;
        public bool isPressed = false;
        public bool isMyTurn = false;
        public string name;
        public Image img;
        public Image img_red;
        public Image img_yellow;
        public event connect4SChoice connect4SChoice;
        public string opponent;

        public Connect4(string name, string opponent)
        {
            InitializeComponent();
            this.name = name;
            this.opponent = opponent;
            boxesColumn1 = new List<PictureBox>();
            boxesColumn2 = new List<PictureBox>();
            boxesColumn3 = new List<PictureBox>();
            boxesColumn4 = new List<PictureBox>();
            boxesColumn5 = new List<PictureBox>();
            boxesColumn6 = new List<PictureBox>();
            boxesColumn7 = new List<PictureBox>();
            boxesColumn8 = new List<PictureBox>();

            occupiedBoxes = new List<PictureBox>();            

            img_red = getImage("connect4_red");
            img = getImage("connect4");
            img_yellow = getImage("connect4_yellow");
            createGrid();

            createChoicePanel();

            ChoiseP.Paint += new PaintEventHandler(createChoicePanel_Paint);
            ChoiseP.MouseMove += new MouseEventHandler(createChoicePanel_MouseMove);
            ChoiseP.MouseDown += new MouseEventHandler(createChoicePanel_MouseDown);
            ChoiseP.MouseUp += new MouseEventHandler(createChoicePanel_MouseUp);            

            Y = 4;
            X = 38;            
        }

        public void createChoicePanel()
        {
            ChoiseP = new ChoisePanel();
            ChoiseP.Location = new Point(115, 30);
            ChoiseP.Size = new Size(640, 80);
            ChoiseP.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(ChoiseP);
        }

        public void createGrid()
        {            
            addBox(boxesColumn1,1);
            addBox(boxesColumn2, 2);
            addBox(boxesColumn3, 3);
            addBox(boxesColumn4, 4);
            addBox(boxesColumn5, 5);
            addBox(boxesColumn6, 6);
            addBox(boxesColumn7, 7);
            addBox(boxesColumn8, 8);            
        }

        public void addBox(List<PictureBox> list, int i)
        {            
            for (int j = 6; j > 0; j--)
            {
                PictureBox pb = new PictureBox();
                pb.Name = "pb_" + j;
                this.Controls.Add(pb);
                pb.Size = new Size(80, 80);
                pb.Location = new Point(35 + (i * 80), 50 + (j * 80));
                pb.Image = img;
                list.Add(pb);
            }
        }

        public void start(bool start)
        {
            isMyTurn = start;
        }

        public void Play(Packet packet)
        {
            ConnectFour con4 = packet.Data as ConnectFour;
            int column = con4.X;
            OpponentDrop(column);
            isMyTurn = con4.ItIsYourTurn;
            Y = 4;
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

        public void createChoicePanel_Paint(object sender, PaintEventArgs e)
        {            
            Rectangle r = new Rectangle(X-35,Y,70,70);
            Graphics g = e.Graphics;

            g.FillEllipse(Brushes.Red, r);
            ChoiseP.Invalidate();
        }

        public void createChoicePanel_MouseMove(object sender, MouseEventArgs m)
        {
            if (isMyTurn)
            {
                if (isPressed)
                {
                    if (m.X > 38 && m.X < 599)
                    {
                        X = m.X;
                    }
                }                
            }
        }

        public void createChoicePanel_MouseDown(object sender, MouseEventArgs m)
        {
            if(isMyTurn)
                isPressed = true;
        }

        public void createChoicePanel_MouseUp(object sender, MouseEventArgs m)
        {
            if (isMyTurn)
            {
                isPressed = false;
                Y = 100;

                if (X > 0 && X < 80)
                    drop(boxesColumn1, img_red,1);
                if (X > 80 && X < 160)
                    drop(boxesColumn2, img_red,2);
                if (X > 160 && X < 240)
                    drop(boxesColumn3, img_red,3);
                if (X > 240 && X < 320)
                    drop(boxesColumn4, img_red,4);
                if (X > 320 && X < 400)
                    drop(boxesColumn5, img_red,5);
                if (X > 400 && X < 480)
                    drop(boxesColumn6, img_red,6);
                if (X > 480 && X < 560)
                    drop(boxesColumn7, img_red,7);
                if (X > 560 && X < 640)
                    drop(boxesColumn8, img_red,8);
            }            
        }

        public void forSending(int column, int row)
        {
            Packet packet = new Packet();
            packet.Flag = Flag.Connect4;
            ConnectFour con4 = new ConnectFour(name, opponent, GameSituation.Normal);
            con4.X = column;
            con4.Y = row;
            con4.SetPlayedBy = name;
            con4.ItIsYourTurn = true;
            packet.Data = con4;
            OnConnect4SChoice(packet);
        }

        public void drop(List<PictureBox> boxesColumn, Image image, int column)
        {
            bool done = false;

            foreach(PictureBox box in boxesColumn)
            {
                switch (box.Name)
                {
                    case "pb_1":
                        if (box.Image == img_red || box.Image == img_yellow)
                        {
                            done = false;
                        }
                        else
                        {
                            box.Image = image;
                            if (isMyTurn)
                            {
                                forSending(column, 1);
                                isMyTurn = false;
                                Y = 100;
                            }
                            done = true;
                        }
                        break;
                    case "pb_2":
                        if (box.Image == img_red || box.Image == img_yellow)
                        { }
                        else
                        {
                            box.Image = image;
                            if (isMyTurn)
                            {
                                forSending(column,2);
                                isMyTurn = false;
                                Y = 100;
                            }
                            done = true;
                        }
                        break;
                    case "pb_3":
                        if (box.Image == img_red || box.Image == img_yellow)
                        { }
                        else
                        {
                            box.Image = image;
                            if (isMyTurn)
                            {
                                forSending(column,3);
                                isMyTurn = false;
                                Y = 100;
                            }
                            done = true;
                        }
                        break;
                    case "pb_4":
                        if (box.Image == img_red || box.Image == img_yellow)
                        { }
                        else
                        {
                            box.Image = image;
                            if (isMyTurn)
                            {
                                forSending(column,4);
                                isMyTurn = false;
                                Y = 100;
                            }
                            done = true;
                        }
                        break;
                    case "pb_5":
                        if (box.Image == img_red || box.Image == img_yellow)
                        { }
                        else
                        {
                            box.Image = image;
                            if (isMyTurn)
                            {
                                forSending(column,5);
                                isMyTurn = false;
                                Y = 100;
                            }
                            done = true;
                        }
                        break;
                    case "pb_6":
                        if (box.Image == img_red || box.Image == img_yellow)
                        { }
                        else
                        {
                            box.Image = image;
                            if (isMyTurn)
                            {
                                forSending(column,6);
                                isMyTurn = false;
                                Y = 100;
                            }
                            done = true;
                        }
                        break;
                }
                if (done)
                    break;
                else
                {
                    Y = 4;
                }
            }
        }

        public void OpponentDrop(int column)
        {
            switch(column)
            {
                case 1: drop(boxesColumn1, img_yellow, column);
                    break;
                case 2: drop(boxesColumn2, img_yellow, column);
                    break;
                case 3: drop(boxesColumn3, img_yellow, column);
                    break;
                case 4: drop(boxesColumn4, img_yellow, column);
                    break;
                case 5: drop(boxesColumn5, img_yellow, column);
                    break;
                case 6: drop(boxesColumn6, img_yellow, column);
                    break;
                case 7: drop(boxesColumn7, img_yellow, column);
                    break;
                case 8: drop(boxesColumn8, img_yellow, column);
                    break;
            }
        }

        protected virtual void OnConnect4SChoice(Packet packet)
        {
            if (packet != null)
                connect4SChoice(packet);
        }

        private void buttonGiveUp_Click(object sender, EventArgs e)
        {
            Packet packet = new Packet();
            packet.Flag = Flag.Connect4;
            ConnectFour con4 = new ConnectFour(name, opponent, GameSituation.Win);
            packet.Data = con4;
            OnConnect4SChoice(packet);
        }
    }
}