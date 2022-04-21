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
        public int Radius { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public static ModelAPI CreateApi()
        {
            return new ModelLayer();
        }

        public abstract void AddBalls(int numberOfBalls);
        public abstract void RemoveBalls();
        public abstract void MoveBalls();
        public abstract void RandomizePositions(int maxWidth, int maxHeight);

    }

    internal class ModelLayer : ModelAPI
    {
        public ModelLayer()
        {
            
            Radius = 10;
            logicLayer = LogicAPI.CreateLayer();
            Width = logicLayer.Board.Width; 
            Height = logicLayer.Board.Height;
            balls = new List<ModelBall>();
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

        public override void MoveBalls()
        {
            logicLayer.MoveBalls();
        }

        public override void RandomizePositions(int maxWidth, int maxHeight)
        {
            logicLayer.RandomizePositions(maxWidth, maxHeight);
        }

        public override void RemoveBalls()
        { 
            balls.Clear();
            logicLayer.RemoveBalls();
        }
    }
}
