namespace NtpApi.Services.Computation
{
    public static class ComputationConfig
    {
        private const double HomeTeamVictoryPoints = 2;
        private const double AwayTeamVictoryPoints = 2.2;

        private const double HomeTeamBigVictoryPoints = 2.2;
        private const double AwayTeamBigVictoryPoints = 3;
        
        private const double HomeTeamDrawPoints = 1;
        private const double AwayTeamDrawPoints = 1.2;
        
        private const double TeamLossPoints = 0;

        public static double GetTeamDrawPoints(bool atHome)
        {
            return atHome
                ? HomeTeamDrawPoints
                : AwayTeamDrawPoints;
        }
        
        public static double GetTeamWinPoints(bool atHome, bool bigDiff)
        {
            return atHome
                ? bigDiff ? HomeTeamBigVictoryPoints : HomeTeamVictoryPoints
                : bigDiff ? AwayTeamBigVictoryPoints : AwayTeamVictoryPoints;
        }
        
        public static double GetTeamLossPoints()
        {
            return TeamLossPoints;
        }

        public static double GetHomeWinChance(double avPoints)
        {
            return 100 / (HomeTeamVictoryPoints / avPoints);
        }

        public static double GetHomeDrawChance(double avPoints)
        {
            return ((100 / (100 / (HomeTeamDrawPoints / avPoints))) * 100) * 0.5;
        }

        public static double GetHomeLossChance(double avPoints)
        {
            return 100 - (100 / (HomeTeamVictoryPoints / avPoints));
        }

        public static double GetAwayWinChance(double avPoints)
        {
            return 100 / (AwayTeamVictoryPoints / avPoints);
        }

        public static double GetAwayDrawChance(double avPoints)
        {
            return ((100 / (100 / (AwayTeamDrawPoints / avPoints))) * 100) * 0.5;
        }

        public static double GetAwayLossChance(double avPoints)
        {
            return 100 - (100 / (AwayTeamVictoryPoints / avPoints));
        }

    }
}