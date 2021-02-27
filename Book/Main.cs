using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Book
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Process currentProcess = Process.GetCurrentProcess();
            string[] textArray1 = new string[] { "程序", "占用内存：", ((currentProcess.WorkingSet64 / 0x400L) / 0x400L).ToString(), "M (", (currentProcess.WorkingSet64 / 0x400L).ToString(), "KB)--当前进程占用内存" };
            Console.WriteLine(   string.Concat(textArray1));
        }
    }
}
