using System;
using System.ComponentModel;

namespace Logic
{
    public class Ball
    {
        public float Position_X { get; set; }
        public float Position_Y { get; set; }
        public float[] Direction { get; set; }
        public float Speed { get; set; }
        public int Radius { get; set; }
        private readonly Random rnd = new Random();

        public Ball(float position_X, float position_Y, int radius)
        {
            Direction = new float[2];
            Position_X = position_X;
            Position_Y = position_Y;
            Radius = radius;
            RandomizeMovement();
        }
        public Ball() : this(10, 10, 10)
        {

        }

        private void RandomizeMovement()
        {
            this.Speed = 1 + rnd.Next(10);
            this.Direction[0] = (float)(1 + rnd.Next(100) * 0.01);
            this.Direction[1] = (float)(1 + rnd.Next(100) * 0.01);
        }

        public void RandomizePosition(int maxWidth, int maxHeight)
        {
            this.Position_X = this.Radius + rnd.Next(maxWidth - this.Radius);
            this.Position_Y = this.Radius + rnd.Next(maxHeight - this.Radius);
        }
        public void Move()
        {
            Position_X += Direction[0] * Speed;
            Position_Y += Direction[1] * Speed;
        }
    }
}
