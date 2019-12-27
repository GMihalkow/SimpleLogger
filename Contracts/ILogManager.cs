namespace SimpleLogger.Contracts
{
    using System;

    public interface ILogManager
    {
        void Log(Exception ex);
    }
}