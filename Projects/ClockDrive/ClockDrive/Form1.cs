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

        private BackGround bg;
        private Road road;
        private Car car;
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
            ImageCache = new Dictionary<string, Bitmap>();

            // 最初に、現在時刻の状態を描いておく
            Draw(DateTime.Now);
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
            bg.SetTime(current);
            car.SetTime(current);

            Invalidate();
        }

        /// <summary>
        /// このフォームを再描画する（ダブルバッファ有効、ちらつき無し）
        /// </summary>
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawBackGround(e.Graphics, currentTime);
            DrawCar(e.Graphics, currentTime);
            DrawDigitalTime(e.Graphics, currentTime);
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
                new Rectangle(0, 0, this.Width, this.Height)
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
            g.DrawImageUnscaled(
                carImage,
                new Rectangle(
                    (int)(carImage.Width * stretchRatio * -0.5F), (int)(carImage.Height * stretchRatio * -0.5F),
                    (int)(carImage.Width * stretchRatio), (int)(carImage.Height * stretchRatio))
                );

            g.ResetTransform(); // ※行列をリセットし、直前までの状態を反映させない
        }

        /// <summary>
        /// 指定された時刻に応じたデジタル時刻を描く
        /// </summary>
        /// <param name="current"></param>
        private void DrawDigitalTime(Graphics g, DateTime current)
        {
            this.Text = string.Format("{0} - {1}", current.ToString(), Application.ProductName);
            var f = new Font("MS UI Gothic", 40);
            for (var i = 0; i < 2; i++)
            {
                g.DrawString(current.ToLongTimeString(),
                             f,
                             (i == 0 ? Brushes.DarkBlue : Brushes.LightBlue),
                             20 - i * 1, this.Height - 100 - i * 2
                             );
            }
        }

        /// <summary>
        /// デバッグビルド専用、右クリックにより、時刻を強制指定できる別画面を表示する。
        /// なお、同時に、タイマーを停止する。
        /// </summary>
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
#if DEBUG
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                timer1.Enabled = false;
                var debugForm = new DebugForm(this);
                debugForm.Show();
                debugForm.Left = this.Left + this.Width;
                debugForm.Top = this.Bottom - this.Height;
            }
#endif
        }
    }
}
