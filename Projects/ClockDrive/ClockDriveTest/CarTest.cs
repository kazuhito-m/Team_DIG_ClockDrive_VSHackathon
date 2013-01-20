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
    class CarTest
    {
        private Road road;
        private Car car;

        [SetUp]
        public void Setup()
        {
            road = new Road(@"C:\Dev_Private\Team_DIG_ClockDrive_VSHackathon\Projects\ClockDrive\ClockDrive\datas\RoadData.csv");
            car = new Car(road);
        }

        [TestCase]
        public void 時間を与えられる()
        {
            car.SetTime(DateTime.Now);
        }

        [TestCase]
        public void プロパティが読める()
        {
            car.SetTime(DateTime.Now);
            PointF position = car.Position;
            double angle = car.Angle;
        }

        [TestCase(0, 0, 0, 0, 0, 30, "異なるはず")]
        [TestCase(0, 1, 0, 0, 2, 0, "異なるはず")]
        [TestCase(0, 59, 30, 1, 0, 0, "異なるはず")]
        [TestCase(11, 59, 30, 12, 0, 0, "異なるはず")]
        [TestCase(0, 0, 0, 12, 0, 0, "同じはず")]
        [TestCase(6, 0, 0, 18, 0, 0, "同じはず")]
        public void 与えた時間に応じて_角度が変化する(int hourA, int minuteA, int secondA, int hourB, int minuteB, int secondB, string expectTo)
        {
            car.SetTime(new DateTime(2000, 1, 1, hourA, minuteA, secondA));
            var angleA = car.Angle;
            car.SetTime(new DateTime(2000, 1, 1, hourB, minuteB, secondB));
            var angleB = car.Angle;
            if (expectTo[0].Equals('異'))
            {
                Assert.AreNotEqual(angleA, angleB);
            }
            else
            {
                Assert.AreEqual(angleA, angleB);
            }
        }
    }
}
