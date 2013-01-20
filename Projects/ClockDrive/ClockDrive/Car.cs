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
        /// 車の位置や角度を算出するためのRoadインスタンス
        /// </summary>
        private Road road;

        /// <summary>
        /// コンストラクタ（Roadインスタンスを受け取って、格納しておく）
        /// </summary>
        /// <param name="road"></param>
        public Car(Road road)
        {
            this.road = road;
        }

        /// <summary>
        /// 基準時刻を与える
        /// </summary>
        /// <param name="t"></param>
        public void SetTime(DateTime t)
        {
            time = t;
        }

        /// <summary>
        /// 車を描くべき位置を取得する
        /// </summary>
        public PointF Position
        {
            get
            {
                return road.GetRoadPosition(time);
            }
        }

        /// <summary>
        /// 車を描くべき角度を取得する（前後２点から滑らかに補完する）
        /// </summary>
        public double Angle
        {
            get
            {
                var posA = road.GetRoadPosition(time.AddSeconds(-5));
                var posB = road.GetRoadPosition(time.AddSeconds(+5));
                var xDiff = (posA.X - posB.X);
                var yDiff = (posA.Y - posB.Y);
                var angle = Math.Atan2(yDiff, xDiff);
                return angle / Math.PI / 2.0 * 360.0 + 90.0;
            }
        }
    }
}
