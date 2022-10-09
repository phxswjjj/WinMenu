using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;

namespace WinMenu
{
    internal class UnityResolver
    {
        public static IUnityContainer Instance { private get; set; }

        internal static void InitMdiForm(Form form)
        {
            Instance.RegisterInstance(form);
        }
        public static IUnityContainer Create()
        {
            return Instance.CreateChildContainer();
        }
    }
}
