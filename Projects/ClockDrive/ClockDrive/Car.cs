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
        public DateTime time { get; private set; }

        /// <summary>
        /// 基準時刻を与える
        /// </summary>
        /// <param name="t"></param>
        public void SetTime(DateTime t)
        {
            time = t;
        }

        public double CulcPositionAngleRatio()
        {
            double divider = ((double)(time.Hour % 12) / 12.0) + (double)(time.Minute % 60 / 60.0 / 12);
            // (double)((time.Hour % 12) / 12.0 * 60 * 60) / (24 * 60 * 60); // ((time.Hour % 12) * 60 * 60 + (time.Minute % 60) * 60 + (time.Second % 60)) / (24 * 60 * 60);
            return divider;
        }

        /// <summary>
        /// 車を描くべき位置を取得する
        /// </summary>
        public Point Position
        {
            get
            {
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
                return (CulcPositionAngleRatio() + 0.75) * 360;
                //return new Random().NextDouble() * 360;
            }
        }
    }
}
