using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace BufferAPI
{
    /// <summary>
    /// This class represents the result of the creation of a Buffer update.
    /// <see href="https://buffer.com/developers/api/updates#updatescreate" />
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class BufferUpdateCreation
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("buffer_count")]
        public int Count { get; set; }

        [JsonProperty("buffer_percentage")]
        public double Percentage { get; set; }

        [JsonProperty("updates")]
        public IEnumerable<BufferUpdate> Updates { get; set; }
    }

    /// <summary>
    /// This class represents a created Buffer update.
    /// <see href="https://buffer.com/developers/api/updates#updatesid" />
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class BufferUpdate
    {
        [JsonProperty("id")]
        public string Id { get; set; }

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

        [JsonProperty("due_at")]
        public long DueAtSeconds { get; set; }

        public DateTime DueAt
        {
            get
            {
                System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                dtDateTime = dtDateTime.AddSeconds(DueAtSeconds).ToLocalTime();
                return dtDateTime;
            }
        }

        [JsonProperty("profile_id")]
        public string ProfileId { get; set; }

        [JsonProperty("profile_service")]
        public string Service { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("text_formatted")]
        public string FormattedText { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("via")]
        public string Via { get; set; }
    }
}
