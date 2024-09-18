using Coditech.API.Endpoint;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper.Utilities;

using Newtonsoft.Json;

using System.Net;

namespace Coditech.API.Client
{
    public class HospitalPathologyTestPricesClient : BaseClient, IHospitalPathologyTestPricesClient
    {
        HospitalPathologyTestPricesEndpoint hospitalPathologyTestPricesEndpoint = null;
        public HospitalPathologyTestPricesClient()
        {
            hospitalPathologyTestPricesEndpoint = new HospitalPathologyTestPricesEndpoint();
        }
        public virtual HospitalPathologyTestPricesListResponse List(int hospitalPathologyPriceCategoryEnumId, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            return Task.Run(async () => await ListAsync(hospitalPathologyPriceCategoryEnumId,expand, filter, sort, pageIndex, pageSize, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<HospitalPathologyTestPricesListResponse> ListAsync(int hospitalPathologyPriceCategoryEnumId, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize, CancellationToken cancellationToken)
        {
            string endpoint = hospitalPathologyTestPricesEndpoint.ListAsync(hospitalPathologyPriceCategoryEnumId,expand, filter, sort, pageIndex, pageSize);
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
                    var objectResponse = await ReadObjectResponseAsync<HospitalPathologyTestPricesListResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else if (status_ == 204)
                {
                    return new HospitalPathologyTestPricesListResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    HospitalPathologyTestPricesListResponse typedBody = JsonConvert.DeserializeObject<HospitalPathologyTestPricesListResponse>(responseData);
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

        public virtual HospitalPathologyTestPricesResponse CreateHospitalPathologyTestPrices(HospitalPathologyTestPricesModel body)
        {
            return Task.Run(async () => await CreateHospitalPathologyTestPricesAsync(body, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<HospitalPathologyTestPricesResponse> CreateHospitalPathologyTestPricesAsync(HospitalPathologyTestPricesModel body, CancellationToken cancellationToken)
        {
            string endpoint = hospitalPathologyTestPricesEndpoint.CreateHospitalPathologyTestPricesAsync();
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
                            ObjectResponseResult<HospitalPathologyTestPricesResponse> objectResponseResult2 = await ReadObjectResponseAsync<HospitalPathologyTestPricesResponse>(response, BindHeaders(response), cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
                            if (objectResponseResult2.Object == null)
                            {
                                throw new CoditechException(objectResponseResult2.Object.ErrorCode, objectResponseResult2.Object.ErrorMessage);
                            }

                            return objectResponseResult2.Object;
                        }
                    case HttpStatusCode.Created:
                        {
                            ObjectResponseResult<HospitalPathologyTestPricesResponse> objectResponseResult = await ReadObjectResponseAsync<HospitalPathologyTestPricesResponse>(response, dictionary, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
                            if (objectResponseResult.Object == null)
                            {
                                throw new CoditechException(objectResponseResult.Object.ErrorCode, objectResponseResult.Object.ErrorMessage);
                            }

                            return objectResponseResult.Object;
                        }
                    default:
                        {
                            string value = ((response.Content != null) ? (await response.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false)) : null);
                            HospitalPathologyTestPricesResponse result = JsonConvert.DeserializeObject<HospitalPathologyTestPricesResponse>(value);
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

        public virtual HospitalPathologyTestPricesResponse GetHospitalPathologyTestPrices(long hospitalPathologyTestPricesId)
        {
            return Task.Run(async () => await GetHospitalPathologyTestPricesAsync(hospitalPathologyTestPricesId, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<HospitalPathologyTestPricesResponse> GetHospitalPathologyTestPricesAsync(long hospitalPathologyTestPricesId, CancellationToken cancellationToken)
        {
            if (hospitalPathologyTestPricesId <= 0)
                throw new System.ArgumentNullException("hospitalPathologyTestPricesId");

            string endpoint = hospitalPathologyTestPricesEndpoint.GetHospitalPathologyTestPricesAsync(hospitalPathologyTestPricesId);
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
                    var objectResponse = await ReadObjectResponseAsync<HospitalPathologyTestPricesResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 204)
                {
                    return new HospitalPathologyTestPricesResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    HospitalPathologyTestPricesResponse typedBody = JsonConvert.DeserializeObject<HospitalPathologyTestPricesResponse>(responseData);
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

        public virtual HospitalPathologyTestPricesResponse UpdateHospitalPathologyTestPrices(HospitalPathologyTestPricesModel body)
        {
            return Task.Run(async () => await UpdateHospitalPathologyTestPricesAsync(body, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<HospitalPathologyTestPricesResponse> UpdateHospitalPathologyTestPricesAsync(HospitalPathologyTestPricesModel body, CancellationToken cancellationToken)
        {
            string endpoint = hospitalPathologyTestPricesEndpoint.UpdateHospitalPathologyTestPricesAsync();
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
                    var objectResponse = await ReadObjectResponseAsync<HospitalPathologyTestPricesResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 201)
                {
                    var objectResponse = await ReadObjectResponseAsync<HospitalPathologyTestPricesResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    HospitalPathologyTestPricesResponse typedBody = JsonConvert.DeserializeObject<HospitalPathologyTestPricesResponse>(responseData);
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

        public virtual TrueFalseResponse DeleteHospitalPathologyTestPrices(ParameterModel body)
        {
            return Task.Run(async () => await DeleteHospitalPathologyTestPricesAsync(body, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<TrueFalseResponse> DeleteHospitalPathologyTestPricesAsync(ParameterModel body, CancellationToken cancellationToken)
        {
            string endpoint = hospitalPathologyTestPricesEndpoint.DeleteHospitalPathologyTestPricesAsync();
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
