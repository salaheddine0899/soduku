using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace soduku
{
    public class SodukuCell: Button
    {
        public int Value { get; set; }
        public bool IsLocked { get; set; }

        public void clear()
        {
            Text = "";
            IsLocked = false;
        }
    }
}
