namespace SimpleLogger.Models.Contracts
{
    using System;

    internal interface ICustomLogger
    {
        void LogException(Exception ex, string additionalMessage);

        void LogMessage(string message);
    }
}