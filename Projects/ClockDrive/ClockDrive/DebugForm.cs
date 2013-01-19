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

        public DebugForm(Form1 sut)
        {
            InitializeComponent();
            SUT = sut;
        }

        private void DebugForm_Load(object sender, EventArgs e)
        {
            TimeHMS.Text = DateTime.Now.ToString();
        }

        private void TimeHMS_TextChanged(object sender, EventArgs e)
        {
            DateTime timeHMS;
            var success = DateTime.TryParse(TimeHMS.Text, out timeHMS);
            SUT.Draw(success ? timeHMS : DateTime.Now);
        }

        private void Simulate_Click(object sender, EventArgs e)
        {
            const int INTERVAL = 60 * 30;
            var current = new DateTime(2000, 1, 1, 0, 0, 0);
            for (var i = 0; i <= (24 * 60  * 60); i += INTERVAL)
            {
                SUT.Draw(current.AddSeconds(i));
                System.Windows.Forms.Application.DoEvents();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            const int INTERVAL = 60 * 1;
            var current = new DateTime(2000, 1, 1, 0, 0, 0);
            for (var i = ((17 + 0) * 60 * 60); i <= ((17 + 1) * 60 * 60); i += INTERVAL)
            {
                SUT.Draw(current.AddSeconds(i));
                System.Windows.Forms.Application.DoEvents();
            }
        }

    }
}
