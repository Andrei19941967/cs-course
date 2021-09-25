using System;
using System.IO;

namespace yrok
{
     public class HybrisWeb
    {
        public int Age { get; set; }
        public string Stage { get; set; }
        public int voidr_;
        public int Vitrina { get; set; }
        public HybrisWeb(int age,string stage, int voidr, int vitrina)
        {
            Age = age;
            Stage = stage;
            Voidr = voidr;
            Vitrina = vitrina;
        }
        public int Voidr
        {
            get
            {
                return voidr_;
            }
            set
            {
                if (value < 91 || value >= 97) throw new Exception("error)");
                else voidr_ = value;
            }
        }
        public override string ToString()
        {
            string k = Age + " " + Stage + " " + Voidr + " " + Vitrina;
            return k;
        }


    }
    class Program
    {
        static void Main(string[] args)
        {
            HybrisWeb h = new HybrisWeb(9, "Kladr", 96, 29);
            Console.WriteLine(h.ToString());
            string input = Console.ReadLine();
            StreamWriter w = new StreamWriter("Hybris.txt");
            w.WriteLine(input);
            w.Close();
            StreamReader r = new StreamReader("Hybris.txt");
            string ss = r.ReadToEnd();
            Console.WriteLine(ss);
            r.Close();

            w.Close();
        }
    }
}
