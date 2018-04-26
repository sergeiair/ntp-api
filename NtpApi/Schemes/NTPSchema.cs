using System;
using GraphQL.Types;
using NtpApi.Queries;

namespace Ntp.Schemes
{
    public class NTPSchema : Schema
    {
        public NTPSchema(Func<Type, GraphType> resolveType) : base(resolveType)
        {
            Query = (NTPQuery)resolveType(typeof(NTPQuery));
        }
    }
}