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

    public partial class InvalidNamePopUp : Form
    {
        public string name;

        public InvalidNamePopUp()
        {
            InitializeComponent();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            name = textBoxName.Text;

            if (name != "")
            {
                name = name;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }

        }
    }
}
