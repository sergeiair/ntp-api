using System.Collections.Generic;
using NtpApi.Models;

namespace NtpApi.Services.Computation
{
    public static class FixturesComputation
    {
     
        public static FixturesPrediction GetPrediction
            (string firstTeamName, string secondTeamName, List<Fixture> items)
        {
            double firstTeamPoints = 0;
            double secondTeamPoints = 0;
            
            foreach (var fixtureItem in items)
            {
                firstTeamPoints += GetRateByGoals(firstTeamName, fixtureItem);
                secondTeamPoints += GetRateByGoals(secondTeamName, fixtureItem);
            }  
    
            return new FixturesPrediction(firstTeamPoints / secondTeamPoints);
        }


        private static double GetRateByGoals (string teamName, Fixture fixture)
        {
            var firstTeamAtHome = fixture.Team_season_home_name == teamName;
            var goalsDiff = fixture.Number_goal_team_home - fixture.Number_goal_team_away;

            if (goalsDiff > 0)
            {
                return ComputationConfig.GetTeamWinPoints(firstTeamAtHome, goalsDiff >= 3);
            }
            
            return goalsDiff == 0 
                ? ComputationConfig.GetTeamDrawPoints(firstTeamAtHome)
                : ComputationConfig.GetTeamLossPoints();
        }
    }
}