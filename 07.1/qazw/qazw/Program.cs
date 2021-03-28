using System;

namespace qazw
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
                        if (s.Split().Length < 2)
                        {
                            Console.WriteLine("Too few words. Input again: ");
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
                int count = 0;
                for (int i = 0; i < s_split.Length; i++)
                {
                    if (s_split[i][0] == 'A' || s_split[i][0] == 'a' || s_split[i][0] == 'А' || s_split[i][0] == 'а')
                    {
                        count++;
                    }
                }

                Console.WriteLine(count);
            }
        }
    }