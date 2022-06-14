using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Data
{
    public abstract class DataAPI : IObserver<BallInterface>, IObservable<BallInterface>
    {
        public abstract int AddBall();

        public abstract int[] GetSize();

        public abstract void OnCompleted();

        public abstract void OnError(Exception error);

        public abstract void OnNext(BallInterface Ball);

        public abstract void SetBallMovement(int id, float direction, bool direction_x);

        public abstract void SetBallMovements(int id, float[] direction);

        public abstract void SwitchBallDirection(int id, bool direction_x);

        public abstract void SetBallSpeed(int id, float speed);

        public abstract float GetBallPositionX(int id);

        public abstract float GetBallTime(int id);

        public abstract float GetBallPositionY(int id);

        public abstract float GetBallSpeed(int id);

        public abstract float[] GetBallMovement(int id);

        public abstract int GetBallRadius(int id);

        public abstract float GetBallMass(int id);

        public abstract int GetBallsCount();

        public abstract void CorrectBallPosition(int id, int x, int y);

        public abstract void RandomizePositions();

        public abstract void StartMovingBall(int id, bool start);

        public abstract IDisposable Subscribe(IObserver<BallInterface> observer);

        public static DataAPI CreateDataBase(int width, int height)
        {
            return new DataBase(width, height);
        }
    }

    public class DataBase : DataAPI
    {
        private Board board;
        private IDisposable unsubscriber;
        private IList<IObserver<BallInterface>> observers;
        private Barrier barrier;
        private Logger logger;
        public bool IsMoving { get; set; } = false;

        public DataBase(int width, int height)
        {
            this.board = new Board(width, height);
            observers = new List<IObserver<BallInterface>>();
            barrier = new Barrier(0);
            logger = new Logger();
            logger.StartLogging();
        }

        private Ball GetBall(int id)
        {
            return board.GetBall(id);
        }

        public override int GetBallsCount()
        {
            return board.Balls.Count;
        }

        public override int GetBallRadius(int id)
        {
            return GetBall(id).Radius;
        }

        public override float[] GetBallMovement(int id)
        {
            return GetBall(id).Movement;
        }

        public override float GetBallTime(int id)
        {
            return GetBall(id).GetTime();
        }

        public override float GetBallSpeed(int id)
        {
            return GetBall(id).Speed;
        }

        public override float GetBallMass(int id)
        {
            return GetBall(id).Mass;
        }

        public override void StartMovingBall(int id, bool start)
        {
            IsMoving = true;
            GetBall(id).StartMovingThread();
        }

        public override void SetBallMovement(int id, float direction, bool direction_x)
        {
            if (direction_x)
                GetBall(id).Movement[0] = direction;
            else
                GetBall(id).Movement[1] = direction;
        }

        public override void SetBallMovements(int id, float[] movement)
        {
            GetBall(id).Movement[0] = movement[0];
            GetBall(id).Movement[1] = movement[1];
        }

        public override void SwitchBallDirection(int id, bool direction_x)
        {
            if (direction_x)
                SetBallMovement(id, GetBall(id).Movement[0] * (-1), direction_x);
            else
                SetBallMovement(id, GetBall(id).Movement[1] * (-1), direction_x);
        }

        public override void SetBallSpeed(int id, float speed)
        {
            GetBall(id).Speed = speed;
        }

        public override void RandomizePositions()
        {
            foreach (Ball ball in GetBalls())
            {
                ball.RandomizePosition(GetSize()[0], GetSize()[1]);
            }
        }

        public override float GetBallPositionX(int id)
        {
            return GetBall(id).Position_X;
        }

        public override float GetBallPositionY(int id)
        {
            return GetBall(id).Position_Y;
        }

        private List<Ball> GetBalls()
        {
            return board.Balls;
        }

        public override int AddBall()
        {
            barrier.AddParticipant();
            int i = board.AddBall();
            Subscribe(GetBall(i));
            return i;
        }

        public override int[] GetSize()
        {
            return board.Size;
        }

        #region Observer

        public override void OnCompleted()
        {
            unsubscriber.Dispose();
        }

        public override void OnError(Exception error)
        {
            throw error;
        }

        public override void OnNext(BallInterface Ball)
        {
            if (IsMoving)
                barrier.SignalAndWait();

            logger.AddLog(GetBall(Ball.Id).GetLogData());

            foreach (IObserver<BallInterface> observer in observers)
            {
                observer.OnNext(Ball);
            }
        }

        public virtual void Subscribe(IObservable<BallInterface> provider)
        {
            if (provider != null)
                unsubscriber = provider.Subscribe(this);
        }

        #endregion Observer

        #region provider

        public override IDisposable Subscribe(IObserver<BallInterface> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
            return new Unsubscriber(observers, observer);
        }

        public override void CorrectBallPosition(int id, int x, int y)
        {
            GetBall(id).CorrectPositions(x, y);
        }

        private class Unsubscriber : IDisposable
        {
            private IList<IObserver<BallInterface>> _observers;
            private IObserver<BallInterface> _observer;

            public Unsubscriber
            (IList<IObserver<BallInterface>> observers, IObserver<BallInterface> observer)
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