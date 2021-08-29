namespace SimonSampleApp.Web.Services.OneSignal.Models
{
    using System;
    using System.Text.Json.Serialization;

    public class AppModel: AppPostModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("players")]
        public int Players { get; set; }

        [JsonPropertyName("messageable_players")]
        public int MessageablePlayers { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("gcm_key")]
        public string GcmKey { get; set; }

        [JsonPropertyName("chrome_key")]
        public string ChromeKey { get; set; }

        [JsonPropertyName("chrome_web_gcm_sender_id")]
        public string ChromeWebGcmSenderId { get; set; }

        [JsonPropertyName("chrome_web_default_notification_icon")]
        public string ChromeWebDefaultNotificationIcon { get; set; }

        [JsonPropertyName("chrome_web_sub_domain")]
        public string ChromeWebSubDomain { get; set; }

        [JsonPropertyName("apns_certificates")]
        public string ApnsCertificates { get; set; }
        
        [JsonPropertyName("apns_env")]
        public string ApnsEnv { get; set; }

        [JsonPropertyName("safari_apns_certificate")]
        public string SafariApnsCertificate { get; set; }

        [JsonPropertyName("safari_push_id")]
        public string SafariPushId { get; set; }

        [JsonPropertyName("safari_icon_16_16")]
        public string SafariIcon1616 { get; set; }

        [JsonPropertyName("safari_icon_32_32")]
        public string SafariIcon3232 { get; set; }

        [JsonPropertyName("safari_icon_64_64")]
        public string SafariIcon6464 { get; set; }

        [JsonPropertyName("safari_icon_128_128")]
        public string SafariIcon128128 { get; set; }

        [JsonPropertyName("safari_icon_256_256")]
        public string SafariIcon256256 { get; set; }

        [JsonPropertyName("basic_auth_key")]
        public string BasicAuthKey { get; set; }
    }
}