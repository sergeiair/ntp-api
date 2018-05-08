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

        public static double GetTeamDrawPoints(bool isAway)
        {
            return isAway
                ? AwayTeamDrawPoints : HomeTeamDrawPoints;
        }
        
        public static double GetTeamWinPoints(bool isAway, bool bigDiff)
        {
            return isAway
                ? bigDiff ? AwayTeamBigVictoryPoints : AwayTeamVictoryPoints
                : bigDiff ? HomeTeamBigVictoryPoints : HomeTeamVictoryPoints;
        }
        
        public static double GetTeamLossPoints()
        {
            return TeamLossPoints;
        }
    }
}