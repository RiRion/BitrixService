using System;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BitrixService.Exceptions;
using BitrixService.Interfaces;
using BitrixService.Models;
using BitrixService.Models.Config;
using TypedHttpClient = BitrixService.Utils.Http.TypedHttpClient;

namespace BitrixService.Services
{
    public class BitrixClient : TypedHttpClient, IBitrixClient
    {
        // BitrixClient FIELDS /////////////////////////////////////////////////////////////////////////////////////////
        private readonly BitrixConfig _config;
        private readonly string _apiUri;

        // PRODUCTS Paths //////////////////////////////////////////////////////////////////////////////////////////////
        private const string GetAllProductsPath = "/GetAllProducts";
        private const string ProductIdWithIeIdPath = "/ProductIdWithIeId";
        private const string GetCategoriesPath = "/GetCategories";
        private const string AddProductsRangePath = "/AddProductsRange";
        private const string DeleteProductsRangePath = "/DeleteProductsRange";
        private const string UpdateProductsRangePath = "/UpdateProductsRange";
        
        // OFFERS Paths ///////////////////////////////////////////////////////////////////////////////////////////////
        private const string GetAllOffersPath = "/GetAllOffers";
        private const string AddOffersRangPath = "/AddOffersRange";
        private const string UpdateOffersPath = "/UpdateOffers";
        private const string DeleteOffersPath = "/DeleteOffers";
        
        public BitrixClient(BitrixConfig config)
        {
            _config = config;
            _apiUri = _config.BaseUri + _config.BasePath;
        }
        public BitrixClient(string baseUri, string basePath, string login, string password) : 
            this(new BitrixConfig(baseUri, basePath, login, password)) { }
        
        // FUNCTIONS ///////////////////////////////////////////////////////////////////////////////////////////////////
        
        public void Login()
                {
                    var authData = Encoding.ASCII.GetBytes($"{_config.Login}:{_config.Password}");
                    DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                        "Basic",
                        Convert.ToBase64String(authData)
                    );
                    var response = GetAsync(_apiUri + "/Login").GetAwaiter().GetResult();
                    if (!response.IsSuccessStatusCode) throw new AuthentificationFailException();
                }
        
        // PRODUCT /////////////////////////////////////////////////////////////////////////////////////////////////////
        
        public async Task<ProductAto[]> GetAllProductsAsync()
        {
            return await GetObjectAsync<ProductAto[]>(_apiUri + GetAllProductsPath);
        }
        
        public async Task<ProductIdWithInternalIdAto[]> GetProductIdWithIeIdAsync()
        {
            return await GetObjectAsync<ProductIdWithInternalIdAto[]>(_apiUri + ProductIdWithIeIdPath);
        }

        public async Task<CategoryAto[]> GetCategoriesAsync()
        {
            return await GetObjectAsync<CategoryAto[]>(_apiUri + GetCategoriesPath);
        }

        public async Task AddProductsRangeAsync(ProductAto[] products)
        {
            var response = await PostObjectAsync(_apiUri + AddProductsRangePath, products);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException($"Status code {response.StatusCode}, message: {response.Content.ReadAsStringAsync()}.");
            }
            await response.Content.ReadAsStringAsync();
        }

        public async Task UpdateAsync(string content) // TODO: Api doesn't supported. 
        { 
            throw new NotImplementedException(); 
        }
        
        public async Task DeleteAsync(int[] ids)
        {
            var response = PostObjectAsync(_apiUri + DeleteProductsRangePath, ids);
            await response.Result.Content.ReadAsStringAsync();
        }
        
        // OFFERS //////////////////////////////////////////////////////////////////////////////////////////////////////

        public async Task<OfferAto[]> GetAllOffersAsync()
        {
            return await GetObjectAsync<OfferAto[]>(_apiUri + GetAllOffersPath);
        }

        public async Task AddOffersRangeAsync(OfferAto[] offers)
        {
            var response = PostObjectAsync(_apiUri + AddOffersRangPath, offers);
            await response.Result.Content.ReadAsStringAsync();
        }

        public async Task UpdateOffersAsync() // TODO: Api doesn't supported.
        {
            throw new NotImplementedException();
        }

        public async Task DeleteOffersAsync(int[] ids)
        {
            var response = PostObjectAsync(_apiUri + DeleteOffersPath, ids);
            await response.Result.Content.ReadAsStringAsync();
        }
    }
}