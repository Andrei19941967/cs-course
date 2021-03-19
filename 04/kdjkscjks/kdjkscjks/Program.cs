using System;

namespace kdjkscjks
{
    class Program
    {
        [Flags]
        enum Colors
        {
            Black = 1,
            Blue = 2,
            Cyan = 4,
            Grey = 8,
            Green = 16,
            Magenta = 32,
            Red = 64,
            White = 128,
            Yellow = 256
        }

        static void Main(string[] args)
        {
            Colors[] cs = new Colors[4];

            //Ввод без цикла
            /*
             * cs[0] = (Colors)Enum.Parse(typeof(Colors), Console.ReadLine());
             * cs[1] = (Colors)Enum.Parse(typeof(Colors), Console.ReadLine());
             * cs[2] = (Colors)Enum.Parse(typeof(Colors), Console.ReadLine());
             * cs[3] = (Colors)Enum.Parse(typeof(Colors), Console.ReadLine());
             */
            //Ввод с циклом
            for (int i = 0; i < cs.Length; i++)
            {
                cs[i] = (Colors)Enum.Parse(typeof(Colors), Console.ReadLine());
            }


            Console.WriteLine(cs[0] + ", " + cs[1] + ", " + cs[2] + ", " + cs[3]);
            Console.WriteLine("----");

            Colors all = Colors.Black | Colors.Blue | Colors.Cyan | Colors.Grey | Colors.Green | Colors.Magenta | Colors.Red | Colors.White | Colors.Yellow;
            all ^= cs[0]; // all = all ^ cs[0]
            all ^= cs[1];
            all ^= cs[2];
            all ^= cs[3];
            Console.WriteLine(all);
        }
    }
}


        
        
    

