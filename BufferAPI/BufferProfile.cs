using System;
using Newtonsoft.Json;

namespace BufferAPI
{
    /// <summary>
    /// This class represents a Buffer profile, i.e., a profile you have linked with your
    /// Buffer account. <see href="https://buffer.com/developers/api/profiles">Buffer API</see>.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class BufferProfile
    {

        [JsonProperty("avatar")]
        public string AvatarUrl { get; set; }

        [JsonProperty("created_at")]
        public long CreatedAtSeconds { get; set; }


        public DateTime CreatedAt
        {
            get
            {
                System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                dtDateTime = dtDateTime.AddSeconds(CreatedAtSeconds).ToLocalTime();
                return dtDateTime;
            }
        }

        [JsonProperty("default")]
        public bool IsDefault { get; set; }

        [JsonProperty("formatted_username")]
        public string FormattedUsername { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("service")]
        public string Service { get; set; }

        [JsonProperty("service_id")]
        public string ServiceId { get; set; }

        [JsonProperty("service_username")]
        public string ServiceUsername { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }
    }
}
