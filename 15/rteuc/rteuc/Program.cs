using System;
using System.IO;

namespace last
{
    delegate void PerfomendHandler(int progress);
    delegate void CompletedHandler();

    class FileWriterWithProgress
    {
        public event PerfomendHandler WritingPerformed;
        public event CompletedHandler WritingCompleted;


        public void WriteBytes(string fileName, byte[] data, float percentageToFireEvent)
        {
            StreamWriter w = new StreamWriter(fileName);

            for (int i = 0; i < data.Length; i++)
            {
                int b = (int)((i + 1) / (double)data.Length * 100.0);
                if (b % (int)(percentageToFireEvent * 100) == 0)
                {
                    WritingPerformed(b);
                }
            }
            WritingCompleted();
        }


    }


    class Program
    {

        static void WriteProgress(int prog)
        {
            Console.WriteLine(prog + "%");
        }
        static void WriteCompleted()
        {
            Console.WriteLine("Complete");
        }
        static void Main(string[] args)
        {
            FileWriterWithProgress f = new FileWriterWithProgress();
            f.WritingPerformed += WriteProgress;
            f.WritingCompleted += WriteCompleted;

            byte[] data = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            f.WriteBytes("file.txt", data, (float)0.1);

        }
    }
}
