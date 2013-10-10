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
    public partial class PopUp : Form
    {
        public string name { get; set; }
        public string ip { get; set; }

        public PopUp()
        {
            InitializeComponent();


            buttonOK.Focus();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (textBoxName.Text == "")
                labelNoName.Text = "Vul naam in aub!";
            else if (textBoxName.Text == "BROADCAST")
                labelNoName.Text = "Vul andere naam in aub!";
            else
                name = textBoxName.Text;

            if (textBoxIP.Text == "")
                labelNoIP.Text = "Vul aub een IP in!";
            else
                ip = textBoxIP.Text;

            if (name != null && ip != null)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
