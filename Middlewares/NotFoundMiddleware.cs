using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Middlewares
{
    public class NotFoundMiddleware
    {
        private readonly RequestDelegate _next;

        public NotFoundMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Invokes the async.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>A Task.</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);

            if (context.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                context.Response.Headers.Add("Author", "ItzTek");
                await context.Response.WriteAsync("Welcome to ItzTek");
            }
        }
    }

    public static class NotFoundMiddlewareExtensions
    {
        public static void UseNotFoundMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<NotFoundMiddleware>();
        }
    }
}