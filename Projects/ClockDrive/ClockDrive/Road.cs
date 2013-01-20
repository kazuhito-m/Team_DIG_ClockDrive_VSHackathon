using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;

namespace ClockDrive
{
    public class Road
    {
        /// <summary>
        /// 道路上の位置の一覧
        /// </summary>
        public List<PointF> roadPositions { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="roadCsvFilePath"></param>
        public Road(string roadCsvFilePath)
        {
            roadPositions = new List<PointF>();
            LoadCsvFromFile(roadCsvFilePath);
        }

        /// <summary>
        /// カンマ区切りかタブ区切りの道路上座標ＣＳＶファイルを読み込み、格納しておく
        /// </summary>
        /// <param name="roadCsvFilePath"></param>
        private void LoadCsvFromFile(string roadCsvFilePath)
        {
            using (var reader = new StreamReader(roadCsvFilePath))
            {
                while (!reader.EndOfStream)
                {
                    var buffer = reader.ReadLine();
                    if (string.IsNullOrEmpty(buffer)) continue;

                    var elements = Regex.Split(buffer, "[,\t]");
                    roadPositions.Add(new PointF(float.Parse(elements[0]), float.Parse(elements[1])));
                }
            }
        }

        /// <summary>
        /// 時刻に応じた角度を算出するための係数を、0～1.0の範囲で得る
        /// </summary>
        /// <returns></returns>
        public static double CalcPositionRatio(DateTime time)
        {
            var ratio = (time.Hour % 12 / 12.0)
                        + (time.Minute % 60 / 60.0 / 12)
                        + (time.Second % 60 / 60.0 / 60.0 / 12);
            return ratio;
        }

        /// <summary>
        /// 与えた時間に応じた、道路上の位置を得る（近傍２点から、微妙な位置を滑らかに補完する）
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public PointF GetRoadPosition(DateTime time)
        {
            var ratio = CalcPositionRatio(time);
            var baseIndex = ratio * roadPositions.Count;
            var idxA = (int)baseIndex % roadPositions.Count;
            var idxB = (idxA + 1) % roadPositions.Count;
            var blendRatio = baseIndex - (int)baseIndex;
            return new PointF(
                (float)(roadPositions[idxA].X * (1.0 - blendRatio) + roadPositions[idxB].X * (blendRatio)),
                (float)(roadPositions[idxA].Y * (1.0 - blendRatio) + roadPositions[idxB].Y * (blendRatio)));
        }
    }
}
