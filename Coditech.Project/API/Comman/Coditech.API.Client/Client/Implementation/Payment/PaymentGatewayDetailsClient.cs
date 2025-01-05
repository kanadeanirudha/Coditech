using Coditech.API.Endpoint;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Exceptions;
using Newtonsoft.Json;
using System.Net;
namespace Coditech.API.Client
{
    public class PaymentGatewayDetailsClient : BaseClient, IPaymentGatewayDetailsClient
    {
        PaymentGatewayDetailsEndpoint paymentGatewayDetailsEndpoint = null;
        public PaymentGatewayDetailsClient()
        {
            paymentGatewayDetailsEndpoint = new PaymentGatewayDetailsEndpoint();
        }
        public virtual PaymentGatewayDetailsListResponse List(string selectedCentreCode, byte paymentGatewayId)
        {
            return Task.Run(async () => await ListAsync(selectedCentreCode, paymentGatewayId, CancellationToken.None)).GetAwaiter().GetResult();
        }
        public virtual async Task<PaymentGatewayDetailsListResponse> ListAsync(string selectedCentreCode, byte paymentGatewayId, CancellationToken cancellationToken)
        {
            string endpoint = paymentGatewayDetailsEndpoint.ListAsync(selectedCentreCode, paymentGatewayId);
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
                    var objectResponse = await ReadObjectResponseAsync<PaymentGatewayDetailsListResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else if (status_ == 204)
                {
                    return new PaymentGatewayDetailsListResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    PaymentGatewayDetailsListResponse typedBody = JsonConvert.DeserializeObject<PaymentGatewayDetailsListResponse>(responseData);
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
        public virtual PaymentGatewayDetailsResponse CreatePaymentGatewayDetails(PaymentGatewayDetailsModel body)
        {
            return Task.Run(async () => await CreatePaymentGatewayDetailsAsync(body, CancellationToken.None)).GetAwaiter().GetResult();
        }
        public virtual async Task<PaymentGatewayDetailsResponse> CreatePaymentGatewayDetailsAsync(PaymentGatewayDetailsModel body, CancellationToken cancellationToken)
        {
            string endpoint = paymentGatewayDetailsEndpoint.CreatePaymentGatewayDetailsAsync();
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
                            ObjectResponseResult<PaymentGatewayDetailsResponse> objectResponseResult2 = await ReadObjectResponseAsync<PaymentGatewayDetailsResponse>(response, BindHeaders(response), cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
                            if (objectResponseResult2.Object == null)
                            {
                                throw new CoditechException(objectResponseResult2.Object.ErrorCode, objectResponseResult2.Object.ErrorMessage);
                            }

                            return objectResponseResult2.Object;
                        }
                    case HttpStatusCode.Created:
                        {
                            ObjectResponseResult<PaymentGatewayDetailsResponse> objectResponseResult = await ReadObjectResponseAsync<PaymentGatewayDetailsResponse>(response, dictionary, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
                            if (objectResponseResult.Object == null)
                            {
                                throw new CoditechException(objectResponseResult.Object.ErrorCode, objectResponseResult.Object.ErrorMessage);
                            }

                            return objectResponseResult.Object;
                        }
                    default:
                        {
                            string value = ((response.Content != null) ? (await response.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false)) : null);
                            PaymentGatewayDetailsResponse result = JsonConvert.DeserializeObject<PaymentGatewayDetailsResponse>(value);
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
        public virtual PaymentGatewayDetailsResponse GetPaymentGatewayDetails(int paymentGatewayDetailId)
        {
            return Task.Run(async () => await GetPaymentGatewayDetailsAsync(paymentGatewayDetailId, CancellationToken.None)).GetAwaiter().GetResult();
        }
        public virtual async Task<PaymentGatewayDetailsResponse> GetPaymentGatewayDetailsAsync(int paymentGatewayDetailId, CancellationToken cancellationToken)
        {
            if (paymentGatewayDetailId <= 0)
                throw new System.ArgumentNullException("paymentGatewayDetailId");

            string endpoint = paymentGatewayDetailsEndpoint.GetPaymentGatewayDetailsAsync(paymentGatewayDetailId);
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
                    var objectResponse = await ReadObjectResponseAsync<PaymentGatewayDetailsResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 204)
                {
                    return new PaymentGatewayDetailsResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    PaymentGatewayDetailsResponse typedBody = JsonConvert.DeserializeObject<PaymentGatewayDetailsResponse>(responseData);
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
        public virtual PaymentGatewayDetailsResponse UpdatePaymentGatewayDetails(PaymentGatewayDetailsModel body)
        {
            return Task.Run(async () => await UpdatePaymentGatewayDetailsAsync(body, CancellationToken.None)).GetAwaiter().GetResult();
        }
        public virtual async Task<PaymentGatewayDetailsResponse> UpdatePaymentGatewayDetailsAsync(PaymentGatewayDetailsModel body, CancellationToken cancellationToken)
        {
            string endpoint = paymentGatewayDetailsEndpoint.UpdatePaymentGatewayDetailsAsync();
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
                    var objectResponse = await ReadObjectResponseAsync<PaymentGatewayDetailsResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 201)
                {
                    var objectResponse = await ReadObjectResponseAsync<PaymentGatewayDetailsResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    PaymentGatewayDetailsResponse typedBody = JsonConvert.DeserializeObject<PaymentGatewayDetailsResponse>(responseData);
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
        public virtual TrueFalseResponse DeletePaymentGatewayDetails(ParameterModel body)
        {
            return Task.Run(async () => await DeletePaymentGatewayDetailsAsync(body, CancellationToken.None)).GetAwaiter().GetResult();
        }
        public virtual async Task<TrueFalseResponse> DeletePaymentGatewayDetailsAsync(ParameterModel body, CancellationToken cancellationToken)
        {
            string endpoint = paymentGatewayDetailsEndpoint.DeletePaymentGatewayDetailsAsync();
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


