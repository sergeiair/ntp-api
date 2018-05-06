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
            ITeamsCollectionRepository<Fixture> fixturesRepository,
            ICollectionRepository<Season> seasonsRepository
        )    
        {
            Field<CountryType>
            (
                "country",
                arguments: new QueryArguments
                (
                    new QueryArgument<NonNullGraphType<StringGraphType>>
                        { Name = "code", Description = "Country code" }
                ),
                resolve: context => countriesRepository
                    .GetItemAsync("Code", context.GetArgument<string>("code"))
                    .Result
            );
            
            Field<ListGraphType<CountryType>>
            (
                "countries",
                resolve: context => countriesRepository
                    .GetItemsAsync()
                    .Result
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
                    .GetItemByIdAsync(context.GetArgument<ObjectId>("id"))
                    .Result
            );

            Field<ListGraphType<SeasonType>>
            (
                "seasons",
                resolve: context => seasonsRepository
                    .GetItemsAsync()
                    .Result
            );

            Field<ListGraphType<FixtureType>>
            (
                "teamsFixtures",
                arguments: new QueryArguments
                (
                    new QueryArgument<NonNullGraphType<StringGraphType>> 
                        { Name = "team1", Description = "Team 1 name" },
                    new QueryArgument<NonNullGraphType<StringGraphType>>
                        { Name = "team2", Description = "Team 2 name" }
                ),
                resolve: context => fixturesRepository
                    .GetItemsByTeamsAsync(
                        context.GetArgument<string>("team1"),
                        context.GetArgument<string>("team2")
                    ).Result
            );

            Field<ListGraphType<FixtureType>>
            (
                "fixtures",
                    resolve: context => fixturesRepository
                        .GetItemsAsync()
                        .Result
            );
        }
    }
}




