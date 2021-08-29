namespace SimonSampleApp.Web.Models
{
    using System.ComponentModel;

    public class OneSignalAppPostModel: OneSignalAppPutModel
    {
        [DisplayName("Site Name")]
        public string SiteName { get; set; }
        
        [DisplayName("Safari Site Origin")]
        public string SafariSiteOrigin { get; set; }
    }
}