using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using NtpApi.Models;
using NtpApi.Settings;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NtpApi.Repositories
{
    public class FixturesRepository : IFixturesRepository
    {
        private readonly Context _context = null;

        public FixturesRepository(IOptions<MongoSettings> settings)
        {
            _context = new Context(settings);
        }

        public async Task<IEnumerable<Fixture>> GetFixturesAsync()
        {
            try
            {
                IEnumerable<Fixture> f = await _context.Fixtures
                        .Find(_ => true)
                        .ToListAsync();

                return f;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Fixture> GetFixtureAsync(ObjectId id)
        {
            try
            {
                return await _context.Fixtures
                        .Find(Builders<Fixture>.Filter.Eq("id", id))
                        .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
