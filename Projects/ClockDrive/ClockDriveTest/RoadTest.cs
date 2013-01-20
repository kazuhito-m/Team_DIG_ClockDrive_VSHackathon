using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClockDrive;
using NUnit.Framework;
using System.Drawing;

namespace ClockDriveTest
{
    [TestFixture]
    class RoadTest
    {
        private Road road;

        [SetUp]
        public void Setup()
        {
            road = new Road(@"C:\Dev_Private\Team_DIG_ClockDrive_VSHackathon\Projects\ClockDrive\ClockDrive\datas\RoadData.csv");
        }

        [TestCase]
        public void 時間を与えられ_道路上の座標を得られる()
        {
            var position = road.GetRoadPosition(DateTime.Now);
        }

        [TestCase(0, 0, 0, 0.0)]
        [TestCase(6, 0, 0, 0.5)]
        [TestCase(12, 0, 0, 0)]
        [TestCase(23, 0, 0, 1.0 - (1.0 / 12))]
        public void 与えた時刻に応じて文字盤上の角度が得られる(int hour, int minute, int second, double angle)
        {
            Assert.AreEqual(angle, Road.CalcPositionRatio(new DateTime(2000, 1, 1, hour, minute, second)));
        }

        [TestCase(0, 0, 0, 0, 0, 10, "異なるはず")]
        [TestCase(0, 1, 0, 0, 2, 0, "異なるはず")]
        [TestCase(0, 59, 50, 1, 0, 0, "異なるはず")]
        [TestCase(11, 59, 50, 12, 0, 0, "異なるはず")]
        [TestCase(0, 0, 0, 12, 0, 0, "同じはず")]
        [TestCase(6, 0, 0, 18, 0, 0, "同じはず")]
        public void 与えた時間に応じて_道路上の座標が変化する(int hourA, int minuteA, int secondA, int hourB, int minuteB, int secondB, string expectTo)
        {
            if (expectTo[0].Equals('異'))
            {
                Assert.AreNotEqual(
                    road.GetRoadPosition(new DateTime(2000, 1, 1, hourA, minuteA, secondA))
                    , road.GetRoadPosition(new DateTime(2000, 1, 1, hourB, minuteB, secondB)));
            }
            else
            {
                Assert.AreEqual(
                    road.GetRoadPosition(new DateTime(2000, 1, 1, hourA, minuteA, secondA))
                    , road.GetRoadPosition(new DateTime(2000, 1, 1, hourB, minuteB, secondB)));
            }
        }
    }
}
