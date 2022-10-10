using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinMenu.UI;

namespace WinMenu.Menu
{
    internal class Login : MenuBase, IMenuItem, ISecurityMeunItem
    {
        public event ChangeSecurityStateEventHandler ChangeSecurityState;

        public void OnClick(object sender, EventArgs e)
        {
            var frm = new FrmLogin();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                var securityArgs = new ChangeSecurityStateEventArgs();
                securityArgs.Data = frm.LoginUser;
                if (this.ChangeSecurityState != null)
                    this.ChangeSecurityState.Invoke(sender, securityArgs);
            }
        }
    }
}
