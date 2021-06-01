using System;

namespace ILogger
{
    public class ConsoleLogger : ILogger
    {
        private static ConsoleLogger _instance;

        public static ConsoleLogger GetInstance()
        {
            if (_instance == null) return (_instance = new ConsoleLogger());
            else return _instance;
        }
        
        private ConsoleLogger(){}
        
        public void Warning(string message)
        {
            Console.WriteLine($"Warning: {message}");
        }

        public void Error(string message)
        {
            Console.WriteLine($"Error: {message}");
        }

        public void Info(string message)
        {
            Console.WriteLine($"Info: {message}");
        }
    }
}