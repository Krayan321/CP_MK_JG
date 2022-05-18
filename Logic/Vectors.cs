using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public struct Vectors
    {
        public static readonly Vectors Zero = new Vectors(0, 0);
        public static readonly Vectors One = new Vectors(1, 1);
        public static readonly Vectors Up = new Vectors(0f, 1f);
        public static readonly Vectors Down = new Vectors(0f, -1f);
        public static readonly Vectors Left = new Vectors(-1f, 0f);
        public static readonly Vectors Right = new Vectors(1f, 0f);

        public float X { get; set; }
        public float Y { get; set; }

        public Vectors(double x, double y)
        {
            X = (float)x;
            Y = (float)y;
        }

        public static float Distance(Vectors point, Vectors point1)
        {
            return (float)Math.Sqrt(DistanceSquared(point, point1));
        }

        public static float DistanceSquared(Vectors point, Vectors point1)
        {
            float xDifference = point.X - point1.X;
            float yDifference = point.Y - point1.Y;
            return xDifference * xDifference + yDifference * yDifference;
        }

        public bool IsZero()
        {
            return Equals(Zero);
        }

        

       
    }
}
