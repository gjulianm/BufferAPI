using System;
using Newtonsoft.Json;

namespace BufferAPI
{
    /// <summary>
    /// This class represents a Buffer account and its details.
    /// <see href="https://buffer.com/developers/api/user" />
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class BufferUser
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("activity_at")]
        public long LastActivitySeconds { get; set; }

        public DateTime LastActivity
        {
            get
            {
                DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                dtDateTime = dtDateTime.AddSeconds(LastActivitySeconds).ToLocalTime();
                return dtDateTime;
            }
        }

        [JsonProperty("created_at")]
        public long CreatedAtSeconds { get; set; }

        public DateTime CreatedAt
        {
            get
            {
                DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                dtDateTime = dtDateTime.AddSeconds(CreatedAtSeconds).ToLocalTime();
                return dtDateTime;
            }
        }

        [JsonProperty("plan")]
        public string Plan { get; set; }

        [JsonProperty("referral_link")]
        public string ReferralLink { get; set; }

        [JsonProperty("secret_email")]
        public string SecretEmail { get; set; }
    }
}
