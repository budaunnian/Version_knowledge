namespace WindowsFormsApp2
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
            this.hWindowControl1 = new HalconDotNet.HWindowControl();
            this.bt_open = new System.Windows.Forms.Button();
            this.bt_close = new System.Windows.Forms.Button();
            this.algoma = new System.Windows.Forms.Button();
            this.read_image = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // hWindowControl1
            // 
            this.hWindowControl1.BackColor = System.Drawing.Color.Black;
            this.hWindowControl1.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl1.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl1.Location = new System.Drawing.Point(12, 12);
            this.hWindowControl1.Name = "hWindowControl1";
            this.hWindowControl1.Size = new System.Drawing.Size(409, 426);
            this.hWindowControl1.TabIndex = 0;
            this.hWindowControl1.WindowSize = new System.Drawing.Size(409, 426);
            this.hWindowControl1.HMouseMove += new HalconDotNet.HMouseEventHandler(this.hWindowControl1_HMouseMove);
            // 
            // bt_open
            // 
            this.bt_open.Location = new System.Drawing.Point(476, 34);
            this.bt_open.Name = "bt_open";
            this.bt_open.Size = new System.Drawing.Size(121, 49);
            this.bt_open.TabIndex = 1;
            this.bt_open.Text = "打开相机";
            this.bt_open.UseVisualStyleBackColor = true;
            this.bt_open.Click += new System.EventHandler(this.bt_open_Click);
            // 
            // bt_close
            // 
            this.bt_close.Location = new System.Drawing.Point(692, 34);
            this.bt_close.Name = "bt_close";
            this.bt_close.Size = new System.Drawing.Size(117, 49);
            this.bt_close.TabIndex = 2;
            this.bt_close.Text = "关闭相机";
            this.bt_close.UseVisualStyleBackColor = true;
            this.bt_close.Click += new System.EventHandler(this.bt_close_Click);
            // 
            // algoma
            // 
            this.algoma.Location = new System.Drawing.Point(692, 111);
            this.algoma.Name = "algoma";
            this.algoma.Size = new System.Drawing.Size(117, 49);
            this.algoma.TabIndex = 4;
            this.algoma.Text = "算法";
            this.algoma.UseVisualStyleBackColor = true;
            this.algoma.Click += new System.EventHandler(this.algoma_Click);
            // 
            // read_image
            // 
            this.read_image.BackColor = System.Drawing.SystemColors.ControlDark;
            this.read_image.Location = new System.Drawing.Point(476, 111);
            this.read_image.Name = "read_image";
            this.read_image.Size = new System.Drawing.Size(121, 49);
            this.read_image.TabIndex = 3;
            this.read_image.Text = "读取图像";
            this.read_image.UseVisualStyleBackColor = false;
            this.read_image.Click += new System.EventHandler(this.read_image_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(964, 473);
            this.Controls.Add(this.algoma);
            this.Controls.Add(this.read_image);
            this.Controls.Add(this.bt_close);
            this.Controls.Add(this.bt_open);
            this.Controls.Add(this.hWindowControl1);
            this.Name = "Form1";
            this.Text = "相机采集";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private HalconDotNet.HWindowControl hWindowControl1;
        private System.Windows.Forms.Button bt_open;
        private System.Windows.Forms.Button bt_close;
        private System.Windows.Forms.Button algoma;
        private System.Windows.Forms.Button read_image;
    }
}

