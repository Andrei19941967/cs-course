using System;

namespace _6work2
{
    class Program
    {
        static void Main(string[] args)
        {
            double a;
            double p;
            int s;
            while (true)
            {
                try
                {
                    Console.WriteLine("Введите сумму первоначального взноса в рублях: ");
                    a = Convert.ToDouble(Console.ReadLine());
                    if (a < 0)
                    {
                        Console.WriteLine("Введено неверное значение. Попробуйте еще раз: ");
                        continue;
                    }
                    Console.WriteLine("Введите ежедневный процент: ");
                    p = Convert.ToDouble(Console.ReadLine());
                    if (p < 0)
                    {
                        Console.WriteLine("Введено неверное значение. Попробуйте еще раз: ");
                        continue;
                    }
                    Console.WriteLine("Введите желаемую сумму: ");
                    s = Convert.ToInt32(Console.ReadLine());
                    if (s < a)
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
            while (a < s)
            {
                a += (a * p);
                count++;
            }

            Console.WriteLine(count);

        }
    }
}