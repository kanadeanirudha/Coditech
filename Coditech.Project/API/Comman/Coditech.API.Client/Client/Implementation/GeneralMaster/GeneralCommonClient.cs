﻿using Coditech.API.Endpoint;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Exceptions;
using Newtonsoft.Json;

namespace Coditech.API.Client
{
    public class GeneralCommonClient : BaseClient, IGeneralCommonClient
    {
        GeneralCommonEndpoint generalCommonEndpoint = null;
        public GeneralCommonClient()
        {
            generalCommonEndpoint = new GeneralCommonEndpoint();
        }

        public virtual GeneralMessagesResponse SendOTP(GeneralMessagesModel body)
        {
            return Task.Run(async () => await SendOTPAsync(body, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<GeneralMessagesResponse> SendOTPAsync(GeneralMessagesModel body, CancellationToken cancellationToken)
        {
            string endpoint = generalCommonEndpoint.SendOTPAsync();
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
                    var objectResponse = await ReadObjectResponseAsync<GeneralMessagesResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 201)
                {
                    var objectResponse = await ReadObjectResponseAsync<GeneralMessagesResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    GeneralMessagesResponse typedBody = JsonConvert.DeserializeObject<GeneralMessagesResponse>(responseData);
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

        public virtual CoditechApplicationSettingListResponse GetCoditechApplicationSettingList(string applicationCodes)
        {
            return Task.Run(async () => await GetCoditechApplicationSettingListAsync(applicationCodes, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<CoditechApplicationSettingListResponse> GetCoditechApplicationSettingListAsync(string applicationCodes, CancellationToken cancellationToken)
        {
            string endpoint = generalCommonEndpoint.GetCoditechApplicationSettingListAsync(applicationCodes);
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
                    var objectResponse = await ReadObjectResponseAsync<CoditechApplicationSettingListResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 204)
                {
                    return new CoditechApplicationSettingListResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    CoditechApplicationSettingListResponse typedBody = JsonConvert.DeserializeObject<CoditechApplicationSettingListResponse>(responseData);
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
        public virtual GeneralEnumaratorListResponse GetDropdownListByCode(string groupCodes)
        {
            return Task.Run(async () => await GetDropdownListByCode(groupCodes, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<GeneralEnumaratorListResponse> GetDropdownListByCode(string groupCodes, CancellationToken cancellationToken)
        {
            string endpoint = generalCommonEndpoint.GetDropdownListByCodeAsync(groupCodes);
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
                    var objectResponse = await ReadObjectResponseAsync<GeneralEnumaratorListResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 204)
                {
                    return new GeneralEnumaratorListResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    GeneralEnumaratorListResponse typedBody = JsonConvert.DeserializeObject<GeneralEnumaratorListResponse>(responseData);
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
        public virtual AccPrequisiteResponse GetAccountPrequisite(int balanceSheetId)
        {
            return Task.Run(async () => await GetAccountPrequisiteAsync(balanceSheetId, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<AccPrequisiteResponse> GetAccountPrequisiteAsync(int balanceSheetId, System.Threading.CancellationToken cancellationToken)
        {
            if (balanceSheetId <= 0)
                throw new System.ArgumentNullException("balanceSheetId");

            string endpoint = generalCommonEndpoint.GetAccountPrequisiteAsync(balanceSheetId);
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
                    var objectResponse = await ReadObjectResponseAsync<AccPrequisiteResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 204)
                {
                    return new AccPrequisiteResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    AccPrequisiteResponse typedBody = JsonConvert.DeserializeObject<AccPrequisiteResponse>(responseData);
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
