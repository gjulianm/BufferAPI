using System.Collections.Generic;
using Newtonsoft.Json;

namespace BufferAPI
{
    [JsonObject(MemberSerialization.OptIn)]
    public class BufferSchedules
    {
        [JsonProperty("days")]
        public IEnumerable<string> Days { get; set; }

        [JsonProperty("times")]
        public IEnumerable<string> Times { get; set; }
    }
}
