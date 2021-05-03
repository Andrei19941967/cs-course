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


        public void LogError(string message)
        {
            DateTime dateTime = DateTime.Now;
            StreamWriter w = new StreamWriter("log.txt", true);
            w.WriteLine(dateTime.ToString() + "\t" + "Error" + "\t" + message);
            w.Close();

        }

        public void LogInfo(string message)
        {
            DateTime dateTime = DateTime.Now;
            StreamWriter w = new StreamWriter("log.txt", true);
            w.WriteLine(dateTime.ToString() + "\t" + "Info" + "\t" + message);
            w.Close();
        }

        public void LogWarning(string message)
        {
            DateTime dateTime = DateTime.Now;
            StreamWriter w = new StreamWriter("log.txt", true);
            w.WriteLine(dateTime.ToString() + "\t" + "Warning" + "\t" + message);
            w.Close();
        }
    }

    class ConsoleLogWriter : ILogWriter
    {
        public ConsoleLogWriter() { }


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
        public MultipleLogWriter(List<ILogWriter> logWriters) : base()
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

    class LogWriterFactory
    {
        static ConsoleLogWriter instanceConsole;
        static FileLogWriter instanceFile;
        static MultipleLogWriter instanceMultiple;

        public ILogWriter GetLogWriter<T>() where T : ILogWriter
        {
            if (typeof(T) == typeof(ConsoleLogWriter))
            {
                return instanceConsole ?? (instanceConsole = new ConsoleLogWriter());
            }
            else if (typeof(T) == typeof(FileLogWriter))
            {
                return instanceFile ?? (instanceFile = new FileLogWriter());
            }
            else
            {
                return instanceMultiple ?? (instanceMultiple =
                    new MultipleLogWriter(new List<ILogWriter>() {
                        instanceConsole ?? (instanceConsole = new ConsoleLogWriter()),
                        instanceFile ?? (instanceFile = new FileLogWriter()) }));
            }

        }

    }


    class Program
    {
        static void Main(string[] args)
        {
            LogWriterFactory factory = new LogWriterFactory();
            FileLogWriter logFile = (FileLogWriter)factory.GetLogWriter<FileLogWriter>();
            ConsoleLogWriter logConsole = (ConsoleLogWriter)factory.GetLogWriter<ConsoleLogWriter>();

            MultipleLogWriter logMulti = (MultipleLogWriter)factory.GetLogWriter<MultipleLogWriter>();

            logFile.LogError("Hello");
            logConsole.LogInfo("World");
            logMulti.LogWarning("My name is Andrei");
        }
    }
}