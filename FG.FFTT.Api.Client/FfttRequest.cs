using System;
using System.Web;
using System.Text;
using System.Linq;
using System.Net.Http;
using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;

using FG.FFTT.Api.Client.Models;
using FG.FFTT.Api.Client.Properties;
using FG.FFTT.Api.Client.Exceptions;
using FG.FFTT.Api.Client.Extensions;

namespace FG.FFTT.Api.Client
{
    /// <summary>
    /// Simplified object to make an HTTP request (only GET)
    /// </summary>
    public class FfttRequest : IDisposable
    {
        #region private fields

        private HttpClient _client;

        #endregion private fields

        #region properties

        public string Hostname { get; }

        public string Route { get; private set; }

        public HttpVerbs Verb { get; private set; }

        public Dictionary<string, string[]> QueryParameters { get; private set; } = new Dictionary<string, string[]>();

        public HttpClient Client => _client ?? (_client = new HttpClient());

        #endregion properties

        #region constructor

        public FfttRequest(string hostname)
        {
            Hostname = hostname;
        }

        #endregion constructor

        #region public methods

        public FfttRequest SetVerb(HttpVerbs verb)
        {
            Verb = verb;

            return this;
        }

        public FfttRequest SetRoute(string route)
        {
            Route = route;

            return this;
        }

        public FfttRequest SetQueryParameters(Dictionary<string, object> queryParameters)
        {
            if (queryParameters == null) return this;

            foreach (KeyValuePair<string, object> qp in queryParameters)
            {
                if (qp.Value != null)
                {
                    string[] value;

                    if (qp.Value.GetType().IsArray)
                    {
                        value = ((IEnumerable)qp.Value)
                            .Cast<object>()
                            .Select(x =>
                            {
                                if (x.GetType().IsEnum)
                                    return ((int)x).ToString();
                                else
                                    return x.ToString();
                            })
                            .ToArray();
                    }
                    else if (qp.Value.GetType().IsEnum)
                        value = new[] { ((int)qp.Value).ToString() };
                    else
                        value = new[] { qp.Value.ToString() };

                    QueryParameters.Add(qp.Key, value);
                }
            }

            return this;
        }

        public async Task<T> ExecuteAsync<T>()
        {
            string value = string.Empty;

            try
            {
                var url = _computeUrl();
                HttpResponseMessage response = await _tryExecuteAsync<T>(url);

                return (value = await _readResponseContentAsync(response)).FromXml<T>();
            }
            catch (Exception e)
            {
                // The api returns the error as an XML string corresponding to the Error object.
                // Two error models are returned (invalid parameters and unauthorized access).
                if (e is InvalidOperationException)
                {
                    try
                    {
                        throw ApiException.InvalidParameters(value.FromXml<Error>().Message);
                    }
                    catch (InvalidOperationException)
                    {
                        throw ApiException.Unauthorized(value.FromXml<UnauthorizedAccess>().Message);
                    }
                }

                // In case of an unknown error, we return the error message returned by the server.
                throw new ApiException($"{ e.InnerException?.Message ?? e.Message }: type {e.GetType().Name }");
            }
        }

        #endregion public methods

        #region private methods

        private string _computeUrl()
        {
            StringBuilder sb = new StringBuilder(Hostname);

            if (Route != null)
                sb.Append($"/{Route}");

            if (QueryParameters.Count > 0)
            {
                sb.Append("?");

                bool firstParameter = true;
                foreach (KeyValuePair<string, string[]> qp in QueryParameters)
                {
                    __tryAddUriSeparator(ref firstParameter);

                    bool firstItem = true;
                    foreach (string item in qp.Value)
                    {
                        __tryAddUriSeparator(ref firstItem);
                        sb.Append($"{qp.Key}={item}");
                    }
                }
            }

            bool __tryAddUriSeparator(ref bool isFirst)
            {
                if (isFirst)
                    return (isFirst = false);

                sb.Append("&");

                return true;
            }

            return Uri.EscapeUriString(sb.ToString());
        }

        private async Task<HttpResponseMessage> _tryExecuteAsync<T>(string url)
        {
            int loop = 0;
            Exception exception;

            HttpResponseMessage response;

            // When we run our unit tests on the remote Github servers, we sometimes encounter connection or timeout problems with the FFTT webservice.
            // This is why we re-run each request at least 3 times in case of error before resending it.
            do
            {
                try
                {
                    switch (Verb)
                    {
                        case HttpVerbs.Get:
                            response = await Client.GetAsync(url);
                            break;

                        default:
                            throw new ApiException(Resources.NotImplementedHTTPVerbsMessage);
                    }

                    return response;
                }
                catch (Exception e) { exception = e; }
            }
            while (++loop < 3);

            throw exception;
        }

        private async Task<string> _readResponseContentAsync(HttpResponseMessage response)
        {
            string value = await response.Content.ReadAsStringAsync();

            // Special case when the content of the response is in "text/html": special characters are decoded.
            // Currently, only the response to retrieve the FFTT news is in this case.
            if (response.Content.Headers.ContentType.MediaType == "text/html")
                value = HttpUtility.HtmlDecode(value);

            return value;
        }

        #endregion private methods

        #region implementation IDisposable

        public void Dispose()
        {
            _client.Dispose();
        }

        #endregion implementation IDisposable
    }
}