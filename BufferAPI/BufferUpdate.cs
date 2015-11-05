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
                DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
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
                DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
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



    /// <summary>
    /// This class represents the media section of a new update to send to buffer
    ///  <see href="https://buffer.com/developers/api/updates#updatescreate" />
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class BufferUpdateMedia
    {
        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("picture")]
        public string Picture { get; set; }

        [JsonProperty("photo")]
        public string Photo { get; set; }

        [JsonProperty("thumbnail")]
        public string Thumbnail { get; set; }

    }

    /// <summary>
    /// This class represents the retweet section of a new update to send to buffer
    ///  <see href="https://buffer.com/developers/api/updates#updatescreate" />
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class BufferUpdateRetweet
    {
        [JsonProperty("tweet_id")]
        public string TweetId { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }

    }




    /// <summary>
    /// This class represents a new update to send to buffer
    ///  <see href="https://buffer.com/developers/api/updates#updatescreate" />
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class BufferUpdateCreate
    {
        public BufferUpdateCreate()
        {
            Media = new BufferUpdateMedia();
            Retweet = new BufferUpdateRetweet();

            Shorten = true;
            Attachment = true;
        }


        //   [JsonProperty("text")]
        public string Text { get; set; }

        //   [JsonProperty("shorten")]
        public bool Shorten { get; set; }

        //   [JsonProperty("now")]
        public bool Now { get; set; }

        //   [JsonProperty("top")]
        public bool Top { get; set; }

        //   [JsonProperty("media")]
        public BufferUpdateMedia Media { get; set; }

        //   [JsonProperty("attachment")]
        public bool Attachment { get; set; }


        public DateTime ScheduledAt { get; set; }

        //    [JsonProperty("scheduled_at")]
        public double ScheduledAtSeconds
        {
            get
            {
                return BufferAPI.TimeConversion.DateTimeToUnixTimestamp(ScheduledAt);
            }
        }

        //   [JsonProperty("retweet")]
        public BufferUpdateRetweet Retweet { get; set; }


    }



}
