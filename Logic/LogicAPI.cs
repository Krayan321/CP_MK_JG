using Data;
using System;
using System.Collections.Generic;

namespace Logic
{
    public abstract class LogicAPI : IObservable<int>, IObserver<Ball>
    {
        public Board Board { get; set; }

        public abstract void MoveBalls(bool Start);

        public abstract int AddBall();

        public abstract void RandomizePositions(int maxWidth, int maxHeight);

        public abstract float[] GetBallPosition(int id);

        public abstract int GetBallRadius(int id);

        public abstract int GetBallsCount();

        public abstract void OnCompleted();

        public abstract void OnError(Exception error);

        public abstract void OnNext(Ball Ball);

        public int[] Size { get; set; }

        public abstract IDisposable Subscribe(IObserver<int> observer);

        public static LogicAPI CreateLayer(DataAPI data = default)
        {
            return new BusinessLogic(data ?? DataAPI.CreateDataBase(750, 350));
        }

        public class BusinessLogic : LogicAPI
        {
            private readonly DataAPI Data;
            private IDisposable unsubscriber;
            private IList<IObserver<int>> observers;
            private Logger logger;

            public BusinessLogic(DataAPI dataLayerAPI)
            {
                Data = dataLayerAPI;
                Subscribe(Data);
                observers = new List<IObserver<int>>();
                logger = new Logger();
                logger.StartLogging();
                Size = Data.GetSize();
            }

            public override void MoveBalls(bool start)
            {
                for (int i = 0; i < Data.GetBallsCount(); i++)
                {
                    Data.StartMovingBall(i, start);
                }
            }

            private void CheckBorders(int id)
            {
                float Position_X = Data.GetBallPositionX(id);
                float Position_Y = Data.GetBallPositionY(id);
                float[] Movement = Data.GetBallMovement(id);
                int Radius = Data.GetBallRadius(id);

                if (Position_X + Radius * 2 + Movement[0] > Size[0] || Position_X + Movement[0] < 0)
                    SwitchDirections(id, true);
                if (Position_Y + Radius * 2 + Movement[1] > Size[1] || Position_Y + Movement[1] < 0)
                    SwitchDirections(id, false);

                Data.CorrectBallPosition(id, Size[0], Size[1]);
            }

            private void CheckCollisions(int id)
            {
                for (int i = 0; i < Data.GetBallsCount(); i++)
                {
                    if (i == id) continue;

                    double distance = Math.Sqrt(Math.Pow((Data.GetBallPositionX(id) + Data.GetBallMovement(id)[0]) - (Data.GetBallPositionX(i) + Data.GetBallMovement(i)[0]), 2)
                                              + Math.Pow((Data.GetBallPositionY(id) + Data.GetBallMovement(id)[1]) - (Data.GetBallPositionY(i) + Data.GetBallMovement(i)[1]), 2));

                    if (Math.Abs(distance) <= Data.GetBallRadius(id) + Data.GetBallRadius(i))
                    {
                        float[] newMovement = ImpulseSpeed(id, i);
                        Data.SetBallMovements(id, new float[2] { newMovement[0], newMovement[1] });
                        Data.SetBallMovements(i, new float[2] { newMovement[2], newMovement[3] });
                    }
                }
            }

            public override int AddBall()
            {
                return Data.AddBall();
            }

            public void SwitchDirections(int id, bool direction_X)
            {
                Data.SwitchBallDirection(id, direction_X);
                NotifyObservers(id);
            }

            public override void RandomizePositions(int maxWidth, int maxHeight)
            {
                Data.RandomizePositions();
            }

            public override float[] GetBallPosition(int id)
            {
                return new float[2] { Data.GetBallPositionX(id), Data.GetBallPositionY(id) };
            }

            public override int GetBallsCount()
            {
                return Data.GetBallsCount();
            }

            public override int GetBallRadius(int id)
            {
                return Data.GetBallRadius(id);
            }

            public BusinessLogic(DataAPI dataLayerAPI, bool sub)
            {
                Data = dataLayerAPI;
                observers = new List<IObserver<int>>();
                Size = Data.GetSize();
            }

            #region observer

            public virtual void Subscribe(IObservable<Ball> provider)
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

            public override void OnNext(Ball Ball)
            {
                logger.AddLog(Data.GetBallLog(Ball.Id));
                CheckBorders(Ball.Id);
                CheckCollisions(Ball.Id);
                NotifyObservers(Ball.Id);
            }

            public void NotifyObservers(int id)
            {
                foreach (var observer in observers)
                {
                    if (observer != null)
                    {
                        observer.OnNext(id);
                    }
                }
                System.Threading.Thread.Sleep(1);
            }

            public float[] ImpulseSpeed(int id, int id2)
            {
                float mass = Data.GetBallMass(id);
                float otherMass = Data.GetBallMass(id2);

                float[] velocity = new float[2] { Data.GetBallMovement(id)[0], Data.GetBallMovement(id)[1] };
                float[] position = new float[2] { Data.GetBallPositionX(id), Data.GetBallPositionY(id) };

                float[] velocityOther = new float[2] { Data.GetBallMovement(id2)[0], Data.GetBallMovement(id2)[1] };
                float[] positionOther = new float[2] { Data.GetBallPositionX(id2), Data.GetBallPositionY(id2) };

                float fDistance = (float)Math.Sqrt((position[0] - positionOther[0]) * (position[0] - positionOther[0]) + (position[1] - positionOther[1]) * (position[1] - positionOther[1]));

                float nx = (positionOther[0] - position[0]) / fDistance;
                float ny = (positionOther[1] - position[1]) / fDistance;

                float tx = -ny;
                float ty = nx;

                float dpTan1 = velocity[0] * tx + velocity[1] * ty;
                float dpTan2 = velocityOther[0] * tx + velocityOther[1] * ty;

                float dpNorm1 = velocity[0] * nx + velocity[1] * ny;
                float dpNorm2 = velocityOther[0] * nx + velocityOther[1] * ny;

                float m1 = (dpNorm1 * (mass - otherMass) + 2.0f * otherMass * dpNorm2) / (mass + otherMass);
                float m2 = (dpNorm2 * (otherMass - mass) + 2.0f * mass * dpNorm1) / (mass + otherMass);

                float[] newMovements = new float[4]
                {
                    tx * dpTan1 + nx * m1,
                    ty * dpTan1 + ny * m1,
                    tx * dpTan2 + nx * m2,
                    ty * dpTan2 + ny * m2
                };

                return newMovements;
            }

            #endregion observer

            #region provider

            public override IDisposable Subscribe(IObserver<int> observer)
            {
                if (!observers.Contains(observer))
                    observers.Add(observer);
                return new Unsubscriber(observers, observer);
            }

            private class Unsubscriber : IDisposable
            {
                private IList<IObserver<int>> _observers;
                private IObserver<int> _observer;

                public Unsubscriber
                (IList<IObserver<int>> observers, IObserver<int> observer)
                {
                    _observers = observers;
                    _observer = observer;
                }

                public void Dispose()
                {
                    if (_observer != null && _observers.Contains(_observer))
                        _observers.Remove(_observer);
                }
            }

            #endregion provider
        }
    }
}