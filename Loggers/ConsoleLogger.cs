using SimpleLogger.Models.Console.Configuration;
using SimpleLogger.Models.Contracts;
using System;

namespace SimpleLogger.Loggers
{
    public sealed class ConsoleLogger : ICustomLogger
    {
        internal ConsoleLogger(ConsoleLoggerConfig config)
        {
            this.Color = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), config.TextColor.ToString());
        }

        internal ConsoleColor Color { get; private set; }

        public void Log(System.Exception ex)
        {
            System.Console.WriteLine(ex.Message);
        }
    }
}