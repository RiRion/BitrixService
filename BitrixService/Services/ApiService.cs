using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BitrixService.Models;
using BitrixService.Services.Interfaces;
using Newtonsoft.Json;

namespace BitrixService.Services
{
    public class ApiService : IApiService, IDisposable
    {
        private readonly CookieContainer cookieContainer;
        private readonly HttpClientHandler clientHandler;
        private readonly ApiConfig _config;
        private readonly HttpClient client;
        private readonly Uri _baseUri;
        private readonly Uri _authUri;
        private readonly Uri _apiUri;


        public ApiService(ApiConfig config)
        {
            cookieContainer = new CookieContainer();
            clientHandler = new HttpClientHandler {CookieContainer = cookieContainer};
            clientHandler.CookieContainer = cookieContainer;
            client = new HttpClient(clientHandler);
            _config = config;
            _baseUri = new Uri(_config.BaseUri);
            _apiUri = new Uri(_config.BaseUri + _config.PrefixApi);
            _authUri = new Uri(_config.BaseUri + _config.PrefixAuth);
        }

        public async Task<bool> Auth()
        {
            var formData = new FormUrlEncodedContent(_config.FormData);
            var response = await client.PostAsync(_baseUri, formData);
            var cookies = cookieContainer.GetCookies(_baseUri)
                .Select(c=>c.ToString()).ToList();
            return cookies.Contains($"BITRIX_SM_LOGIN={_config.Login}");
        }

        public async Task<string> GetAsync()
        {
            await CheckAuth();
            var response = client.GetAsync(_apiUri);
            return await response.Result.Content.ReadAsStringAsync();
        }

        public async Task PostAsync(StringContent content)
        {
            await CheckAuth();
            await client.PostAsync(_apiUri, content);
        }

        public async Task DeleteAsync(int[] id)
        {
            await CheckAuth();
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Delete;
            request.Content = new StringContent(JsonConvert.SerializeObject(id));
            request.RequestUri = _apiUri;
            await client.SendAsync(request);
        }

        public async Task UpdateAsync(StringContent updateContent)
        {
            await CheckAuth();
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Put;
            request.Content = updateContent;
            request.RequestUri = _apiUri;
            await client.SendAsync(request);
        }

        public async Task CheckAuth()
        {
            var response = client.GetAsync(_authUri);
            var body = await response.Result.Content.ReadAsStringAsync();
            if (!body.Contains(_config.Login))
            {
                cookieContainer.GetCookies(_baseUri).ToList().ForEach(c => c.Expired = true);
                await Auth();
            }
        }

        public void Dispose()
        {
            client?.Dispose();
        }
    }
}