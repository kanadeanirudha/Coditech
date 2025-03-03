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
    public class AccGLTransactionClient : BaseClient, IAccGLTransactionClient
    {
        AccGLTransactionEndpoint accGLTransactionEndpoint = null;
        public AccGLTransactionClient()
        {
            accGLTransactionEndpoint = new AccGLTransactionEndpoint();
        }

        public virtual AccGLTransactionListResponse List(string selectedCentreCode, int accSetupBalanceSheetId, short generalFinancialYearId ,short accSetupTransactionTypeId,byte accSetupBalanceSheetTypeId,  IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            return Task.Run(async () => await ListAsync(selectedCentreCode, accSetupBalanceSheetId, generalFinancialYearId, accSetupTransactionTypeId, accSetupBalanceSheetTypeId, expand, filter, sort, pageIndex, pageSize, CancellationToken.None)).GetAwaiter().GetResult();
        }
        

        public virtual async Task<AccGLTransactionListResponse> ListAsync(string selectedCentreCode,int accSetupBalanceSheetId ,short generalFinancialYearId, short accSetupTransactionTypeId, byte accSetupBalanceSheetTypeId,  IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize, CancellationToken cancellationToken)
        {
            string endpoint = accGLTransactionEndpoint.ListAsync(selectedCentreCode, accSetupBalanceSheetId, generalFinancialYearId, accSetupTransactionTypeId, accSetupBalanceSheetTypeId, expand, filter, sort, pageIndex, pageSize);
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
                    var objectResponse = await ReadObjectResponseAsync<AccGLTransactionListResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else if (status_ == 204)
                {
                    return new AccGLTransactionListResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    AccGLTransactionListResponse typedBody = JsonConvert.DeserializeObject<AccGLTransactionListResponse>(responseData);
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

        public virtual AccGLTransactionResponse CreateGLTransaction(AccGLTransactionModel body)
        {
            return Task.Run(async () => await CreateGLTransactionAsync(body, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<AccGLTransactionResponse> CreateGLTransactionAsync(AccGLTransactionModel body, CancellationToken cancellationToken)
        {
            string endpoint = accGLTransactionEndpoint.CreateGLTransactionAsync();
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
                            ObjectResponseResult<AccGLTransactionResponse> objectResponseResult2 = await ReadObjectResponseAsync<AccGLTransactionResponse>(response, BindHeaders(response), cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
                            if (objectResponseResult2.Object == null)
                            {
                                throw new CoditechException(objectResponseResult2.Object.ErrorCode, objectResponseResult2.Object.ErrorMessage);
                            }

                            return objectResponseResult2.Object;
                        }
                    case HttpStatusCode.Created:
                        {
                            ObjectResponseResult<AccGLTransactionResponse> objectResponseResult = await ReadObjectResponseAsync<AccGLTransactionResponse>(response, dictionary, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
                            if (objectResponseResult.Object == null)
                            {
                                throw new CoditechException(objectResponseResult.Object.ErrorCode, objectResponseResult.Object.ErrorMessage);
                            }

                            return objectResponseResult.Object;
                        }
                    default:
                        {
                            string value = ((response.Content != null) ? (await response.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false)) : null);
                            AccGLTransactionResponse result = JsonConvert.DeserializeObject<AccGLTransactionResponse>(value);
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

        public virtual AccGLTransactionResponse GetGLTransaction(long accGLTransactionId)
        {
            return Task.Run(async () => await GetGLTransactionAsync(accGLTransactionId, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<AccGLTransactionResponse> GetGLTransactionAsync(long accGLTransactionId, System.Threading.CancellationToken cancellationToken)
        {
            if (accGLTransactionId <= 0)
                throw new System.ArgumentNullException("accGLTransactionId");

            string endpoint = accGLTransactionEndpoint.GetGLTransactionAsync(accGLTransactionId);
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
                    var objectResponse = await ReadObjectResponseAsync<AccGLTransactionResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 204)
                {
                    return new AccGLTransactionResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    AccGLTransactionResponse typedBody = JsonConvert.DeserializeObject<AccGLTransactionResponse>(responseData);
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

        public virtual AccGLTransactionResponse UpdateGLTransaction(AccGLTransactionModel body)
        {
            return Task.Run(async () => await UpdateGLTransactionAsync(body, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<AccGLTransactionResponse> UpdateGLTransactionAsync(AccGLTransactionModel body, System.Threading.CancellationToken cancellationToken)
        {
            string endpoint = accGLTransactionEndpoint.UpdateGLTransactionAsync();
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
                    var objectResponse = await ReadObjectResponseAsync<AccGLTransactionResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 201)
                {
                    var objectResponse = await ReadObjectResponseAsync<AccGLTransactionResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    AccGLTransactionResponse typedBody = JsonConvert.DeserializeObject<AccGLTransactionResponse>(responseData);
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

        //public virtual TrueFalseResponse DeleteUpdateGLTransaction(ParameterModel body)
        //{
        //    return Task.Run(async () => await DeleteBalanceSheetAsync(body, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        //}

        //public virtual async Task<TrueFalseResponse> DeleteBalanceSheetAsync(ParameterModel body, System.Threading.CancellationToken cancellationToken)
        //{
        //    string endpoint = accGLTransactionEndpoint.DeleteBalanceSheetAsync();
        //    HttpResponseMessage response = null;
        //    var disposeResponse = true;
        //    try
        //    {
        //        ApiStatus status = new ApiStatus();
        //        response = await PostResourceToEndpointAsync(endpoint, JsonConvert.SerializeObject(body), status, cancellationToken).ConfigureAwait(false);
        //        var headers_ = BindHeaders(response);
        //        var status_ = (int)response.StatusCode;
        //        if (status_ == 200)
        //        {
        //            var objectResponse = await ReadObjectResponseAsync<TrueFalseResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
        //            if (objectResponse.Object == null)
        //            {
        //                throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
        //            }
        //            return objectResponse.Object;
        //        }
        //        else
        //        {
        //            string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        //            TrueFalseResponse typedBody = JsonConvert.DeserializeObject<TrueFalseResponse>(responseData);
        //            UpdateApiStatus(typedBody, status, response);
        //            throw new CoditechException(status.ErrorCode, status.ErrorMessage, status.StatusCode);
        //        }
        //    }
        //    finally
        //    {
        //        if (disposeResponse)
        //            response.Dispose();
        //    }
        //}
    }
}
