namespace Shared.Middlewares
{
    using Microsoft.AspNetCore.Http;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="SecurityHeadersMiddleware" />.
    /// </summary>
    public class SecurityHeadersMiddleware
    {
        /// <summary>
        /// Defines the next.
        /// </summary>
        private readonly RequestDelegate next;

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityHeadersMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next<see cref="RequestDelegate"/>.</param>
        public SecurityHeadersMiddleware(RequestDelegate next)
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
            context.Response.OnStarting(state =>
            {
                var ctx = (HttpContext)state;

                if (!ctx.Response.Headers.ContainsKey("Arr-Disable-Session-Affinity"))
                {
                    ctx.Response.Headers.Add("Arr-Disable-Session-Affinity", "True"); // Disables the Azure ARRAffinity cookie
                }

                if (ctx.Response.Headers.ContainsKey("Server"))
                {
                    ctx.Response.Headers.Remove("Server"); // For security reasons
                }

                if (ctx.Response.Headers.ContainsKey("x-powered-by") || ctx.Response.Headers.ContainsKey("X-Powered-By"))
                {
                    ctx.Response.Headers.Remove("x-powered-by");
                    ctx.Response.Headers.Remove("X-Powered-By");
                }

                if (!ctx.Response.Headers.ContainsKey("X-Frame-Options"))
                {
                    ctx.Response.Headers.Add("X-Frame-Options", "DENY");
                }

                if (!ctx.Response.Headers.ContainsKey("X-Xss-Protection"))
                {
                    ctx.Response.Headers.Add("X-Xss-Protection", "1; mode=block");
                }

                if (!ctx.Response.Headers.ContainsKey("X-Content-Type-Options"))
                {
                    ctx.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                }

                if (!ctx.Response.Headers.ContainsKey("Referrer-Policy"))
                {
                    ctx.Response.Headers.Add("Referrer-Policy", "no-referrer");
                }

                if (!ctx.Response.Headers.ContainsKey("X-Permitted-Cross-Domain-Policies"))
                {
                    ctx.Response.Headers.Add("X-Permitted-Cross-Domain-Policies", "none");
                }

                if (!ctx.Response.Headers.ContainsKey("Feature-Policy"))
                {
                    ctx.Response.Headers.Add("Feature-Policy", "accelerometer 'none'; camera 'none'; geolocation 'none'; gyroscope 'none'; magnetometer 'none'; microphone 'none'; payment 'none'; usb 'none'");
                }

                if (!ctx.Response.Headers.ContainsKey("Content-Security-Policy"))
                {
                    ctx.Response.Headers.Add("Content-Security-Policy", "default-src 'self'");
                }

                return Task.FromResult(0);
            }, context);

            await next(context);
        }
    }
}