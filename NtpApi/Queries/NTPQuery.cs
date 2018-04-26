using GraphQL.Types;
using MongoDB.Bson;
using NtpApi.Models;
using NtpApi.Repositories;
using NtpApi.Types;

namespace NtpApi.Queries
{
    public class NTPQuery : ObjectGraphType
    {
        public NTPQuery
        (
            ICollectionRepository<Country> countriesRepository,
            ICollectionRepository<Fixture> fixturesRepository,
            ICollectionRepository<Season> seasonsRepository
        )    
        {
            Field<CountryType>
            (
                "country",
                arguments: new QueryArguments
                (
                    new QueryArgument<NonNullGraphType<StringGraphType>>
                        { Name = "id", Description = "Country id" }
                ),
                resolve: context => countriesRepository
                    .GetItemByIdAsync(context.GetArgument<ObjectId>("id")).Result
            );
            
            Field<ListGraphType<CountryType>>
            (
                "countries",
                resolve: context => countriesRepository.GetItemsAsync().Result
            );

            Field<SeasonType>
            (
                "season",
                arguments: new QueryArguments
                (
                    new QueryArgument<NonNullGraphType<StringGraphType>>
                    { Name = "id", Description = "Season id" }
                ),
                resolve: context => seasonsRepository
                    .GetItemByIdAsync(context.GetArgument<ObjectId>("id")).Result
            );

            Field<ListGraphType<SeasonType>>
            (
                "seasons",
                resolve: context => seasonsRepository.GetItemsAsync().Result
            );

            Field<FixtureType>
            (
                "fixture",
                arguments: new QueryArguments
                (
                    new QueryArgument<NonNullGraphType<IntGraphType>> 
                        { Name = "id", Description = "Fixture id" }
                ),
                resolve: context => fixturesRepository
                    .GetItemByIdAsync(context.GetArgument<ObjectId>("id")).Result
            );

            Field<ListGraphType<FixtureType>>
            (
                "fixtures",
                resolve: context => fixturesRepository.GetItemsAsync().Result
            );
        }
    }
}




