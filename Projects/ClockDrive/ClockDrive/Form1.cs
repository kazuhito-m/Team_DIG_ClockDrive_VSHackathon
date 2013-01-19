using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace ClockDrive
{
    public partial class Form1 : Form
    {

        private BackGround bg { get; set; }
        private Road road { get; set; }
        private Car car { get; set; }
        public DateTime currentTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            bg = new BackGround(Application.StartupPath + @"\images\");
            road = new Road();
            car = new Car();
        }

        /// <summary>
        /// メインループ
        /// </summary>
        private void timer1_Tick(object sender, EventArgs e)
        {
            Draw(DateTime.Now);
        }

        /// <summary>
        /// 指定された時刻の時計イメージをすべて描く
        /// </summary>
        /// <param name="current"></param>
        public void Draw(DateTime current)
        {
            currentTime = current;
            Invalidate();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="current"></param>
        private void DrawBackGround(Graphics g, DateTime current)
        {
            bg.SetTime(current);

            var srcImage = new Bitmap(bg.SrcImagePath);
            var destImage = new Bitmap(bg.DestImagePath);
            var numberImage = new Bitmap(Application.StartupPath + @"\images\road.png");

            var cm = new ColorMatrix();
            cm.Matrix33 = (float)bg.BlendRatio;
            var ia = new ImageAttributes();
            ia.SetColorMatrix(cm);
            // ブレンド元の背景を描く
            g.DrawImage(
                srcImage,
                new Rectangle(0, 0, this.Width, this.Height)
                );
            // ブレンド先の背景を描く
            g.DrawImage(
                destImage,
                new Rectangle(0, 0, this.Width, this.Height),
                0, 0, destImage.Width, destImage.Height, GraphicsUnit.Pixel, ia
                );

            // デジタル時刻を描く
            this.Text = string.Format("{0} - {1}", current.ToString(), Application.ProductName);
            var fnt = new Font("MS UI Gothic", 40);
            g.DrawString(current.ToLongTimeString(), fnt, Brushes.Gray, 20, this.Height - 100);
        }

        /// <summary>
        /// 
        /// </summary>
        private void DrawCar(Graphics g, DateTime current)
        {
            car.SetTime(current);

            var carImage = new Bitmap(Application.StartupPath + @"\images\car.png");
            // 車を回転させて描く
            g.ResetTransform();
            g.TranslateTransform(this.Width / 2, this.Height / 2);
            g.TranslateTransform(car.Position.X, car.Position.Y);
            g.RotateTransform((float)(car.Angle));
            g.DrawImage(
                carImage,
                //new Rectangle(car.Position.X, car.Position.Y, 30, 30)
                new Rectangle(0, 0, 30, 30)
                );
        }

        /// <summary>
        /// デバッグ用、時刻を強制指定できる別画面を表示する
        /// </summary>
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                timer1.Enabled = false;
                var debugForm = new DebugForm(this);
                debugForm.Show();
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawBackGround(e.Graphics, currentTime);
            DrawCar(e.Graphics, currentTime);
        }
    }
}
