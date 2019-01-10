using System;
using System.Collections;
using System.Collections.Generic;
using NtpApi.Models;

namespace NtpApi.Services.Computation
{
    public static class FixturesComputation
    {

        public static FixturesPrediction GetPrediction
            (string firstTeamName, string secondTeamName, IList<Fixture> fixtures)
        {
            Hashtable combinedCalculation = CombineResults(
                CalculateByGoals(firstTeamName, fixtures),
                Simulate(firstTeamName, fixtures.Count),
                CalculateByRanking(firstTeamName, secondTeamName)
            );

            return new FixturesPrediction(combinedCalculation);
        }

        private static Hashtable CombineResults(int[] resultsHist, int[] resultsSim, int[] resultsRank)
        {
            int histWeight = 8;
            int simWeight = 1;
            int rankWeight = 10;
            int overallWeight = histWeight + simWeight + rankWeight;

            int rawHomeWinRate = (resultsHist[0] * histWeight) + (resultsSim[0] * simWeight) + (resultsRank[0] * rankWeight);
            int rawHomeDrawRate = (resultsHist[1] * histWeight) + (resultsSim[1] * simWeight) + (resultsRank[1] * rankWeight);
            int rawAwayWinRate = (resultsHist[2] * histWeight) + (resultsSim[2] * simWeight) + (resultsRank[2] * rankWeight);
            int rawAwayDrawRate = (resultsHist[3] * histWeight) + (resultsSim[3] * simWeight) + (resultsRank[3] * rankWeight);

            return new Hashtable() {
                {"homeWin" , rawHomeWinRate / overallWeight},
                {"homeDraw", rawHomeDrawRate  / overallWeight},
                {"awayWin", rawAwayWinRate / overallWeight },
                {"awayDraw", rawAwayDrawRate / overallWeight }
            };
        }

        private static int[] CalculateByRanking(string firstTeamName, string secondTeamName)
        {
            int firstTeamClass = RankingsHelper.GetTeamClass(firstTeamName);
            int secondTeamClass = RankingsHelper.GetTeamClass(secondTeamName);
            
            switch (firstTeamClass - secondTeamClass)
            {
                case 1:
                    return new int[4] { 53, 53, 47, 47 };
                case -1:
                    return new int[4] { 47, 47, 53, 53 };
                case 2:
                    return new int[4] { 57, 57, 43, 43 };
                case -2:
                    return new int[4] { 43, 43, 57, 57 };
                case 3:
                    return new int[4] { 67, 67, 43, 43 };
                case -3:
                    return new int[4] { 43, 43, 67, 67 };
                case 4:
                    return new int[4] { 80, 80, 20, 20 };
                case -4:
                    return new int[4] { 20, 20, 80, 80 };
                default:
                    return new int[4] { 50, 50, 50, 50 };
            }
        }

        private static int[] Simulate(string firstTeamName, int fixturesNumber)
        {
            int firstTeamWinRate = 0;
            int firstTeamDrawRate = 0;
            int iterationsNumber = fixturesNumber < 5 ? 1 : 2;

            for (int i = 0; i <= iterationsNumber; i++)
            {
                int goalsDiff = SimulateTeamScore() - SimulateTeamScore();

                firstTeamWinRate += GoalsHelper.GetWinRate(true, goalsDiff);
                firstTeamDrawRate += GoalsHelper.GetDrawRate(true, goalsDiff);
            }

            return new int[4] {
                firstTeamWinRate  / iterationsNumber,
                firstTeamDrawRate / iterationsNumber,
                100 - (firstTeamWinRate  / iterationsNumber),
                100 - (firstTeamDrawRate  / iterationsNumber)
            };

        }

        private static int SimulateTeamScore()
        {
            Random rand = new Random();
            int goals = 0;
            int numberOfActions = 270;

            while (numberOfActions > 0)
            {
                int score = 0;

                for (int i = 0; i < 7; i++)
                {
                    score += rand.Next(1, 3);
                }

                if (score == 14)
                {
                    goals += 1;
                }

                numberOfActions -= 1;
            }

            return goals;
        }

        private static int[] CalculateByGoals(string firstTeamName, IList<Fixture> fixtures)
        {
            int firstTeamWinRate = 0;
            int firstTeamDrawRate = 0;

            foreach (var fixture in fixtures)
            {
                bool playsAthome = fixture.Team_season_home_name.Contains(firstTeamName);
                int goalsDiff = playsAthome
                    ? fixture.Number_goal_team_home - fixture.Number_goal_team_away
                    : fixture.Number_goal_team_away - fixture.Number_goal_team_home;

                firstTeamWinRate += GoalsHelper.GetWinRate(playsAthome, goalsDiff);
                firstTeamDrawRate += GoalsHelper.GetDrawRate(playsAthome, goalsDiff);
            }

            return new int[4] {
                firstTeamWinRate  / fixtures.Count,
                firstTeamDrawRate / fixtures.Count,
                100 - (firstTeamWinRate  / fixtures.Count),
                100 - (firstTeamDrawRate  / fixtures.Count)
            };
        }
    }
}