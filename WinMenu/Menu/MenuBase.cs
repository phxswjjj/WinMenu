using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinMenu.Menu
{
    internal abstract class MenuBase
    {
        public ViewModeType ViewMode { get; set; } = ViewModeType.Always;
        public string AccessString { get; set; }
        public bool Visible { get; set; } = true;
    }

    public enum ViewModeType
    {
        /// <summary>
        /// 預設，總是顯示(不檢查權限)
        /// </summary>
        Always,
        /// <summary>
        /// 只在登入狀態下顯示，沒權限則反灰
        /// </summary>
        Login,
        /// <summary>
        /// 只在未登入狀態下顯示
        /// </summary>
        NotLogin,
        /// <summary>
        /// 只在登入狀態下顯示，沒權限則看不到
        /// </summary>
        Limit,
        /// <summary>
        /// 只在登入狀態下顯示
        /// </summary>
        OnlyLogin,
    }
}
