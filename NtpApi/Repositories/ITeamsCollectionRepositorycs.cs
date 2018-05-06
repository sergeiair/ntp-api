
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NtpApi.Repositories
{
    public interface ITeamsCollectionRepository<T> : ICollectionRepository<T>
    {
        Task<IEnumerable<T>> GetItemsByTeamsAsync(string team1, string team2);
    }
}
