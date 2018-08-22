using System.Collections;
using System.Collections.Generic;
using NtpApi.Models;

namespace NtpApi.Services.Computation
{
    public static class FixturesComputation
    {

        public static FixturesPrediction GetPrediction
            (string firstTeamName, IEnumerable<Fixture> fixtures)
        {
            double[] teamsPoints = GetCalculatedPoints(firstTeamName, fixtures);

            return new FixturesPrediction(new Hashtable() {
                { "homeWin" ,  ComputationConfig.GetHomeWinChance(teamsPoints[0]) },
                { "homeDraw" ,  ComputationConfig.GetHomeDrawChance(teamsPoints[0]) },
                { "homeLoss" ,  ComputationConfig.GetHomeLossChance(teamsPoints[0]) },
                { "awayWin" ,  ComputationConfig.GetAwayWinChance(teamsPoints[1]) },
                { "awayDraw" ,  ComputationConfig.GetAwayDrawChance(teamsPoints[1]) },
                { "awayLoss" ,  ComputationConfig.GetAwayLossChance(teamsPoints[1]) }
            });
        }

        private static double[] GetCalculatedPoints(string firstTeamName, IEnumerable<Fixture> fixtures)
        {
            double firstTeamPonints = 0.1;
            double secondTeamPonints = 0.1;
            int fixtruesNumber = 0;

            foreach (var fixture in fixtures)
            {
                bool isFirstTeamAtHome = fixture.Team_season_home_name.Contains(firstTeamName);

                fixtruesNumber++;
                firstTeamPonints += GetPointsByGoals(fixture, isFirstTeamAtHome);
                secondTeamPonints += GetPointsByGoals(fixture, !isFirstTeamAtHome);
            }

            return new double[2]
            {
                secondTeamPonints / fixtruesNumber,
                secondTeamPonints / fixtruesNumber,
            };
        }

        private static double GetPointsByGoals (Fixture fixture, bool teamAtHome)
        {
            int goalsDiff = teamAtHome
                ? fixture.Number_goal_team_home - fixture.Number_goal_team_away
                : fixture.Number_goal_team_away - fixture.Number_goal_team_home; 
            
            if (goalsDiff > 0)
            {
                return ComputationConfig.GetTeamWinPoints(teamAtHome, goalsDiff >= 3);
            } else {
                return goalsDiff == 0
                    ? ComputationConfig.GetTeamDrawPoints(teamAtHome)
                    : ComputationConfig.GetTeamLossPoints();
            }
        }
    }
}