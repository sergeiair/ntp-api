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
    public class SeasonsRepository : ICollectionRepository<Season>
    {
        private readonly Context _context;

        public SeasonsRepository(IOptions<MongoSettings> settings)
        {
            _context = new Context(settings);
        }

        public async Task<IEnumerable<Season>> GetItemsAsync()
        {
            try
            {
                return await _context.Seasons
                    .Find(_ => true)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<IEnumerable<Season>> GetItemsAsync(string paramName, object paramValue)
        {
            try
            {
                return await _context.Seasons
                    .Find(Builders<Season>.Filter.Eq(paramName, paramValue))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<Season> GetItemByIdAsync(ObjectId id)
        {
            try
            {
                return await _context.Seasons
                    .Find(Builders<Season>.Filter.Eq("id", id))
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<Season> GetItemAsync(string paramName, object paramValue)
        {
            try
            {
                return await _context.Seasons
                    .Find(Builders<Season>.Filter.Eq(paramName, paramValue))
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
