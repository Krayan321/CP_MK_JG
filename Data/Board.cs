using System;
using System.Collections.Generic;

namespace Data
{
    public class Board
    {
        public int[] Size { get; set; }
        public List<Ball> Balls { get; private set; }
        public int BallsCounter { get; set; } = 0;

        public Board(int width, int height, int numberOfBalls) : this(width, height)
        {
            for (int i = 0; i < numberOfBalls; i++)
            {
                AddBall();
            }
        }

        public Board(int width, int height)
        {
            Size = new int[2] { width, height };
            Balls = new List<Ball>();
        }

        public int AddBall()
        {
            Random rnd = new Random();
            int newValue = 5 + rnd.Next(15);
            Balls.Add(new Ball(BallsCounter++));
            GetBall(BallsCounter - 1).Mass = (float)(Math.PI * (newValue * newValue));
            GetBall(BallsCounter - 1).Radius = newValue;
            return BallsCounter - 1;
        }

        public Ball GetBall(int id)
        {
            return Balls[id];
        }
    }
}