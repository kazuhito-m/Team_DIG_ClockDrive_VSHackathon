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
        private Car car;

        [SetUp]
        public void Setup()
        {
            car = new Car();
        }

        [TestCase]
        public void 時間を与えられる()
        {
            car.SetTime(DateTime.Now);
        }

        [TestCase]
        public void プロパティが読める()
        {
            Point position = car.Position;
            double angle = car.Angle;
        }

        [TestCase(0, 0, 0, 0.0)]
        [TestCase(6, 0, 0, 0.5)]
        [TestCase(12, 0, 0, 0)]
        [TestCase(23, 0, 0, 1.0 - (1.0 / 12))]
        public void 与えた時刻に応じて文字盤上の角度が得られる(int hour, int minute, int second, double angle)
        {
            car.SetTime(new DateTime(2000, 1, 1, hour, minute, second));
            Assert.AreEqual(angle, car.CulcPositionAngleRatio());
        }
    }
}
