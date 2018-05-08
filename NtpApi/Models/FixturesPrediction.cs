namespace NtpApi.Models
{
    public class FixturesPrediction
    {
        public FixturesPrediction(double rate)
        {
            Rate = rate;
        }
        
        public double Rate { get; }
    }
}