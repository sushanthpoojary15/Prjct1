
using H2H.Physiotherapy.Services.Request;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace H2H.Physiotherapy.API.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class RequestContextMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext, IRequestContext requestContext)
        {
           
            var userId = httpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var role = httpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
            var username = httpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            requestContext.UserRole = role;
            requestContext.UserId = userId;
            requestContext.UserName = username;
            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.

}
