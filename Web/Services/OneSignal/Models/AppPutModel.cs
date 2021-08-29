namespace SimonSampleApp.Web.Services.OneSignal.Models
{
    using System.Text.Json.Serialization;

    public class AppPutModel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        [JsonPropertyName("chrome_web_origin")]
        public string ChromeWebOrigin { get; set; }
    }
}