using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClockDrive
{
    public partial class DebugForm : Form
    {
        private Form1 SUT;

        /// <summary>
        /// 生成時に、親フォームへの参照を退避しておく
        /// </summary>
        public DebugForm(Form1 sut)
        {
            InitializeComponent();
            SUT = sut;
        }

        /// <summary>
        /// 時刻の初期値をセットしておく
        /// </summary>
        private void DebugForm_Load(object sender, EventArgs e)
        {
            TimeHMS.Text = DateTime.Now.ToString();
        }

        /// <summary>
        /// 時刻値が変更されたら、親フォームの描画内容へ反映させる
        /// </summary>
        private void TimeHMS_TextChanged(object sender, EventArgs e)
        {
            DateTime timeHMS;
            var success = DateTime.TryParse(TimeHMS.Text, out timeHMS);
            SUT.Draw(success ? timeHMS : DateTime.Now);
        }

        /// <summary>
        /// 早回しでシミュレート開始する（２４時間 or １時間）
        /// </summary>
        private void Simulate_Click(object sender, EventArgs e)
        {
            var totalHours = (sender == Simulate24Hour ? 24 : 1);
            var intervalSeconds = (sender == Simulate24Hour ? 60 * 20 : 45);
            var startTime = (sender == Simulate24Hour ? new DateTime(2000, 1, 1, 0, 0, 0) : DateTime.Now);
            for (var i = 0; i <= (totalHours * 60 * 60); i += intervalSeconds)
            {
                SUT.Draw(startTime.AddSeconds(i));
                Application.DoEvents();
            }
        }

    }
}
