using Coditech.Admin.Utilities;
using Coditech.API.Endpoint;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Exceptions;

using Newtonsoft.Json;

namespace Coditech.API.Client
{
    public partial class UserClient : BaseClient, IUserClient
    {
        private System.Lazy<JsonSerializerSettings> _settings;
        UserModuleEndpoint userModuleEndpoint = null;
        public UserClient()
        {
            _settings = new System.Lazy<Newtonsoft.Json.JsonSerializerSettings>(CreateSerializerSettings);
        }
        private JsonSerializerSettings CreateSerializerSettings()
        {
            var settings = new JsonSerializerSettings();
            UpdateJsonSerializerSettings(settings);
            return settings;
        }

        partial void UpdateJsonSerializerSettings(JsonSerializerSettings settings);
        /// <summary>
        /// Login to application.
        /// </summary>
        /// <param name="body">User Model.</param>
        /// <returns>Success</returns>
        /// <exception cref="CoditechException">A server side error occurred.</exception>
        public virtual UserModel Login(IEnumerable<string> expand, UserLoginModel body)
        {
            return Task.Run(async () => await LoginAsync(expand, body, CancellationToken.None)).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Login to application.
        /// </summary>
        /// <param name="body">User Model.</param>
        /// <returns>Success</returns>
        /// <exception cref="CoditechException">A server side error occurred.</exception>
        public virtual Task<UserModel> LoginAsync(IEnumerable<string> expand, UserLoginModel body)
        {
            return LoginAsync(expand, body, CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>
        /// Login to application.
        /// </summary>
        /// <param name="body">User Model.</param>
        /// <returns>Success</returns>
        /// <exception cref="CoditechException">A server side error occurred.</exception>
        public virtual async Task<UserModel> LoginAsync(IEnumerable<string> expand, UserLoginModel body, System.Threading.CancellationToken cancellationToken)
        {
            string endpoint = CoditechAdminSettings.CoditechUserApiRootUri;
            endpoint += "/" + "User/Login";
            HttpResponseMessage response_ = null;
            var disposeResponse_ = true;

            try
            {
                ApiStatus status = new ApiStatus();

                response_ = await PostResourceToEndpointAsync(endpoint, JsonConvert.SerializeObject(body), status, cancellationToken).ConfigureAwait(false);

                var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                if (response_.Content != null && response_.Content.Headers != null)
                {
                    foreach (var item_ in response_.Content.Headers)
                        headers_[item_.Key] = item_.Value;
                }
                var status_ = (int)response_.StatusCode;
                if (status_ == 200)
                {
                    var objectResponse_ = await ReadObjectResponseAsync<UserModel>(response_, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse_.Object == null)
                    {
                        throw new CoditechException(objectResponse_.Object.ErrorCode, objectResponse_.Object.ErrorMessage);
                    }
                    return objectResponse_.Object;
                }
                else
                {
                    string responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                    UserModel typedBody = JsonConvert.DeserializeObject<UserModel>(responseData_, JsonSerializerSettings);
                    UpdateApiStatus(typedBody, status, response_);
                    throw new CoditechException(status.ErrorCode, status.ErrorMessage, status.StatusCode);
                }

            }
            finally
            {
                if (disposeResponse_)
                    response_.Dispose();
            }
        }
        //protected virtual async Task<ObjectResponseResult<T>> ReadObjectResponseAsync<T>(System.Net.Http.HttpResponseMessage response, System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> headers, System.Threading.CancellationToken cancellationToken)
        //{
        //    if (response == null || response.Content == null)
        //    {
        //        return new ObjectResponseResult<T>(default(T), string.Empty);
        //    }

        //    if (ReadResponseAsString)
        //    {
        //        var responseText = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        //        try
        //        {
        //            var typedBody = JsonConvert.DeserializeObject<T>(responseText, JsonSerializerSettings);
        //            return new ObjectResponseResult<T>(typedBody, responseText);
        //        }
        //        catch (JsonException exception)
        //        {
        //            var message = "Could not deserialize the response body string as " + typeof(T).FullName + ".";
        //            throw;
        //        }
        //    }
        //    else
        //    {
        //        try
        //        {
        //            using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
        //            using (var streamReader = new System.IO.StreamReader(responseStream))
        //            using (var jsonTextReader = new JsonTextReader(streamReader))
        //            {
        //                var serializer = JsonSerializer.Create(JsonSerializerSettings);
        //                var typedBody = serializer.Deserialize<T>(jsonTextReader);
        //                return new ObjectResponseResult<T>(typedBody, string.Empty);
        //            }
        //        }
        //        catch (JsonException exception)
        //        {
        //            var message = "Could not deserialize the response body stream as " + typeof(T).FullName + ".";
        //            throw;
        //        }
        //    }
        //}

        public virtual UserModuleResponse GetActiveModuleList(short userId)
        {
            return Task.Run(async () => await GetActiveModuleAsync(userId, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<UserModuleResponse> GetActiveModuleAsync(short userId, System.Threading.CancellationToken cancellationToken)
        {
            if (userId <= 0)
                throw new System.ArgumentNullException("userId");

            string endpoint = userModuleEndpoint.GetActiveModuleAsync(userId);
            HttpResponseMessage response = null;
            var disposeResponse = true;
            try
            {
                ApiStatus status = new ApiStatus();

                response = await GetResourceFromEndpointAsync(endpoint, status, cancellationToken).ConfigureAwait(false);
                Dictionary<string, IEnumerable<string>> headers_ = BindHeaders(response);
                var status_ = (int)response.StatusCode;
                if (status_ == 200)
                {
                    var objectResponse = await ReadObjectResponseAsync<UserModuleResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 204)
                {
                    return new UserModuleResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    UserModuleResponse typedBody = JsonConvert.DeserializeObject<UserModuleResponse>(responseData);
                    UpdateApiStatus(typedBody, status, response);
                    throw new CoditechException(status.ErrorCode, status.ErrorMessage, status.StatusCode);
                }
            }
            finally
            {
                if (disposeResponse)
                    response.Dispose();
            }
        }

        protected JsonSerializerSettings JsonSerializerSettings { get { return _settings.Value; } }

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
        public bool ReadResponseAsString { get; set; }
    }
}
