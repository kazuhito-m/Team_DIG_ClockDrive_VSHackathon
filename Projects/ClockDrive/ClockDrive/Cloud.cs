using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ClockDrive
{
    public class Cloud
    {
        private Point BackgroundSize;
        public List<PointF> Positions { get; private set; }

        public Cloud(int width, int height, int count)
        {
            BackgroundSize = new Point(width, height);
            Positions = new List<PointF>();
            var r = new Random();
            for (var i = 0; i < count; i++)
                Positions.Add(new PointF(NewX(r), (height * 2 / count * i)));
        }

        internal float NewX(Random r)
        {
            return (float)(r.NextDouble() * BackgroundSize.X * 2 - BackgroundSize.X * 0.5);
        }

        internal float NewY(Random r)
        {
            return (float)(r.NextDouble() * BackgroundSize.Y * 2 - BackgroundSize.Y * 0.5);
        }

        public void Move(double elapsed)
        {
            var r = new Random();
            for (var i = 0; i < Positions.Count; i++)
            {
                float newX = (float)(Positions[i].X - elapsed);
                float newY = (float)(Positions[i].Y - elapsed);
                if (newX < -BackgroundSize.X * 0.5) { newX += BackgroundSize.X * 2; }
                if (newY < -BackgroundSize.Y * 0.5) { newY += BackgroundSize.Y * 2; newX = NewX(r); }
                Positions[i] = new PointF(newX, newY);
            }
        }
    }
}
