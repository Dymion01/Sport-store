using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Sport_store.Infrastructure
{
    public static class UrlExtensions
    {
        public static string PathAndQuery(this HttpRequest reguest) =>
            reguest.QueryString.HasValue ? $"{reguest.Path}{reguest.QueryString}" : reguest.Path.ToString();
    }
}
