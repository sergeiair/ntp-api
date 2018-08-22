using System.Linq;
using GraphQL.Types;
using NtpApi.Models;
using NtpApi.Repositories;
using NtpApi.Services.Computation;
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
                    .GetItemAsync("code", context.GetArgument<string>("code"))
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
                    new QueryArgument<NonNullGraphType<IntGraphType>>
                        { Name = "idSeason", Description = "Season id" }
                ),
                resolve: context => seasonsRepository
                    .GetItemAsync("Id_season", context.GetArgument<int>("idSeason"))
                    .Result
            );

            Field<ListGraphType<SeasonType>>
            (
                "seasons",
                resolve: context => seasonsRepository
                    .GetItemsAsync()
                    .Result
            );

            Field<FixturesPredictionType>
            (
                "teamsPrediction",
                arguments: new QueryArguments
                (
                    new QueryArgument<NonNullGraphType<StringGraphType>> 
                        { Name = "team1", Description = "Team 1 name" },
                    new QueryArgument<NonNullGraphType<StringGraphType>>
                        { Name = "team2", Description = "Team 2 name" }
                ),
                resolve: context =>
                {
                    var firstTeam = context.GetArgument<string>("team1");
                    var secondTeam = context.GetArgument<string>("team2");
                    var fixtures = fixturesRepository
                        .GetItemsByTeamsAsync(firstTeam, secondTeam)
                        .Result;        
                    
                    return FixturesComputation.GetPrediction(
                        firstTeam, fixtures.ToList()
                    );
                }
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




