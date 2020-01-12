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
            var template = @"Date: {{date}}\n" +
                            "Thread: {{thread}}\n" +
                            "Application: {{app}}\n" +
                            "Exception Type: {{exceptionType}}\n" +
                            "Exception Message: {{exceptionMessage}}\n" +
                            "Stack Trace: {{stackTrace}}\n" +
                            "Log Level: {{level}}\n" +
                            "Inner Exception Type: {{innerExceptionType}}\n" +
                            "Inner Exception Message: {{innerExceptionMessage}}\n" +
                            "Inner Exception Stack Trace: {{innerExceptionStackTrace}}";

            template = template.Replace("{{date}}", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
            template = template.Replace("{{thread}}", Thread.CurrentThread.ManagedThreadId.ToString());
            template = template.Replace("{{app}}", this.config.ApplicationName);
            template = template.Replace("{{exceptionType}}", ex.GetType().FullName);
            template = template.Replace("{{exceptionMessage}}", ex.Message);
            template = template.Replace("{{stackTrace}}", ex.StackTrace);
            template = template.Replace("{{level}}", this.config.Level.ToString());
            template = template.Replace("{{innerExceptionType}}", ex.InnerException?.GetType().Name ?? "None");
            template = template.Replace("{{innerExceptionMessage}}", ex.InnerException?.Message ?? "None");
            template = template.Replace("{{innerExceptionStackTrace}}", ex.InnerException?.StackTrace ?? "None");

            this.smtpClient.Send(new MailMessage(this.config.From, this.config.To, this.config.ApplicationName, template));
        }

        public void LogMessage(string message)
        {
            this.smtpClient.Send(new MailMessage(this.config.From, this.config.To, this.config.ApplicationName, message));
        }
    }
}