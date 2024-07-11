using Coditech.Common.API.Model.Response;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper;
using Coditech.Common.Logger;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;

using Newtonsoft.Json;

using System.Collections.Specialized;
using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace Coditech.API.Client
{
    public abstract class BaseClient
    {
        private static readonly ICoditechLogging _coditechLogging = CoditechDependencyResolver.GetService<ICoditechLogging>();
        private static IConfigurationSection settings = CoditechDependencyResolver.GetService<IConfiguration>().GetSection("appsettings");

        private string _domainName;
        private string _domainKey;
        private int _apiRequestTimeout = 0;
        private string _DomainHeader;
        private string _localeId;
        public long UserMasterId { get; set; }
        public bool RefreshCache { get; set; }
        private string LoginAsHeader => UserMasterId > 0 ? $"LoginAsUserId: {UserMasterId}" : string.Empty;
        ///// <summary>
        ///// API request timeout in milliseconds.
        ///// </summary>
        public int RequestTimeout
        {
            get
            {
                if (_apiRequestTimeout > 0)
                    return _apiRequestTimeout;

                //If invalid value is present for ApiRequestTimeout key in the appsetting file then default value will be used.
                int defaultApiRequestTimeout = 10000000;
                int configApiRequestTimeout;
                int.TryParse(settings["ApiRequestTimeout"], out configApiRequestTimeout);
                return configApiRequestTimeout > 0 ? configApiRequestTimeout : defaultApiRequestTimeout;
            }
            set
            {
                _apiRequestTimeout = value;
            }
        }
        public string DomainName
        {
            get
            {
                if (!string.IsNullOrEmpty(_domainName))
                    return _domainName;

                return settings["ApiDomainName"];
            }
            set { _domainName = value; }
        }

        public string DomainKey
        {
            get
            {
                if (!string.IsNullOrEmpty(_domainKey))
                    return _domainKey;

                return settings["ApiDomainKey"];
            }
            set { _domainKey = value; }
        }

        //Get the IPAddress of the user.
        private string MinifiedJsonResponseHeader
        {
            get
            {
                bool b = Convert.ToBoolean(settings["MinifiedJsonResponseFromAPI"]);
                return GetHeaderFormattedString("Minified-Json-Response", b.ToString());
            }
        }
        private string DomainHeader
        {
            get
            {
                if (!string.IsNullOrEmpty(_DomainHeader))
                    return _DomainHeader;

                return "Coditech-DomainName: " + HttpContextHelper.Request.Headers[HeaderNames.Host].ToString().Trim();
            }

            set { _DomainHeader = value; }
        }
        private string GetHeaderFormattedString(string header, string value)
        {
            return new StringBuilder(header).Append(": ").Append(value).ToString();
        }

        #region Public
        /// <summary>
        /// Gets a resource from an endpoint.
        /// </summary>
        /// <typeparam name="T">The type of resource to retrieve.</typeparam>
        /// <param name="endpoint">The endpoint where the resource resides.</param>
        /// <param name="status">The status of the API call; treat this as an out parameter.</param>
        /// <returns>The resource.</returns>
        public T GetResourceFromEndpoint<T>(string endpoint, ApiStatus status) where T : BaseResponse
        {
            string baseEndPoint = endpoint;
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(endpoint);
            req.KeepAlive = false; // Prevents "server committed a protocol violation" error
            req.Method = "GET";
            req.Timeout = RequestTimeout;


            //Set header for api request
            SetHeaders(req, endpoint);

            T result = GetResultFromResponse<T>(req, status, baseEndPoint, "GET");
            return result;
        }

        /// <summary>
        /// Posts resource data to an endpoint, usually for creating a new resource.
        /// </summary>
        /// <typeparam name="T">The type of resource being created.</typeparam>
        /// <param name="endpoint">The endpoint that accepts posting resource data.</param>
        /// <param name="data">The data for the resource.</param>
        /// <param name="status">The status of the API call; treat this as an out parameter.</param>
        /// <returns>The newly created resource.</returns>
        public T PostResourceToEndpoint<T>(string endpoint, string data, ApiStatus status) where T : BaseResponse
        {
            string baseEndPoint = endpoint;
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(endpoint);
            req.KeepAlive = false; // Prevents "server committed a protocol violation" error
            req.Method = "POST";
            req.ContentType = "application/json";
            req.ContentLength = dataBytes.Length;
            req.Timeout = RequestTimeout;

            //Set header for api request
            SetHeaders(req, endpoint);
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(dataBytes, 0, dataBytes.Length);
            }

            T result = GetResultFromResponse<T>(req, status, baseEndPoint, "POST", data);
            return result;
        }

        /// <summary>
        /// Post resource data to an endpoint.
        /// </summary>
        /// <typeparam name="T">The type of resource being updated.</typeparam>
        /// <param name="endpoint">The endpoint where the resource resides.</param>
        /// <param name="data">The data for the resource.</param>
        /// <param name="status">The status of the API call; treat this as an out parameter.</param>
        /// <returns>The resource.</returns>
        public async Task<T> PostResourceToEndpointAsync<T>(string endpoint, string data, ApiStatus status) where T : BaseResponse
        {
            string baseEndPoint = endpoint;
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(endpoint);
            req.KeepAlive = false; // Prevents "server committed a protocol violation" error
            req.Method = "POST";
            req.ContentType = "application/json";
            req.ContentLength = dataBytes.Length;
            req.Timeout = RequestTimeout;


            //Set header for api request
            SetHeaders(req, endpoint);

            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(dataBytes, 0, dataBytes.Length);
            }

            T result = await GetResultFromResponseAsync<T>(req, status, baseEndPoint, "POST", data);
            return result;

        }

        /// <summary>
        /// Post resource form data
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="formData"></param>
        /// <param name="status"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PostResourceToEndpointAsync(string endpoint, MultipartFormDataContent formData, ApiStatus status, CancellationToken cancellationToken)
        {
            string baseEndPoint = endpoint;

            HttpRequestMessage req = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(endpoint),
                Content = formData
            };

            // Set headers for API request.
            req = SetHeaders(req, endpoint);

            return await GetResultFromResponseAsync(req, status, cancellationToken, baseEndPoint, "POST", formData.ToString());
        }


        /// <summary>
        /// Post resource data to an endpoint.
        /// </summary>
        /// <typeparam name="T">The type of resource being updated.</typeparam>
        /// <param name="endpoint">The endpoint where the resource resides.</param>
        /// <param name="data">The data for the resource.</param>
        /// <param name="status">The status of the API call; treat this as an out parameter.</param>
        /// <returns>The resource.</returns>
        public async Task<HttpResponseMessage> PostResourceToEndpointAsync(string endpoint, string data, ApiStatus status, CancellationToken cancellationToken)
        {
            string baseEndPoint = endpoint;

            HttpRequestMessage req = new HttpRequestMessage();

            //Set header for api request.
            req = SetHeaders(req, endpoint);
            req.Method = new HttpMethod("POST");
            req.Content = new StringContent(data, Encoding.UTF8, "application/json");

            return await GetResultFromResponseAsync(req, status, cancellationToken, baseEndPoint, "POST", data);

        }
        /// <summary>
        /// Puts resource data to an endpoint, usually for updating an existing resource.
        /// </summary>
        /// <typeparam name="T">The type of resource being updated.</typeparam>
        /// <param name="endpoint">The endpoint where the resource resides.</param>
        /// <param name="data">The data for the resource.</param>
        /// <param name="status">The status of the API call; treat this as an out parameter.</param>
        /// <returns>The updated resource.</returns>
        public T PutResourceToEndpoint<T>(string endpoint, string data, ApiStatus status) where T : BaseResponse
        {
            string baseEndPoint = endpoint;
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(endpoint);

            req.KeepAlive = false; // Prevents "server committed a protocol violation" error
            req.Method = "PUT";
            req.ContentType = "application/json";
            req.ContentLength = dataBytes.Length;
            req.Timeout = RequestTimeout;

            //Set header for api request
            SetHeaders(req, endpoint);

            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(dataBytes, 0, dataBytes.Length);
            }

            T result = GetResultFromResponse<T>(req, status, baseEndPoint, "PUT", data);
            return result;
        }

        public void UpdateApiStatus(dynamic result, ApiStatus status, HttpResponseMessage response)
        {
            if (status == null)
                status = new ApiStatus();

            if (result != null)
            {
                status.HasError = result.HasError;
                status.ErrorCode = result.ErrorCode;
                status.ErrorMessage = result.ErrorMessage;
            }
            if (response != null)
                status.StatusCode = response.StatusCode;
        }

        /// <summary>
        /// Gets a resource from an endpoint.
        /// </summary>
        /// <typeparam name="T">The type of resource to retrieve.</typeparam>
        /// <param name="endpoint">The endpoint where the resource resides.</param>
        /// <param name="status">The status of the API call; treat this as an out parameter.</param>
        /// <returns>The resource.</returns>
        public async Task<T> GetResourceFromEndpointAsync<T>(string endpoint, ApiStatus status) where T : BaseResponse
        {
            string baseEndPoint = endpoint;
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(endpoint);
            req.KeepAlive = false; // Prevents "server committed a protocol violation" error
            req.Method = "GET";
            req.Timeout = RequestTimeout;


            //Set header for api request.
            SetHeaders(req, endpoint);

            T result = await GetResultFromResponseAsync<T>(req, status, baseEndPoint, "GET");

            return result;

        }

        /// <summary>
        /// Puts resource data to an endpoint, usually for updating an existing resource.
        /// </summary>
        /// <typeparam name="T">The type of resource being updated.</typeparam>
        /// <param name="endpoint">The endpoint where the resource resides.</param>
        /// <param name="data">The data for the resource.</param>
        /// <param name="status">The status of the API call; treat this as an out parameter.</param>
        /// <returns>The updated resource.</returns>
        public async Task<T> PutResourceToEndpointAsync<T>(string endpoint, string data, ApiStatus status) where T : BaseResponse
        {
            string baseEndPoint = endpoint;
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(endpoint);
            req.KeepAlive = false; // Prevents "server committed a protocol violation" error
            req.Method = "PUT";
            req.ContentType = "application/json";
            req.ContentLength = dataBytes.Length;
            req.Timeout = RequestTimeout;


            //Set header for api request
            SetHeaders(req, endpoint);

            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(dataBytes, 0, dataBytes.Length);
            }

            T result = await GetResultFromResponseAsync<T>(req, status, baseEndPoint, "PUT", data);
            return result;

        }

        protected virtual async Task<ObjectResponseResult<T>> ReadObjectResponseAsync<T>(System.Net.Http.HttpResponseMessage response, IReadOnlyDictionary<string, IEnumerable<string>> headers, System.Threading.CancellationToken cancellationToken)
        {
            if (response == null || response.Content == null)
            {
                return new ObjectResponseResult<T>(default(T), string.Empty);
            }

            if (ReadResponseAsString)
            {
                var responseText = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                try
                {
                    //var typedBody = JsonConvert.DeserializeObject<T>(responseText, JsonSerializerSettings);
                    var typedBody = JsonConvert.DeserializeObject<T>(responseText);
                    return new ObjectResponseResult<T>(typedBody, responseText);
                }
                catch (JsonException exception)
                {
                    var message = "Could not deserialize the response body string as " + typeof(T).FullName + ".";
                    throw;
                }
            }
            else
            {
                try
                {
                    using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                    using (var streamReader = new System.IO.StreamReader(responseStream))
                    using (var jsonTextReader = new JsonTextReader(streamReader))
                    {
                        //var serializer = JsonSerializer.Create(JsonSerializerSettings);
                        var serializer = JsonSerializer.Create();
                        var typedBody = serializer.Deserialize<T>(jsonTextReader);
                        return new ObjectResponseResult<T>(typedBody, string.Empty);
                    }
                }
                catch (JsonException exception)
                {
                    var message = "Could not deserialize the response body stream as " + typeof(T).FullName + ".";
                    throw;
                }
            }
        }
        public bool ReadResponseAsString { get; set; }
        #endregion
        #region private
        private async Task<HttpResponseMessage> GetResultFromResponseAsync(HttpRequestMessage request, ApiStatus status, CancellationToken cancellationToken, string endpoint = "", string methodType = "", string data = "")
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                request.RequestUri = new Uri(endpoint);
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromMilliseconds(10000000);
                    client.DefaultRequestHeaders.ConnectionClose = false;
                    response = await client.SendAsync(request);
                    UpdateApiStatus(response, status);
                    return response;
                }
            }
            catch (HttpRequestException ex)
            {
                LogRequestResponseDetails(request, ex, status.StatusCode);

                // This deserialization is used to get the error information
                var result = DeserializeResponseStream(response);
                switch (result.ErrorCode)
                {
                    case ErrorCodes.WebAPIKeyNotFound:
                        ThrowApiKeyNotFoundException();
                        break;
                    case ErrorCodes.InvalidDomainConfiguration:
                    case ErrorCodes.InvalidSqlConfiguration:
                    case ErrorCodes.InvalidCoditechLicense:
                        ThrowMisconfigurationException(result.ErrorCode, result.ErrorMessage);
                        break;
                    case ErrorCodes.UnAuthorized:
                        result = await HandleAsyncUnAuthorizedRequest(status, cancellationToken, endpoint, methodType, data);
                        break;
                    default:
                        UpdateApiStatus(response, status, result);
                        break;
                }
            }
            catch (Exception ex)
            {
                LogRequestResponseDetails(request, ex, status.StatusCode);
            }
            return response;

        }

        private dynamic DeserializeResponseStream(HttpResponseMessage response)
        {
            if (response != null)
            {
                using (Stream body = response.Content.ReadAsStream())
                {
                    if (body != null)
                    {
                        using (StreamReader stream = new StreamReader(body))
                        {
                            using (JsonTextReader jsonReader = new JsonTextReader(stream))
                            {
                                JsonSerializer jsonSerializer = new JsonSerializer();
                                try
                                {
                                    return jsonSerializer.Deserialize(jsonReader);
                                }
                                catch (JsonReaderException ex)
                                {
                                    _coditechLogging.LogMessage(ex.Message, string.Empty, TraceLevel.Error);
                                    throw new CoditechException(null, ex.Message);
                                }
                                catch (Exception ex)
                                {
                                    _coditechLogging.LogMessage(ex.Message, string.Empty, TraceLevel.Error);
                                }
                            }
                        }
                    }
                }
            }

            return null;

        }

        private T GetResultFromResponse<T>(HttpWebRequest request, ApiStatus status, string endpoint = "", string methodType = "", string data = "") where T : BaseResponse
        {
            T result = null;

            try
            {
                request.UserAgent = HttpContextHelper.Request.Headers[HeaderNames.UserAgent];
                using (HttpWebResponse rsp = (HttpWebResponse)request.GetResponse())
                {
                    // This deserialization gives back the populated resource
                    result = DeserializeResponseStream<T>(rsp);
                    UpdateApiStatus(result, rsp, status);
                }
            }
            catch (WebException ex)
            {
                LogRequestResponseDetails(request, ex, status.StatusCode);
                using (HttpWebResponse rsp = (HttpWebResponse)ex.Response)
                {
                    result = DeserializeResponseStream<T>(rsp);

                    switch (result.ErrorCode)
                    {
                        case ErrorCodes.WebAPIKeyNotFound:
                            ThrowApiKeyNotFoundException();
                            break;
                        case ErrorCodes.InvalidDomainConfiguration:
                        case ErrorCodes.InvalidSqlConfiguration:
                        case ErrorCodes.InvalidCoditechLicense:
                            ThrowMisconfigurationException(result.ErrorCode, result.ErrorMessage);
                            break;
                        case ErrorCodes.UnAuthorized:
                            result = HandleUnAuthorizedRequest<T>(status, endpoint, methodType, data);
                            break;
                        default:
                            UpdateApiStatus(result, rsp, status);
                            break;
                    }

                }
            }
            catch (Exception ex)
            {
                LogRequestResponseDetails(request, ex, status.StatusCode);
            }

            return result;
        }

        private async Task<T> GetResultFromResponseAsync<T>(HttpWebRequest request, ApiStatus status, string endpoint = "", string methodType = "", string data = "") where T : BaseResponse
        {
            T result = null;

            try
            {
                using (HttpWebResponse rsp = (HttpWebResponse)await request.GetResponseAsync())
                {
                    // This deserialization gives back the populated resource
                    result = DeserializeResponseStream<T>(rsp);
                    UpdateApiStatus(result, rsp, status);
                }
            }
            catch (WebException ex)
            {
                LogRequestResponseDetails(request, ex, status.StatusCode);
                using (HttpWebResponse rsp = (HttpWebResponse)ex.Response)
                {
                    // This deserialization is used to get the error information
                    result = DeserializeResponseStream<T>(rsp);
                    switch (result.ErrorCode)
                    {
                        case ErrorCodes.WebAPIKeyNotFound:
                            ThrowApiKeyNotFoundException();
                            break;
                        case ErrorCodes.InvalidDomainConfiguration:
                        case ErrorCodes.InvalidSqlConfiguration:
                        case ErrorCodes.InvalidCoditechLicense:
                            ThrowMisconfigurationException(result.ErrorCode, result.ErrorMessage);
                            break;
                        case ErrorCodes.UnAuthorized:
                            result = await HandleAsyncUnAuthorizedRequest<T>(status, endpoint, methodType, data);
                            break;
                        default:
                            UpdateApiStatus(result, rsp, status);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogRequestResponseDetails(request, ex, status.StatusCode);
            }
            return result;
        }
        private void UpdateApiStatus<T>(T result, HttpWebResponse response, ApiStatus status) where T : BaseResponse
        {
            if (status == null)
                status = new ApiStatus();

            if (result != null)
            {
                status.HasError = result.HasError;
                status.ErrorCode = result.ErrorCode;
                status.ErrorMessage = result.ErrorMessage;
            }
            if (response != null) status.StatusCode = response.StatusCode;

        }

        private T DeserializeResponseStream<T>(HttpWebResponse response) where T : BaseResponse
        {
            if (response != null)
            {
                using (Stream body = response.GetResponseStream())
                {
                    if (body != null)
                    {
                        using (StreamReader stream = new StreamReader(body))
                        {
                            using (JsonTextReader jsonReader = new JsonTextReader(stream))
                            {
                                JsonSerializer jsonSerializer = new JsonSerializer();
                                try
                                {
                                    return jsonSerializer.Deserialize<T>(jsonReader);
                                }
                                catch (JsonReaderException ex)
                                {
                                    _coditechLogging.LogMessage(ex.Message, string.Empty, TraceLevel.Error);
                                    throw new CoditechException(null, ex.Message);
                                }
                                catch (Exception ex)
                                {
                                    _coditechLogging.LogMessage(ex.Message, string.Empty, TraceLevel.Error);
                                }
                            }
                        }
                    }
                }
            }
            return default(T);
        }

        //Set headers for api request.
        protected virtual HttpRequestMessage SetHeaders(HttpRequestMessage req, string endpoint = "")
        {
            SetAuthorizationHeader(req, endpoint);
            SetLoginAsHeader(req);
            SetDomainHeader(req);
            //SetMinifiedJsonResponseHeader(req);
            return req;
        }

        private void SetHeaders(HttpWebRequest req, string endpoint = "")
        {
            SetAuthorizationHeader(req, endpoint);
            SetLoginAsHeader(req);
            SetDomainHeader(req);
            SetMinifiedJsonResponseHeader(req);
        }

        //Set Authorization request.
        private void SetAuthorizationHeader(HttpRequestMessage req, string endpoint)
            => req.Headers.Add(GetHeaderKey(GetAuthorizationHeader(DomainName, DomainKey, endpoint)), GetHeaderValue(GetAuthorizationHeader(DomainName, DomainKey, endpoint)));

        //Set Authorization request.
        private void SetAuthorizationHeader(HttpWebRequest req, string endpoint) =>
            req.Headers.Add(GetAuthorizationHeader(DomainName, DomainKey, endpoint));

        //Set login as header
        private void SetLoginAsHeader(HttpRequestMessage req)
        {
            if (!string.IsNullOrEmpty(LoginAsHeader))
                req.Headers.Add(GetHeaderKey(LoginAsHeader), GetHeaderValue(LoginAsHeader));
        }

        //Set login as header
        private void SetLoginAsHeader(HttpWebRequest req)
        {
            if (!string.IsNullOrEmpty(LoginAsHeader))
                req.Headers.Add(LoginAsHeader);
        }
        //Sets the header to receive minified JSON response from API. (It returns JSON by removing null properties and default value properties)
        private void SetMinifiedJsonResponseHeader(HttpWebRequest req)
        {
            if (!string.IsNullOrEmpty(MinifiedJsonResponseHeader))
                req.Headers.Add(MinifiedJsonResponseHeader);
        }

        //Sets the header to receive minified JSON response from API. (It returns JSON by removing null properties and default value properties)
        private void SetMinifiedJsonResponseHeader(HttpRequestMessage req)
        {
            if (!string.IsNullOrEmpty(MinifiedJsonResponseHeader))
                req.Headers.Add(GetHeaderKey(MinifiedJsonResponseHeader), GetHeaderValue(MinifiedJsonResponseHeader));
        }

        //Set Domain header
        private void SetDomainHeader(HttpWebRequest req)
        {
            if (!string.IsNullOrEmpty(DomainHeader))
                req.Headers.Add(DomainHeader);
        }
        //Set Domain header
        private void SetDomainHeader(HttpRequestMessage req)
        {
            if (!string.IsNullOrEmpty(DomainHeader))
                req.Headers.Add(GetHeaderKey(DomainHeader), GetHeaderValue(DomainHeader));
        }
        private string GetHeaderKey(string key)
        {
            var stringArray = key.Split(":");
            return (stringArray[0]).Trim();
        }
        private string GetHeaderValue(string value)
        {
            var stringArray = value.Split(":");
            return (stringArray[1]).Trim();
        }
        private string GetAuthorizationHeader(string domainName, string domainKey, string endpoint = "")
        {
            return $"Authorization: Basic {HelperUtility.EncodeBase64($"{domainName}|{domainKey}")}";
        }

        private string GetAuthorizationHeader(string domainName, string domainKey)
        {
            return $"Basic {HelperUtility.EncodeBase64($"{domainName}|{domainKey}")}";
        }

         /// <summary>
        /// Log request-response details to BaseClient Component
        /// Request Details : URL, ReqHeaders
        /// Response Details: Response StatusCode 
        /// </summary>
        /// <param name="request">HttpWebRequest Object</param>
        /// <param name="ex">exception object</param>
        /// <param name="statusCode">status code</param>
        private void LogRequestResponseDetails(HttpWebRequest request, Exception ex, HttpStatusCode statusCode)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"URL - {request.RequestUri}{Environment.NewLine}")
            .Append($"Req-Headers - {Environment.NewLine + GetHeaderDetails(request.Headers)}")
            .Append($"Response Status - {statusCode}{Environment.NewLine}").Append($"Error Message - {ex.Message}");

            CoditechLoggingHelper.CoditechLogging.LogMessage(stringBuilder.ToString(), "BaseClient", TraceLevel.Error);
        }
        /// <summary>
        /// Log request-response details to BaseClient Component
        /// Request Details : URL, ReqHeaders
        /// Response Details: Response StatusCode 
        /// </summary>
        /// <param name="request">HttpWebRequest Object</param>
        /// <param name="ex">exception object</param>
        /// <param name="statusCode">status code</param>
        private void LogRequestResponseDetails(HttpRequestMessage request, Exception ex, HttpStatusCode statusCode)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"URL - {request.RequestUri}{Environment.NewLine}")
            .Append($"Req-Headers - {Environment.NewLine + GetHeaderDetails(request.Headers)}")
            .Append($"Response Status - {statusCode}{Environment.NewLine}").Append($"Error Message - {ex.Message}");

            _coditechLogging.LogMessage(stringBuilder.ToString(), "BaseClient", TraceLevel.Error);

        }

        /// <summary>
        /// Get Request Header Details
        /// loop through Request Header collection and stores it's key-value pairwise detail in string
        /// </summary>
        /// <param name="collection">Request Header Collection</param>
        /// <returns>string - Reqest Header Value and it's key</returns>
        private string GetHeaderDetails(WebHeaderCollection collection)
        {
            string header = "Request Header Details - ";
            NameValueCollection headers = collection;
            for (int index = 0; index < headers.Count; index++)
            {
                //Skip Logging of Authorization Key
                if (headers.GetKey(index) == "Authorization")
                    continue;
                header = string.Concat(header, $"Key - {headers.GetKey(index)}, Value- {headers.Get(index)}{Environment.NewLine}");
            }
            return header;
        }
        /// <summary>
        /// Get Request Header Details
        /// loop through Request Header collection and stores it's key-value pairwise detail in string
        /// </summary>
        /// <param name="collection">Request Header Collection</param>
        /// <returns>string - Reqest Header Value and it's key</returns>
        private string GetHeaderDetails(HttpRequestHeaders collection)
        {
            string header = "Request Header Details - ";
            NameValueCollection headers = new NameValueCollection();
            foreach (var headerCollection in collection)
            {
                string headerName = headerCollection.Key;
                string headerContent = string.Join(",", headerCollection.Value.ToArray());
                headers.Add(headerName, headerContent);
            }

            for (int index = 0; index < headers.Count; index++)
            {
                //Skip Logging of Authorization Key
                if (headers.GetKey(index) == "Authorization")
                    continue;
                header = string.Concat(header, $"Key - {headers.GetKey(index)}, Value- {headers.Get(index)}{Environment.NewLine}");
            }
            return header;

        }
        private void ThrowApiKeyNotFoundException()
        {
            throw new CoditechException(ErrorCodes.WebAPIKeyNotFound, "Web API Key Not Found");
        }

        private void ThrowMisconfigurationException(int? errorCode, string errorMessage)
        {
            switch (errorCode)
            {
                case ErrorCodes.InvalidDomainConfiguration:
                case ErrorCodes.InvalidSqlConfiguration:
                case ErrorCodes.InvalidCoditechLicense:
                    throw new CoditechException(errorCode, errorMessage);
                default:
                    break;
            }
        }
        //Handle unauthorized request and again request with valid token.
        private T HandleUnAuthorizedRequest<T>(ApiStatus status, string endpoint = "", string methodType = "", string data = "") where T : BaseResponse
        {
            switch (methodType.ToLower())
            {
                case "get":
                    return GetResourceFromEndpoint<T>(endpoint, status);
                case "post":
                    return PostResourceToEndpoint<T>(endpoint, data, status);
                case "put":
                    return PutResourceToEndpoint<T>(endpoint, data, status);
            }
            return GetResourceFromEndpoint<T>(endpoint, status);
        }
        private Task<T> HandleAsyncUnAuthorizedRequest<T>(ApiStatus status, string endpoint, string methodType, string data) where T : BaseResponse
        {
            switch (methodType.ToLower())
            {
                case "get":
                    return GetResourceFromEndpointAsync<T>(endpoint, status);
                case "post":
                    return PostResourceToEndpointAsync<T>(endpoint, data, status);
                case "put":
                    return PutResourceToEndpointAsync<T>(endpoint, data, status);
            }
            return GetResourceFromEndpointAsync<T>(endpoint, status);
        }
        private Task<HttpResponseMessage> HandleAsyncUnAuthorizedRequest(ApiStatus status, CancellationToken cancellationToken, string endpoint, string methodType, string data)
        {
            switch (methodType.ToLower())
            {
                case "get":
                    return GetResourceFromEndpointAsync(endpoint, status, cancellationToken);
                case "post":
                    return PostResourceToEndpointAsync(endpoint, data, status, cancellationToken);
                case "put":
                    return PutResourceToEndpointAsync(endpoint, data, status, cancellationToken);
            }
            return GetResourceFromEndpointAsync(endpoint, status, cancellationToken);
        }

        public async Task<HttpResponseMessage> GetResourceFromEndpointAsync(string endpoint, ApiStatus status, CancellationToken cancellationToken)
        {
            string baseEndPoint = endpoint;
            HttpRequestMessage request = new HttpRequestMessage();

            //Set header for api request.
            request = SetHeaders(request, endpoint);
            request.Method = new HttpMethod("GET");

            return await GetResultFromResponseAsync(request, status, cancellationToken, baseEndPoint, "GET");

        }
        /// Puts resource data to an endpoint, usually for updating an existing resource.
        /// </summary>
        /// <typeparam name="T">The type of resource being updated.</typeparam>
        /// <param name="endpoint">The endpoint where the resource resides.</param>
        /// <param name="data">The data for the resource.</param>
        /// <param name="status">The status of the API call; treat this as an out parameter.</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The updated resource.</returns>
        public async Task<HttpResponseMessage> PutResourceToEndpointAsync(string endpoint, string data, ApiStatus status, CancellationToken cancellationToken)
        {
            string baseEndPoint = endpoint;

            HttpRequestMessage req = new HttpRequestMessage();

            //Set header for api request.
            req = SetHeaders(req, endpoint);
            req.Method = new HttpMethod("PUT");
            req.Content = new StringContent(data, Encoding.UTF8, "application/json");

            return await GetResultFromResponseAsync(req, status, cancellationToken, baseEndPoint, "PUT", data);

        }
        private void UpdateApiStatus(HttpResponseMessage response, ApiStatus status, dynamic result = null)
        {
            if (status == null)
                status = new ApiStatus();

            if (result != null)
            {
                status.HasError = result.HasError;
                status.ErrorCode = result.ErrorCode;
                status.ErrorMessage = result.ErrorMessage;
            }

            if (response != null) status.StatusCode = response.StatusCode;

        }

        #endregion

        #region protected
        protected struct ObjectResponseResult<T>
        {
            public ObjectResponseResult(T responseObject, string responseText)
            {
                this.Object = responseObject;
                this.Text = responseText;
            }

            public T Object { get; }

            public string Text { get; }
        }

        protected string ConvertToString(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value == null)
            {
                return "";
            }

            if (value is System.Enum)
            {
                var name = System.Enum.GetName(value.GetType(), value);
                if (name != null)
                {
                    var field = System.Reflection.IntrospectionExtensions.GetTypeInfo(value.GetType()).GetDeclaredField(name);
                    if (field != null)
                    {
                        var attribute = System.Reflection.CustomAttributeExtensions.GetCustomAttribute(field, typeof(System.Runtime.Serialization.EnumMemberAttribute))
                            as System.Runtime.Serialization.EnumMemberAttribute;
                        if (attribute != null)
                        {
                            return attribute.Value != null ? attribute.Value : name;
                        }
                    }

                    var converted = System.Convert.ToString(System.Convert.ChangeType(value, System.Enum.GetUnderlyingType(value.GetType()), cultureInfo));
                    return converted == null ? string.Empty : converted;
                }
            }
            else if (value is bool)
            {
                return System.Convert.ToString((bool)value, cultureInfo).ToLowerInvariant();
            }
            else if (value is byte[])
            {
                return System.Convert.ToBase64String((byte[])value);
            }
            else if (value.GetType().IsArray)
            {
                var array = System.Linq.Enumerable.OfType<object>((System.Array)value);
                return string.Join(",", System.Linq.Enumerable.Select(array, o => ConvertToString(o, cultureInfo)));
            }

            var result = System.Convert.ToString(value, cultureInfo);
            return result == null ? "" : result;
        }

        protected Dictionary<string, IEnumerable<string>> BindHeaders(HttpResponseMessage response)
        {
            var headers_ = System.Linq.Enumerable.ToDictionary(response.Headers, h_ => h_.Key, h_ => h_.Value);
            if (response.Content != null && response.Content.Headers != null)
            {
                foreach (var item_ in response.Content.Headers)
                    headers_[item_.Key] = item_.Value;
            }

            return headers_;
        }
        #endregion
    }
}
