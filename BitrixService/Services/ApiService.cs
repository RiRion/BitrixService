using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BitrixService.Models;
using BitrixService.Services.Interfaces;
using Newtonsoft.Json;

namespace BitrixService.Services
{
    public class ApiService : IApiService, IDisposable
    {
        private CookieContainer cookieContainer;
        private HttpClientHandler clientHandler;
        private readonly ApiConfig _config;
        private HttpClient client;
        private readonly Uri authUri;
        private readonly Uri apiUri;


        public ApiService(ApiConfig config)
        {
            clientHandler = new HttpClientHandler {CookieContainer = cookieContainer};
            client = new HttpClient(clientHandler);
            _config = config;
            apiUri = new Uri(_config.BaseUri + _config.PrefixApi);
            authUri = new Uri(_config.BaseUri + _config.PrefixAuth);
        }

        public async Task<bool> Auth()
        {
            cookieContainer = new CookieContainer();
            var formData = new FormUrlEncodedContent(_config.FormData);
            var response = await client.PostAsync(authUri, formData);
            var cookies = cookieContainer.GetCookies(new Uri(_config.BaseUri))
                .Select(c=>c.ToString()).ToList();
            return cookies.Contains($"BITRIX_SM_LOGIN={_config.Login}");
        }

        public async Task<string> GetAsync()
        {
            await CheckAuth();
            var response = client.GetAsync(apiUri);
            return await response.Result.Content.ReadAsStringAsync();
        }

        public async Task PostAsync(StringContent content)
        {
            await CheckAuth();
            await client.PostAsync(apiUri, content);
        }

        public async Task DeleteAsync(int[] id)
        {
            await CheckAuth();
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Delete;
            request.Content = new StringContent(JsonConvert.SerializeObject(id));
            request.RequestUri = apiUri;
            await client.SendAsync(request);
        }

        public async Task UpdateAsync(StringContent updateContent)
        {
            await CheckAuth();
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Put;
            request.Content = updateContent;
            request.RequestUri = apiUri;
            await client.SendAsync(request);
        }

        public async Task CheckAuth()
        {
            var response = client.GetAsync(authUri);
            var body = await response.Result.Content.ReadAsStringAsync();
            if (!body.Contains(_config.Login))
            {
                await Auth();
            }
        }

        public void Dispose()
        {
            client?.Dispose();
        }
    }
}