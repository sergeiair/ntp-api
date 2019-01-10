using System;
using System.Collections;
using NtpApi.Models;

namespace NtpApi.Services.Computation
{
    public static class PredictionTune
    {
        public static bool Team1bookmakersOnWin { get; set; }
        public static bool Team1newCoach { get; set; }
        public static int Team1keyPlayersInjure { get; set; }
        public static int Team1teamMotivation { get; set; }
        public static int Team1keyStrikersForm { get; set; }
        public static int Team1winRate { get; set; }
        public static int Team1drawRate { get; set; }

        public static bool Team2bookmakersOnWin { get; set; }
        public static bool Team2newCoach { get; set; }
        public static int Team2keyPlayersInjure { get; set; }
        public static int Team2teamMotivation { get; set; }
        public static int Team2keyStrikersForm { get; set; }
        public static int Team2winRate { get; set; }
        public static int Team2drawRate { get; set; }

        private readonly static double bookmakersWeight = 1;
        private readonly static double newCoachWeight = 1;
        private readonly static double injureWeight = 0.9;
        private readonly static double strikersFormWeight = 0.5;
        private readonly static double motivationWeight = 0.3;

        public static FixturesPrediction GetTunedPrediction()
        {
            double team1Effect = GetBookmakersEffect(Team1bookmakersOnWin)
                + GetNewCoachEffect(Team1newCoach)
                + GetInjureEffect(Team1keyPlayersInjure)
                + GetMotivationEffect(Team1teamMotivation)
                + GetStrikersFormEffect(Team1keyStrikersForm);

            double team2Effect = GetBookmakersEffect(Team2bookmakersOnWin)
                + GetNewCoachEffect(Team2newCoach)
                + GetInjureEffect(Team2keyPlayersInjure)
                + GetMotivationEffect(Team2teamMotivation)
                + GetStrikersFormEffect(Team2keyStrikersForm);

            double effectsDiffForTeam = (team1Effect - team2Effect) / 2;

            return new FixturesPrediction(
                new Hashtable() {
                    {"homeWin" , Team1winRate + effectsDiffForTeam},
                    {"homeDraw", Team1drawRate + effectsDiffForTeam},
                    {"awayWin", Team2winRate - effectsDiffForTeam },
                    {"awayDraw", Team2drawRate - effectsDiffForTeam }
                }
           );
        }

        private static double GetBookmakersEffect(bool winPredicted)
        {
            Random rand = new Random();

            return winPredicted
                ? rand.Next(1, 3) * bookmakersWeight
                : rand.Next(-3, -1) * bookmakersWeight;
        }

        private static double GetNewCoachEffect(bool isNew)
        {
            if (isNew)
            {
                Random rand = new Random();

                return rand.Next(2, 4) * newCoachWeight;
            }
            else
            {
                return 0;
            }
        }

        private static double GetInjureEffect(int injureLevel)
        {
            if (injureLevel > 0)
            {
                Random rand = new Random();

                return rand.Next(-10, injureLevel * -1) * injureWeight;
            } else
            {
                return 0;
            }
        }

        private static double GetMotivationEffect(int motivationLevel)
        {
            Random rand = new Random();

            return motivationLevel > 5
                ? rand.Next(1, motivationLevel) * strikersFormWeight
                : rand.Next(-5 + motivationLevel, 0) * strikersFormWeight;
        }

        private static double GetStrikersFormEffect(int formLevel)
        {
            Random rand = new Random();

            return formLevel > 5
                ? rand.Next(0, formLevel) * motivationWeight
                : rand.Next(-5 + formLevel, 0) * motivationWeight;
        }
    }
}
