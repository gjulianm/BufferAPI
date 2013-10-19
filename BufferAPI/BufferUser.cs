using Newtonsoft.Json;
using System;

namespace BufferAPI
{
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
                System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
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
                System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
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
