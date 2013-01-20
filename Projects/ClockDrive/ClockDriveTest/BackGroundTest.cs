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

        [TestCase(0, 0, 0, "bg01.png", "bg02.png", 0)]
        [TestCase(5, 59, 59, "bg01.png", "bg02.png", 1.0 - (1.0 / 60.0 / 60.0 / 6.0))]
        [TestCase(6, 0, 0, "bg02.png", "bg03.png", 0)]
        [TestCase(11, 59, 59, "bg02.png", "bg03.png", 1.0 - (1.0 / 60.0 / 60.0 / 6.0))]
        [TestCase(12, 0, 0, "bg03.png", "bg04.png", 0)]
        [TestCase(23, 59, 59, "bg04.png", "bg01.png", 1.0 - (1.0 / 60.0 / 60.0 / 6.0))]
        public void 背景４枚のとき_時刻指定に応じた_適切な画像ファイルパスのペアとブレンド率を返す(int hour, int minute, int second, string bgA, string bgB, double blend)
        {
            bg.ImageFileNames = new string[] { "bg01.png", "bg02.png", "bg03.png", "bg04.png" };
            bg.SetTime(new DateTime(2000, 1, 1, hour, minute, second));
            Assert.That(bg.SrcImagePath, Is.EqualTo(bgA));
            Assert.That(bg.DestImagePath, Is.EqualTo(bgB));
            Assert.That(bg.BlendRatio, Is.EqualTo(blend));
        }

        [TestCase(0, 0, 0, "bg01.png", "bg02.png", 0)]
        [TestCase(3, 59, 59, "bg01.png", "bg02.png", 1.0 - (1.0 / 60.0 / 60.0 / 4.0))]
        [TestCase(4, 0, 0, "bg02.png", "bg03.png", 0)]
        [TestCase(7, 59, 59, "bg02.png", "bg03.png", 1.0 - (1.0 / 60.0 / 60.0 / 4.0))]
        [TestCase(8, 0, 0, "bg03.png", "bg04.png", 0)]
        [TestCase(23, 59, 59, "bg06.png", "bg01.png", 1.0 - (1.0 / 60.0 / 60.0 / 4.0))]
        public void 背景６枚のときも_時刻指定に応じた_適切な画像ファイルパスのペアとブレンド率を返す(int hour, int minute, int second, string bgA, string bgB, double blend)
        {
            bg.ImageFileNames = new string[] { "bg01.png", "bg02.png", "bg03.png", "bg04.png", "bg05.png", "bg06.png" };
            bg.SetTime(new DateTime(2000, 1, 1, hour, minute, second));
            Assert.That(bg.SrcImagePath, Is.EqualTo(bgA));
            Assert.That(bg.DestImagePath, Is.EqualTo(bgB));
            Assert.That(bg.BlendRatio, Is.EqualTo(blend));
        }
    }
}
