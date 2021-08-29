namespace SimonSampleApp.Web.Services.OneSignal
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using Models;

    public interface IOneSignalService
    {
        Task<AppModel[]> GetAppsAsync(CancellationToken cToken = default);
        
        Task<AppModel?> GetAppAsync(string id, CancellationToken cToken = default);
        
        Task<AppModel> CreateAppAsync(AppPostModel model, CancellationToken cToken = default);
        
        Task<AppModel> UpdateAppAsync(string id, AppPutModel model, CancellationToken cToken = default);
    }
    
    public class OneSignalService: IOneSignalService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<OneSignalService> _logger;

        public OneSignalService(HttpClient httpClient, ILogger<OneSignalService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<AppModel[]> GetAppsAsync(CancellationToken cToken = default)
        {
            var stringResponse = await _httpClient.GetStringAsync("apps", cToken).ConfigureAwait(false);
            return JsonSerializer.Deserialize<AppModel[]>(stringResponse) ?? Array.Empty<AppModel>();
        }

        public async Task<AppModel?> GetAppAsync(string id, CancellationToken cToken = default)
        {
            try
            {
                var stringResponse = await _httpClient.GetStringAsync($"apps/{id}", cToken).ConfigureAwait(false);
                return JsonSerializer.Deserialize<AppModel>(stringResponse);

            }
            catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<AppModel> CreateAppAsync(AppPostModel model, CancellationToken cToken = default)
        {
            var responseMessage = await _httpClient.PostAsJsonAsync("apps", model, cToken).ConfigureAwait(false);
            responseMessage.EnsureSuccessStatusCode();

            var appModel = await responseMessage.Content.ReadFromJsonAsync<AppModel>(cancellationToken: cToken).ConfigureAwait(false);
            if (appModel == null)
                throw new InvalidOperationException("POST response is null");

            return appModel;
        }

        public async Task<AppModel> UpdateAppAsync(string id, AppPutModel model, CancellationToken cToken = default)
        {
            var responseMessage = await _httpClient.PutAsJsonAsync($"apps/{id}", model, cToken).ConfigureAwait(false);
            responseMessage.EnsureSuccessStatusCode();

            var appModel = await responseMessage.Content.ReadFromJsonAsync<AppModel>(cancellationToken: cToken).ConfigureAwait(false);
            if (appModel == null)
                throw new InvalidOperationException("POST response is null");

            return appModel;
        }
    }
}