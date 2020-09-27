using System.Threading.Tasks;
using BitrixService.Contracts.ApiModels;
using BitrixService.Contracts.Models;

namespace BitrixService.ApiClients.Stripmag.Interfaces
{
    public interface IStripmagClient
    {
        Task<ProductFromSupplierAto[]> GetProductsFromSupplierAsync();
        Task<OfferAto[]> GetOffersFromSupplierAsync();
    }
}