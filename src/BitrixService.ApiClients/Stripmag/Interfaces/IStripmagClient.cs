using System.Threading.Tasks;
using BitrixService.Contracts.ApiModels;

namespace BitrixService.ApiClients.Stripmag.Interfaces
{
    public interface IStripmagClient
    {
        Task<ProductFromSupplierAto[]> GetProductsFromSupplierAsync();
        Task<OfferAto[]> GetOffersFromSupplierAsync();
    }
}