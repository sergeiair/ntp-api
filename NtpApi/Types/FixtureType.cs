using GraphQL.Types;
using NtpApi.Models;
using NtpApi.Repositories;

namespace NtpApi.Types
{
    public class FixtureType : ObjectGraphType<Fixture>
    {
        public FixtureType(IFixturesRepository fixturesRepository)
        {
            Field(fixture => fixture.Id, type: typeof(ObjectGraphType<FixtureType>)).Description("Fixture id.");
            Field(fixture => fixture.Id_country,type: typeof(ObjectGraphType<FixtureType>)).Description("Fixture country id.");
        }
    }
}
