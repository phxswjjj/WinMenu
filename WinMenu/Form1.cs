using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinMenu.Menu;
using WinMenu.Security;

namespace WinMenu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            UnityResolver.InitMdiForm(this);

            InitializeMenu();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            OnChangeSecurityState(menuStrip1, ChangeSecurityStateEventArgs.Empty);
        }

        private void InitializeMenu()
        {
            var menuBuilder = new SecurityMenuBuilder()
                .LoadXML("Menu.xml")
                .SetChangeSecurityStateAction(OnChangeSecurityState);

            menuStrip1.Items.AddRange(menuBuilder.Build());
        }

        private void OnChangeSecurityState(object sender, ChangeSecurityStateEventArgs e)
        {
            var mItem = menuStrip1;

            UserInfo user = null;
            if (e != ChangeSecurityStateEventArgs.Empty)
                user = e.Data;

            foreach (ToolStripMenuItem item in mItem.Items)
            {
                //最大化後會出現功能選單，直接略過
                if (item.Tag == null) continue;
                CheckPermission(item, user);
            }
        }

        private void CheckPermission(ToolStripMenuItem mItem, UserInfo user)
        {
            mItem.Enabled = true;
            mItem.Visible = true;

            var menuData = mItem.Tag as MenuBase;
            if (menuData == null)
                throw new Exception($"必須有 MenuBase");

            menuData.Visible = true;

            if (mItem.DropDownItems.Count > 0)
            {
                foreach (ToolStripMenuItem item in mItem.DropDownItems)
                    CheckPermission(item, user);

                var isVisiable = false;
                foreach (ToolStripMenuItem item in mItem.DropDownItems)
                {
                    var subMenuData = item.Tag as MenuBase;
                    if (subMenuData == null)
                        throw new Exception($"必須有 MenuBase");
                    if (subMenuData.Visible)
                    {
                        isVisiable = true;
                        break;
                    }
                }
                if (!isVisiable)
                    menuData.Visible = false;
            }
            else
            {
                var accessString = menuData.AccessString;

                var hasPermission = false;
                if (user != null)
                {
                    hasPermission = user.AccessStrings.Contains(accessString);
                    if (!hasPermission)
                    {
                        foreach (var similarAccessString in user.AccessStrings)
                        {
                            if (!similarAccessString.EndsWith("*"))
                                continue;
                            if (accessString.StartsWith(similarAccessString.TrimEnd('*')))
                                hasPermission = true;
                        }
                    }
                }
                switch (menuData.ViewMode)
                {
                    case ViewModeType.Limit:
                        if (!hasPermission)
                            menuData.Visible = false;
                        break;
                    case ViewModeType.Login:
                        //已登入但未限制
                        if (user != null && string.IsNullOrEmpty(accessString))
                            hasPermission = true;
                        if (!hasPermission)
                            mItem.Enabled = false;
                        break;
                    case ViewModeType.NotLogin:
                        if (user != null)
                            menuData.Visible = false;
                        break;
                    case ViewModeType.OnlyLogin:
                        if (user == null)
                            menuData.Visible = false;
                        break;
                }
            }

            mItem.Visible = menuData.Visible;
        }
    }
}
