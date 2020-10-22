using System.Threading.Tasks;
using BitrixService.Models.ApiModels;

namespace BitrixService.Clients.Loveberi.Interfaces
{
    public interface ILoveberiClient
    {
        void Login();
        
        Task<VendorIdAto[]> GetVendorsInternalIdWithExternalIdAsync();
        
        Task<VendorAto[]> GetVendorsAsync();
        
        Task AddVendorsAsync(VendorAto[] vendors);
        
        Task DeleteVendors(int[] ids);
        
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