using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClockDrive;
using NUnit.Framework;

namespace ClockDriveTest
{
    [TestFixture]
    public class BackGroundTest
    {
        private BackGround bg;

        [SetUp]
        public void Setup()
        {
            bg = new BackGround(@"C:\Dev_Private\Team_DIG_ClockDrive_VSHackathon\Projects\ClockDrive\ClockDrive\bin\Debug\images\");
        }

        [TestCase]
        public void 時間を与えられる()
        {
            bg.SetTime(DateTime.Now);
        }

        [TestCase]
        public void プロパティが読める()
        {
            string pathA = bg.SrcImagePath;
            string pathB = bg.DestImagePath;
            double blendRate = bg.BlendRatio;
        }

        [TestCase]
        public void プロパティが読み込み可能なファイルパスを返す()
        {
            var pathA = bg.SrcImagePath;
            Assert.True(System.IO.File.Exists(pathA), pathA);

            var pathB = bg.DestImagePath;
            Assert.True(System.IO.File.Exists(pathB), pathB);
        }

        //[TestCase]
        //public void ()
        //{
        //    var pathA = bg.SrcImagePath;
        //    Assert.True(System.IO.File.Exists(pathA), pathA);

        //    var pathB = bg.DestImagePath;
        //    Assert.True(System.IO.File.Exists(pathB), pathB);
        //}
    }
}
