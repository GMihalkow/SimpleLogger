using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace SimpleLogger.Models.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    internal enum ConsoleTextColor
    {
        [EnumMember(Value = "gray")]
        Gray = 0,

        [EnumMember(Value = "yellow")]
        Yellow = 1,

        [EnumMember(Value = "red")]
        Red = 2,

        [EnumMember(Value = "blue")]
        Blue = 3,

        [EnumMember(Value = "white")]
        White = 4,

        [EnumMember(Value = "green")]
        Green = 5,
    }
}