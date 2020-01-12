namespace SimpleLogger.Models.Contracts
{
    using System;

    internal interface ICustomLogger
    {
        void LogException(Exception ex);

        void LogMessage(string message);
    }
}