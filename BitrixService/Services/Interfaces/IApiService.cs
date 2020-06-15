using System.Net.Http;
using System.Threading.Tasks;

namespace BitrixService.Services.Interfaces
{
    public interface IApiService
    {
        Task<bool> Auth();
        Task<string> GetAsync();
        Task PostAsync(StringContent content);
        Task DeleteAsync(int[] id);
        Task UpdateAsync(StringContent updateContent);
        Task CheckAuth();
    }
}