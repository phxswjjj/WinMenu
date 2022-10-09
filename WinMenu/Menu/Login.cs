﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinMenu.UI;

namespace WinMenu.Menu
{
    internal class Login : MenuBase, IMenuItem
    {
        public void OnClick(object sender, EventArgs e)
        {
            var frm = new FrmLogin();
            frm.ShowDialog();
        }
    }
}
