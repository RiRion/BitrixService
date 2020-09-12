using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BitrixService.Models;
using Newtonsoft.Json;

namespace BitrixService.Services
{
    public class ApiService : IDisposable
    {
        private readonly CookieContainer cookieContainer;
        private readonly SocketsHttpHandler clientHandler;
        private readonly ApiConfig _config;
        private readonly HttpClient client;
        private readonly Uri _baseUri;
        private readonly string _apiUri;

        private const string AuthPath = "/Auth";
        private const string GetAllProductsPath = "/GetAllProducts";
        private const string ProductIdWithIeIdPath = "/ProductIdWithIeId";
        private const string GetCategoriesPath = "/GetCategories";
        private const string AddProductsRangePath = "/AddProductsRange";
        private const string DeleteProductsRangePath = "/DeleteProductsRange";
        private const string UpdateProductsRangePath = "/UpdateProductsRange";


        public ApiService(string baseUri, string basePath, string login, string password) : 
            this(new ApiConfig(baseUri, basePath, login, password)) { }
        public ApiService(ApiConfig config)
        {
            cookieContainer = new CookieContainer();
            clientHandler = new SocketsHttpHandler {CookieContainer = cookieContainer};
            client = new HttpClient(clientHandler);
            _config = config;
            _baseUri = new Uri(_config.BaseUri);
            _apiUri = _config.BaseUri + _config.BasePath;
        }

        private async Task<bool> AuthAsync()
        {
            var formData = new FormUrlEncodedContent(_config.FormData);
            await client.PostAsync(_baseUri, formData);
            var cookies = cookieContainer.GetCookies(_baseUri)
                .Select(c=>c.ToString()).ToList();
            return cookies.Contains($"BITRIX_SM_LOGIN={_config.Login}");
        }

        public async Task<string> GetAllProductsAsync()
        {
            await CheckAuth();
            var response = client.GetAsync(_apiUri + GetAllProductsPath);
            return await response.Result.Content.ReadAsStringAsync();
        }
        
        public async Task<string> ProductIdWithIeIdAsync()
        {
            await CheckAuth();
            var response = client.GetAsync(_apiUri + ProductIdWithIeIdPath);
            return await response.Result.Content.ReadAsStringAsync();
        }

        public async Task<string> GetCategoriesAsync()
        {
            await CheckAuth();
            var response = client.GetAsync(_apiUri + GetCategoriesPath);
            return await response.Result.Content.ReadAsStringAsync();
        }

        public async Task<string> AddProductsRangeAsync(string content)
        {
            await CheckAuth();
            var contentToSend = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(_apiUri + AddProductsRangePath, contentToSend);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Status code {response.StatusCode}, message: {response.Content.ReadAsStringAsync()}.");
            }
            return await response.Content.ReadAsStringAsync();
        }

        public async Task UpdateAsync(string content)
        {
            await CheckAuth();
            var request = new HttpRequestMessage();
            request.Method = HttpMethod.Put;
            request.Content = new StringContent(content, Encoding.UTF8, "application/json");
            request.RequestUri = new Uri(_apiUri + UpdateProductsRangePath);
            await client.SendAsync(request);
        }
        
        public async Task<string> DeleteAsync(int[] id)
            {
                await CheckAuth(); 
                var request = new HttpRequestMessage(); 
                request.Method = HttpMethod.Delete;
                request.Content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json");
                request.RequestUri = new Uri(_apiUri + DeleteProductsRangePath);
                var response = await client.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Status code {response.StatusCode}, message: {response.Content.ReadAsStringAsync()}.");
                }
                return await response.Content.ReadAsStringAsync();
            }
        
        private async Task CheckAuth()
        {
            var response = await client.GetAsync(_apiUri + AuthPath);
            if (!response.IsSuccessStatusCode)
            {
                cookieContainer.GetCookies(_baseUri).ToList().ForEach(c => c.Expired = true);
                if (!AuthAsync().GetAwaiter().GetResult())
                {
                    throw new Exception("Authentications false.");
                }
            }
        }

        public void Dispose()
        {
            client?.Dispose();
        }
    }
}