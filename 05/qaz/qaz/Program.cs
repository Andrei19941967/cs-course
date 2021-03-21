using System;

namespace qaz
{
    class Program
    {
       
            enum Shape
            {
                Circle = 1,
                Triangle,
                Rectangle
            }

            static void Main(string[] args)
            {
                Console.WriteLine("Enter number of shape(1 - Circle, 2 - Triangle, 3 - Rectangle): ");

                try
                {
                    Shape shape = (Shape)Enum.Parse(typeof(Shape), Console.ReadLine());
                    if (shape == Shape.Circle)
                    {
                        Console.WriteLine("Enter diametr: ");
                        int d = Convert.ToInt32(Console.ReadLine());
                        if (d == 0) throw new Exception("The value should not be equal to zero.");
                        if (d < 0) throw new Exception("The value should be more than zero.");

                        Console.WriteLine("P = " + (d * Math.PI));
                        Console.WriteLine("S = " + ((Math.PI * d * d) / 4));
                    }
                    else if (shape == Shape.Triangle)
                    {
                        Console.WriteLine("Enter size of side: ");
                        int s = Convert.ToInt32(Console.ReadLine());
                        if (s == 0) throw new Exception("The value should not be equal to zero.");
                        if (s < 0) throw new Exception("The value should be more than zero.");

                        Console.WriteLine("P = " + (3 * s));
                        Console.WriteLine("S = " + ((s * s * Math.Sqrt(3)) / 4));
                    }
                    else if (shape == Shape.Rectangle)
                    {
                        Console.WriteLine("Enter width: ");
                        int w = Convert.ToInt32(Console.ReadLine());
                        if (w == 0) throw new Exception("The value should not be equal to zero.");
                        if (w < 0) throw new Exception("The value should be more than zero.");

                        Console.WriteLine("Enter height: ");
                        int h = Convert.ToInt32(Console.ReadLine());
                        if (h == 0) throw new Exception("The value should not be equal to zero.");
                        if (h < 0) throw new Exception("The value should be more than zero.");

                        Console.WriteLine("P = " + (2 * w + 2 * h));
                        Console.WriteLine("S = " + (w * h));
                    }
                    else
                    {
                        throw new Exception("You have entered the number of a non-existen shape.");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("You entered wrong data!");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
        }
    }



