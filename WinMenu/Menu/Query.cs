using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinMenu.UI;

namespace WinMenu.Menu
{
    internal class Query : MdiMenuItemBase, IMenuItem
    {
        public Query(Form parent) : base(parent)
        {
        }

        public void OnClick(object sender, EventArgs e)
        {
            var frm = this.CreateForm<FrmQuery>();
            frm.Show();
        }
    }
}
