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
            if(!Enum.IsDefined(typeof(ConsoleColor), (int)config.TextColor))
            {
                throw new ArgumentException($"SimpleLogger doens't support this color type --{config.TextColor.ToString()}.");
            }

            this.Color = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), config.TextColor.ToString());
        }

        internal ConsoleColor Color { get; private set; }

        public void LogException(Exception ex, string additionalMessage) => ConsoleHelper.Log("Exception message: " + ex.Message + " --" + additionalMessage, ConsoleColor.Red);
        
        public void LogMessage(string message) => ConsoleHelper.Log(message, this.Color);
    }
}