using Newtonsoft.Json.Linq;
using SimpleLogger.Contracts;
using SimpleLogger.Exceptions;
using SimpleLogger.Loggers;
using SimpleLogger.Models.Console.Configuration;
using SimpleLogger.Models.Email.Configuration;
using System;
using System.IO;

namespace SimpleLogger
{
    public class LogManager : ILogManager
    {
        public LogManager()
        {
            try
            {
                var config = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory.ToString() + "/logSettings.json");

                var loggerSettings = JObject.Parse(config);

                var consoleLoggerConfig = loggerSettings["consoleLogger"].ToObject<ConsoleLoggerConfig>();
                var emailLoggerConfig = loggerSettings["emailLogger"].ToObject<EmailLoggerConfig>();

                this.ConsoleLogger = new ConsoleLogger(consoleLoggerConfig);
                this.EmailLogger = new EmailLogger(emailLoggerConfig);
            }
            catch (SimpleLoggerException)
            {
                throw new SimpleLoggerException("Error logsettings.json not found.");
            }
        }

        internal ConsoleLogger ConsoleLogger { get; private set; }

        internal EmailLogger EmailLogger { get; private set; }

        public void Log(System.Exception ex)
        {
            if (this.ConsoleLogger != null) { this.ConsoleLogger.Log(ex); }
            if (this.EmailLogger != null) { this.EmailLogger.Log(ex); }
        }
    }
}