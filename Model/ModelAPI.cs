using Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reactive;
using System.Reactive.Linq;

namespace Model
{
    public abstract class ModelAPI : IObserver<int>, IObservable<IBall>
    {
        internal LogicAPI Logic;
        public List<ModelBall> balls;
        public int Radius { get; set; }
        public int[] Size { get; set; }

        public static ModelAPI CreateApi()
        {
            return new ModelLayer();
        }

        public abstract void AddBalls(int numberOfBalls);

        public abstract void AddNewBalls(int numberOfBalls);

        public abstract void MoveBalls(bool start);

        public abstract void RandomizePositions(int maxWidth, int maxHeight);

        public abstract void Subscribe(IObservable<int> provider);

        public abstract void OnCompleted();

        public abstract void OnError(Exception error);

        public abstract void OnNext(int value);

        public abstract IDisposable Subscribe(IObserver<IBall> observer);
    }

    public interface IBall : INotifyPropertyChanged
    {
        double Top { get; }
        double Left { get; }
        int Diameter { get; }
    }

    public class BallChangeEventArgs : EventArgs
    {
        public IBall Ball { get; set; }
    }

    public interface INotifyBallChanged
    {
        event EventHandler<BallChangeEventArgs> BallChanged;
    }

    internal class ModelLayer : ModelAPI
    {
        private IDisposable unsubscriber;
        private IList<IObserver<int>> observers;

        public event EventHandler<BallChangeEventArgs> BallChanged;

        private IObservable<EventPattern<BallChangeEventArgs>> eventObservable = null;

        public ModelLayer()
        {
            eventObservable = Observable.FromEventPattern<BallChangeEventArgs>(this, "BallChanged");
            Radius = 10;
            Logic = Logic ?? LogicAPI.CreateLayer();
            Size = new int[2] { Logic.Size[0], Logic.Size[1] };
            balls = new List<ModelBall>();
            AddBalls(Logic.GetBallsCount());
            Subscribe(Logic);
        }

        public override void AddBalls(int numberOfBalls)
        {
            for (int i = 0; i < numberOfBalls; i++)
            {
                AddModelBall(i);
            }
        }

        public override void AddNewBalls(int numberOfBalls)
        {
            for (int i = 0; i < numberOfBalls; i++)
            {
                AddModelBall(Logic.AddBall());
            }

            foreach (ModelBall ball in balls)
            {
                BallChanged?.Invoke(this, new BallChangeEventArgs() { Ball = ball });
            }
        }

        public void AddModelBall(int id)
        {
            float[] position = Logic.GetBallPosition(id);
            int radius = Logic.GetBallRadius(id);
            Random rnd = new Random();
            int color = rnd.Next(typeof(ModelBall.Color).GetFields().Length - 1);
            balls.Add(new ModelBall(id, position[0], position[1], radius, ((ModelBall.Color)color).ToString()));
        }

        public override void MoveBalls(bool start)
        {
            Logic.MoveBalls(start);
        }

        public void UpdateBall(int id)
        {
            float[] position = Logic.GetBallPosition(id);
            balls[id].Left = position[0];
            balls[id].Top = position[1];
        }

        public override void RandomizePositions(int maxWidth, int maxHeight)
        {
            Logic.RandomizePositions(maxWidth, maxHeight);
        }

        #region observer

        public override void Subscribe(IObservable<int> provider)
        {
            if (provider != null)
                unsubscriber = provider.Subscribe(this);
        }

        public override void OnCompleted()
        {
            unsubscriber.Dispose();
        }

        public override void OnError(Exception error)
        {
            throw error;
        }

        public override void OnNext(int id)
        {
            UpdateBall(id);
        }

        #endregion observer

        #region provider

        public override IDisposable Subscribe(IObserver<IBall> observer)
        {
            return eventObservable.Subscribe(x => observer.OnNext(x.EventArgs.Ball), ex => observer.OnError(ex), () => observer.OnCompleted());
        }

        #endregion provider
    }
}