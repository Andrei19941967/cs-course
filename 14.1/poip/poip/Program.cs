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

    abstract class AbstractLogWriter : ILogWriter
    {
        enum LogType
        {
            INFO,
            ERROR,
            WARNING
        }

        protected string FileName;
        protected StreamWriter w;
        protected DateTime dateTime;
        public AbstractLogWriter()
        {

        }

        public AbstractLogWriter(string filename)
        {
            FileName = filename;
        }


        protected void Ready()
        {
            dateTime = DateTime.Now;
            if (!String.IsNullOrEmpty(FileName)) w = new StreamWriter(FileName, true);
        }

        public abstract void LogError(string message);
        public abstract void LogInfo(string message);
        public abstract void LogWarning(string message);

    }

    class FileLogWriter : AbstractLogWriter
    {

        private static FileLogWriter instance;

        private FileLogWriter(string fileName) : base(fileName)
        {

        }

        public static FileLogWriter GetInstance()
        {
            return instance ?? (instance = new FileLogWriter("log.txt"));
        }

        public override void LogError(string message)
        {
            Ready();
            w.WriteLine(dateTime.ToString() + "\t" + "Error" + "\t" + message);
            w.Close();

        }

        public override void LogInfo(string message)
        {
            Ready();
            w.WriteLine(dateTime.ToString() + "\t" + "Info" + "\t" + message);
            w.Close();
        }

        public override void LogWarning(string message)
        {
            Ready();
            w.WriteLine(dateTime.ToString() + "\t" + "Warning" + "\t" + message);
            w.Close();
        }
    }

    class ConsoleLogWriter : AbstractLogWriter
    {

        private static ConsoleLogWriter instance;

        private ConsoleLogWriter() { }

        public static ConsoleLogWriter GetInstance()
        {
            return instance ?? (instance = new ConsoleLogWriter());
        }


        public override void LogError(string message)
        {
            Ready();
            Console.WriteLine(dateTime.ToString() + "\t" + "Error" + "\t" + message);
        }

        public override void LogInfo(string message)
        {
            Ready();
            Console.WriteLine(dateTime.ToString() + "\t" + "Info" + "\t" + message);
        }

        public override void LogWarning(string message)
        {
            Ready();
            Console.WriteLine(dateTime.ToString() + "\t" + "Warning" + "\t" + message);
        }
    }

    class MultipleLogWriter : AbstractLogWriter
    {

        List<ILogWriter> LogWriters { get; set; }
        public MultipleLogWriter(List<ILogWriter> logWriters) : base()
        {
            LogWriters = logWriters;
        }


        public override void LogError(string message)
        {
            foreach (var logger in LogWriters)
            {
                logger.LogError(message);
            }
        }

        public override void LogInfo(string message)
        {
            foreach (var logger in LogWriters)
            {
                logger.LogInfo(message);
            }
        }

        public override void LogWarning(string message)
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
            FileLogWriter logFile = FileLogWriter.GetInstance();
            ConsoleLogWriter logConsole = ConsoleLogWriter.GetInstance();

            MultipleLogWriter logMulti = new MultipleLogWriter(new List<ILogWriter>() { logConsole, logFile });

            logMulti.LogError("Hello");
            logMulti.LogInfo("World");
            logMulti.LogWarning("My name is Andrei");
        }
    }
}