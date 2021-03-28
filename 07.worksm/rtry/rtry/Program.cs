using System;

namespace rtry
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter two real numbers: ");
            double n1 = Convert.ToDouble(Console.ReadLine());
            double n2 = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine(n1 + " * " + n2 + " = " + (n1 * n2));
            Console.WriteLine(String.Format("{0} + {1} = {2}", n1, n2, n2 + n1));
            Console.WriteLine($"{n1} - {n2} = {n1 - n2}");
        }
    }
}