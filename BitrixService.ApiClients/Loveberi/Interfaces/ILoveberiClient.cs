using System.Threading.Tasks;
using BitrixService.Contracts.Models;

namespace BitrixService.ApiClients.Loveberi.Interfaces
{
    public interface ILoveberiClient
    {
        void Login();
        Task<ProductAto[]> GetAllProductsAsync();
        Task<ProductIdWithInternalIdAto[]> GetProductIdWithIeIdAsync();
        Task<CategoryAto[]> GetCategoriesAsync();
        Task AddProductsRangeAsync(ProductAto[] products);

        Task UpdateAsync(string content);

        Task DeleteAsync(int[] ids);
        Task<OfferAto[]> GetAllOffersAsync();
        Task AddOffersRangeAsync(OfferAto[] offers);

        Task UpdateOffersAsync();

        Task DeleteOffersAsync(int[] ids);
    }
}