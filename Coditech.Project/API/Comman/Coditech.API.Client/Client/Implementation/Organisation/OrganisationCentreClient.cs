using Coditech.API.Endpoint;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper.Utilities;
using Coditech.Model;
using Newtonsoft.Json;
using System.Net;

namespace Coditech.API.Client
{
    public class OrganisationCentreClient : BaseClient, IOrganisationCentreClient
    {
        OrganisationCentreEndpoint organisationCentreEndpoint = null;
        public OrganisationCentreClient()
        {
            organisationCentreEndpoint = new OrganisationCentreEndpoint();
        }
        #region OrganisationCentre
        public virtual OrganisationCentreListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            return Task.Run(async () => await ListAsync(expand, filter, sort, pageIndex, pageSize, CancellationToken.None)).GetAwaiter().GetResult();
        }
        public virtual async Task<OrganisationCentreListResponse> ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize, CancellationToken cancellationToken)
        {
            string endpoint = organisationCentreEndpoint.ListAsync(expand, filter, sort, pageIndex, pageSize);
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
                    var objectResponse = await ReadObjectResponseAsync<OrganisationCentreListResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else if (status_ == 204)
                {
                    return new OrganisationCentreListResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    OrganisationCentreListResponse typedBody = JsonConvert.DeserializeObject<OrganisationCentreListResponse>(responseData);
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

        public virtual OrganisationCentreResponse CreateOrganisationCentre(OrganisationCentreModel body)
        {
            return Task.Run(async () => await CreateOrganisationAsync(body, CancellationToken.None)).GetAwaiter().GetResult();
        }
        public virtual async Task<OrganisationCentreResponse> CreateOrganisationAsync(OrganisationCentreModel body, CancellationToken cancellationToken)
        {
            string endpoint = organisationCentreEndpoint.CreateOrganisationAsync();
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
                            ObjectResponseResult<OrganisationCentreResponse> objectResponseResult2 = await ReadObjectResponseAsync<OrganisationCentreResponse>(response, BindHeaders(response), cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
                            if (objectResponseResult2.Object == null)
                            {
                                throw new CoditechException(objectResponseResult2.Object.ErrorCode, objectResponseResult2.Object.ErrorMessage);
                            }

                            return objectResponseResult2.Object;
                        }
                    case HttpStatusCode.Created:
                        {
                            ObjectResponseResult<OrganisationCentreResponse> objectResponseResult = await ReadObjectResponseAsync<OrganisationCentreResponse>(response, dictionary, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
                            if (objectResponseResult.Object == null)
                            {
                                throw new CoditechException(objectResponseResult.Object.ErrorCode, objectResponseResult.Object.ErrorMessage);
                            }

                            return objectResponseResult.Object;
                        }
                    default:
                        {
                            string value = ((response.Content != null) ? (await response.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false)) : null);
                            OrganisationCentreResponse result = JsonConvert.DeserializeObject<OrganisationCentreResponse>(value);
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
        public virtual OrganisationCentreResponse GetOrganisationCentre(short organisationCentreId)
        {
            return Task.Run(async () => await GetOrganisationAsync(organisationCentreId, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<OrganisationCentreResponse> GetOrganisationAsync(short organisationCentreId, CancellationToken cancellationToken)
        {
            if (organisationCentreId <= 0)
                throw new System.ArgumentNullException("organisationCentreId");

            string endpoint = organisationCentreEndpoint.GetOrganisationAsync(organisationCentreId);
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
                    var objectResponse = await ReadObjectResponseAsync<OrganisationCentreResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 204)
                {
                    return new OrganisationCentreResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    OrganisationCentreResponse typedBody = JsonConvert.DeserializeObject<OrganisationCentreResponse>(responseData);
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
        public virtual OrganisationCentreResponse UpdateOrganisationCentre(OrganisationCentreModel body)
        {
            return Task.Run(async () => await UpdateOrganisationAsync(body, CancellationToken.None)).GetAwaiter().GetResult();
        }
        public virtual async Task<OrganisationCentreResponse> UpdateOrganisationAsync(OrganisationCentreModel body, CancellationToken cancellationToken)
        {
            string endpoint = organisationCentreEndpoint.UpdateOrganisationAsync();
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
                    var objectResponse = await ReadObjectResponseAsync<OrganisationCentreResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 201)
                {
                    var objectResponse = await ReadObjectResponseAsync<OrganisationCentreResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    OrganisationCentreResponse typedBody = JsonConvert.DeserializeObject<OrganisationCentreResponse>(responseData);
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
        public virtual TrueFalseResponse DeleteOrganisationCentre(ParameterModel body)
        {
            return Task.Run(async () => await DeleteOrganisationAsync(body, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<TrueFalseResponse> DeleteOrganisationAsync(ParameterModel body, CancellationToken cancellationToken)
        {
            string endpoint = organisationCentreEndpoint.DeleteOrganisationAsync();
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
        #endregion

        #region PrintingFormat
        public virtual OrganisationCentrePrintingFormatResponse GetPrintingFormat(short organisationCentreId)
        {
            return Task.Run(async () => await GetPrintingFormatAsync(organisationCentreId, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<OrganisationCentrePrintingFormatResponse> GetPrintingFormatAsync(short organisationCentreId, CancellationToken cancellationToken)
        {
            if (organisationCentreId <= 0)
                throw new System.ArgumentNullException("organisationCentreId");

            string endpoint = organisationCentreEndpoint.GetPrintingFormatAsync(organisationCentreId);
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
                    var objectResponse = await ReadObjectResponseAsync<OrganisationCentrePrintingFormatResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 204)
                {
                    return new OrganisationCentrePrintingFormatResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    OrganisationCentrePrintingFormatResponse typedBody = JsonConvert.DeserializeObject<OrganisationCentrePrintingFormatResponse>(responseData);
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
        public virtual OrganisationCentrePrintingFormatResponse UpdatePrintingFormat(OrganisationCentrePrintingFormatModel body)
        {
            return Task.Run(async () => await UpdatePrintingFormatAsync(body, CancellationToken.None)).GetAwaiter().GetResult();
        }
        public virtual async Task<OrganisationCentrePrintingFormatResponse> UpdatePrintingFormatAsync(OrganisationCentrePrintingFormatModel body, CancellationToken cancellationToken)
        {
            string endpoint = organisationCentreEndpoint.UpdatePrintingFormatAsync();
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
                    var objectResponse = await ReadObjectResponseAsync<OrganisationCentrePrintingFormatResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 201)
                {
                    var objectResponse = await ReadObjectResponseAsync<OrganisationCentrePrintingFormatResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    OrganisationCentrePrintingFormatResponse typedBody = JsonConvert.DeserializeObject<OrganisationCentrePrintingFormatResponse>(responseData);
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
        #endregion

        #region CentrewiseGSTSetup
        public virtual OrganisationCentrewiseGSTCredentialResponse GetCentrewiseGSTSetup(short organisationCentreId)
        {
            return Task.Run(async () => await GetCentrewiseGSTSetupAsync(organisationCentreId, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<OrganisationCentrewiseGSTCredentialResponse> GetCentrewiseGSTSetupAsync(short organisationCentreId, CancellationToken cancellationToken)
        {
            if (organisationCentreId <= 0)
                throw new System.ArgumentNullException("organisationCentreId");

            string endpoint = organisationCentreEndpoint.GetCentrewiseGSTSetupAsync(organisationCentreId);
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
                    var objectResponse = await ReadObjectResponseAsync<OrganisationCentrewiseGSTCredentialResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 204)
                {
                    return new OrganisationCentrewiseGSTCredentialResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    OrganisationCentrewiseGSTCredentialResponse typedBody = JsonConvert.DeserializeObject<OrganisationCentrewiseGSTCredentialResponse>(responseData);
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
        public virtual OrganisationCentrewiseGSTCredentialResponse UpdateCentrewiseGSTSetup(OrganisationCentrewiseGSTCredentialModel body)
        {
            return Task.Run(async () => await UpdateCentrewiseGSTSetupAsync(body, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<OrganisationCentrewiseGSTCredentialResponse> UpdateCentrewiseGSTSetupAsync(OrganisationCentrewiseGSTCredentialModel body, CancellationToken cancellationToken)
        {
            string endpoint = organisationCentreEndpoint.UpdateCentrewiseGSTSetupAsync();
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
                    var objectResponse = await ReadObjectResponseAsync<OrganisationCentrewiseGSTCredentialResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 201)
                {
                    var objectResponse = await ReadObjectResponseAsync<OrganisationCentrewiseGSTCredentialResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    OrganisationCentrewiseGSTCredentialResponse typedBody = JsonConvert.DeserializeObject<OrganisationCentrewiseGSTCredentialResponse>(responseData);
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
        #endregion

        #region CentrewiseSmtpSetup
        public virtual OrganisationCentrewiseSmtpSettingResponse GetCentrewiseSmtpSetup(short organisationCentreId)
        {
            return Task.Run(async () => await GetCentrewiseSmtpSetupAsync(organisationCentreId, CancellationToken.None)).GetAwaiter().GetResult();
        }
        public virtual async Task<OrganisationCentrewiseSmtpSettingResponse> GetCentrewiseSmtpSetupAsync(short organisationCentreId, CancellationToken cancellationToken)
        {
            if (organisationCentreId <= 0)
                throw new System.ArgumentNullException("organisationCentreId");

            string endpoint = organisationCentreEndpoint.GetCentrewiseSmtpSetupAsync(organisationCentreId);
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
                    var objectResponse = await ReadObjectResponseAsync<OrganisationCentrewiseSmtpSettingResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 204)
                {
                    return new OrganisationCentrewiseSmtpSettingResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    OrganisationCentrewiseSmtpSettingResponse typedBody = JsonConvert.DeserializeObject<OrganisationCentrewiseSmtpSettingResponse>(responseData);
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

        public virtual OrganisationCentrewiseSmtpSettingResponse UpdateCentrewiseSmtpSetup(OrganisationCentrewiseSmtpSettingModel body)
        {
            return Task.Run(async () => await UpdateCentrewiseSmtpSetupAsync(body, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<OrganisationCentrewiseSmtpSettingResponse> UpdateCentrewiseSmtpSetupAsync(OrganisationCentrewiseSmtpSettingModel body, CancellationToken cancellationToken)
        {
            string endpoint = organisationCentreEndpoint.UpdateCentrewiseSmtpSetupAsync();
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
                    var objectResponse = await ReadObjectResponseAsync<OrganisationCentrewiseSmtpSettingResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 201)
                {
                    var objectResponse = await ReadObjectResponseAsync<OrganisationCentrewiseSmtpSettingResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    OrganisationCentrewiseSmtpSettingResponse typedBody = JsonConvert.DeserializeObject<OrganisationCentrewiseSmtpSettingResponse>(responseData);
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
        #endregion

        #region CentrewiseSmsSetup
        public virtual OrganisationCentrewiseSmsSettingResponse GetCentrewiseSmsSetup(short organisationCentreId, byte generalSmsProviderId)
        {
            return Task.Run(async () => await GetCentrewiseSmsSetupAsync(organisationCentreId, generalSmsProviderId, CancellationToken.None)).GetAwaiter().GetResult();
        }
        public virtual async Task<OrganisationCentrewiseSmsSettingResponse> GetCentrewiseSmsSetupAsync(short organisationCentreId, byte generalSmsProviderId, CancellationToken cancellationToken)
        {
            if (organisationCentreId <= 0)
                throw new System.ArgumentNullException("organisationCentreId");

            string endpoint = organisationCentreEndpoint.GetCentrewiseSmsSetupAsync(organisationCentreId, generalSmsProviderId);
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
                    var objectResponse = await ReadObjectResponseAsync<OrganisationCentrewiseSmsSettingResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 204)
                {
                    return new OrganisationCentrewiseSmsSettingResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    OrganisationCentrewiseSmsSettingResponse typedBody = JsonConvert.DeserializeObject<OrganisationCentrewiseSmsSettingResponse>(responseData);
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

        public virtual OrganisationCentrewiseSmsSettingResponse UpdateCentrewiseSmsSetup(OrganisationCentrewiseSmsSettingModel body)
        {
            return Task.Run(async () => await UpdateCentrewiseSmsSetupAsync(body, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<OrganisationCentrewiseSmsSettingResponse> UpdateCentrewiseSmsSetupAsync(OrganisationCentrewiseSmsSettingModel body, CancellationToken cancellationToken)
        {
            string endpoint = organisationCentreEndpoint.UpdateCentrewiseSmsSetupAsync();
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
                    var objectResponse = await ReadObjectResponseAsync<OrganisationCentrewiseSmsSettingResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 201)
                {
                    var objectResponse = await ReadObjectResponseAsync<OrganisationCentrewiseSmsSettingResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    OrganisationCentrewiseSmsSettingResponse typedBody = JsonConvert.DeserializeObject<OrganisationCentrewiseSmsSettingResponse>(responseData);
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
        #endregion

        #region CentrewiseWhatsAppSetup
        public virtual OrganisationCentrewiseWhatsAppSettingResponse GetCentrewiseWhatsAppSetup(short organisationCentreId, byte generalWhatsAppProviderId)
        {
            return Task.Run(async () => await GetCentrewiseWhatsAppSetupAsync(organisationCentreId, generalWhatsAppProviderId, CancellationToken.None)).GetAwaiter().GetResult();
        }
        public virtual async Task<OrganisationCentrewiseWhatsAppSettingResponse> GetCentrewiseWhatsAppSetupAsync(short organisationCentreId, byte generalWhatsAppProviderId, CancellationToken cancellationToken)
        {
            if (organisationCentreId <= 0)
                throw new System.ArgumentNullException("organisationCentreId");

            string endpoint = organisationCentreEndpoint.GetCentrewiseWhatsAppSetupAsync(organisationCentreId, generalWhatsAppProviderId);
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
                    var objectResponse = await ReadObjectResponseAsync<OrganisationCentrewiseWhatsAppSettingResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 204)
                {
                    return new OrganisationCentrewiseWhatsAppSettingResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    OrganisationCentrewiseWhatsAppSettingResponse typedBody = JsonConvert.DeserializeObject<OrganisationCentrewiseWhatsAppSettingResponse>(responseData);
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

        public virtual OrganisationCentrewiseWhatsAppSettingResponse UpdateCentrewiseWhatsAppSetup(OrganisationCentrewiseWhatsAppSettingModel body)
        {
            return Task.Run(async () => await UpdateCentrewiseWhatsAppSetupAsync(body, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<OrganisationCentrewiseWhatsAppSettingResponse> UpdateCentrewiseWhatsAppSetupAsync(OrganisationCentrewiseWhatsAppSettingModel body, CancellationToken cancellationToken)
        {
            string endpoint = organisationCentreEndpoint.UpdateCentrewiseWhatsAppSetupAsync();
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
                    var objectResponse = await ReadObjectResponseAsync<OrganisationCentrewiseWhatsAppSettingResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 201)
                {
                    var objectResponse = await ReadObjectResponseAsync<OrganisationCentrewiseWhatsAppSettingResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    OrganisationCentrewiseWhatsAppSettingResponse typedBody = JsonConvert.DeserializeObject<OrganisationCentrewiseWhatsAppSettingResponse>(responseData);
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
        #endregion

        #region CentrewiseEmailTemplate
        public virtual OrganisationCentrewiseEmailTemplateResponse GetCentrewiseEmailTemplateSetup(short organisationCentreId, string emailTemplateCode, string templateType)
        {
            return Task.Run(async () => await GetCentrewiseEmailTemplateSetupAsync(organisationCentreId, emailTemplateCode, templateType, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<OrganisationCentrewiseEmailTemplateResponse> GetCentrewiseEmailTemplateSetupAsync(short organisationCentreId, string emailTemplateCode, string templateType, CancellationToken cancellationToken)
        {
            if (organisationCentreId <= 0)
                throw new System.ArgumentNullException("organisationCentreId");

            string endpoint = organisationCentreEndpoint.GetCentrewiseEmailTemplateSetupAsync(organisationCentreId, emailTemplateCode, templateType);
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
                    var objectResponse = await ReadObjectResponseAsync<OrganisationCentrewiseEmailTemplateResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 204)
                {
                    return new OrganisationCentrewiseEmailTemplateResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    OrganisationCentrewiseEmailTemplateResponse typedBody = JsonConvert.DeserializeObject<OrganisationCentrewiseEmailTemplateResponse>(responseData);
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

        public virtual OrganisationCentrewiseEmailTemplateResponse UpdateCentrewiseEmailTemplateSetup(OrganisationCentrewiseEmailTemplateModel body)
        {
            return Task.Run(async () => await UpdateCentrewiseEmailTemplateSetupAsync(body, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<OrganisationCentrewiseEmailTemplateResponse> UpdateCentrewiseEmailTemplateSetupAsync(OrganisationCentrewiseEmailTemplateModel body, CancellationToken cancellationToken)
        {
            string endpoint = organisationCentreEndpoint.UpdateCentrewiseEmailTemplateSetupAsync();
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
                    var objectResponse = await ReadObjectResponseAsync<OrganisationCentrewiseEmailTemplateResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 201)
                {
                    var objectResponse = await ReadObjectResponseAsync<OrganisationCentrewiseEmailTemplateResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    OrganisationCentrewiseEmailTemplateResponse typedBody = JsonConvert.DeserializeObject<OrganisationCentrewiseEmailTemplateResponse>(responseData);
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
        #endregion

        #region CentrewiseSMSTemplate
        public virtual OrganisationCentrewiseEmailTemplateResponse GetCentrewiseSMSTemplateSetup(short organisationCentreId, string emailTemplateCode)
        {
            return Task.Run(async () => await GetCentrewiseSMSTemplateSetupAsync(organisationCentreId, emailTemplateCode, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<OrganisationCentrewiseEmailTemplateResponse> GetCentrewiseSMSTemplateSetupAsync(short organisationCentreId, string emailTemplateCode, CancellationToken cancellationToken)
        {
            if (organisationCentreId <= 0)
                throw new System.ArgumentNullException("organisationCentreId");

            string endpoint = organisationCentreEndpoint.GetCentrewiseSMSTemplateSetupAsync(organisationCentreId, emailTemplateCode);
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
                    var objectResponse = await ReadObjectResponseAsync<OrganisationCentrewiseEmailTemplateResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 204)
                {
                    return new OrganisationCentrewiseEmailTemplateResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    OrganisationCentrewiseEmailTemplateResponse typedBody = JsonConvert.DeserializeObject<OrganisationCentrewiseEmailTemplateResponse>(responseData);
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

        public virtual OrganisationCentrewiseEmailTemplateResponse UpdateCentrewiseSMSTemplateSetup(OrganisationCentrewiseEmailTemplateModel body)
        {
            return Task.Run(async () => await UpdateCentrewiseSMSTemplateSetupAsync(body, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<OrganisationCentrewiseEmailTemplateResponse> UpdateCentrewiseSMSTemplateSetupAsync(OrganisationCentrewiseEmailTemplateModel body, CancellationToken cancellationToken)
        {
            string endpoint = organisationCentreEndpoint.UpdateCentrewiseSMSTemplateSetupAsync();
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
                    var objectResponse = await ReadObjectResponseAsync<OrganisationCentrewiseEmailTemplateResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 201)
                {
                    var objectResponse = await ReadObjectResponseAsync<OrganisationCentrewiseEmailTemplateResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    OrganisationCentrewiseEmailTemplateResponse typedBody = JsonConvert.DeserializeObject<OrganisationCentrewiseEmailTemplateResponse>(responseData);
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
        #endregion

        #region CentrewiseWhatsAppTemplate
        public virtual OrganisationCentrewiseEmailTemplateResponse GetCentrewiseWhatsAppTemplateSetup(short organisationCentreId, string emailTemplateCode)
        {
            return Task.Run(async () => await GetCentrewiseWhatsAppTemplateSetupAsync(organisationCentreId, emailTemplateCode, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<OrganisationCentrewiseEmailTemplateResponse> GetCentrewiseWhatsAppTemplateSetupAsync(short organisationCentreId, string emailTemplateCode, CancellationToken cancellationToken)
        {
            if (organisationCentreId <= 0)
                throw new System.ArgumentNullException("organisationCentreId");

            string endpoint = organisationCentreEndpoint.GetCentrewiseWhatsAppTemplateSetupAsync(organisationCentreId, emailTemplateCode);
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
                    var objectResponse = await ReadObjectResponseAsync<OrganisationCentrewiseEmailTemplateResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 204)
                {
                    return new OrganisationCentrewiseEmailTemplateResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    OrganisationCentrewiseEmailTemplateResponse typedBody = JsonConvert.DeserializeObject<OrganisationCentrewiseEmailTemplateResponse>(responseData);
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

        public virtual OrganisationCentrewiseEmailTemplateResponse UpdateCentrewiseWhatsAppTemplateSetup(OrganisationCentrewiseEmailTemplateModel body)
        {
            return Task.Run(async () => await UpdateCentrewiseWhatsAppTemplateSetupAsync(body, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<OrganisationCentrewiseEmailTemplateResponse> UpdateCentrewiseWhatsAppTemplateSetupAsync(OrganisationCentrewiseEmailTemplateModel body, CancellationToken cancellationToken)
        {
            string endpoint = organisationCentreEndpoint.UpdateCentrewiseWhatsAppTemplateSetupAsync();
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
                    var objectResponse = await ReadObjectResponseAsync<OrganisationCentrewiseEmailTemplateResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 201)
                {
                    var objectResponse = await ReadObjectResponseAsync<OrganisationCentrewiseEmailTemplateResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    OrganisationCentrewiseEmailTemplateResponse typedBody = JsonConvert.DeserializeObject<OrganisationCentrewiseEmailTemplateResponse>(responseData);
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
        #endregion

        #region Centrewise UserName
        public virtual OrganisationCentrewiseUserNameRegistrationResponse GetCentrewiseUserName(short organisationCentreId, short organisationCentrewiseUserNameRegistrationId)
        {
            return Task.Run(async () => await GetCentrewiseUserNameAsync(organisationCentreId, organisationCentrewiseUserNameRegistrationId, CancellationToken.None)).GetAwaiter().GetResult();
        }
        public virtual async Task<OrganisationCentrewiseUserNameRegistrationResponse> GetCentrewiseUserNameAsync(short organisationCentreId, short organisationCentrewiseUserNameRegistrationId, CancellationToken cancellationToken)
        {
            if (organisationCentreId <= 0)
                throw new System.ArgumentNullException("organisationCentreId");

            string endpoint = organisationCentreEndpoint.GetCentrewiseUserNameAsync(organisationCentreId, organisationCentrewiseUserNameRegistrationId);
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
                    var objectResponse = await ReadObjectResponseAsync<OrganisationCentrewiseUserNameRegistrationResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 204)
                {
                    return new OrganisationCentrewiseUserNameRegistrationResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    OrganisationCentrewiseUserNameRegistrationResponse typedBody = JsonConvert.DeserializeObject<OrganisationCentrewiseUserNameRegistrationResponse>(responseData);
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

        public virtual OrganisationCentrewiseUserNameRegistrationResponse UpdateCentrewiseUserName(OrganisationCentrewiseUserNameRegistrationModel body)
        {
            return Task.Run(async () => await UpdateCentrewiseUserNameAsync(body, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<OrganisationCentrewiseUserNameRegistrationResponse> UpdateCentrewiseUserNameAsync(OrganisationCentrewiseUserNameRegistrationModel body, CancellationToken cancellationToken)
        {
            string endpoint = organisationCentreEndpoint.UpdateCentrewiseUserNameAsync();
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
                    var objectResponse = await ReadObjectResponseAsync<OrganisationCentrewiseUserNameRegistrationResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                if (status_ == 201)
                {
                    var objectResponse = await ReadObjectResponseAsync<OrganisationCentrewiseUserNameRegistrationResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    OrganisationCentrewiseUserNameRegistrationResponse typedBody = JsonConvert.DeserializeObject<OrganisationCentrewiseUserNameRegistrationResponse>(responseData);
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
        #endregion
    }
}
