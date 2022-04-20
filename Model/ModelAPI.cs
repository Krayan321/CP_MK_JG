using Logic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public abstract class ModelAPI
    {
        internal LogicAPI logicLayer;
        public List<ModelBall> balls;
        public static ModelAPI CreateApi()
        {
            return new ModelLayer();
        }

        public abstract void AddBalls(int numberOfBalls);
    }

    internal class ModelLayer : ModelAPI
    {
        public ModelLayer()
        {
            logicLayer = LogicAPI.CreateLayer();
            foreach (Ball ball in logicLayer.GetBalls())
            {
                balls.Add(new ModelBall(ball));
            }
        }

        public override void AddBalls(int numberOfBalls)
        {
            for (int i = 0; i < numberOfBalls; i++)
            {
                Ball newBall = new Ball();
                logicLayer.AddBall(newBall);
                balls.Add(new ModelBall(newBall));
            }
        }
    }
}
