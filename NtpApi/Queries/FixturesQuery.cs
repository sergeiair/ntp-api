using GraphQL.Types;
using MongoDB.Bson;
using NtpApi.Repositories;
using NtpApi.Types;

namespace NtpApi.Queries
{
    public class FixturesQuery : ObjectGraphType
    {
        public FixturesQuery(IFixturesRepository fixturesRepository)
        {
            Field<FixtureType>
            (
                "fixture",
                arguments: new QueryArguments
                (
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id", Description = "Fixture id" }
                ),
                resolve: context => fixturesRepository.GetFixtureAsync(context.GetArgument<ObjectId>("id")).Result
            );

            Field<ListGraphType<FixtureType>>
            (
                "fixtures",
                resolve: context => fixturesRepository.GetFixturesAsync().Result
            );
        }
    }
}
