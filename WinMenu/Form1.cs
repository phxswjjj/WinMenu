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
            InitializeMenu();
        }

        private void InitializeMenu()
        {
            var menu = new MenuBuilder()
                .LoadXML("Menu.xml");
            menuStrip1.Items.AddRange(menu.Build());
        }
    }
}
