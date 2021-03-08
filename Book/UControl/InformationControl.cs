using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Book.UControl
{
    /// <summary>
    /// 信息流显示区
    /// </summary>
    public partial class InformationControl : UserControl
    {
        public InformationControl()
        {
            InitializeComponent();
        }


        public void rBox_State_AddString(RichTextBox a, StringEvertArgs b)
        {
            if (!a.IsDisposed)
            {
                try
                {
                    if (a.InvokeRequired)
                    { a.Invoke(new Action<RichTextBox, StringEvertArgs>(rBox_State_AddString), new object[] { a, b }); }
                    else
                    { AddLog(a, Color.WhiteSmoke, b.Text); }
                }
                catch
                {

                }
            }
        }


        /// <summary>
        /// 字符串格式化 写入RichTextBox中
        /// </summary>
        /// <param name="RichTextBox">待写入的控件</param>
        /// <param name="str">待写入文件文本</param>
        /// <returns>true ok</returns>
        public bool AddLog(RichTextBox control, Color color, string appendText)
        {
            string msg = "";
            msg = string.Format("[{0}]\r\n{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), appendText + Environment.NewLine);
            //可添加直接写入某个控件或文本文件中    
            try
            {
                lock (control)
                {
                    control.SelectionColor = color;
                    control.AppendText(msg);
                    control.ScrollToCaret();
                    return true;
                }
            }
            catch { return false; }
        }
 
    }

     /// <summary>
    /// 包含String的事件数组
    /// </summary>
    public class StringEvertArgs : EventArgs
    {
        private string text = "";
        /// <summary>
        /// 内容
        /// </summary>
        public string Text
        {
            get { return text; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="str">输入的字符</param>
        public StringEvertArgs(string str)
        {
            this.text = str;
        }
    }
}
