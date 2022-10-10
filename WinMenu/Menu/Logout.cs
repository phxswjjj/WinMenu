using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinMenu.Menu
{
    internal class Logout : MenuBase, IMenuItem, ISecurityMeunItem
    {
        public event ChangeSecurityStateEventHandler ChangeSecurityState;

        public void OnClick(object sender, EventArgs e)
        {
            if (this.ChangeSecurityState != null)
                this.ChangeSecurityState.Invoke(sender, ChangeSecurityStateEventArgs.Empty);
        }
    }
}
