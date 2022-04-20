using System;

namespace Logic
{
    public class Ball
    {
        public float Position_X { get; set; }
        public float Position_Y { get; set; }
        public float[] Direction { get; set; }
        public float Speed { get; set; }
        public float Radius { get; set; }

        public Ball(float position_X, float position_Y, float radius)
        {
            Direction = new float[2];
            Position_X = position_X;
            Position_Y = position_Y;
            Radius = radius;
            RandomizeMovement();
        }

        public void RandomizeMovement()
        {
            Random rnd = new Random();

            this.Speed = 1 + rnd.Next(10);
            this.Direction[0] = (float)(1 + rnd.Next(100) * 0.01);
            this.Direction[1] = (float)(1 + rnd.Next(100) * 0.01);
        }

        public void Move()
        {
            Position_X += Direction[0] * Speed;
            Position_Y += Direction[1] * Speed;
        }
    }
}
