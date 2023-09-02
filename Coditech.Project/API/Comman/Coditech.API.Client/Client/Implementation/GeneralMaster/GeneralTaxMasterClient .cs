using Coditech.Admin.Utilities;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper.Utilities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net;

namespace Coditech.API.Client
{
    public partial class GeneralTaxMasterClient : BaseClient, IGeneralTaxMasterClient
    {
        public GeneralTaxMasterClient()
        {
        }
        /// <summary>
        /// Gets list of GeneralTaxMaster.
        /// </summary>
        /// <returns>Success</returns>
        /// <exception cref="CoditechException">A server side error occurred.</exception>
        public virtual GeneralTaxMasterListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            return Task.Run(async () => await ListAsync(expand, filter, sort, pageIndex, pageSize, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>
        /// Gets list of GeneralTaxMaster.
        /// </summary>
        /// <returns>Success</returns>
        /// <exception cref="CoditechException">A server side error occurred.</exception>
        public virtual async Task<GeneralTaxMasterListResponse> ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize, CancellationToken cancellationToken)
        {
            string endpoint = CoditechAdminSettings.CoditechOrganisationApiRootUri;
            endpoint += "/" + "GeneralTaxMaster/GetTaxMasterList";
            endpoint += BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize);
            HttpResponseMessage response = null;
            var disposeResponse = true;

            try
            {
                ApiStatus status = new ApiStatus();
                response = await GetResourceFromEndpointAsync(endpoint, status, cancellationToken).ConfigureAwait(false);
                var headers_ = System.Linq.Enumerable.ToDictionary(response.Headers, h_ => h_.Key, h_ => h_.Value);

                if (response.Content != null && response.Content.Headers != null)
                {
                    foreach (var item_ in response.Content.Headers)
                        headers_[item_.Key] = item_.Value;
                }
                var status_ = (int)response.StatusCode;
                if (status_ == 200)
                {
                    var objectResponse = await ReadObjectResponseAsync<GeneralTaxMasterListResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 204)
                {
                    return new GeneralTaxMasterListResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    GeneralTaxMasterListResponse typedBody = Newtonsoft.Json.JsonConvert.DeserializeObject<GeneralTaxMasterListResponse>(responseData);
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
        public virtual GeneralTaxMasterResponse CreateTaxMastert(GeneralTaxMasterModel body)
        {
            return Task.Run(async () => await CreateTaxMasterAsync(body, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<GeneralTaxMasterResponse> CreateTaxMasterAsync(GeneralTaxMasterModel body, CancellationToken cancellationToken)
        {
            string znodeApiRootUri = CoditechAdminSettings.CoditechOrganisationApiRootUri;
            znodeApiRootUri += "/GeneralTaxMaster/CreateTaxMaster";
            HttpResponseMessage response = null;
            bool disposeResponse = true;
            try
            {
                ApiStatus status = new ApiStatus();
                response = await PostResourceToEndpointAsync(znodeApiRootUri, JsonConvert.SerializeObject(body), status, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
                Dictionary<string, IEnumerable<string>> dictionary = response.Headers.ToDictionary<KeyValuePair<string, IEnumerable<string>>, string, IEnumerable<string>>((KeyValuePair<string, IEnumerable<string>> h_) => h_.Key, (KeyValuePair<string, IEnumerable<string>> h_) => h_.Value);
                if (response.Content != null && response.Content.Headers != null)
                {
                    foreach (KeyValuePair<string, IEnumerable<string>> header in response.Content.Headers)
                    {
                        dictionary[header.Key] = header.Value;
                    }
                }
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        {
                            ObjectResponseResult<GeneralTaxMasterResponse> objectResponseResult2 = await ReadObjectResponseAsync<GeneralTaxMasterResponse>(response, dictionary, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
                            if (objectResponseResult2.Object == null)
                            {
                                throw new CoditechException(objectResponseResult2.Object.ErrorCode, objectResponseResult2.Object.ErrorMessage);
                            }

                            return objectResponseResult2.Object;
                        }
                    case HttpStatusCode.Created:
                        {
                            ObjectResponseResult<GeneralTaxMasterResponse> objectResponseResult = await ReadObjectResponseAsync<GeneralTaxMasterResponse>(response, dictionary, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
                            if (objectResponseResult.Object == null)
                            {
                                throw new CoditechException(objectResponseResult.Object.ErrorCode, objectResponseResult.Object.ErrorMessage);
                            }

                            return objectResponseResult.Object;
                        }
                    default:
                        {
                            string value = ((response.Content != null) ? (await response.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false)) : null);
                            GeneralTaxMasterResponse result = JsonConvert.DeserializeObject<GeneralTaxMasterResponse>(value);
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

        /// <summary>
        /// Get generalTaxMaster by generalTaxMaster id.
        /// </summary>
        /// <param name="generalTaxMasterId">GeneralTaxMaster id to get generalTaxMaster details.</param>
        /// <returns>Success</returns>
        /// <exception cref="CoditechException">A server side error occurred.</exception>
        public virtual GeneralTaxMasterResponse GetTaxMaster(int generalTaxMasterId)
        {
            return Task.Run(async () => await GetTaxMasterAsync(generalTaxMasterId, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>
        /// Get generalTaxMaster by generalTaxMaster id.
        /// </summary>
        /// <param name="generalTaxMasterId">GeneralTaxMaster id to get generalTaxMaster details.</param>
        /// <returns>Success</returns>
        /// <exception cref="CoditechException">A server side error occurred.</exception>
        public virtual async Task<GeneralTaxMasterResponse> GetTaxMasterAsync(int generalTaxMasterId, System.Threading.CancellationToken cancellationToken)
        {
            if (generalTaxMasterId == null)
                throw new System.ArgumentNullException("generalTaxMasterId");

            string endpoint = CoditechAdminSettings.CoditechOrganisationApiRootUri;
            endpoint += "/" + "GeneralTaxMaster/GetTaxMaster?generalTaxMasterId=" + generalTaxMasterId;
            endpoint = endpoint.Replace("{generalTaxMasterId}", System.Uri.EscapeDataString(ConvertToString(generalTaxMasterId, System.Globalization.CultureInfo.InvariantCulture)));
            HttpResponseMessage response = null;
            var disposeResponse = true;
            try
            {
                ApiStatus status = new ApiStatus();
                response = await GetResourceFromEndpointAsync(endpoint, status, cancellationToken).ConfigureAwait(false);
                var headers_ = System.Linq.Enumerable.ToDictionary(response.Headers, h_ => h_.Key, h_ => h_.Value);

                if (response.Content != null && response.Content.Headers != null)
                {
                    foreach (var item_ in response.Content.Headers)
                        headers_[item_.Key] = item_.Value;
                }
                var status_ = (int)response.StatusCode;
                if (status_ == 200)
                {
                    var objectResponse = await ReadObjectResponseAsync<GeneralTaxMasterResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 204)
                {
                    return new GeneralTaxMasterResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    GeneralTaxMasterResponse typedBody = Newtonsoft.Json.JsonConvert.DeserializeObject<GeneralTaxMasterResponse>(responseData);
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

        /// <summary>
        /// Update generalTaxMaster.
        /// </summary>
        /// <param name="body">model to update.</param>
        /// <returns>Success</returns>
        /// <exception cref="CoditechException">A server side error occurred.</exception>
        public virtual GeneralTaxMasterResponse UpdateTaxMaster(GeneralTaxMasterModel body)
        {
            return Task.Run(async () => await UpdateTaxMaster
            Async(body, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>
        /// Update generalTaxMaster.
        /// </summary>
        /// <param name="body">model to update.</param>
        /// <returns>Success</returns>
        /// <exception cref="CoditechException">A server side error occurred.</exception>
        public virtual async Task<GeneralTaxMasterResponse> UpdateTaxMasterAsync(GeneralTaxMasterModel body, System.Threading.CancellationToken cancellationToken)
        {
            string endpoint = CoditechAdminSettings.CoditechOrganisationApiRootUri;
            endpoint += "/" + "GeneralTaxMaster/UpdateTaxMaster";
            HttpResponseMessage response = null;
            var disposeResponse = true;
            try
            {
                ApiStatus status = new ApiStatus();

                response = await PutResourceToEndpointAsync(endpoint, JsonConvert.SerializeObject(body), status, cancellationToken).ConfigureAwait(false);

                var headers_ = System.Linq.Enumerable.ToDictionary(response.Headers, h_ => h_.Key, h_ => h_.Value);
                if (response.Content != null && response.Content.Headers != null)
                {
                    foreach (var item_ in response.Content.Headers)
                        headers_[item_.Key] = item_.Value;
                }
                var status_ = (int)response.StatusCode;
                if (status_ == 200)
                {
                    var objectResponse = await ReadObjectResponseAsync<GeneralTaxMasterResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 201)
                {
                    var objectResponse = await ReadObjectResponseAsync<GeneralTaxMasterResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    GeneralTaxMasterResponse typedBody = Newtonsoft.Json.JsonConvert.DeserializeObject<GeneralTaxMasterResponse>(responseData);
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

        /// <summary>
        /// Delete generalTaxMaster.
        /// </summary>
        /// <param name="body">GeneralTaxMaster Id.</param>
        /// <returns>Success</returns>
        /// <exception cref="CoditechException">A server side error occurred.</exception>
        public virtual TrueFalseResponse DeleteTaxMaster(ParameterModel body)
        {
            return Task.Run(async () => await DeleteTaxMasterAsync(body, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>
        /// Delete generalTaxMaster.
        /// </summary>
        /// <param name="body">GeneralTaxMaster Id.</param>
        /// <returns>Success</returns>
        /// <exception cref="CoditechException">A server side error occurred.</exception>
        public virtual async Task<TrueFalseResponse> DeleteTaxMasterAsync(ParameterModel body, System.Threading.CancellationToken cancellationToken)
        {
            string endpoint = CoditechAdminSettings.CoditechOrganisationApiRootUri;
            endpoint += "/" + "GeneralTaxMaster/DeleteTaxMaster";
            HttpResponseMessage response = null;
            var disposeResponse = true;

            try
            {
                ApiStatus status = new ApiStatus();
                response = await PostResourceToEndpointAsync(endpoint, JsonConvert.SerializeObject(body), status, cancellationToken).ConfigureAwait(false);

                var headers_ = System.Linq.Enumerable.ToDictionary(response.Headers, h_ => h_.Key, h_ => h_.Value);
                if (response.Content != null && response.Content.Headers != null)
                {
                    foreach (var item_ in response.Content.Headers)
                        headers_[item_.Key] = item_.Value;
                }
                var status_ = (int)response.StatusCode;
                if (status_ == 200)
                {
                    var objectResponse = await ReadObjectResponseAsync<TrueFalseResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    TrueFalseResponse typedBody = Newtonsoft.Json.JsonConvert.DeserializeObject<TrueFalseResponse>(responseData);
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
    }
}
