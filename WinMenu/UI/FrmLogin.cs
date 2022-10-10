using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinMenu.Security;

namespace WinMenu.UI
{
    public partial class FrmLogin : Form
    {
        public UserInfo LoginUser { get; set; }

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnUser1_Click(object sender, EventArgs e)
        {
            var access = new List<string>()
            {
                "Func:Query",
            };

            var user = new UserInfo();
            user.AccessStrings = access.ToArray();
            this.LoginUser = user;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnUser2_Click(object sender, EventArgs e)
        {
            var access = new List<string>()
            {
                "User:QueryX",
            };

            var user = new UserInfo();
            user.AccessStrings = access.ToArray();
            this.LoginUser = user;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
