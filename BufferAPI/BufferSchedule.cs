using System.Collections.Generic;
using Newtonsoft.Json;

namespace BufferAPI
{
    /// <summary>
    /// This class represents the posting scheduls associated with a certain profile.
    /// See <see href="https://buffer.com/developers/api/profiles#schedules">Buffer API</see>.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class BufferSchedules
    {
        [JsonProperty("days")]
        public IEnumerable<string> Days { get; set; }

        [JsonProperty("times")]
        public IEnumerable<string> Times { get; set; }
    }
}
