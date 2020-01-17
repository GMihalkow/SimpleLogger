using System;

namespace SimpleLogger.Helpers
{
    internal class ConsoleHelper
    {
        internal static void Log(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;

            Console.WriteLine(message);

            Console.ResetColor();
        }
    }
}