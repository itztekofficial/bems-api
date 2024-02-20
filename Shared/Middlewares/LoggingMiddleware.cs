namespace Shared.Middlewares
{
    using Microsoft.AspNetCore.Http;
    using Serilog;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="LoggingMiddleware" />.
    /// </summary>
    public class LoggingMiddleware
    {
        /// <summary>
        /// Defines the next.
        /// </summary>
        private readonly RequestDelegate next;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggingMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next<see cref="RequestDelegate"/>.</param>
        public LoggingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        /// <summary>
        /// The Invoke.
        /// </summary>
        /// <param name="context">The context<see cref="HttpContext"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                context.Response.OnStarting(state =>
                {
                    var ctx = (HttpContext)state;

                    return Task.FromResult(0);
                }, context);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
            }

            await next(context);
        }
    }
}