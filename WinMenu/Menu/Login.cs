using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinMenu.UI;

namespace WinMenu.Menu
{
    internal class Login : IMenuItem
    {
        public void OnClick(object sender, EventArgs e)
        {
            var frm = new FrmLogin();
            frm.ShowDialog();
        }
    }
}
