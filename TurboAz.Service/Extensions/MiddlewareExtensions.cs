using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboAz.Service.Infrastructure;

namespace TurboAz.Service.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseRateLimiting(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RateLimitingMiddleware>();
        }
    }
}
