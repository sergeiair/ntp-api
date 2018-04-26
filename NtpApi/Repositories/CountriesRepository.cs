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
    public class CountriesRepository : ICollectionRepository<Country>
    {
        private readonly Context _context;

        public CountriesRepository(IOptions<MongoSettings> settings)
        {
            _context = new Context(settings);
        }

        public async Task<IEnumerable<Country>> GetItemsAsync()
        {
            try
            {
                return await _context.Countries
                    .Find(_ => true)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<IEnumerable<Country>> GetItemsAsync(string paramName, object paramValue)
        {
            try
            {
                return await _context.Countries
                    .Find(Builders<Country>.Filter.Eq(paramName, paramValue))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<Country> GetItemByIdAsync(ObjectId id)
        {
            try
            {
                return await _context.Countries
                    .Find(Builders<Country>.Filter.Eq("id", id))
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<Country> GetItemAsync(string paramName, object paramValue)
        {
            try
            {
                return await _context.Countries
                    .Find(Builders<Country>.Filter.Eq(paramName, paramValue))
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
