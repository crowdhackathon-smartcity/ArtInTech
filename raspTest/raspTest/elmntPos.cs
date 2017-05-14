using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace raspTest
{
    class elmntPos
    {
        public elmntPos(string top, string left, string widgth, string height)
        {
            this.top = Convert.ToInt32(top);
            this.left = Convert.ToInt32(left);
            this.widgth = Convert.ToInt32(widgth);
            this.height = Convert.ToInt32(height);
        }

        public int top { get;}
        public int left { get; }
        public int widgth { get;}
        public int height { get; }
    }
}
