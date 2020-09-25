using System.Net.Http;
using System.Threading.Tasks;

namespace BitrixService.Utils.Http.Interfaces
{
    public interface ITypedHttpClient
    {
        Task<T> GetObjectAsync<T>(string uri);
        Task<HttpResponseMessage> PostObjectAsync<T>(string uri, T obj);
        Task<HttpResponseMessage> PutObjectAsync<T>(string uri, T obj);
        Task<HttpResponseMessage> DeleteObjectAsync<T>(string uri, T obj);
    }
}