﻿using System;
using Logic;

namespace ViewModel
{
    public class ViewBall
    {
        private readonly Ball logicBall;

        public float Position_X;
        public float Position_Y;
        public float[] Direction;
        public float Speed;
        public float Radius;

        public ViewBall(Ball logicBall)
        {
            this.logicBall = logicBall;

            Update();
        }

        public void Update()
        {
            Position_X = logicBall.Position_X;
            Position_Y = logicBall.Position_Y;
            Direction = logicBall.Direction;
            Speed = logicBall.Speed;
            Radius = logicBall.Radius;
        }
    }
}
