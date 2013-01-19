using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClockDrive
{
    public class BackGround
    {
        public string ImagePath { get; set; }
        public DateTime time { get; private set; }
        private List<string> ImageFileNames = new List<string> { "bg01.png", "bg02.png", "bg03.png", "bg04.png" };

        public string SrcImagePath
        {
            get
            {
                var path = ImagePath + ImageFileNames[(time.Hour / 6 + 0) % 4];
                return path;
            }
        }
        public string DestImagePath
        {
            get
            {
                var path = ImagePath + ImageFileNames[(time.Hour / 6 + 1) % 4];
                return path;
            }
        }
        public double BlendRatio
        {
            get
            {
                return (double)time.Hour % 6 / 6.0;
            }
        }

        public BackGround(string imagePath)
        {
            ImagePath = imagePath;
        }

        public void SetTime(DateTime t)
        {
            time = t;
        }

    }
}
