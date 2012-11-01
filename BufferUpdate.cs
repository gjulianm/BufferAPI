using System.Collections.Generic;
using Newtonsoft.Json;
using System;

namespace BufferAPI
{
    public class BufferUpdateCreation
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("buffer_count")]
        public int Count { get; set; }

        [JsonProperty("buffer_percentage")]
        public int Percentage { get; set; }

        [JsonProperty("updates")]
        public IEnumerable<BufferUpdate> Updates { get; set; }
    }

    public class BufferUpdate
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("due_at")]
        public DateTime DueAt { get; set; }

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
