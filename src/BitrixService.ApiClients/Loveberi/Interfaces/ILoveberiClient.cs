using System.Threading.Tasks;
using BitrixService.Contracts.ApiModels;

namespace BitrixService.ApiClients.Loveberi.Interfaces
{
    public interface ILoveberiClient
    {
        void Login();
        
        Task<VendorIdAto[]> GetVendorsAsync();
        
        Task<ProductAto[]> GetAllProductsAsync();
        
        Task<ProductIdWithInternalIdAto[]> GetProductIdWithIeIdAsync();
        
        Task<CategoryAto[]> GetCategoriesAsync();
        
        Task AddProductsRangeAsync(ProductAto[] products);

        Task UpdateProductsAsync(ProductAto[] products);

        Task DeleteProductsAsync(int[] ids);
        
        Task<OfferAto[]> GetAllOffersAsync();
        
        Task AddOffersRangeAsync(OfferAto[] offers);

        Task UpdateOffersAsync(OfferAto[] offers);

        Task DeleteOffersAsync(int[] ids);
    }
}