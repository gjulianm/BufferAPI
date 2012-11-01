using System;
using Newtonsoft.Json;

namespace BufferAPI
{
    public class BufferProfile
    {

        [JsonProperty("avatar")]
        public string AvatarUrl { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("default")]
        public bool IsDefault { get; set; }

        [JsonProperty("formatted_username")]
        public string FormattedUsername { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("schedules")]
        public BufferSchedules Schedules { get; set; }

        [JsonProperty("service")]
        public string Service { get; set; }

        [JsonProperty("service_id")]
        public long ServiceId { get; set; }

        [JsonProperty("service_username")]
        public string ServiceUsername { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }
    }
}
