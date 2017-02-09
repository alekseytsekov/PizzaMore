namespace PizzaMore.Utility
{
    using System.IO;

    public static class Logger
    {
        private const string LogFileName = "log.txt";

        public static void Log(string message)
        {
            File.AppendAllText(LogFileName, message);
        }
    }
}
