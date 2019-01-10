using System;

namespace NtpApi.Services.Computation
{
    public partial class RankingsHelper
    {
        public static int GetTeamClass(string teamName)
        {
            if (Array.Exists(PrimeTeams, n => n.Contains(teamName.ToLower())))
            {
                return 4;
            }
            else if (Array.Exists(TopTeams, n => n.Contains(teamName.ToLower())))
            {
                return 3;
            }
            else if (Array.Exists(MidTeams, n => n.Contains(teamName.ToLower())))
            {
                return 2;
            }
            else if (Array.Exists(AverageTeams, n => n.Contains(teamName.ToLower())))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
