using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public struct BallLogData
    {
        public int Id { get; }
        public float Position_X { get; }
        public float Position_Y { get; }
        public float[] Movement { get; }
        public float Speed { get; }
        
        public BallLogData(int id, float x, float y, float[] movement, float speed)
        {
            Id = id; 
            Position_X = x; 
            Position_Y = y; 
            Movement = movement; 
            Speed = speed; 
        }
    }
}
