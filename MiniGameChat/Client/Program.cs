using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    internal static class ClientMain
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            PopUp popUop = new PopUp();
            if (popUop.ShowDialog() == DialogResult.OK)
            {
                string name = popUop.name;
                string ip = popUop.ip;
                popUop.Dispose();
                Application.Run(new ClientGUI(ip, name));
            }
        }
    }
}
