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

            int minus_X = Convert.ToBoolean(rnd.Next(2)) ? 1 : -1;
            int minus_Y = Convert.ToBoolean(rnd.Next(2)) ? 1 : -1;

            this.Direction[0] = (float)((1 + rnd.Next(100)) * 0.01 * minus_X);
            this.Direction[1] = (float)((1 + rnd.Next(100)) * 0.01 * minus_Y);
        }

        public void RandomizePosition(int maxWidth, int maxHeight)
        {
            this.Position_X = this.Radius + rnd.Next(maxWidth - this.Radius);
            this.Position_Y = this.Radius + rnd.Next(maxHeight - this.Radius);
        }

        private void SwitchDirections(bool direction_X)
        {
            if (direction_X) 
                this.Direction[0] *= -1;
            else
                this.Direction[1] *= -1;
        }
        public void Move(int maxWidth, int maxHeight)
        {
            if (Position_X + Radius * 2 + Direction[0] * Speed > maxWidth || Position_X + Direction[0] * Speed < 0)
                SwitchDirections(true);
            if (Position_Y + Radius * 2 + Direction[1] * Speed > maxHeight || Position_Y + Direction[1] * Speed < 0)
                SwitchDirections(false);

            Position_X += Direction[0] * Speed;
            Position_Y += Direction[1] * Speed;
        }
    }
}
