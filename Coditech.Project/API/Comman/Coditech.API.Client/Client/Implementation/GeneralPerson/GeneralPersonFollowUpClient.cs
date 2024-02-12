using Coditech.API.Endpoint;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.GeneralPerson.GeneralPersonFollowUp;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper.Utilities;
using Newtonsoft.Json;
using System.Net;

namespace Coditech.API.Client
{
    public class GeneralPersonFollowUpClient : BaseClient, IGeneralPersonFollowUpClient
    {
        GeneralPersonFollowUpEndpoint generalPersonFollowUpEndpoint = null;
        public GeneralPersonFollowUpClient()
        {
            generalPersonFollowUpEndpoint = new GeneralPersonFollowUpEndpoint();
        }
        public virtual GeneralPersonFollowUpListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            return Task.Run(async () => await ListAsync(expand, filter, sort, pageIndex, pageSize, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<GeneralPersonFollowUpListResponse> ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize, CancellationToken cancellationToken)
        {
            string endpoint = generalPersonFollowUpEndpoint.ListAsync(expand, filter, sort, pageIndex, pageSize);
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
                    var objectResponse = await ReadObjectResponseAsync<GeneralPersonFollowUpListResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else if (status_ == 204)
                {
                    return new GeneralPersonFollowUpListResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    GeneralPersonFollowUpListResponse typedBody = JsonConvert.DeserializeObject<GeneralPersonFollowUpListResponse>(responseData);
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

        public virtual GeneralPersonFollowUpResponse CreatePersonFollowUp(GeneralPersonFollowUpModel body)
        {
            return Task.Run(async () => await CreatePersonFollowUpAsync(body, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<GeneralPersonFollowUpResponse> CreatePersonFollowUpAsync(GeneralPersonFollowUpModel body, CancellationToken cancellationToken)
        {
            string endpoint = generalPersonFollowUpEndpoint.CreatePersonFollowUpAsync();
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
                            ObjectResponseResult<GeneralPersonFollowUpResponse> objectResponseResult2 = await ReadObjectResponseAsync<GeneralPersonFollowUpResponse>(response, BindHeaders(response), cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
                            if (objectResponseResult2.Object == null)
                            {
                                throw new CoditechException(objectResponseResult2.Object.ErrorCode, objectResponseResult2.Object.ErrorMessage);
                            }

                            return objectResponseResult2.Object;
                        }
                    case HttpStatusCode.Created:
                        {
                            ObjectResponseResult<GeneralPersonFollowUpResponse> objectResponseResult = await ReadObjectResponseAsync<GeneralPersonFollowUpResponse>(response, dictionary, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
                            if (objectResponseResult.Object == null)
                            {
                                throw new CoditechException(objectResponseResult.Object.ErrorCode, objectResponseResult.Object.ErrorMessage);
                            }

                            return objectResponseResult.Object;
                        }
                    default:
                        {
                            string value = ((response.Content != null) ? (await response.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false)) : null);
                            GeneralPersonFollowUpResponse result = JsonConvert.DeserializeObject<GeneralPersonFollowUpResponse>(value);
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

        public virtual GeneralPersonFollowUpResponse GetPersonFollowUp(long generalPersonFollowUpId)
        {
            return Task.Run(async () => await GetPersonFollowUpAsync(generalPersonFollowUpId, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<GeneralPersonFollowUpResponse> GetPersonFollowUpAsync(long generalPersonFollowUpId, System.Threading.CancellationToken cancellationToken)
        {
            if (generalPersonFollowUpId <= 0)
                throw new System.ArgumentNullException("generalPersonFollowUpId");

            string endpoint = generalPersonFollowUpEndpoint.GetPersonFollowUpAsync(generalPersonFollowUpId);
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
                    var objectResponse = await ReadObjectResponseAsync<GeneralPersonFollowUpResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 204)
                {
                    return new GeneralPersonFollowUpResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    GeneralPersonFollowUpResponse typedBody = JsonConvert.DeserializeObject<GeneralPersonFollowUpResponse>(responseData);
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

        public virtual GeneralPersonFollowUpResponse UpdatePersonFollowUp(GeneralPersonFollowUpModel body)
        {
            return Task.Run(async () => await UpdatePersonFollowUpAsync(body, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<GeneralPersonFollowUpResponse> UpdatePersonFollowUpAsync(GeneralPersonFollowUpModel body, System.Threading.CancellationToken cancellationToken)
        {
            string endpoint = generalPersonFollowUpEndpoint.UpdatePersonFollowUpAsync();
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
                    var objectResponse = await ReadObjectResponseAsync<GeneralPersonFollowUpResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 201)
                {
                    var objectResponse = await ReadObjectResponseAsync<GeneralPersonFollowUpResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    GeneralPersonFollowUpResponse typedBody = JsonConvert.DeserializeObject<GeneralPersonFollowUpResponse>(responseData);
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

        public virtual TrueFalseResponse DeletePersonFollowUp(ParameterModel body)
        {
            return Task.Run(async () => await DeletePersonFollowUpAsync(body, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<TrueFalseResponse> DeletePersonFollowUpAsync(ParameterModel body, System.Threading.CancellationToken cancellationToken)
        {
            string endpoint = generalPersonFollowUpEndpoint.DeletePersonFollowUpAsync();
            HttpResponseMessage response = null;
            var disposeResponse = true;
            try
            {
                ApiStatus status = new ApiStatus();

                response = await PostResourceToEndpointAsync(endpoint, JsonConvert.SerializeObject(body), status, cancellationToken).ConfigureAwait(false);

                var headers_ = BindHeaders(response);
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
                    TrueFalseResponse typedBody = JsonConvert.DeserializeObject<TrueFalseResponse>(responseData);
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
