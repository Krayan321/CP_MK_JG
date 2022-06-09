using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public interface BallInterface : IObservable<BallInterface>
    {
        float Position_X { get; set; }
        float Position_Y { get; set; }
        float[] Movement { get; set; }
        float Speed { get; set; }
        int Radius { get; }
        int Id { get; }
        float Mass { get; }
    }
}
