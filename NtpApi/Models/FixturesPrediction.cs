using System.Collections;

namespace NtpApi.Models
{
    public class FixturesPrediction
    {
        public FixturesPrediction(Hashtable rates)
        {
            Rates = rates;
        }
        
        public Hashtable Rates { get; }
    }
}