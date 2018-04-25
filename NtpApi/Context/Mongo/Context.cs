using Microsoft.Extensions.Options;
using MongoDB.Driver;
using NtpApi.Settings;
using NtpApi.Models;

public class Context
{
    private readonly IMongoDatabase _database = null;

    public Context(IOptions<MongoSettings> settings)
    {
        try
        {
            MongoClient client = new MongoClient(settings.Value.ConnectionString);
            
            _database = client.GetDatabase(settings.Value.Database);
            
        } catch (System.Exception ex)
        {
            throw ex;
        }
        
    }

    public IMongoCollection<Fixture> Fixtures
    {
        get
        {
            return _database.GetCollection<Fixture>("fixtures");
        }
    }
}