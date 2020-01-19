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
        private readonly ConsoleLogger consoleLogger;

        private readonly EmailLogger emailLogger;

        public LogManager()
        {
            // TODO [GM]: Look this up
            // AppDomain.CurrentDomain.BaseDirectory return a different directory depending on the project type
            // exe => bin/debug
            // iis hoster => project root
            var config = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory.ToString() + "/logSettings.json");

            var loggerSettings = JObject.Parse(config);

            var consoleLoggerConfig = loggerSettings["consoleLogger"].ToObject<ConsoleLoggerConfig>();
            var emailLoggerConfig = loggerSettings["emailLogger"].ToObject<EmailLoggerConfig>();

            this.consoleLogger = new ConsoleLogger(consoleLoggerConfig);
            this.emailLogger = new EmailLogger(emailLoggerConfig);
        }

        internal ConsoleLogger ConsoleLogger
        {
            get
            {
                if (this.consoleLogger == null) { throw new SimpleLoggerException("Console logger is not found."); }

                return this.consoleLogger;
            }
        }

        internal EmailLogger EmailLogger
        {
            get
            {
                if (this.emailLogger == null) { throw new SimpleLoggerException("Email logger is not found."); }

                return this.emailLogger;
            }
        }

        public void ConsoleLog(string message) => this.ConsoleLogger.LogMessage(message);

        public void ConsoleLog(Exception ex) => this.consoleLogger.LogException(ex, additionalMessage: null);

        public void EmailLog(string message) => this.EmailLogger.LogMessage(message);

        public void EmailLog(Exception ex) => this.EmailLogger.LogException(ex);

        public void GlobalLog(string message)
        {
            if (this.ConsoleLogger != null) { this.ConsoleLogger.LogMessage(message); }
            if (this.EmailLogger != null) { this.EmailLogger.LogMessage(message); }
        }

        public void GlobalExceptionLog(Exception ex, string additionalMessage = null)
        {
            if (this.ConsoleLogger != null) { this.ConsoleLogger.LogException(ex, additionalMessage); }
            if (this.EmailLogger != null) { this.EmailLogger.LogException(ex, additionalMessage); }
        }
    }
}