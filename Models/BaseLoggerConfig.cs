using Newtonsoft.Json;
using SimpleLogger.Models.Enums;

namespace SimpleLogger.Models
{
    internal abstract class BaseLoggerConfig
    {
        [JsonProperty]
        internal bool IsEnabled { get; set; }

        [JsonProperty]
        internal LogLevel Level { get; set; }

        [JsonProperty]
        internal string ApplicationName { get; set; }
    }
}