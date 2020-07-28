using System.Threading.Tasks;

namespace GameBox.Admin.UI.Services.Contracts
{
    public interface IHttpClientService
    {
        Task<T> GetAsync<T>(string url);

        Task<T> PutAsync<T>(string url, object body);

        Task<T> PostAsync<T>(string url, object body);

        Task<T> DeleteAsync<T>(string url);
    }
}