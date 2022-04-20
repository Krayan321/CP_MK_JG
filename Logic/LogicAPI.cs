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
        public abstract List<Ball> GetBalls();
        public static LogicAPI CreateLayer(DataAPI data = default(DataAPI))
        {
            return new BusinessLogic(data == null ? DataAPI.CreateDataBase() : data);
        }

        private class BusinessLogic : LogicAPI
        {
            private readonly DataAPI MyDataLayer;
            public BusinessLogic(DataAPI dataLayerAPI)
            {
                MyDataLayer = dataLayerAPI;
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
        }
    }

}

