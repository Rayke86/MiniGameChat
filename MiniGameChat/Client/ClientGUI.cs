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
    }
}
