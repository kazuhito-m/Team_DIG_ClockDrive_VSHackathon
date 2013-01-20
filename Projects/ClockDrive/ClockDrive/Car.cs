using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ClockDrive
{
    public class Car
    {
        /// <summary>
        /// 与えられた時刻
        /// </summary>
        internal DateTime time;

        /// <summary>
        /// 基準時刻を与える
        /// </summary>
        /// <param name="t"></param>
        public void SetTime(DateTime t)
        {
            time = t;
        }

        /// <summary>
        /// 時刻に応じた角度を算出するための係数を、0～1.0の範囲で得る
        /// </summary>
        /// <returns></returns>
        public double CulcPositionAngleRatio()
        {
            var ratio = (time.Hour % 12 / 12.0) + (time.Minute % 60 / 60.0 / 12) + (time.Second % 60 / 60.0 / 60.0 / 12);
            return ratio;
        }

        /// <summary>
        /// 車を描くべき位置を取得する
        /// </summary>
        public Point Position
        {
            get
            {
                //TODO: 本当はRoadクラスから得るべき
                double divider = CulcPositionAngleRatio() - 0.25;
                var angle = (divider == 0 ? 0 : (Math.PI * 2) * divider);
                return new Point(
                    (int)(0 + 150 * Math.Cos(angle)),
                    (int)(0 + 150 * Math.Sin(angle))
                    );
            }
        }

        /// <summary>
        /// 車を描くべき角度を取得する
        /// </summary>
        public double Angle
        {
            get
            {
                //TODO: 本当はRoadクラスから得るべき（前後あわせて３位置から補完する）
                return (CulcPositionAngleRatio() + 0.75) * 360;
            }
        }
    }
}
