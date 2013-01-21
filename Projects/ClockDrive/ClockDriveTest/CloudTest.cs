using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ClockDrive;

namespace ClockDriveTest
{
    [TestFixture]
    class CloudTest
    {
        private Cloud cloud;

        [SetUp]
        public void Setup()
        {
            cloud = new Cloud(512, 512, 10);
        }

        [TestCase]
        public void 雲はすべてバラバラの位置()
        {
            for (var i = 0; i < cloud.Positions.Count - 1; i++)
                Assert.That(cloud.Positions[i], Is.Not.EqualTo(cloud.Positions[i + 1]));
        }

        [TestCase]
        public void 徐々に移動していく()
        {
            var prevPos = cloud.Positions[0];
            cloud.Move(1);
            Assert.That(cloud.Positions[0], Is.Not.EqualTo(prevPos));
        }

    }
}
