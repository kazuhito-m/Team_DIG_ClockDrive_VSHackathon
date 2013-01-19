namespace ClockDrive
{
    partial class DebugForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.TimeHMS = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Simulate = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TimeHMS
            // 
            this.TimeHMS.Location = new System.Drawing.Point(119, 12);
            this.TimeHMS.Name = "TimeHMS";
            this.TimeHMS.Size = new System.Drawing.Size(217, 19);
            this.TimeHMS.TabIndex = 0;
            this.TimeHMS.TextChanged += new System.EventHandler(this.TimeHMS_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "時刻？(HH:MM:SS)";
            // 
            // Simulate
            // 
            this.Simulate.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Simulate.Location = new System.Drawing.Point(14, 88);
            this.Simulate.Name = "Simulate";
            this.Simulate.Size = new System.Drawing.Size(322, 78);
            this.Simulate.TabIndex = 2;
            this.Simulate.Text = "２４時間ぐるっと回す！！";
            this.Simulate.UseVisualStyleBackColor = true;
            this.Simulate.Click += new System.EventHandler(this.Simulate_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button1.Location = new System.Drawing.Point(12, 172);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(322, 78);
            this.button1.TabIndex = 3;
            this.button1.Text = "１時間ドライブする！！";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // DebugForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 262);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Simulate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TimeHMS);
            this.Name = "DebugForm";
            this.Text = "DebugForm";
            this.Load += new System.EventHandler(this.DebugForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TimeHMS;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Simulate;
        private System.Windows.Forms.Button button1;
    }
}