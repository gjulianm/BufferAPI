using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Newtonsoft.Json;

namespace BufferAPI
{
    public class BufferUser
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("activity_at")]
        public DateTime LastActivity { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("plan")]
        public string Plan { get; set; }

        [JsonProperty("referral_link")]
        public string ReferralLink { get; set; }

        [JsonProperty("secret_email")]
        public string SecretEmail { get; set; }
    }
}
