﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;

namespace Data
{
    public class Ball : IObservable<Ball>
    {
        public float Position_X { get; set; }
        public float Position_Y { get; set; }
        public float[] Direction { get; set; }
        public float Speed { get; set; }
        public int Radius { get; set; }
        public int Id { get;}
        public double Mass { get; set; }
        internal readonly IList<IObserver<Ball>> observers;
        private readonly Random rnd = new Random();
        private Thread BallThread;

        public Ball(float position_X, float position_Y, int radius, int id)
        {
            Id = id;
            Direction = new float[2];
            Position_X = position_X;
            Position_Y = position_Y;
            Radius = radius;
            observers = new List<IObserver<Ball>>();
            RandomizeMovement();
        }
        public Ball(int id) : this(10, 10, 10, id)
        {

        }

        public void RandomizeMovement()
        {
            this.Speed = 1 + rnd.Next(10);

            int minus_X = Convert.ToBoolean(rnd.Next(2)) ? 1 : -1;
            int minus_Y = Convert.ToBoolean(rnd.Next(2)) ? 1 : -1;

            this.Direction[0] = (float)((1 + rnd.Next(100)) * 0.01 * minus_X);
            this.Direction[1] = (float)((1 + rnd.Next(100)) * 0.01 * minus_Y);
        }

        public void RandomizePosition(int maxWidth, int maxHeight)
        {
            this.Position_X = this.Radius + rnd.Next(maxWidth - this.Radius * 2);
            this.Position_Y = this.Radius + rnd.Next(maxHeight - this.Radius * 2);
        }

        public void SwitchDirections(bool direction_X)
        {
            if (direction_X) 
                this.Direction[0] *= -1;
            else
                this.Direction[1] *= -1;
        }
        public void Move()
        {
            if (Position_X + Radius * 2 + Direction[0] * Speed > maxWidth || Position_X + Direction[0] * Speed < 0)
                SwitchDirections(true);
            if (Position_Y + Radius * 2 + Direction[1] * Speed > maxHeight || Position_Y + Direction[1] * Speed < 0)
                SwitchDirections(false);

            Position_X += Direction[0] * Speed;
            Position_Y += Direction[1] * Speed;
        }

        public void StartMoving()
        {
            while (true)
            {
                Move();

                foreach (var observer in observers.ToList())
                {
                    if (observer != null)
                    {
                        observer.OnNext(this);
                    }
                }
                System.Threading.Thread.Sleep(1);
            }
        }

        public void StartMovingThread(int maxWidth, int maxHeight)
        {
            this.BallThread = new Thread(StartMoving);
            BallThread.Start();
        }

        public IDisposable Subscribe(IObserver<Ball> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
            return new Unsubscriber(observers, observer);
        }

        private class Unsubscriber : IDisposable
        {
            private IList<IObserver<Ball>> _observers;
            private IObserver<Ball> _observer;

            public Unsubscriber(IList<IObserver<Ball>> observers, IObserver<Ball> observer)
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