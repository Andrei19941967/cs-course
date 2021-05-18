using System;

namespace Square.Figure
{
    public sealed class Square
    {
        private double _side;
        public Square(double side)
        {
            _side = side;
        }
        public double Calculate(Func<double, double> operation)
        {
            return operation(_side);
        }
    }
}
