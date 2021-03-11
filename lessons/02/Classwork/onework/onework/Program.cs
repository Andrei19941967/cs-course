using System;

namespace onework
{
    class Program
    {
        static void Main(string[] args)
        {

            
            
             int a = 1;
            int b = 3;

            a = Convert.ToInt32(Console.ReadLine());
            b = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(a + b);
            Console.WriteLine(a - b);
            Console.WriteLine(a * b);
            Console.WriteLine(a / b);


            int q = Convert.ToInt32(Console.ReadLine());
            string s = Console.ReadLine();
            int w = Convert.ToInt32(Console.ReadLine());
            if (s == "%")
            {
                Console.WriteLine(q % w);


            }
            if (s == "+")
            {
                Console.WriteLine(q + w);
            }
            if (s == "-")
            {
                Console.WriteLine(q - w);
            }
            if (s == "/")
            {
                Console.WriteLine(q / w);
            }
            if (s == "*")
            {
                Console.WriteLine(q * w);
            }

            else 
            {
                int pas = Convert.ToInt32(Console.ReadLine());
                s= Math.Pow(w, pas);
            }


        
        }
    }
}

