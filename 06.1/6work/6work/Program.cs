using System;

namespace _6work
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите натуральное число не более 2 миллиардов: ");
            int a;
            while (true)
            {
                try
                {
                    a = Convert.ToInt32(Console.ReadLine());
                    if (a < 0 || a > 200000000)
                    {
                        Console.WriteLine("Введено неверное значение. Попробуйте еще раз: ");
                        continue;
                    }
                }
                catch (OverflowException e)
                {
                    Console.WriteLine(e.Message + ". " + "Попробуйте еще раз: ");
                    continue;
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e.Message + ". " + "Попробуйте еще раз: ");
                    continue;
                }
                break;
            }

            int count = 0;
            while (a != 0)
            {
                int last = a % 10;
                if (last % 2 == 0) count++;

                a /= 10;
            }

            Console.WriteLine("В веденном числе " + count + " четных чисел ");

        }
    }
}
