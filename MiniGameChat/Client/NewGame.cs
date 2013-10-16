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
    public partial class NewGame : Form
    {
        public NewGame()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
                this.DialogResult = DialogResult.OK;
                this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
