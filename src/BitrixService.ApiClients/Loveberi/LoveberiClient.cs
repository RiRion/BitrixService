using System;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BitrixService.ApiClients.Loveberi.Exceptions;
using BitrixService.ApiClients.Loveberi.Interfaces;
using BitrixService.ApiClients.Models.Config;
using BitrixService.Contracts.ApiModels;
using TypedHttpClient = BitrixService.Common.Http.TypedHttpClient;

namespace BitrixService.ApiClients.Loveberi
{
    public class LoveberiClient : TypedHttpClient, ILoveberiClient
    {
        // BitrixClient FIELDS /////////////////////////////////////////////////////////////////////////////////////////
        private readonly LoveberiClientConfig _clientConfig;
        
        // VENDOR Paths ////////////////////////////////////////////////////////////////////////////////////////////////
        private const string GetVendorsPath = "/GetVendors";

        // PRODUCTS Paths //////////////////////////////////////////////////////////////////////////////////////////////
        private const string GetAllProductsPath = "/GetAllProducts";
        private const string ProductIdWithIeIdPath = "/ProductIdWithIeId";
        private const string GetCategoriesPath = "/GetCategories";
        private const string AddProductsRangePath = "/AddProductsRange";
        private const string DeleteProductsRangePath = "/DeleteProductsRange";
        private const string UpdateProductsRangePath = "/UpdateProductsRange";
        
        // OFFERS Paths ////////////////////////////////////////////////////////////////////////////////////////////////
        private const string GetAllOffersPath = "/GetAllOffers";
        private const string AddOffersRangPath = "/AddOffersRange";
        private const string UpdateOffersPath = "/UpdateOffers";
        private const string DeleteOffersPath = "/DeleteOffers";
        
        public LoveberiClient(LoveberiClientConfig clientConfig)
        {
            _clientConfig = clientConfig;
            BaseAddress = new Uri(clientConfig.BaseUri + clientConfig.BasePath);
        }
        public LoveberiClient(string baseUri, string basePath, string login, string password) : 
            this(new LoveberiClientConfig(baseUri, basePath, login, password)) { }
        
        // FUNCTIONS ///////////////////////////////////////////////////////////////////////////////////////////////////
        
        public void Login()
                {
                    var authData = Encoding.ASCII.GetBytes($"{_clientConfig.Login}:{_clientConfig.Password}");
                    DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                        "Basic",
                        Convert.ToBase64String(authData)
                    );
                    var response = GetAsync(BaseAddress + "/Login").GetAwaiter().GetResult();
                    if (!response.IsSuccessStatusCode) throw new AuthentificationFailException();
                }
        
        // VENDOR //////////////////////////////////////////////////////////////////////////////////////////////////////

        public async Task<VendorIdAto[]> GetVendorsAsync()
        {
            return await GetObjectAsync<VendorIdAto[]>(BaseAddress + GetVendorsPath);
        }
        
        // PRODUCT /////////////////////////////////////////////////////////////////////////////////////////////////////
        
        public async Task<ProductAto[]> GetAllProductsAsync()
        {
            return await GetObjectAsync<ProductAto[]>(BaseAddress + GetAllProductsPath);
        }
        
        public async Task<ProductIdWithInternalIdAto[]> GetProductIdWithIeIdAsync()
        {
            return await GetObjectAsync<ProductIdWithInternalIdAto[]>(BaseAddress + ProductIdWithIeIdPath);
        }

        public async Task<CategoryAto[]> GetCategoriesAsync()
        {
            return await GetObjectAsync<CategoryAto[]>(BaseAddress + GetCategoriesPath);
        }

        public async Task AddProductsRangeAsync(ProductAto[] products)
        {
            using (var response = await PostObjectAsync(BaseAddress + AddProductsRangePath, products))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new ApplicationException($"Status code {response.StatusCode}, message: {response.Content.ReadAsStringAsync()}.");
                }
                await response.Content.ReadAsStringAsync();
            }
        }

        public async Task UpdateProductsAsync(ProductAto[] products)
        {
            using (var response = await PostObjectAsync(BaseAddress + UpdateProductsRangePath, products))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new ApplicationException($"Status code {response.StatusCode}, message: {response.Content.ReadAsStringAsync()}.");
                }

                await response.Content.ReadAsStringAsync();   
            }
        }
        
        public async Task DeleteProductsAsync(int[] ids)
        {
            using (var response = PostObjectAsync(BaseAddress + DeleteProductsRangePath, ids))
            {
                await response.Result.Content.ReadAsStringAsync();
            }
        }
        
        // OFFERS //////////////////////////////////////////////////////////////////////////////////////////////////////

        public async Task<OfferAto[]> GetAllOffersAsync()
        {
            return await GetObjectAsync<OfferAto[]>(BaseAddress + GetAllOffersPath);
        }

        public async Task AddOffersRangeAsync(OfferAto[] offers)
        {
            using (var response = PostObjectAsync(BaseAddress + AddOffersRangPath, offers))
            {
                await response.Result.Content.ReadAsStringAsync();
            }
        }

        public async Task UpdateOffersAsync(OfferAto[] offers)
        {
            using (var response = await PostObjectAsync(BaseAddress + UpdateOffersPath, offers))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new ApplicationException($"Status code {response.StatusCode}, message: {response.Content.ReadAsStringAsync()}.");
                }

                await response.Content.ReadAsStringAsync();
            }
        }

        public async Task DeleteOffersAsync(int[] ids)
        {
            using (var response = PostObjectAsync(BaseAddress + DeleteOffersPath, ids))
            {
                await response.Result.Content.ReadAsStringAsync();
            }
        }
    }
} 