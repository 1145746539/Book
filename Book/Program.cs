using Book.高级控件.对话框;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Book
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //判断程序有没有打开
            bool isAppRunning = false;
            System.Threading.Mutex mutex = new System.Threading.Mutex(true, System.Diagnostics.Process.GetCurrentProcess().ProcessName, out isAppRunning);
            if (!isAppRunning)
            {
                MessageBox.Show("程序已运行，不能再次打开！");
                Environment.Exit(1);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormDialogue());

           // Application.Run(new Main());
        }
    }
}
