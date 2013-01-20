using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ClockDrive
{
    public class BackGround
    {
        /// <summary>
        /// 与えられた時刻
        /// </summary>
        internal DateTime time;

        /// <summary>
        /// すべての背景画像のファイルパス一覧
        /// </summary>
        internal string[] ImageFileNames;

        /// <summary>
        /// 背景画像Ａのファイルパス（２枚ブレンドするうち、先にベタ塗りする方）
        /// たとえば全部で４枚なら、２４Ｈを４等分し、６時間ごとに切り替わっていく
        /// </summary>
        public string SrcImagePath
        {
            get { return ImageFileNames[(0 + time.Hour / (24 / ImageFileNames.Length)) % (ImageFileNames.Length)]; }
        }

        /// <summary>
        /// 背景画像Ｂのファイルパス（２枚ブレンドするうち、後で半透明に描く方）
        /// たとえば全部で４枚なら、２４Ｈを４等分し、６時間ごとに切り替わっていく
        /// </summary>
        public string DestImagePath
        {
            get { return ImageFileNames[(1 + time.Hour / (24 / ImageFileNames.Length)) % (ImageFileNames.Length)]; }
        }

        /// <summary>
        /// 背景画像ＡとＢのブレンド率
        /// たとえば全部で４枚なら、２４Ｈを４等分し、６時間ごとに0～1.0を繰り返す
        /// </summary>
        public double BlendRatio
        {
            get
            {
                var ratio = ((time.Hour % (24 / ImageFileNames.Length))
                            + (time.Minute % 60 / 60.0)
                            + (time.Second % 60 / 60.0 / 60.0)
                            ) / (24 / ImageFileNames.Length);
                return ratio;
            }
        }

        /// <summary>
        /// コンストラクタ（背景画像を読み込むフォルダを受け取って、背景画像のファイル名一覧を取得する）
        /// </summary>
        /// <param name="imagePath"></param>
        public BackGround(string imagePath)
        {
            ImageFileNames = Directory.GetFiles(imagePath, "bg*.png");
        }

        /// <summary>
        /// 基準時刻を与える
        /// </summary>
        /// <param name="t"></param>
        public void SetTime(DateTime t)
        {
            time = t;
        }

    }
}
