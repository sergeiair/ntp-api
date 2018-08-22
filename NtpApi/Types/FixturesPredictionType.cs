using GraphQL.Types;
using NtpApi.Models;

namespace NtpApi.Types
{
    public class FixturesPredictionType : ObjectGraphType<FixturesPrediction>
    {
        public FixturesPredictionType()
        {
            Field(prediction => prediction.Rates, type: typeof(StringGraphType));
        }
    }
}