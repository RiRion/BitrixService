using System;
using System.Threading.Tasks;
using BitrixService.ApiClients.Stripmag.Interfaces;
using BitrixService.ApiClients.Stripmag.Models.Config;
using BitrixService.Common.Http;
using BitrixService.Contracts.ApiModels;
using CsvHelper.Configuration;

namespace BitrixService.ApiClients.Stripmag
{
    public class StripmagClient : TypedHttpClient, IStripmagClient
    {
        private readonly StripmagClientConfig _clientConfig;
        private readonly CsvConfiguration _csvConfiguration;

        public StripmagClient(StripmagClientConfig clientConfig, CsvConfiguration csvConfiguration)
        {
            _clientConfig = clientConfig;
            _csvConfiguration = csvConfiguration;
            BaseAddress = new Uri(clientConfig.BaseUri);
        }

        public async Task<ProductFromSupplierAto[]> GetProductsFromSupplierAsync()
        {
            return await GetCsvObjectAsync<ProductFromSupplierAto>(
                BaseAddress + _clientConfig.ProductPath, _csvConfiguration);
        }
        
        public async Task<OfferAto[]> GetOffersFromSupplierAsync()
        {
            return await GetCsvObjectAsync<OfferAto>(BaseAddress + _clientConfig.OfferPath, _csvConfiguration);
        }
    }
}