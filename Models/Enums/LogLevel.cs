using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace SimpleLogger.Models.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    internal enum LogLevel
    {
        [EnumMember(Value = "info")]
        Info = 1,

        [EnumMember(Value = "warning")]
        Warning = 2,

        [EnumMember(Value = "error")]
        Error = 3
    }
}