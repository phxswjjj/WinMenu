using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinMenu.Menu
{
    internal interface ISecurityMeunItem
    {
        event ChangeSecurityStateEventHandler ChangeSecurityState;
    }

    public delegate void ChangeSecurityStateEventHandler(object sender, ChangeSecurityStateEventArgs e);

    public class ChangeSecurityStateEventArgs
    {
        public static readonly ChangeSecurityStateEventArgs Empty = new ChangeSecurityStateEventArgs();
    }
}
