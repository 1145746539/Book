
namespace Book
{
    using Book.UControl;
    using Book.Util;
    using System;
    using System.Diagnostics;
    using System.Threading;
    using System.Windows.Forms;

    public partial class Main : Form
    {
        InformationControl IC = null;
        public Main()
        {
            InitializeComponent();
            IC = new InformationControl() { Dock = DockStyle.Fill };
            panel_show.Controls.Add(IC);


            Thread thread = new Thread(RunIC);
            thread.IsBackground = true;
            thread.Start();
        }

        private void RunIC()
        {
            while (true)
            {
                IC.rBox_State_AddString(null, new StringEvertArgs("随机来一个：" + new Random().Next(100).ToString()));
                Thread.Sleep(1000);
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Process currentProcess = Process.GetCurrentProcess();
            string[] textArray1 = new string[] { "程序", "占用内存：", ((currentProcess.WorkingSet64 / 0x400L) / 0x400L).ToString(), "M (", (currentProcess.WorkingSet64 / 0x400L).ToString(), "KB)--当前进程占用内存" };
            Console.WriteLine(string.Concat(textArray1));
        }
    }
}
