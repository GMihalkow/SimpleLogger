using Newtonsoft.Json;
using SimpleLogger.Models.Enums;

namespace SimpleLogger.Models.Console.Configuration
{
    internal class ConsoleLoggerConfig : BaseLoggerConfig
    {
        [JsonProperty]
        internal ConsoleTextColor TextColor { get; set; }
    }
}