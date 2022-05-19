using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Logic;

namespace Model
{
    public class ModelBall : IBall
    {
        //private readonly Ball logicBall;
        public int Id { get; set; }
        public int Diameter { get; }
        public event PropertyChangedEventHandler PropertyChanged;
        private double left;
        private double top;
        public double Top
        {
            get { return top; }
            set
            {
                if (top == value) return;
                top = value;
                RaisePropertyChanged();
            }
        }
        
        public double Left 
        { 
            get { return left; } 
            set 
            {
                if (left == value) return;
                left = value; 
                RaisePropertyChanged();
            } 
        }
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

        public ModelBall(int id, double left, double top, int radius, string Color = "Green")
        {
            Id = id;
            this.BallColor = Color.ToString();
            Top = top;
            Left = left;
            Diameter = radius * 2;
        }

        public void Move(double poitionX, double positionY)
        {
            Left = poitionX;
            Top = positionY;
        }

        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
