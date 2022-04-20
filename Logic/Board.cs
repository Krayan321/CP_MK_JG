using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public class Board
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public List<Ball> Balls { get; private set; }

        public Board(int width, int height, int numberOfBalls) : this(width, height)
        {
            for (int i = 0; i < numberOfBalls; i++)
            {
                Balls.Add(new Ball());
            }
        }

        public Board(int width, int height)
        {
            Width = width;
            Height = height;
            Balls = new List<Ball>();
        }

        public void AddBall(Ball ball)
        {
            if (!(ball is null))
            {
                Balls.Add(ball);
            }
        }

        public void RemoveBall(Ball ball)
        {
            if (!(ball is null))
            {
                Balls.Remove(ball);
            }
        }

        public void MoveBalls()
        {
            foreach (Ball ball in Balls)
            {
                ball.Move();
            }
        }
    }
}
