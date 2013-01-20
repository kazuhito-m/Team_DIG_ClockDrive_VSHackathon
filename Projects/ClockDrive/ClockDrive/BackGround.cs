using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        /// </summary>
        public string SrcImagePath
        {
            get
            {
                var path = ImageFileNames[(time.Hour / 6 + 0) % 4];
                return path;
            }
        }

        /// <summary>
        /// 背景画像Ｂのファイルパス（２枚ブレンドするうち、後で半透明に描く方）
        /// </summary>
        public string DestImagePath
        {
            get
            {
                var path = ImageFileNames[(time.Hour / 6 + 1) % 4];
                return path;
            }
        }

        /// <summary>
        /// 背景画像ＡとＢのブレンド率
        /// </summary>
        public double BlendRatio
        {
            get
            {
                return (double)time.Hour % 6 / 6.0;
            }
        }

        /// <summary>
        /// コンストラクタ（背景画像を読み込むフォルダを決定し、背景画像のファイル名一覧を取得する）
        /// </summary>
        /// <param name="imagePath"></param>
        public BackGround(string imagePath)
        {
            ImageFileNames = System.IO.Directory.GetFiles(imagePath, "bg*.png");
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
