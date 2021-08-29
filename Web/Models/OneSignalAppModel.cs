namespace SimonSampleApp.Web.Models
{
    using System;

    public class OneSignalAppModel: OneSignalAppPostModel
    {
        public string Id { get; set; }

        public int Players { get; set; }

        public int MessageablePlayers { get; set; }

        public DateTime UpdatedAt { get; set; }
        
        public DateTime CreatedAt { get; set; }

        public string GcmKey { get; set; }

        public string ChromeKey { get; set; }

        public string ChromeWebGcmSenderId { get; set; }

        public string ChromeWebDefaultNotificationIcon { get; set; }

        public string ChromeWebSubDomain { get; set; }

        public string ApnsCertificates { get; set; }
        
        public string ApnsEnv { get; set; }

        public string SafariApnsCertificate { get; set; }

        public string SafariPushId { get; set; }

        public string SafariIcon1616 { get; set; }

        public string SafariIcon3232 { get; set; }

        public string SafariIcon6464 { get; set; }

        public string SafariIcon128128 { get; set; }

        public string SafariIcon256256 { get; set; }

        public string BasicAuthKey { get; set; }
    }
}