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
                        firstTeam,
                        secondTeam,
                        fixtures.ToArray()
                    );
                }
            );

            Field<FixturesPredictionType>
            (
                "predictionTune",
                arguments: new QueryArguments
                (
                    new QueryArgument<NonNullGraphType<IntGraphType>>
                        { Name = "team1winRate", Description = "Team 1, win rate" },
                    new QueryArgument<NonNullGraphType<IntGraphType>>
                        { Name = "team1drawRate", Description = "Team 1, draw rate" },
                    new QueryArgument<NonNullGraphType<IntGraphType>>
                        { Name = "team1keyPlayersInjure", Description = "Team 1, key players injure" },
                    new QueryArgument<NonNullGraphType<IntGraphType>>
                        { Name = "team1teamMotivation", Description = "Team 1, team motivation" },
                    new QueryArgument<NonNullGraphType<IntGraphType>>
                        { Name = "team1keyStrikersForm", Description = "Team 1, key strikers form" },
                    new QueryArgument<NonNullGraphType<BooleanGraphType>>
                        { Name = "team1bookmakersOnWin", Description = "Team 1, bookmakers put on win" },
                    new QueryArgument<NonNullGraphType<BooleanGraphType>>
                        { Name = "team1newCoach", Description = "Team 1, new coach" },
                    new QueryArgument<NonNullGraphType<IntGraphType>>
                        { Name = "team2winRate", Description = "Team 2, win rate" },
                    new QueryArgument<NonNullGraphType<IntGraphType>>
                        { Name = "team2drawRate", Description = "Team 2, draw rate" },
                    new QueryArgument<NonNullGraphType<IntGraphType>>
                        { Name = "team2keyPlayersInjure", Description = "Team 2, key players injure" },
                    new QueryArgument<NonNullGraphType<IntGraphType>>
                        { Name = "team2teamMotivation", Description = "Team 2, team motivation" },
                    new QueryArgument<NonNullGraphType<IntGraphType>>
                        { Name = "team2keyStrikersForm", Description = "Team 2, key strikers form" },
                    new QueryArgument<NonNullGraphType<BooleanGraphType>>
                        { Name = "team2bookmakersOnWin", Description = "Team 2, bookmakers put on win" },
                    new QueryArgument<NonNullGraphType<BooleanGraphType>>
                        { Name = "team2newCoach", Description = "Team 2, new coach" }
                ),
                resolve: context =>
                {
                    PredictionTune.Team1winRate = context.GetArgument<int>("team1winRate");
                    PredictionTune.Team1drawRate = context.GetArgument<int>("team1drawRate");

                    PredictionTune.Team1keyPlayersInjure = context.GetArgument<int>("team1keyPlayersInjure");
                    PredictionTune.Team1teamMotivation = context.GetArgument<int>("team1teamMotivation");
                    PredictionTune.Team1keyStrikersForm = context.GetArgument<int>("team1keyStrikersForm");
                    PredictionTune.Team1bookmakersOnWin = context.GetArgument<bool>("team1bookmakersOnWin");
                    PredictionTune.Team1newCoach = context.GetArgument<bool>("team1newCoach");

                    PredictionTune.Team2winRate = context.GetArgument<int>("team2winRate");
                    PredictionTune.Team2drawRate = context.GetArgument<int>("team2drawRate");

                    PredictionTune.Team2keyPlayersInjure = context.GetArgument<int>("team2keyPlayersInjure");
                    PredictionTune.Team2teamMotivation = context.GetArgument<int>("team2teamMotivation");
                    PredictionTune.Team2keyStrikersForm = context.GetArgument<int>("team2keyStrikersForm");
                    PredictionTune.Team2bookmakersOnWin = context.GetArgument<bool>("team2bookmakersOnWin");
                    PredictionTune.Team2newCoach = context.GetArgument<bool>("team2newCoach");

                    return PredictionTune.GetTunedPrediction();
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




