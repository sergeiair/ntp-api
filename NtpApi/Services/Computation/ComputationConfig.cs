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
                ? HomeTeamDrawPoints : AwayTeamDrawPoints;
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
    }
}