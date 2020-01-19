namespace SimpleLogger.Contracts
{
    using System;

    public interface ILogManager
    {
        void GlobalExceptionLog(Exception ex, string additionalMessage = null);

        void ConsoleLog(Exception ex);

        void EmailLog(Exception ex);

        void GlobalLog(string message);

        void ConsoleLog(string message);

        void EmailLog(string message);
    }
}