using System;

namespace ptr
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Input string: ");

            string s = "";
            while (true)
            {
                try
                {
                    s = Console.ReadLine();
                    if (string.IsNullOrEmpty(s))
                    {
                        Console.WriteLine("Empty string. Input again: ");
                        continue;
                    }
                }
                catch (OutOfMemoryException e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Input again: ");
                    continue;
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Input again: ");
                    continue;
                }
                break;
            }

            string[] s_split = s.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < s_split.Length; i++)
            {
                s_split[i] = s_split[i].ToLower();
            }

            s = string.Join(' ', s_split);
            for (int i = s.Length - 1; i >= 0; i--) Console.Write(s[i]);
        }
    }
}

