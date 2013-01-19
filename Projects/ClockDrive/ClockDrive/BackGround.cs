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

        public BackGround(string imagePath)
        {
            ImagePath = imagePath;
        }

        public string SrcImagePath
        {
            get
            {
                switch (time.Hour / 6)
                {
                    case 0:
                        return ImagePath + @"bg01.png";
                    case 1:
                        return ImagePath + @"bg02.png";
                    case 2:
                        return ImagePath + @"bg03.png";
                    case 3:
                        return ImagePath + @"bg04.png";
                    default:
                        return ImagePath + @"bg01.png";
                }
            }
        }
        public string DestImagePath
        {
            get
            {
                switch (time.Hour / 6)
                {
                    case 0:
                        return ImagePath + @"bg02.png";
                    case 1:
                        return ImagePath + @"bg03.png";
                    case 2:
                        return ImagePath + @"bg04.png";
                    case 3:
                        return ImagePath + @"bg01.png";
                    default:
                        return ImagePath + @"bg06.png";
                }
            }
        }
        public double BlendRatio
        {
            get
            {
                return (double)time.Hour % 6 / 6.0;
            }
        }

        public void SetTime(DateTime t)
        {
            time = t;
        }


    }
}
