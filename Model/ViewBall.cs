using System;
using Logic;

namespace ViewModel
{
    public class ViewBall
    {
        private readonly Ball logicBall;

        public float Position_X
        {
            get { return logicBall.Position_X; }
        }
        public float Position_Y
        {
            get { return logicBall.Position_Y; }
        }
        public float[] Direction
        {
            get { return logicBall.Direction; }
        }
        public float Speed
        {
            get { return logicBall.Speed; }
        }
        public int Radius
        {
            get { return logicBall.Radius; }
        }

        public ViewBall(Ball logicBall)
        {
            this.logicBall = logicBall;
        }




    }
}
