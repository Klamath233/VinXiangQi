using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VinXiangQi
{
    internal static class Program
    {
        public static Mainform ProgramMainform = null;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ProgramMainform = new Mainform();
            Application.Run(ProgramMainform);
        }
    }
}
