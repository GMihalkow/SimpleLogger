using SimpleLogger.Models.Contracts;
using SimpleLogger.Models.Email.Configuration;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading;

namespace SimpleLogger.Loggers
{
    internal sealed class EmailLogger : ICustomLogger
    {
        private readonly string errorTemplate = 
            @"Date: {{date}}" + Environment.NewLine +
            "Thread: {{thread}}" + Environment.NewLine + 
            "Application: {{app}}" + Environment.NewLine +
            "Exception Type: {{exceptionType}}" + Environment.NewLine +
            "Exception Message: {{exceptionMessage}}" + Environment.NewLine +
            "Stack Trace: {{stackTrace}}" + Environment.NewLine +
            "Log Level: {{level}}" + Environment.NewLine +
            "Inner Exception Type: {{innerExceptionType}}" + Environment.NewLine +
            "Inner Exception Message: {{innerExceptionMessage}}" + Environment.NewLine + 
            "Inner Exception Stack Trace: {{innerExceptionStackTrace}}";

        private readonly EmailLoggerConfig config;

        private readonly SmtpClient smtpClient;

        public EmailLogger(EmailLoggerConfig config)
        {
            this.config = config;

            this.smtpClient = new SmtpClient()
            {
                Host = this.config.SmtpHost,
                Port = this.config.SmtpPort,
                EnableSsl = true,
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(this.config.Username, this.config.Password),
            };
        }

        public void LogException(Exception ex)
        {
            var result = this.GenerateMessage(ex);

            this.smtpClient.Send(new MailMessage(this.config.From, this.config.To, this.config.ApplicationName, result));
        }

        public void LogException(Exception ex, string additionalMessage)
        {
            var result = this.GenerateMessage(ex, additionalMessage);

            this.smtpClient.Send(new MailMessage(this.config.From, this.config.To, this.config.ApplicationName, result));
        }

        public void LogMessage(string message)
        {
            this.smtpClient.Send(new MailMessage(this.config.From, this.config.To, this.config.ApplicationName, message));
        }

        private string GenerateMessage(Exception ex, string additionalMessage = null)
        {
            var result = errorTemplate;

            result = result.Replace("{{date}}", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            result = result.Replace("{{thread}}", Thread.CurrentThread.ManagedThreadId.ToString());
            result = result.Replace("{{app}}", this.config.ApplicationName);
            result = result.Replace("{{exceptionType}}", ex.GetType().FullName);
            result = result.Replace("{{exceptionMessage}}", ex.Message);
            result = result.Replace("{{stackTrace}}", ex.StackTrace);
            result = result.Replace("{{level}}", this.config.Level.ToString());
            result = result.Replace("{{innerExceptionType}}", ex.InnerException?.GetType().Name ?? "None");
            result = result.Replace("{{innerExceptionMessage}}", ex.InnerException?.Message ?? "None");
            result = result.Replace("{{innerExceptionStackTrace}}", ex.InnerException?.StackTrace ?? "None");

            if (additionalMessage != null) { result = result + "\n--" + additionalMessage; }

            return result.TrimEnd();
        }
    }
}