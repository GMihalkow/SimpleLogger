namespace SimpleLogger.Contracts
{
    using System;

    public interface ILogManager
    {
        void GlobalLog(Exception ex);

        void ConsoleLog(Exception ex);

        void EmailLog(Exception ex);

        void GlobalLog(string message);

        void ConsoleLog(string message);

        void EmailLog(string message);
    }
}