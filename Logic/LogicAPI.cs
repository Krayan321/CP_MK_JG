using Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public abstract class LogicAPI
    {
        public Board Board { get; set; }

        public abstract void MoveBalls();
        public abstract void AddBall(Ball ball);
        public abstract void RemoveBall(Ball ball);
        public abstract void RemoveBalls();
        public abstract List<Ball> GetBalls();
        public abstract void RandomizePositions(int maxWidth, int maxHeight);
        public static LogicAPI CreateLayer(DataAPI data = default)
        {
            return new BusinessLogic(data ?? DataAPI.CreateDataBase());
        }

        private class BusinessLogic : LogicAPI
        {
            private readonly DataAPI MyDataLayer;
            public BusinessLogic(DataAPI dataLayerAPI)
            {
                MyDataLayer = dataLayerAPI;
                Board = new Board(750, 350);
            }

            public override void MoveBalls()
            {
                Board.MoveBalls();
            }

            public override void AddBall(Ball ball)
            {
                Board.AddBall(ball);
            }
            public override void RemoveBall(Ball ball)
            {
                Board.RemoveBall(ball);
            }

            public override List<Ball> GetBalls()
            {
                return Board.GetBalls();
            }

            public override void RandomizePositions(int maxWidth, int maxHeight)
            {
                foreach(Ball ball in GetBalls())
                {
                    ball.RandomizePosition(maxWidth, maxHeight);
                }
            }

            public override void RemoveBalls()
            {
                Board.Balls.Clear();
            }
        }
    }

}

