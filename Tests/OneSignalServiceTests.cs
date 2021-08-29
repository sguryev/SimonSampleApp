namespace SimonSampleApp.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;
    using Web;
    using Web.Services.OneSignal;
    using Web.Services.OneSignal.Models;

    public class OneSignalServiceTests
    {
        private readonly IServiceProvider _serviceProvider = Program.CreateHostBuilder(Array.Empty<string>()).Build().Services;
        
        private IOneSignalService _service;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _service = _serviceProvider.GetRequiredService<IOneSignalService>();
        }

        [Test]
        public async Task GetAppsTest()
        {
            var apps = await _service.GetAppsAsync().ConfigureAwait(false);
            Assert.IsTrue(apps.Any(a => a.Id == "e39ac3a6-c093-4b94-ab42-19eda0eb918f"));
        }
        
        [TestCase("e39ac3a6-c093-4b94-ab42-19eda0eb918f")]
        public async Task GetAppTest(string id)
        {
            var app = await _service.GetAppAsync(id).ConfigureAwait(false);
            Assert.AreEqual(id, app?.Id);
        }
        
        [TestCase("App1", "http://app1.com")]
        public async Task CreateAppTest(string name, string url)
        {
            var postModel = new AppPostModel
            {
                Name = name,
                SiteName = name,
                ChromeWebOrigin = url,
                SafariSiteOrigin = url
            };

            var app = await _service.CreateAppAsync(postModel).ConfigureAwait(false);
            Assert.AreEqual(name, app.Name);
            Assert.AreEqual(name, app.SiteName);
            Assert.AreEqual(url, app.ChromeWebOrigin);
            Assert.AreEqual(url, app.SafariSiteOrigin);
        }
    }
}