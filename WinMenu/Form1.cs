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

        private void InitializeMenu()
        {
            var menuBuilder = new SecurityMenuBuilder()
                .LoadXML("Menu.xml")
                .SetChangeSecurityStateAction(MenuStrip1_ChangeSecurityState);

            menuStrip1.Items.AddRange(menuBuilder.Build());
        }

        private void MenuStrip1_ChangeSecurityState(object sender, ChangeSecurityStateEventArgs e)
        {
            if (e == ChangeSecurityStateEventArgs.Empty)
                MessageBox.Show("權限異動-Logout");
            else
                MessageBox.Show("權限異動-Login");
        }
    }
}
