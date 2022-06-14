using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Data
{
    public class Ball : BallInterface
    {
        public float Position_X { get; set; }
        public float Position_Y { get; set; }
        public float[] Movement { get; set; }
        public float Speed { get; set; }
        public int Radius { get; set; }
        public int Id { get; }
        public float Mass { get; set; }
        internal readonly IList<IObserver<BallInterface>> observers;
        private readonly Random rnd = new Random();
        private Thread BallThread;
        private Stopwatch watch = new Stopwatch();
        private float time = 0;

        public Ball(float position_X, float position_Y, int radius, int id, float mass)
        {
            Id = id;
            Mass = mass;
            Movement = new float[2];
            Position_X = position_X;
            Position_Y = position_Y;
            Radius = radius;
            observers = new List<IObserver<BallInterface>>();
            RandomizeMovement();
        }

        public Ball(int id) : this(10, 10, 10, id, 10)
        {
        }

        public void RandomizeMovement()
        {
            int minus_X = Convert.ToBoolean(rnd.Next(2)) ? 1 : -1;
            int minus_Y = Convert.ToBoolean(rnd.Next(2)) ? 1 : -1;

            this.Movement[0] = (float)((1 + rnd.Next(100)) * 0.01 * minus_X);
            this.Movement[1] = (float)((1 + rnd.Next(100)) * 0.01 * minus_Y);
        }

        public BallLogData GetLogData()
        {
            BallLogData data = new BallLogData(Id, Position_X, Position_Y, Movement, Speed);
            return data;
        }

        public float GetTime()
        {
            return this.time;
        }

        public void RandomizePosition(int maxWidth, int maxHeight)
        {
            this.Position_X = rnd.Next(maxWidth - 1 - this.Radius * 2);
            this.Position_Y = rnd.Next(maxHeight - 1 - this.Radius * 2);
            NotifyObservers();
        }

        public void SwitchDirections(bool direction_X)
        {
            if (direction_X)
                this.Movement[0] *= -1;
            else
                this.Movement[1] *= -1;
            NotifyObservers();
        }

        public void Move()
        {
            watch.Stop();
            this.time = watch.ElapsedMilliseconds;
            float elapsed = this.time > 0 ? this.time : 1;
            //elapsed /= 1000;

            Position_X += Movement[0] * elapsed;
            Position_Y += Movement[1] * elapsed;
            NotifyObservers();
            watch.Restart();
        }

        public void StartMoving()
        {
            while (true)
            {
                Move();
                NotifyObservers();
            }
        }

        public void NotifyObservers()
        {
            foreach (var observer in observers.ToList())
            {
                if (observer != null)
                {
                    observer.OnNext(this);
                }
            }
        }

        public void StartMovingThread()
        {
            if (BallThread is null)
                this.BallThread = new Thread(StartMoving);

            BallThread.IsBackground = true;
            BallThread.Start();
        }

        public void CorrectPositions(int x, int y)
        {
            if (Position_X < 0)
                Position_X = Radius * 2;
            if (Position_X > x)
                Position_X = x - Radius * 2;

            if (Position_Y < 0)
                Position_Y = Radius * 2;
            if (Position_Y > y)
                Position_Y = y - Radius * 2;
        }

        public IDisposable Subscribe(IObserver<BallInterface> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
            return new Unsubscriber(observers, observer);
        }

        private class Unsubscriber : IDisposable
        {
            private IList<IObserver<BallInterface>> _observers;
            private IObserver<BallInterface> _observer;

            public Unsubscriber(IList<IObserver<BallInterface>> observers, IObserver<BallInterface> observer)
            {
                this._observers = observers;
                this._observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                    _observers.Remove(_observer);
            }
        }
    }
}