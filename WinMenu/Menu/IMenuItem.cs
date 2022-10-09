using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinMenu.Menu
{
    internal interface IMenuItem
    {
        void OnClick(object sender, EventArgs e);
    }
}
