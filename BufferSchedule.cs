using System.Collections.Generic;
using Newtonsoft.Json;

namespace BufferAPI
{
    public class BufferSchedules
    {
        [JsonProperty("days")]
        public IEnumerable<string> Days { get; set; }

        [JsonProperty("times")]
        public IEnumerable<string> Times { get; set; }
    }
}
