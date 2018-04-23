using GraphQL.Types;
using NtpApi.Models;
using NtpApi.Repositories;

namespace NtpApi.Types
{
    public class FixtureType : ObjectGraphType<Fixture>
    {
        public FixtureType(IFixturesRepository fixturesRepository)
        {
            Field(fixture => fixture.Id, type: typeof(StringGraphType));
            Field(fixture => fixture.Id_country, type: typeof(IntGraphType));
            Field(fixture => fixture.Id_league, type: typeof(IntGraphType));
            Field(fixture => fixture.Id_season, type: typeof(IntGraphType));
            Field(fixture => fixture.Id_stage, type: typeof(IntGraphType));
            Field(fixture => fixture.Id_referee, type: typeof(IntGraphType));
            Field(fixture => fixture.Id_team_season_away, type: typeof(IntGraphType));
            Field(fixture => fixture.Id_team_season_home, type: typeof(IntGraphType));
            Field(fixture => fixture.Number_goal_team_away, type: typeof(IntGraphType));
            Field(fixture => fixture.Number_goal_team_home, type: typeof(IntGraphType));
            Field(fixture => fixture.Spectators, type: typeof(IntGraphType));

            Field(fixture => fixture.Fixture_status_short, type: typeof(StringGraphType));
            Field(fixture => fixture.Game_ended_at, type: typeof(StringGraphType));
            Field(fixture => fixture.Referee_name, type: typeof(StringGraphType));
            Field(fixture => fixture.Round, type: typeof(StringGraphType));
            Field(fixture => fixture.Stadium, type: typeof(StringGraphType));
            Field(fixture => fixture.Team_season_home_name, type: typeof(StringGraphType));
            Field(fixture => fixture.Team_season_away_name, type: typeof(StringGraphType));
        }
    }
}
