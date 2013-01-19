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
            road = new Road();
        }

        [TestCase]
        public void 時間を与えられ_道路上の座標を得られる()
        {
            Point position = road.GetRoadPosition(DateTime.Now);
        }
    }
}
