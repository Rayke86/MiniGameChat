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
            textBox.Enabled = false;
        }

        public void addChat(string text)
        {
            try
            {
                textBox.AppendText(text);
                textBox.AppendText(Environment.NewLine);
            }
            catch (Exception e)
            {
                Console.WriteLine("can't append text");
                Console.WriteLine(e.Message);
            }
            
        }
    }
}
