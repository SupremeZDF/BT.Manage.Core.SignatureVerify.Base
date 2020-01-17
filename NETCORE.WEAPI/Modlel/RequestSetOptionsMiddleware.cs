using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace NETCORE.WEAPI
{
    public class RequestSetOptionsMiddleware
    {
        public readonly RequestDelegate _next;

        public RequestSetOptionsMiddleware(RequestDelegate next)  
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext) 
        {
            StringValues option = httpContext.Request.Query["option"];

            if (!string.IsNullOrWhiteSpace(option))
            {
                httpContext.Items["option"] = WebUtility.HtmlEncode(option);
            }

            await _next(httpContext);
        }
    }
}
