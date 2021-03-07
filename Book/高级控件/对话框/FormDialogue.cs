/*
 * 1.文件对话框 行为：Filter（选择文件类型） MultiSelect（是否多选） 
 *   添加Textbox控件 设置multiLine=true ，ScrollBars=Both
 *   添加pictureBox控件， 图像源两种方式加载。
 * 
 * */

namespace Book.高级控件.对话框
{
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    public partial class FormDialogue : Form
    {
        public FormDialogue()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            openFileDialog1.Title = "简单选择器";
            this.openFileDialog1.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|(*.*)|图片(*.jpg)|*.jpg";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string strFilePath = openFileDialog1.FileName;
                string strExt = Path.GetExtension(strFilePath);

                if (".txt" == strExt)
                {

                    using (StreamReader sr = new StreamReader(strFilePath))
                    {
                        textBox1.Text = sr.ReadToEnd();
                    }
                }
                else if (".jpg" == strExt)
                { 
                    //图片设置填充风格
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

                    //1.第一种加载方式
                    //pictureBox1.Image = Image.FromFile(strFilePath);
                    //2,第二种加载方式
                    pictureBox1.Image = new Bitmap(strFilePath);

                }
                else
                {
                    //打开文件、文件夹、exe、网址
                    System.Diagnostics.Process.Start(strFilePath);
                }


            }
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            saveFileDialog1.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|(*.*)|图片(*.jpg)|*.jpg";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string savePath = saveFileDialog1.FileName;

                StreamWriter sw = new StreamWriter(savePath);
                sw.Write(textBox1.Text);
                sw.Close();
            }
        }

        private void button3_Click(object sender, System.EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                string foldPath = folderBrowserDialog1.SelectedPath;
                //打开文件、文件夹、exe、网址
                System.Diagnostics.Process.Start(foldPath);
            }
        }

        private void button4_Click(object sender, System.EventArgs e)
        {

            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.ForeColor = colorDialog1.Color;

            }
        }

        private void button5_Click(object sender, System.EventArgs e)
        {
            if (fontDialog1.ShowDialog() ==DialogResult.OK)
            {
                textBox1.Font = fontDialog1.Font;
            }
        }
    }
}
