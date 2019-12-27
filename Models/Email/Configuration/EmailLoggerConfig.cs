using Newtonsoft.Json;

namespace SimpleLogger.Models.Email.Configuration
{
    internal class EmailLoggerConfig : BaseLoggerConfig
    {
        [JsonProperty]
        internal string SmtpHost { get; set; }

        [JsonProperty]
        internal int SmtpPort { get; set; }

        [JsonProperty]
        internal string Username { get; set; }

        [JsonProperty]
        internal string Password { get; set; }

        [JsonProperty]
        internal string From { get; set; }

        [JsonProperty]
        internal string To { get; set; }
    }
}