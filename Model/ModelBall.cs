using System;
using Logic;

namespace Model
{
    public class ModelBall
    {
        private readonly Ball logicBall;
        public enum Color
        {
            Khaki,
            LawnGreen,
            HotPink,
            SteelBlue,
            SpringGreen,
            Yellow,
            YellowGreen,
            Tomato,
            Red,
            Ivory,
            Crimson,
            Chartreuse,
            Coral,
            Chocolate,
            DarkOliveGreen,
            DarkGreen,
            LightSeaGreen,
            Lime,
            LimeGreen,
            Linen,
            Magenta,
            Maroon,
            Orchid,
            Lavender
        }
        public string BallColor { get; set; }

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
            get { return logicBall.Radius * 2; }
        }

        public ModelBall(Ball logicBall, string Color = "Green")
        {
            this.BallColor = Color.ToString();
            this.logicBall = logicBall;
        }
    }
}
