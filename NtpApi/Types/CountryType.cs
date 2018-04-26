using System;
using GraphQL;
using GraphQL.Types;
using MongoDB.Bson;
using NtpApi.Models;
using NtpApi.Repositories;

namespace NtpApi.Types
{
    public class CountryType : ObjectGraphType<Country>
    {
        public CountryType(ICollectionRepository<Fixture> fixturesRepository)
        {
            Field(country => country.Id, type: typeof(StringGraphType));
            Field(country => country.Id_country, type: typeof(IntGraphType));
            Field(country => country.Name, type: typeof(StringGraphType));
            Field(country => country.Code, type: typeof(StringGraphType));
            
            Field<ListGraphType<FixtureType>>(
               "fixtures",
               resolve: context => fixturesRepository
                    .GetItemsAsync("Id_country", context.Source.Id_country).Result
            );
        }
    }
}
