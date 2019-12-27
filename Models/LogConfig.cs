using SimpleLogger.Models.Console.Configuration;
using SimpleLogger.Models.Email.Configuration;

namespace SimpleLogger.Models
{
    internal class LogConfig
    {
        internal ConsoleLoggerConfig ConsoleLoggerConfig { get; set; }

        internal EmailLoggerConfig EmailLoggerConfig { get; set; }
    }
}