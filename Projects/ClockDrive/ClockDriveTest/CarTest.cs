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
        [TestCase(12, 0, 0, 180.0)]
        public void プロパティが読める(int hour, int minute, int second, double angle)
        {
            car.SetTime(new DateTime(2000, 1, 1, hour, minute, second));
            Assert.AreEqual(angle, car.CulcPositionAngleRatio());
        }
    }
}
