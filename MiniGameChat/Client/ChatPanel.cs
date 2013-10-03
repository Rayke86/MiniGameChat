using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class ChatPanel : UserControl
    {
        public ChatPanel()
        {
            InitializeComponent();
        }

        public void addChat(string text)
        {
            textBox.AppendText(text);
            textBox.AppendText(Environment.NewLine);

        }
    }
}
