using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Core.Util
{
    /// <summary>
    /// Class helps to call api
    /// </summary>
    public enum EnumAPIHeader
    {
        None = 0,
        AuthHeader = 1,
        DataHeader = 2,
        CustomHeader = 3
    }

    /// <summary>
    /// The Api client.
    /// </summary>
    public class APIClient
    {
        /// <summary>
        /// Gets or sets the application key.
        /// </summary>
        public string ApplicationKey { get; set; } = "";

        /// <summary>
        /// Gets or sets the secret key.
        /// </summary>
        public string SecretKey { get; set; } = "";

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string UserName { get; set; } = "";

        /// <summary>
        /// Gets or sets the auth token.
        /// </summary>
        public string AuthToken { get; set; } = "";

        /// <summary>
        /// Gets or sets the app version.
        /// </summary>
        public string AppVersion { get; set; } = "";

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password { get; set; } = "";

        /// <summary>
        /// Gets or sets the url.
        /// </summary>
        private string url { get; set; }

        private EnumAPIHeader isHeader;
        private Dictionary<string, string> HeaderData;

        /// <summary>
        /// Gets or sets the base u r l.
        /// </summary>
        private string BaseURL { get; set; } = string.Empty;

        /// <summary>
        /// Posts the rest async.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>A Task.</returns>
        private async Task<IRestResponse> PostRestAsync<TInput>(TInput data)
        {
            var client = new RestClient(BaseURL);
            var request = new RestRequest(url, Method.POST)
            {
                RequestFormat = DataFormat.Json
            };
            request.AddJsonBody(data);

            var response = await client.ExecuteAsync(request);
            return response;
        }

        /// <summary>
        /// Posts the rest.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>An IRestResponse.</returns>
        public IRestResponse PostRest<TInput>(TInput data)
        {
            var _task = Task.Run(() => PostRestAsync<TInput>(data));
            _task.Wait();
            return _task.Result;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="APIClient"/> class.
        /// </summary>
        /// <param name="_isHeader">The _is header.</param>
        /// <param name="_BaseURL">The _ base u r l.</param>
        public APIClient(EnumAPIHeader _isHeader, string _BaseURL)
        {
            isHeader = _isHeader;
            BaseURL = _BaseURL;
            if (string.IsNullOrWhiteSpace(BaseURL) == true)
                throw new SystemException("Please define Base URL.");
            if (_isHeader == EnumAPIHeader.CustomHeader)
            {
                if (HeaderData == null)
                    throw new SystemException("Please add custom header values.");
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="APIClient"/> class.
        /// </summary>
        /// <param name="_isHeader">The _is header.</param>
        /// <param name="_BaseURL">The _ base u r l.</param>
        /// <param name="_HeaderKeyValue">The _ header key value.</param>
        public APIClient(EnumAPIHeader _isHeader, string _BaseURL, Dictionary<string, string> _HeaderKeyValue)
        {
            isHeader = _isHeader;
            BaseURL = _BaseURL;
            HeaderData = _HeaderKeyValue;
            if (string.IsNullOrWhiteSpace(BaseURL) == true)
                throw new SystemException("Please define Base URL.");
            if (_isHeader == EnumAPIHeader.CustomHeader)
            {
                if (HeaderData == null)
                    throw new SystemException("Please add custom header values.");
            }
        }

        /// <summary>
        /// create api call url.
        /// </summary>
        /// <param name="_suburl"></param>
        /// <param name="Parms"></param>
        public void PrepareQueryString(string _suburl, IDictionary<string, string> Parms)
        {
            url = QueryHelpers.AddQueryString(BaseURL + _suburl, Parms);
        }

        /// <summary>
        /// Prepares the query string.
        /// </summary>
        /// <param name="_suburl">The _suburl.</param>
        public void PrepareQueryString(string _suburl)
        {
            url = BaseURL + _suburl;
        }

        /// <summary>
        /// Prepares the query string rest sharp.
        /// </summary>
        /// <param name="_suburl">The _suburl.</param>
        public void PrepareQueryStringRestSharp(string _suburl)
        {
            url = _suburl;
        }

        /// <summary>
        /// get http client
        /// </summary>
        /// <param name="_cli"></param>
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
        /// Create api request header
        /// </summary>
        /// <param name="_cli"></param>
        public void BuildHeaders(HttpClient _cli)
        {
            _cli.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            _cli.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
            if (isHeader != EnumAPIHeader.None)
            {
                if (isHeader == EnumAPIHeader.CustomHeader)
                {
                    foreach (var v in HeaderData.Keys)
                    {
                        _cli.DefaultRequestHeaders.Add(v, HeaderData[v]);
                    }
                }
                else
                {
                    _cli.DefaultRequestHeaders.Add("ApplicationKey", ApplicationKey);
                    _cli.DefaultRequestHeaders.Add("SecretKey", SecretKey);
                    _cli.DefaultRequestHeaders.Add("UserName", UserName);
                    if (isHeader == EnumAPIHeader.AuthHeader)
                        _cli.DefaultRequestHeaders.Add("Password", Password);
                    else if (isHeader == EnumAPIHeader.DataHeader)
                        _cli.DefaultRequestHeaders.Add("AuthToken", AuthToken);
                    _cli.DefaultRequestHeaders.Add("AppVersion", AppVersion);
                }
            }
        }

        /// <summary>
        /// Gets the async.
        /// </summary>
        /// <returns>A Task.</returns>
        private async Task<HttpResponseMessage> GetAsync()
        {
            try
            {
                var cli = GetHttpClient();
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslpolicyerror) => true;
                HttpResponseMessage resp = await cli.GetAsync(url);
                return resp;
            }
            catch (Exception ex)
            {
                Log.WriteLog("APIHelper", "GetAsync - 2"
                    + Environment.NewLine
                    + url, ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Posts the async.
        /// </summary>
        /// <param name="Data">The data.</param>
        /// <returns>A Task.</returns>
        private async Task<HttpResponseMessage> PostAsync<T>(T Data)
        {
            try
            {
                HttpResponseMessage resp = null;
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslpolicyerror) => true;
                var cli = GetHttpClient();
                if (typeof(T) == typeof(string))
                {
                    var cnt = new StringContent((string)(object)Data, System.Text.Encoding.UTF8, "text/json");
                    resp = await cli.PostAsync(url, cnt);
                }
                else
                {
                    var cnt = new StringContent(JsonConvert.SerializeObject(Data), System.Text.Encoding.UTF8, "text/json");
                    resp = await cli.PostAsync(url, cnt);
                }
                ////Core.Util.Log.WriteLog("APIHelper", "PostAsync - 1", url, Newtonsoft.Json.JsonConvert.SerializeObject(Data));
                return resp;
            }
            catch (Exception ex)
            {
                Log.WriteLog("APIHelper", "PostAsync - 2"
                    + Environment.NewLine
                    + url
                    + Environment.NewLine
                    + JsonConvert.SerializeObject(Data), ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Puts the async.
        /// </summary>
        /// <param name="Data">The data.</param>
        /// <returns>A Task.</returns>
        private async Task<HttpResponseMessage> PutAsync<T>(T Data)
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslpolicyerror) => true;
            HttpResponseMessage resp = null;
            var cli = GetHttpClient();
            var cnt = new StringContent(JsonConvert.SerializeObject(Data), System.Text.Encoding.UTF8, "text/json");
            resp = await cli.PutAsync(url, cnt);
            return resp;
        }

        /// <summary>
        /// Patches the async.
        /// </summary>
        /// <param name="Data">The data.</param>
        /// <returns>A Task.</returns>
        private async Task<HttpResponseMessage> PatchAsync<T>(T Data)
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslpolicyerror) => true;
            HttpResponseMessage resp = null;
            var cli = GetHttpClient();
            HttpRequestMessage req = new(new HttpMethod("PATCH"), url)
            {
                Content = new StringContent(JsonConvert.SerializeObject(Data))
            };
            resp = await cli.SendAsync(req);
            return resp;
        }

        /// <summary>
        /// Deletes the async.
        /// </summary>
        /// <returns>A Task.</returns>
        private async Task<HttpResponseMessage> DeleteAsync()
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslpolicyerror) => true;
            HttpResponseMessage resp = null;
            var cli = GetHttpClient();
            resp = await cli.DeleteAsync(url);
            return resp;
        }

        /// <summary>
        /// send get request
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage Get()
        {
            var task = Task.Run(() => { return GetAsync(); });
            task.Wait();
            var rst = task.Result;
            return rst;
        }

        /// <summary>
        /// send post rwquest
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Data"></param>
        /// <returns></returns>
        public HttpResponseMessage Post<T>(T Data)
        {
            var task = Task.Run(() => { return PostAsync(Data); });
            task.Wait();
            var rst = task.Result;
            return rst;
        }

        /// <summary>
        /// send put request
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Data"></param>
        /// <returns></returns>
        public HttpResponseMessage Put<T>(T Data)
        {
            var task = Task.Run(() => { return PutAsync(Data); });
            task.Wait();
            var rst = task.Result;
            return rst;
        }

        /// <summary>
        /// send patch request
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Data"></param>
        /// <returns></returns>
        public HttpResponseMessage Patch<T>(T Data)
        {
            var task = Task.Run(() => { return PatchAsync(Data); });
            task.Wait();
            var rst = task.Result;
            return rst;
        }

        /// <summary>
        /// Send delete request
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage Delete()
        {
            var task = Task.Run(() => { return DeleteAsync(); });
            task.Wait();
            var rst = task.Result;
            return rst;
        }

        /// <summary>
        /// Builds the response.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns>An APIResult.</returns>
        public APIResult<T> BuildResponse<T>(HttpResponseMessage response)
        {
            APIResult<T> resultInfo = new APIResult<T>();
            if (response == null)
            {
                resultInfo.message = string.Empty;
                return resultInfo;
            }

            resultInfo.StatusCode = response.StatusCode;
            var res = response.Content.ReadAsStringAsync().Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                if (typeof(T) == typeof(String))
                {
                    resultInfo.Data = (T)(object)res;
                }
                else
                {
                    resultInfo.Data = JsonConvert.DeserializeObject<T>(res);
                }
            }
            else
            {
                resultInfo.message = res;
            }
            return resultInfo;
        }

        /// <summary>
        /// Builds the response.
        /// </summary>
        /// <param name="Data">The data.</param>
        /// <returns>An APIResult.</returns>
        public APIResult<T> BuildResponse<T>(string Data)
        {
            APIResult<T> resultInfo = new()
            {
                StatusCode = HttpStatusCode.OK
            };
            var res = Data;
            if (typeof(T) == typeof(String))
            {
                resultInfo.Data = (T)(object)res;
            }
            else
            {
                resultInfo.Data = JsonConvert.DeserializeObject<T>(res);
            }

            return resultInfo;
        }
    }

    /// <summary>
    /// The a p i result.
    /// </summary>
    public class APIResult<T>
    {
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Gets or sets the status code.
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string message { get; set; }
    }
}