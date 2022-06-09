using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicTests
{
    internal class TestDataAPI : DataAPI
    {
        public Board board { get; set; }
        IList<IObserver<Ball>> observers;
        public TestDataAPI()
        {
            board = new Board(100, 100);
        }
        public override int AddBall()
        {
            board.AddBall();
            return board.Balls.Count - 1;
        }

        public override float GetBallMass(int id)
        {
            throw new NotImplementedException();
        }

        public override float[] GetBallMovement(int id)
        {
            throw new NotImplementedException();
        }

        public override float GetBallPositionX(int id)
        {
            throw new NotImplementedException();
        }

        public override float GetBallPositionY(int id)
        {
            throw new NotImplementedException();
        }

        public override int GetBallRadius(int id)
        {
            throw new NotImplementedException();
        }

        public override int GetBallsCount()
        {
            return board.Balls.Count;
        }

        public override float GetBallSpeed(int id)
        {
            throw new NotImplementedException();
        }

        public override int[] GetSize()
        {
            return new int[2] { board.Size[0], board.Size[1] };
        }

        public override void OnCompleted()
        {
            return;
        }

        public override void OnError(Exception error)
        {
            return;
        }

        public override void OnNext(Ball Ball)
        {
            return;
        }

        public override void RandomizePositions()
        {
            throw new NotImplementedException();
        }

        public override void SetBallMovement(int id, float direction, bool direction_x)
        {
            throw new NotImplementedException();
        }

        public override void SetBallMovements(int id, float[] direction)
        {
            throw new NotImplementedException();
        }

        public override void SetBallSpeed(int id, float speed)
        {
            throw new NotImplementedException();
        }

        public override void StartMovingBall(int id, bool start)
        {
            throw new NotImplementedException();
        }

        public override IDisposable Subscribe(IObserver<Ball> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
            return new Unsubscriber(observers, observer);
        }

        private class Unsubscriber : IDisposable
        {
            private IList<IObserver<Ball>> _observers;
            private IObserver<Ball> _observer;

            public Unsubscriber
            (IList<IObserver<Ball>> observers, IObserver<Ball> observer)
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

        public override void SwitchBallDirection(int id, bool direction_x)
        {
            throw new NotImplementedException();
        }

        public override string GetBallLog(int id)
        {
            throw new NotImplementedException();
        }

        public override float GetBallTime(int id)
        {
            throw new NotImplementedException();
        }
    }
}
