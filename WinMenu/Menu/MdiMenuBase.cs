using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinMenu.Menu
{
    internal abstract class MdiMenuBase : MenuBase
    {
        protected readonly Form MdiContainer;

        public MdiMenuBase(Form parent)
        {
            this.MdiContainer = parent;
        }

        protected Form CreateForm<T>()
            where T : Form, new()
        {
            var frm = this.MdiContainer.MdiChildren.FirstOrDefault(f => f.GetType() == typeof(T));
            if (frm == null)
            {
                frm = new T();
                frm.MdiParent = this.MdiContainer;
            }
            frm.WindowState = FormWindowState.Maximized;
            return frm;
        }
    }
}
