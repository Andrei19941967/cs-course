using System;
using Calculator.Figure;
using Square.Figure;
using Square.Calculate;
using Calculator.Operation;

namespace testclass
{
    class Program
    {
        static void Main(string[] args)
        {
            Calculator.Figure.Circle circle = new Circle(4);
            Console.WriteLine("Circle perimetr: " + circle.Calculate(Calculator.Operation.CircleOperations.Perimeter));
            Console.WriteLine("Circle square: " + circle.Calculate(Calculator.Operation.CircleOperations.Square));

            Square.Figure.Square square = new Square.Figure.Square(4);
            Console.WriteLine("Square perimetr: " + square.Calculate(Square.Calculate.SquareOperations.Perimeter));
            Console.WriteLine("Square square: " + square.Calculate(Square.Calculate.SquareOperations.Square));

        }
    }
}
