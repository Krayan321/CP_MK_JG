using Logic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

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
            logicLayer = logicLayer ?? LogicAPI.CreateLayer();
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
                Random rnd = new Random();
                int color = rnd.Next(typeof(ModelBall.Color).GetFields().Length - 1);
                balls.Add(new ModelBall(newBall, ((ModelBall.Color)color).ToString()));
            }
        }

        public override void MoveBalls()
        {
            while(true)
            {
                logicLayer.MoveBalls();
                Thread.Sleep(10);
            }
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
