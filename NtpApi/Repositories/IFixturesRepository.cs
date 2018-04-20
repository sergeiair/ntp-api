using MongoDB.Bson;
using NtpApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NtpApi.Repositories
{
    public interface IFixturesRepository
    {
        Task<IEnumerable<Fixture>> GetFixturesAsync();
        Task<Fixture> GetFixtureAsync(ObjectId id);
    }
}
