using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PacketLibrary;

namespace Client
{
    public partial class ClientGUI : Form
    {
        public ClientGUI()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

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
            string senderName = "";     //username
            string receiver = tabController.SelectedTab.Name;
            string text = textChat.Text;

            Packet packet = new Packet();
            packet.Flag = Flag.Chat;
            ChatMessage message = new ChatMessage();
            message.Sender = senderName;
            message.Message = text;
            message.Receiver = receiver; 

            textChat.Clear();
            textChat.Focus();
        }

        public void incomingChat(string text, string name)
        {
            ChatPanel panel = (ChatPanel)tabController.TabPages[0].Controls[0];
            
            youChat(panel, text);
        }

        private void youChat(ChatPanel panel, string text)
        {
            panel.addChat(text);
        }
    }
}
