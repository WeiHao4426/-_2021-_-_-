namespace comController
{
    partial class Form1
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.connect = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.clear = new System.Windows.Forms.Button();
            this.DisConnect = new System.Windows.Forms.Button();
            this.stateTEXT = new System.Windows.Forms.TextBox();
            this.nowMODE = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // connect
            // 
            this.connect.Location = new System.Drawing.Point(45, 35);
            this.connect.Name = "connect";
            this.connect.Size = new System.Drawing.Size(107, 66);
            this.connect.TabIndex = 0;
            this.connect.Text = "连接";
            this.connect.UseVisualStyleBackColor = true;
            this.connect.Click += new System.EventHandler(this.connect_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(210, 140);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(166, 219);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // clear
            // 
            this.clear.Location = new System.Drawing.Point(45, 159);
            this.clear.Name = "clear";
            this.clear.Size = new System.Drawing.Size(107, 53);
            this.clear.TabIndex = 2;
            this.clear.Text = "清除输入";
            this.clear.UseVisualStyleBackColor = true;
            this.clear.Click += new System.EventHandler(this.clear_Click);
            // 
            // DisConnect
            // 
            this.DisConnect.Location = new System.Drawing.Point(45, 301);
            this.DisConnect.Name = "DisConnect";
            this.DisConnect.Size = new System.Drawing.Size(107, 58);
            this.DisConnect.TabIndex = 3;
            this.DisConnect.Text = "断开";
            this.DisConnect.UseVisualStyleBackColor = true;
            this.DisConnect.Click += new System.EventHandler(this.DisConnect_Click);
            // 
            // stateTEXT
            // 
            this.stateTEXT.Location = new System.Drawing.Point(424, 56);
            this.stateTEXT.Name = "stateTEXT";
            this.stateTEXT.Size = new System.Drawing.Size(87, 28);
            this.stateTEXT.TabIndex = 4;
            this.stateTEXT.TextChanged += new System.EventHandler(this.stateTEXT_TextChanged);
            // 
            // nowMODE
            // 
            this.nowMODE.Location = new System.Drawing.Point(210, 56);
            this.nowMODE.Name = "nowMODE";
            this.nowMODE.Size = new System.Drawing.Size(100, 28);
            this.nowMODE.TabIndex = 5;
            this.nowMODE.TextChanged += new System.EventHandler(this.nowMODE_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.nowMODE);
            this.Controls.Add(this.stateTEXT);
            this.Controls.Add(this.DisConnect);
            this.Controls.Add(this.clear);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.connect);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button connect;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button clear;
        private System.Windows.Forms.Button DisConnect;
        private System.Windows.Forms.TextBox stateTEXT;
        private System.Windows.Forms.TextBox nowMODE;
    }
}

