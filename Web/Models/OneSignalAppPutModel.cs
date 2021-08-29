namespace SimonSampleApp.Web.Models
{
    using System.ComponentModel;

    public class OneSignalAppPutModel 
    {
        public string Name { get; set; }
        
        [DisplayName("Chrome Web Origin")]
        public string ChromeWebOrigin { get; set; }
    }
}