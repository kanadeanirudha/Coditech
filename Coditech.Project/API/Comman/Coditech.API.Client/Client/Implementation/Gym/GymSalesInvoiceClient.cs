﻿using Coditech.API.Endpoint;
using Coditech.Common.API.Model.Response;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper.Utilities;

using Newtonsoft.Json;

namespace Coditech.API.Client
{
    public class GymSalesInvoiceClient : BaseClient, IGymSalesInvoiceClient
    {
        GymSalesInvoiceEndpoint gymSalesInvoiceEndpoint = null;
        public GymSalesInvoiceClient()
        {
            gymSalesInvoiceEndpoint = new GymSalesInvoiceEndpoint();
        }
        public virtual GymMemberSalesInvoiceListResponse GymMemberServiceSalesInvoiceList(string SelectedCentreCode, DateTime? toDate, DateTime? fromDate, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            return Task.Run(async () => await GymMemberServiceSalesInvoiceListAsync(SelectedCentreCode, toDate, fromDate, expand, filter, sort, pageIndex, pageSize, CancellationToken.None)).GetAwaiter().GetResult();
        }

        public virtual async Task<GymMemberSalesInvoiceListResponse> GymMemberServiceSalesInvoiceListAsync(string SelectedCentreCode, DateTime? toDate, DateTime? fromDate, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize, CancellationToken cancellationToken)
        {
            string endpoint = gymSalesInvoiceEndpoint.GymMemberServiceSalesInvoiceListAsync(SelectedCentreCode, toDate, fromDate, expand, filter, sort, pageIndex, pageSize);
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
                    var objectResponse = await ReadObjectResponseAsync<GymMemberSalesInvoiceListResponse>(response, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse.Object == null)
                    {
                        throw new CoditechException(objectResponse.Object.ErrorCode, objectResponse.Object.ErrorMessage);
                    }
                    return objectResponse.Object;
                }
                else if (status_ == 204)
                {
                    return new GymMemberSalesInvoiceListResponse();
                }
                else
                {
                    string responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    GymMemberSalesInvoiceListResponse typedBody = JsonConvert.DeserializeObject<GymMemberSalesInvoiceListResponse>(responseData);
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
