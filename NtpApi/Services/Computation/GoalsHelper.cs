namespace NtpApi.Services.Computation
{
    public static class GoalsHelper
    {
        private static readonly int BigChance = 75;
        private static readonly int GoodChance = 65;
        private static readonly int MidChance = 50;
        private static readonly int WeakChance = 35;
        private static readonly int LowChance = 25;

        public static int GetWinRate(bool playsAtHome, int goalsDiff)
        {
            int awayCorrection = playsAtHome ? 0 : 10;

            if (goalsDiff > 3)
            {
                return BigChance + awayCorrection;
            }
            else if (goalsDiff > 0)
            {
                return GoodChance + awayCorrection;
            }
            else if (goalsDiff == 0)
            {
                return WeakChance + awayCorrection;
            }
            else
            {
                return LowChance + awayCorrection;
            }
        }

        public static int GetDrawRate(bool playsAtHome, int goalsDiff)
        {
            int awayCorrection = playsAtHome ? 0 : 10;

            if (goalsDiff > 3)
            {
                return LowChance + (awayCorrection / 2);
            }
            else if (goalsDiff > 0)
            {
                return WeakChance + (awayCorrection / 2);
            }
            else if (goalsDiff == 0)
            {
                return MidChance + (awayCorrection / 2);
            }
            else
            {
                return LowChance + (awayCorrection / 2);
            }
        }
    }
}