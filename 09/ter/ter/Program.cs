using System;
using System.Diagnostics;

namespace ter
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = Convert.ToInt32(Console.ReadLine());
            int m = 0;
            Stopwatch w = new Stopwatch();
            w.Start();
            for( int i =1;i<= Math.Sqrt(n);i++)
            {
                if(n % i == 0)
                {
                    if (n / i != i) m += 2;
                    else m++;
                }
            }
            w.Stop();
            Console.WriteLine(m);
            Console.WriteLine(w.ElapsedMilliseconds);
        }
    }
}
