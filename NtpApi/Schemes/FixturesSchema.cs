using System;
using GraphQL.Types;
using NtpApi.Queries;

namespace Ntp.Schemes
{
    public class FixturesSchema : Schema
    {
        public FixturesSchema(Func<Type, GraphType> resolveType)
            : base(resolveType)
        {
            Query = (FixturesQuery)resolveType(typeof(FixturesQuery));
        }
    }
}