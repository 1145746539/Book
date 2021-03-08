namespace Book.UControl
{
    partial class InformationControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.rBox_State = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rBox_State
            // 
            this.rBox_State.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.rBox_State.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rBox_State.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rBox_State.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rBox_State.Location = new System.Drawing.Point(0, 0);
            this.rBox_State.Name = "rBox_State";
            this.rBox_State.ReadOnly = true;
            this.rBox_State.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rBox_State.Size = new System.Drawing.Size(195, 277);
            this.rBox_State.TabIndex = 4;
            this.rBox_State.Text = "";
            // 
            // InformationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rBox_State);
            this.Name = "InformationControl";
            this.Size = new System.Drawing.Size(195, 277);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rBox_State;
    }
}
