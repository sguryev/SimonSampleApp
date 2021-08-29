namespace SimonSampleApp.Web.Services.OneSignal.Models
{
    using System.Text.Json.Serialization;

    public class AppPostModel: AppPutModel
    {
        [JsonPropertyName("site_name")]
        public string SiteName { get; set; }
        
        [JsonPropertyName("safari_site_origin")]
        public string SafariSiteOrigin { get; set; }
    }
}