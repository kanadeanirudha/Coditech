using Coditech.Admin.Utilities;
using Coditech.API.Endpoint;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Exceptions;

using Newtonsoft.Json;

using System.Net;

namespace Coditech.API.Client
{
    public partial class UserClient : BaseClient, IUserClient
    {
        private System.Lazy<JsonSerializerSettings> _settings;
        UserEndpoint userEndpoint = null;
        public UserClient()
        {
            _settings = new System.Lazy<Newtonsoft.Json.JsonSerializerSettings>(CreateSerializerSettings);
            userEndpoint = new UserEndpoint();
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

        public virtual UserModuleListResponse GetActiveModuleList()
        {
            return Task.Run(async () => await GetActiveModuleAsync(System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<UserModuleListResponse> GetActiveModuleAsync(System.Threading.CancellationToken cancellationToken)
        {
            string endpoint = userEndpoint.GetActiveModuleAsync();
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
                    var objectResponse = await ReadObjectResponseAsync<UserModuleListResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 204)
                {
                    return new UserModuleListResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    UserModuleListResponse typedBody = JsonConvert.DeserializeObject<UserModuleListResponse>(responseData);
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

        public virtual UserMenuListResponse GetActiveMenuList(string moduleCode)
        {
            return Task.Run(async () => await GetActiveMenuListAsync(moduleCode, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<UserMenuListResponse> GetActiveMenuListAsync(string moduleCode, System.Threading.CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(moduleCode))
            {
                throw new System.ArgumentNullException("moduleCode");
            }

            string endpoint = userEndpoint.GetActiveMenuListAsync(moduleCode);
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
                    var objectResponse = await ReadObjectResponseAsync<UserMenuListResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 204)
                {
                    return new UserMenuListResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    UserMenuListResponse typedBody = JsonConvert.DeserializeObject<UserMenuListResponse>(responseData);
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

        public virtual GeneralPersonResponse InsertPersonInformation(GeneralPersonModel body)
        {
            return Task.Run(async () => await InsertPersonInformationAsync(body, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<GeneralPersonResponse> InsertPersonInformationAsync(GeneralPersonModel body, CancellationToken cancellationToken)
        {
            string endpoint = userEndpoint.InsertPersonInformationAsync();
            HttpResponseMessage response = null;
            bool disposeResponse = true;
            try
            {
                ApiStatus status = new ApiStatus();
                response = await PostResourceToEndpointAsync(endpoint, JsonConvert.SerializeObject(body), status, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
                Dictionary<string, IEnumerable<string>> dictionary = BindHeaders(response);

                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        {
                            ObjectResponseResult<GeneralPersonResponse> objectResponseResult2 = await ReadObjectResponseAsync<GeneralPersonResponse>(response, BindHeaders(response), cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
                            if (objectResponseResult2.Object == null)
                            {
                                throw new CoditechException(objectResponseResult2.Object.ErrorCode, objectResponseResult2.Object.ErrorMessage);
                            }

                            return objectResponseResult2.Object;
                        }
                    case HttpStatusCode.Created:
                        {
                            ObjectResponseResult<GeneralPersonResponse> objectResponseResult1 = await ReadObjectResponseAsync<GeneralPersonResponse>(response, dictionary, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
                            if (objectResponseResult1.Object == null)
                            {
                                throw new CoditechException(objectResponseResult1.Object.ErrorCode, objectResponseResult1.Object.ErrorMessage);
                            }

                            return objectResponseResult1.Object;
                        }
                    default:
                        {
                            string value = ((response.Content != null) ? (await response.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false)) : null);
                            GeneralPersonResponse result = JsonConvert.DeserializeObject<GeneralPersonResponse>(value);
                            UpdateApiStatus(result, status, response);
                            throw new CoditechException(status.ErrorCode, status.ErrorMessage, status.StatusCode);
                        }
                }
            }
            finally
            {
                if (disposeResponse)
                {
                    response.Dispose();
                }
            }
        }

        public virtual GeneralPersonResponse GetPersonInformation(long personId)
        {
            return Task.Run(async () => await GetPersonInformationAsync(personId, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        
        public virtual async Task<GeneralPersonResponse> GetPersonInformationAsync(long personId, System.Threading.CancellationToken cancellationToken)
        {
            if (personId <= 0)
                throw new System.ArgumentNullException("personId");

            string endpoint = userEndpoint.GetPersonInformationAsync(personId);
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
                    var objectResponse = await ReadObjectResponseAsync<GeneralPersonResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 204)
                {
                    return new GeneralPersonResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    GeneralPersonResponse typedBody = JsonConvert.DeserializeObject<GeneralPersonResponse>(responseData);
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

        public virtual GeneralPersonResponse UpdatePersonInformation(GeneralPersonModel body)
        {
            return Task.Run(async () => await UpdatePersonInformationAsync(body, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<GeneralPersonResponse> UpdatePersonInformationAsync(GeneralPersonModel body, System.Threading.CancellationToken cancellationToken)
        {
            string endpoint = userEndpoint.UpdatePersonInformationAsync();
            HttpResponseMessage response = null;
            var disposeResponse = true;
            try
            {
                ApiStatus status = new ApiStatus();

                response = await PutResourceToEndpointAsync(endpoint, JsonConvert.SerializeObject(body), status, cancellationToken).ConfigureAwait(false);

                var headers_ = BindHeaders(response);
                var status_ = (int)response.StatusCode;
                if (status_ == 200)
                {
                    var objectResponse = await ReadObjectResponseAsync<GeneralPersonResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 201)
                {
                    var objectResponse = await ReadObjectResponseAsync<GeneralPersonResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    GeneralPersonResponse typedBody = JsonConvert.DeserializeObject<GeneralPersonResponse>(responseData);
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

        public virtual GeneralPersonAddressListResponse GetGeneralPersonAddresses(long personId)
        {
            return Task.Run(async () => await GetGeneralPersonAddressesAsync(personId, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }
        public virtual async Task<GeneralPersonAddressListResponse> GetGeneralPersonAddressesAsync(long personId, System.Threading.CancellationToken cancellationToken)
        {
            if (personId <= 0)
                throw new System.ArgumentNullException("personId");

            string endpoint = userEndpoint.GetGeneralPersonAddressesAsync(personId);
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
                    var objectResponse = await ReadObjectResponseAsync<GeneralPersonAddressListResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 204)
                {
                    return new GeneralPersonAddressListResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    GeneralPersonAddressListResponse typedBody = JsonConvert.DeserializeObject<GeneralPersonAddressListResponse>(responseData);
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

        public virtual GeneralPersonAddressResponse InsertUpdateGeneralPersonAddress(GeneralPersonAddressModel body)
        {
            return Task.Run(async () => await InsertUpdateGeneralPersonAddressAsync(body, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<GeneralPersonAddressResponse> InsertUpdateGeneralPersonAddressAsync(GeneralPersonAddressModel body, System.Threading.CancellationToken cancellationToken)
        {
            string endpoint = userEndpoint.InsertUpdateGeneralPersonAddressAsync();
            HttpResponseMessage response = null;
            var disposeResponse = true;
            try
            {
                ApiStatus status = new ApiStatus();

                response = await PutResourceToEndpointAsync(endpoint, JsonConvert.SerializeObject(body), status, cancellationToken).ConfigureAwait(false);

                var headers_ = BindHeaders(response);
                var status_ = (int)response.StatusCode;
                if (status_ == 200)
                {
                    var objectResponse = await ReadObjectResponseAsync<GeneralPersonAddressResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 201)
                {
                    var objectResponse = await ReadObjectResponseAsync<GeneralPersonAddressResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    GeneralPersonAddressResponse typedBody = JsonConvert.DeserializeObject<GeneralPersonAddressResponse>(responseData);
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

    }
}
