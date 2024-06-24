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
            return Task.Run(async () => await ListAsync(expand, filter, sort, pageIndex, pageSize, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
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
            return Task.Run(async () => await GetOrganisationAsync(organisationCentreId, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<OrganisationCentreResponse> GetOrganisationAsync(short organisationCentreId, System.Threading.CancellationToken cancellationToken)
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
            return Task.Run(async () => await UpdateOrganisationAsync(body, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }
        public virtual async Task<OrganisationCentreResponse> UpdateOrganisationAsync(OrganisationCentreModel body, System.Threading.CancellationToken cancellationToken)
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
            return Task.Run(async () => await DeleteOrganisationAsync(body, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<TrueFalseResponse> DeleteOrganisationAsync(ParameterModel body, System.Threading.CancellationToken cancellationToken)
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
            return Task.Run(async () => await GetPrintingFormatAsync(organisationCentreId, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<OrganisationCentrePrintingFormatResponse> GetPrintingFormatAsync(short organisationCentreId, System.Threading.CancellationToken cancellationToken)
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
            return Task.Run(async () => await UpdatePrintingFormatAsync(body, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }
        public virtual async Task<OrganisationCentrePrintingFormatResponse> UpdatePrintingFormatAsync(OrganisationCentrePrintingFormatModel body, System.Threading.CancellationToken cancellationToken)
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
            return Task.Run(async () => await GetCentrewiseGSTSetupAsync(organisationCentreId, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<OrganisationCentrewiseGSTCredentialResponse> GetCentrewiseGSTSetupAsync(short organisationCentreId, System.Threading.CancellationToken cancellationToken)
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
            return Task.Run(async () => await UpdateCentrewiseGSTSetupAsync(body, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<OrganisationCentrewiseGSTCredentialResponse> UpdateCentrewiseGSTSetupAsync(OrganisationCentrewiseGSTCredentialModel body, System.Threading.CancellationToken cancellationToken)
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
            return Task.Run(async () => await GetCentrewiseSmtpSetupAsync(organisationCentreId, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }
        public virtual async Task<OrganisationCentrewiseSmtpSettingResponse> GetCentrewiseSmtpSetupAsync(short organisationCentreId, System.Threading.CancellationToken cancellationToken)
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
            return Task.Run(async () => await UpdateCentrewiseSmtpSetupAsync(body, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<OrganisationCentrewiseSmtpSettingResponse> UpdateCentrewiseSmtpSetupAsync(OrganisationCentrewiseSmtpSettingModel body, System.Threading.CancellationToken cancellationToken)
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
        public virtual OrganisationCentrewiseSmsSettingResponse GetCentrewiseSmsSetup(short organisationCentreId)
        {
            return Task.Run(async () => await GetCentrewiseSmsSetupAsync(organisationCentreId, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }
        public virtual async Task<OrganisationCentrewiseSmsSettingResponse> GetCentrewiseSmsSetupAsync(short organisationCentreId, System.Threading.CancellationToken cancellationToken)
        {
            if (organisationCentreId <= 0)
                throw new System.ArgumentNullException("organisationCentreId");

            string endpoint = organisationCentreEndpoint.GetCentrewiseSmsSetupAsync(organisationCentreId);
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
            return Task.Run(async () => await UpdateCentrewiseSmsSetupAsync(body, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<OrganisationCentrewiseSmsSettingResponse> UpdateCentrewiseSmsSetupAsync(OrganisationCentrewiseSmsSettingModel body, System.Threading.CancellationToken cancellationToken)
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

        #region CentrewiseEmailTemplate
        public virtual OrganisationCentrewiseEmailTemplateResponse GetCentrewiseEmailTemplateSetup(short organisationCentreId, string emailTemplateCode)
        {
            return Task.Run(async () => await GetCentrewiseEmailTemplateSetupAsync(organisationCentreId,emailTemplateCode, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<OrganisationCentrewiseEmailTemplateResponse> GetCentrewiseEmailTemplateSetupAsync(short organisationCentreId, string emailTemplateCode, System.Threading.CancellationToken cancellationToken)
        {
            if (organisationCentreId <= 0)
                throw new System.ArgumentNullException("organisationCentreId");

            string endpoint = organisationCentreEndpoint.GetCentrewiseEmailTemplateSetupAsync(organisationCentreId,emailTemplateCode);
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
            return Task.Run(async () => await UpdateCentrewiseEmailTemplateSetupAsync(body, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<OrganisationCentrewiseEmailTemplateResponse> UpdateCentrewiseEmailTemplateSetupAsync(OrganisationCentrewiseEmailTemplateModel body, System.Threading.CancellationToken cancellationToken)
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

        #region Centrewise UserName
        public virtual OrganisationCentrewiseUserNameRegistrationResponse GetCentrewiseUserName(short organisationCentreId,short organisationCentrewiseUserNameRegistrationId)
        {
            return Task.Run(async () => await GetCentrewiseUserNameAsync(organisationCentreId, organisationCentrewiseUserNameRegistrationId,System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }
        public virtual async Task<OrganisationCentrewiseUserNameRegistrationResponse> GetCentrewiseUserNameAsync(short organisationCentreId, short organisationCentrewiseUserNameRegistrationId, System.Threading.CancellationToken cancellationToken)
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
            return Task.Run(async () => await UpdateCentrewiseUserNameAsync(body, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<OrganisationCentrewiseUserNameRegistrationResponse> UpdateCentrewiseUserNameAsync(OrganisationCentrewiseUserNameRegistrationModel body, System.Threading.CancellationToken cancellationToken)
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
