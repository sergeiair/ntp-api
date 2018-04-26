using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NtpApi.Repositories
{
    public interface ICollectionRepository<T>
    {
        Task<IEnumerable<T>> GetItemsAsync(string paramName, object paramValue);
        Task<IEnumerable<T>> GetItemsAsync();
        Task<T> GetItemByIdAsync(ObjectId id);
        Task<T> GetItemAsync(string paramName, object paramValue);
    }
}
