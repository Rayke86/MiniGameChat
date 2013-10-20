using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class ChoisePanel : System.Windows.Forms.Panel
    {
        public ChoisePanel()
        {
            this.SetStyle(System.Windows.Forms.ControlStyles.AllPaintingInWmPaint | 
                System.Windows.Forms.ControlStyles.UserPaint | 
                System.Windows.Forms.ControlStyles.DoubleBuffer, true);
            this.BorderStyle = System.Windows.Forms.BorderStyle.None;
        }
    }
}
