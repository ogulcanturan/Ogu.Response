using System.Collections.Generic;

namespace Ogu.AspNetCore.Response.Abstractions
{
    public static class Constants
    {
        public static readonly HashSet<int> NoResponseStatusCodes = new HashSet<int> { 204, 205, 304 };
    }
}