using SimpleLogger.Helpers;
using SimpleLogger.Models.Console.Configuration;
using SimpleLogger.Models.Contracts;
using System;

namespace SimpleLogger.Loggers
{
    internal sealed class ConsoleLogger : ICustomLogger
    {
        internal ConsoleLogger(ConsoleLoggerConfig config)
        {
            this.Color = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), config.TextColor.ToString());
        }

        internal ConsoleColor Color { get; private set; }

        public void LogException(Exception ex)
        {
            ConsoleHelper.Log(ex.Message, ConsoleColor.Red);
        }

        public void LogMessage(string message)
        {
            ConsoleHelper.Log(message, this.Color);
        }
    }
}