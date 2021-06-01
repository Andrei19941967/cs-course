namespace ILogger
{
    
    public interface ILogger
    {
        public void Warning(string message);
        public void Error(string message);
        public void Info(string message);
    }
}