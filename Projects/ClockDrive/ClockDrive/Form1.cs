using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ClockDrive
{
    public partial class Form1 : Form
    {
        /// <summary>ウインドウの角丸化</summary>
        [DllImport("user32.dll")]
        static extern int SetWindowRgn(IntPtr hWnd, IntPtr hRgn, bool bRedraw);

        /// <summary>ウインドウの角丸化</summary>
        [DllImport("gdi32.dll")]
        static extern IntPtr CreateRoundRectRgn(int x1, int y1, int x2, int y2, int cx, int cy);
 
        internal BackGround bg;
        internal Road road;
        internal Car car;
        internal Cloud cloud;

        private Dictionary<string, Bitmap> ImageCache;
        private DateTime currentTime;

        /// <summary>
        /// 各種モデルクラスをインスタンス化する
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            bg = new BackGround(Application.StartupPath + @"\images\");
            road = new Road(Application.StartupPath + @"\datas\RoadData.csv");
            car = new Car(road);
            cloud = new Cloud(Width, Height, 15);

            ImageCache = new Dictionary<string, Bitmap>();

            // このウインドウの角を丸める
            IntPtr rgn1 = CreateRoundRectRgn(0, 0, 512, 512, 128, 128);
            SetWindowRgn(Handle, rgn1, true);

            // 最初に、現在時刻の状態を描いておく
            Draw(DateTime.Now);
        }

        /// <summary>
        /// タイマーを開始する
        /// </summary>
        public void EnableTimer()
        {
            timer1.Enabled = true;
        }

        /// <summary>
        /// メインループ
        /// </summary>
        private void timer1_Tick(object sender, EventArgs e)
        {
            cloud.Move(1);
            Draw(DateTime.Now);
        }

        /// <summary>
        /// 指定された時刻の時計イメージをすべて描く
        /// </summary>
        /// <param name="current"></param>
        public void Draw(DateTime current)
        {
            currentTime = current;
            bg.SetTime(current);
            car.SetTime(current);

            Invalidate();
        }

        /// <summary>
        /// このフォームを再描画する（ダブルバッファ有効、ちらつき無し）
        /// </summary>
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            DrawBackGround(g, currentTime);
            DrawCar(g, currentTime);
            DrawClouds(g);
            DrawDigitalTime(g, currentTime);
        }

        /// <summary>
        /// 最初に画像を使うときにだけ実際にファイルから読み込んで、あとはキャッシュを使いまわす
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private Bitmap GetCachedBitmap(string filePath)
        {
            // 画像キャッシュにまだ格納されてなければ、実際にファイルから読み込む
            if (!ImageCache.ContainsKey(filePath)) ImageCache[filePath] = new Bitmap(filePath);
            return ImageCache[filePath];
        }

        /// <summary>
        /// 指定された時刻に応じた背景を描く
        /// </summary>
        /// <param name="current"></param>
        private void DrawBackGround(Graphics g, DateTime current)
        {
            var srcImage = GetCachedBitmap(bg.SrcImagePath);
            var destImage = GetCachedBitmap(bg.DestImagePath);

            // ２枚の背景画像のブレンド比率を、ColorMatrixへセットする
            var cm = new ColorMatrix() { Matrix33 = (float)bg.BlendRatio };
            var ia = new ImageAttributes();
            ia.SetColorMatrix(cm);

            // ブレンド元の背景画像を描く（不透明ベタ塗り）
            g.DrawImage(
                srcImage,
                0, 0, this.Width, this.Height
                );

            // ブレンド先の背景画像を描く（半透明）
            g.DrawImage(
                destImage,
                new Rectangle(0, 0, this.Width, this.Height),
                0, 0, destImage.Width, destImage.Height, GraphicsUnit.Pixel, ia
                );
        }

        /// <summary>
        /// 指定された時刻に応じて、適切な位置と角度で車を描く
        /// </summary>
        private void DrawCar(Graphics g, DateTime current)
        {
            var carImage = GetCachedBitmap(Application.StartupPath + @"\images\car.png");
            const float stretchRatio = 1F;

            g.ResetTransform(); // ※行列をリセットし、直前までの状態を反映させない

            g.TranslateTransform(car.Position.X, car.Position.Y);   //時刻に応じた適切な位置へ移動させる
            g.RotateTransform((float)(car.Angle));                  //回転させる
            g.DrawImage(
                carImage,
                new Rectangle(
                    (int)(carImage.Width * stretchRatio * -0.5F), (int)(carImage.Height * stretchRatio * -0.5F),
                    (int)(carImage.Width * stretchRatio), (int)(carImage.Height * stretchRatio))
                );

            g.ResetTransform(); // ※行列をリセットし、直前までの状態を反映させない
        }

        /// <summary>
        /// 雲の影を描く
        /// </summary>
        /// <param name="current"></param>
        private void DrawClouds(Graphics g)
        {
            var cloudImage = GetCachedBitmap(Application.StartupPath + @"\images\cloud.png");
            foreach (var p in cloud.Positions)
                g.DrawImage(
                    cloudImage,
                    p.X, p.Y, cloudImage.Width, cloudImage.Height
                    );
        }

        /// <summary>
        /// 指定された時刻に応じて、フォームのフォントを用いて、デジタル時刻を描く
        /// </summary>
        /// <param name="current"></param>
        private void DrawDigitalTime(Graphics g, DateTime current)
        {
            this.Text = string.Format("{0} - {1}", current.ToString(), Application.ProductName);
            var f = this.Font;
            for (var i = 0; i < 2; i++)
            {
                g.DrawString(current.ToLongTimeString(),
                             f,
                             (i == 0 ? Brushes.DarkBlue : Brushes.LightBlue),
                             20 - i * 1, this.Height - 60 - i * 2
                             );
            }
        }

        private Point previousPosition;

        /// <summary>
        /// マウスボタン押下への応答
        /// </summary>
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            previousPosition = e.Location;
        }

        /// <summary>
        /// 左ドラッグでフォーム移動
        /// </summary>
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.Left += (e.X - previousPosition.X);
                this.Top += (e.Y - previousPosition.Y);
            }
        }

        #region コンテキストメニュー

        /// <summary>
        /// 終了メニュー
        /// </summary>
        private void ExitMenu_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// タイマー停止と再開
        /// </summary>
        private void ToggleTimerMenu_Click(object sender, EventArgs e)
        {
            timer1.Enabled = !timer1.Enabled;
        }

        /// <summary>
        /// 開発用メニュー
        /// </summary>
        private void DebugMenu_Click(object sender, EventArgs e)
        {
#if DEBUG
            // デバッグビルド専用、右クリックにより、時刻を強制指定できる別画面を表示する。（タイマーを停止する）
            timer1.Enabled = false;
            var debugForm = new DebugForm(this);
            debugForm.Show();

            debugForm.Left = this.Left + this.Width;
            debugForm.Top = this.Bottom - this.Height;

            //画面からはみ出しちゃったら、反対側に表示する
            if (debugForm.Right > Screen.GetWorkingArea(this).Right) debugForm.Left = this.Left - debugForm.Width;
#endif
        }

        #endregion

    }
}
