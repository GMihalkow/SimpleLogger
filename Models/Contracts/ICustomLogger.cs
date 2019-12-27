namespace SimpleLogger.Models.Contracts
{
    using System;

    internal interface ICustomLogger
    {
        void Log(Exception ex);
    }
}