/*
 * 番外： 图片有点模糊 右键项目属性 选择安全性- 启用ClickOnce安全设置 - 然后打开Properties下的app.manifest取消注释以下代码
 *  <application xmlns="urn:schemas-microsoft-com:asm.v3">
    <windowsSettings>
      <dpiAware xmlns="http://schemas.microsoft.com/SMI/2005/WindowsSettings">true</dpiAware>
    </windowsSettings>
   </application>

 *  1. menuStrip  给ToolStripMenuItem添加图标 在外观image选择图片 给ToolStripMenuItem添加快捷键 属性-杂项 - ShortcutKeys 
 *     添加一个富文本 richTextBox 
 *      
 *  2.ContextMenuStrip   关联控件 - 选中你需要关联的控件 - 属性 - 行为 - ContextMenuStrip
 * 
 * */


namespace Book.高级控件.菜单栏
{
    using System.IO;
    using System.Windows.Forms;

    public partial class MenuBarcs : Form
    {
        public MenuBarcs()
        {
            InitializeComponent();
        }

        private void 新建ToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            this.Text = "无标题 - 记事本";
        }

        private void 打开ToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string strFile = openFileDialog1.FileName;

                StreamReader sr = new StreamReader(strFile);
                richTextBox1.Text = sr.ReadToEnd();
                sr.Close();


            }
        }

        private void 保存ToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            saveFileDialog1.Filter = "文本文件(*.txt)|*.txt";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {

                string strPath = saveFileDialog1.FileName;
                //方式1
                //StreamWriter sw = new StreamWriter(strPath);
                //sw.Write(richTextBox1.Text); 
                //sw.Close();

                //方式2
                richTextBox1.SaveFile(strPath, RichTextBoxStreamType.TextTextOleObjs); 
            }
        }

        private void 关闭ToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void 撤销ToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            richTextBox1.Undo();
        }

        private void 剪切ToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void 复制ToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void 粘贴ToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void 全选ToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            richTextBox1.SelectAll();   
        }

        private void 清空ToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            richTextBox1.Clear();
        }
    }
}
