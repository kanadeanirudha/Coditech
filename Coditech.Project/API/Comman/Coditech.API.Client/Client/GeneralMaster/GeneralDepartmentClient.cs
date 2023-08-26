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
    public partial class GeneralDepartmentClient : BaseClient, IGeneralDepartmentClient
    {
        public GeneralDepartmentClient()
        {
        }
        /// <summary>
        /// Gets list of GeneralDepartment.
        /// </summary>
        /// <returns>Success</returns>
        /// <exception cref="CoditechException">A server side error occurred.</exception>
        public virtual GeneralDepartmentListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            return Task.Run(async () => await ListAsync(expand, filter, sort, pageIndex, pageSize, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>
        /// Gets list of GeneralDepartment.
        /// </summary>
        /// <returns>Success</returns>
        /// <exception cref="CoditechException">A server side error occurred.</exception>
        public virtual async Task<GeneralDepartmentListResponse> ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize, CancellationToken cancellationToken)
        {
            string endpoint = CoditechAdminSettings.CoditechOrganisationApiRootUri;
            endpoint += "/" + "GeneralDepartmentMaster/GetDepartmentList";
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
                    var objectResponse = await ReadObjectResponseAsync<GeneralDepartmentListResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else if (status_ == 204)
                {
                    return new GeneralDepartmentListResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    GeneralDepartmentListResponse typedBody = Newtonsoft.Json.JsonConvert.DeserializeObject<GeneralDepartmentListResponse>(responseData);
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

        public virtual GeneralDepartmentResponse CreateDepartment(GeneralDepartmentModel body)
        {
            return Task.Run(async () => await CreateDepartmentAsync(body, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<GeneralDepartmentResponse> CreateDepartmentAsync(GeneralDepartmentModel body, CancellationToken cancellationToken)
        {
            string znodeApiRootUri = CoditechAdminSettings.CoditechOrganisationApiRootUri;
            znodeApiRootUri += "/GeneralDepartmentMaster/CreateDepartment";
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
                            ObjectResponseResult<GeneralDepartmentResponse> objectResponseResult2 = await ReadObjectResponseAsync<GeneralDepartmentResponse>(response, dictionary, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
                            if (objectResponseResult2.Object == null)
                            {
                                throw new CoditechException(objectResponseResult2.Object.ErrorCode, objectResponseResult2.Object.ErrorMessage);
                            }

                            return objectResponseResult2.Object;
                        }
                    case HttpStatusCode.Created:
                        {
                            ObjectResponseResult<GeneralDepartmentResponse> objectResponseResult = await ReadObjectResponseAsync<GeneralDepartmentResponse>(response, dictionary, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
                            if (objectResponseResult.Object == null)
                            {
                                throw new CoditechException(objectResponseResult.Object.ErrorCode, objectResponseResult.Object.ErrorMessage);
                            }

                            return objectResponseResult.Object;
                        }
                    default:
                        {
                            string value = ((response.Content != null) ? (await response.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false)) : null);
                            GeneralDepartmentResponse result = JsonConvert.DeserializeObject<GeneralDepartmentResponse>(value);
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
        /// Get generalDepartment by generalDepartment id.
        /// </summary>
        /// <param name="generalDepartmentId">GeneralDepartment id to get generalDepartment details.</param>
        /// <returns>Success</returns>
        /// <exception cref="CoditechException">A server side error occurred.</exception>
        public virtual GeneralDepartmentResponse GetDepartment(int generalDepartmentId)
        {
            return Task.Run(async () => await GetDepartmentAsync(generalDepartmentId, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>
        /// Get generalDepartment by generalDepartment id.
        /// </summary>
        /// <param name="generalDepartmentId">GeneralDepartment id to get generalDepartment details.</param>
        /// <returns>Success</returns>
        /// <exception cref="CoditechException">A server side error occurred.</exception>
        public virtual async Task<GeneralDepartmentResponse> GetDepartmentAsync(int generalDepartmentId, System.Threading.CancellationToken cancellationToken)
        {
            if (generalDepartmentId == null)
                throw new System.ArgumentNullException("generalDepartmentId");

            string endpoint = CoditechAdminSettings.CoditechOrganisationApiRootUri;
            endpoint += "/" + "GeneralDepartmentMaster/GetDepartment?deneralDepartmentMasterId=" + generalDepartmentId;
            endpoint = endpoint.Replace("{generalDepartmentId}", System.Uri.EscapeDataString(ConvertToString(generalDepartmentId, System.Globalization.CultureInfo.InvariantCulture)));
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
                    var objectResponse = await ReadObjectResponseAsync<GeneralDepartmentResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 204)
                {
                    return new GeneralDepartmentResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    GeneralDepartmentResponse typedBody = Newtonsoft.Json.JsonConvert.DeserializeObject<GeneralDepartmentResponse>(responseData);
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
        /// Update generalDepartment.
        /// </summary>
        /// <param name="body">model to update.</param>
        /// <returns>Success</returns>
        /// <exception cref="CoditechException">A server side error occurred.</exception>
        public virtual GeneralDepartmentResponse UpdateDepartment(GeneralDepartmentModel body)
        {
            return Task.Run(async () => await UpdateDepartmentAsync(body, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>
        /// Update generalDepartment.
        /// </summary>
        /// <param name="body">model to update.</param>
        /// <returns>Success</returns>
        /// <exception cref="CoditechException">A server side error occurred.</exception>
        public virtual async Task<GeneralDepartmentResponse> UpdateDepartmentAsync(GeneralDepartmentModel body, System.Threading.CancellationToken cancellationToken)
        {
            string endpoint = CoditechAdminSettings.CoditechOrganisationApiRootUri;
            endpoint += "/" + "GeneralDepartmentMaster/UpdateDepartment";
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
                    var objectResponse = await ReadObjectResponseAsync<GeneralDepartmentResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 201)
                {
                    var objectResponse = await ReadObjectResponseAsync<GeneralDepartmentResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    GeneralDepartmentResponse typedBody = Newtonsoft.Json.JsonConvert.DeserializeObject<GeneralDepartmentResponse>(responseData);
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
        /// Delete generalDepartment.
        /// </summary>
        /// <param name="body">GeneralDepartment Id.</param>
        /// <returns>Success</returns>
        /// <exception cref="CoditechException">A server side error occurred.</exception>
        public virtual TrueFalseResponse DeleteDepartment(ParameterModel body)
        {
            return Task.Run(async () => await DeleteDepartmentAsync(body, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>
        /// Delete generalDepartment.
        /// </summary>
        /// <param name="body">GeneralDepartment Id.</param>
        /// <returns>Success</returns>
        /// <exception cref="CoditechException">A server side error occurred.</exception>
        public virtual async Task<TrueFalseResponse> DeleteDepartmentAsync(ParameterModel body, System.Threading.CancellationToken cancellationToken)
        {
            string endpoint = CoditechAdminSettings.CoditechOrganisationApiRootUri;
            endpoint += "/" + "GeneralDepartmentMaster/DeleteDepartment";
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
