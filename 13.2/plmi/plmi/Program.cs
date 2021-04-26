using System;
using System.IO;
using System.Collections.Generic;
namespace abstractProject
{
    public interface ILogWriter
    {
        void LogInfo(string message);

        void LogWarning(string message);

        void LogError(string message);
    }


    class FileLogWriter : ILogWriter
    {
        private string FileName { get; set; }
        public FileLogWriter(string fileName)
        {
            FileName = fileName;
        }
        public void LogError(string message)
        {
            StreamWriter w = new StreamWriter(FileName, true);
            DateTime dateTime = DateTime.Now;
            w.WriteLine(dateTime.ToString() + "\t" + "Error" + "\t" + message);
            w.Close();

        }

        public void LogInfo(string message)
        {
            StreamWriter w = new StreamWriter(FileName, true);
            DateTime dateTime = DateTime.Now;
            w.WriteLine(dateTime.ToString() + "\t" + "Info" + "\t" + message);
            w.Close();
        }

        public void LogWarning(string message)
        {
            StreamWriter w = new StreamWriter(FileName, true);
            DateTime dateTime = DateTime.Now;
            w.WriteLine(dateTime.ToString() + "\t" + "Warning" + "\t" + message);
            w.Close();
        }
    }

    class ConsoleLogWriter : ILogWriter
    {
        public void LogError(string message)
        {
            DateTime dateTime = DateTime.Now;
            Console.WriteLine(dateTime.ToString() + "\t" + "Error" + "\t" + message);
        }

        public void LogInfo(string message)
        {
            DateTime dateTime = DateTime.Now;
            Console.WriteLine(dateTime.ToString() + "\t" + "Info" + "\t" + message);
        }

        public void LogWarning(string message)
        {
            DateTime dateTime = DateTime.Now;
            Console.WriteLine(dateTime.ToString() + "\t" + "Warning" + "\t" + message);
        }
    }

    class MultipleLogWriter : ILogWriter
    {
        List<ILogWriter> LogWriters { get; set; }
        public MultipleLogWriter(List<ILogWriter> logWriters)
        {
            LogWriters = logWriters;
        }

        public void LogError(string message)
        {
            foreach (var logger in LogWriters)
            {
                logger.LogError(message);
            }
        }

        public void LogInfo(string message)
        {
            foreach (var logger in LogWriters)
            {
                logger.LogInfo(message);
            }
        }

        public void LogWarning(string message)
        {
            foreach (var logger in LogWriters)
            {
                logger.LogWarning(message);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            FileLogWriter logFile = new FileLogWriter("log.txt");
            ConsoleLogWriter logConsole = new ConsoleLogWriter();
            MultipleLogWriter logMulti = new MultipleLogWriter(new List<ILogWriter>() { logConsole, logFile });

            logMulti.LogError("Hello");
            logMulti.LogInfo("World");
            logMulti.LogWarning("My name is zaur");
        }
    }
}
