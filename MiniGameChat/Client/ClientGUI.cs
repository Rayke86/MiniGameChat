using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class ClientGUI : Form
    {
        public ClientGUI()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            
            tabController.TabPages.Add("BroadCast");
            tabController.TabPages[0].Controls.Add(new ChatPanel());
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            //string text = textChat.Text;
            //int index = tabController.SelectedTab.TabIndex;
            //ChatPanel panel = (ChatPanel)tabController.TabPages[index].Controls[0]; 

            //youChat(panel,text);

            textChat.Clear();
            textChat.Focus();
        }



        private void youChat(ChatPanel panel, string text)
        {
            panel.addChat(text);
        }
    }
}
