namespace Shared.Util
{
    using Serilog;
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="ApiHelper" />.
    /// </summary>
    public class ApiHelper
    {
        /// <summary>
        /// The GetHttpClient.
        /// </summary>
        /// <returns>The <see cref="HttpClient"/>.</returns>
        public HttpClient GetHttpClient()
        {
            HttpClient cli = new HttpClient();
            cli.Timeout = new TimeSpan(0, 0, 30);
            BuildHeaders(cli);
            cli.DefaultRequestHeaders.Accept.Clear();
            cli.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            return cli;
        }

        /// <summary>
        /// The BuildHeaders.
        /// </summary>
        /// <param name="_cli">The _cli<see cref="HttpClient"/>.</param>
        public void BuildHeaders(HttpClient _cli)
        {
            _cli.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            _cli.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
        }

        /// <summary>
        /// The GetAsync.
        /// </summary>
        /// <param name="url">The url<see cref="string"/>.</param>
        /// <returns>The <see cref="Task{HttpResponseMessage}"/>.</returns>
        private async Task<HttpResponseMessage> GetAsync(string url)
        {
            try
            {
                using (var cli = GetHttpClient())
                {
                    ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslpolicyerror) => true;
                    HttpResponseMessage resp = await cli.GetAsync(url);

                    return resp;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Get " + url);
                return null;
            }
        }

        /// <summary>
        /// The Get.
        /// </summary>
        /// <param name="url">The url<see cref="string"/>.</param>
        /// <returns>The <see cref="HttpResponseMessage"/>.</returns>
        public HttpResponseMessage Get(string url)
        {
            var task = Task.Run(() => { return GetAsync(url); });
            task.Wait();
            var rst = task.Result;
            return rst;
        }

        /// <summary>
        /// The PostAsync.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="Data">The Data<see cref="T"/>.</param>
        /// <param name="url">The url<see cref="string"/>.</param>
        /// <returns>The <see cref="Task{HttpResponseMessage}"/>.</returns>
        private async Task<HttpResponseMessage> PostAsync<T>(T Data, string url)
        {
            try
            {
                HttpResponseMessage resp = null;
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslpolicyerror) => true;

                using (var cli = GetHttpClient())
                {
                    if (typeof(T) == typeof(string))
                    {
                        var cnt = new StringContent((string)(object)Data, System.Text.Encoding.UTF8, "text/json");
                        resp = await cli.PostAsync(url, cnt);
                    }
                    return resp;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "PostAsync " + url);
                return null;
            }
        }

        /// <summary>
        /// The Post.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        /// <param name="Data">The Data<see cref="T"/>.</param>
        /// <param name="url">The url<see cref="string"/>.</param>
        /// <returns>The <see cref="HttpResponseMessage"/>.</returns>
        public HttpResponseMessage Post<T>(T Data, string url)
        {
            var task = Task.Run(() => { return PostAsync(Data, url); });
            task.Wait();
            var rst = task.Result;
            return rst;
        }
    }
}