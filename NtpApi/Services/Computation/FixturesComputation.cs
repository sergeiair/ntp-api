using System.Collections.Generic;
using NtpApi.Models;

namespace NtpApi.Services.Computation
{
    public static class FixturesComputation
    {
     
        public static FixturesPrediction GetPrediction
            (string firstTeamName, string secondTeamName, IEnumerable<Fixture> items)
        {
            double firstTeamPoints = 0;
            double secondTeamPoints = 0;
            
            foreach (var fixtureItem in items)
            {
                firstTeamPoints += GetPointsByGoals(firstTeamName, fixtureItem);
                secondTeamPoints += GetPointsByGoals(secondTeamName, fixtureItem);
            }  
            
            return new FixturesPrediction(firstTeamPoints / secondTeamPoints);
        }


        private static double GetPointsByGoals (string teamName, Fixture fixture)
        {
            var teamAtHome = fixture.Team_season_home_name.Contains(teamName);
            var goalsDiff = teamAtHome
                ? fixture.Number_goal_team_home - fixture.Number_goal_team_away
                : fixture.Number_goal_team_away - fixture.Number_goal_team_home; 
            
            if (goalsDiff > 0)
            {
                return ComputationConfig.GetTeamWinPoints(teamAtHome, goalsDiff >= 3);
            }
            
            return goalsDiff == 0 
                ? ComputationConfig.GetTeamDrawPoints(teamAtHome)
                : ComputationConfig.GetTeamLossPoints();
        }
    }
}