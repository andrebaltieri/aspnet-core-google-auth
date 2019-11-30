using System;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GAuth.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace GAuth.Middlewares
{
    public class GoogleAuthMiddleware
    {
        private readonly RequestDelegate _next;

        public GoogleAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"];

            if (!string.IsNullOrEmpty(token))
            {
                var user = TokenService.GetTokenInfo(token);
                var identity = new ClaimsIdentity();
                identity.AddClaim(new Claim(ClaimTypes.Name, user.Email));
                Thread.CurrentPrincipal = new ClaimsPrincipal(identity);
            }
            else
            {
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync("{ \"message\":  \"Token inválido\" }", Encoding.UTF8);
            }

            await _next(context);
        }
    }

    public static class GoogleAuthMiddlewareExtensions
    {
        public static IApplicationBuilder UseGoogleAuth(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GoogleAuthMiddleware>();
        }
    }
}