using Coditech.Admin.Utilities;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.Exceptions;
using Coditech.Common.Helper.Utilities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Coditech.API.Client
{
    public partial class GeneralCountryClient : BaseClient, IGeneralCountryClient
    {
        public GeneralCountryClient()
        {
        }
        /// <summary>
        /// Gets list of GeneralCountry.
        /// </summary>
        /// <returns>Success</returns>
        /// <exception cref="CoditechException">A server side error occurred.</exception>
        public virtual GeneralCountryListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            return Task.Run(async () => await ListAsync(expand, filter, sort, pageIndex, pageSize, System.Threading.CancellationToken.None)).GetAwaiter().GetResult();
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>
        /// Gets list of GeneralCountry.
        /// </summary>
        /// <returns>Success</returns>
        /// <exception cref="CoditechException">A server side error occurred.</exception>
        public virtual async Task<GeneralCountryListResponse> ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize, CancellationToken cancellationToken)
        {
            string endpoint = CoditechAdminSettings.CoditechOrganisationApiRootUri;
            endpoint += "/" + "GeneralCountryMaster/GetCountryList";
            endpoint += BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize);
            HttpResponseMessage response_ = null;
            var disposeResponse_ = true;

            try
            {
                ApiStatus status = new ApiStatus();
                response_ = await GetResourceFromEndpointAsync(endpoint, status, cancellationToken).ConfigureAwait(false);
                var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                if (response_.Content != null && response_.Content.Headers != null)
                {
                    foreach (var item_ in response_.Content.Headers)
                        headers_[item_.Key] = item_.Value;
                }
                var status_ = (int)response_.StatusCode;
                if (status_ == 200)
                {
                    var objectResponse_ = await ReadObjectResponseAsync<GeneralCountryListResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
                    if (objectResponse_.Object == null)
                    {
                        throw new CoditechException(objectResponse_.Object.ErrorCode, objectResponse_.Object.ErrorMessage);
                    }
                    return objectResponse_.Object;
                }
                else
                if (status_ == 204)
                {
                    return new GeneralCountryListResponse();
                }
                else
                {
                    string responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                    GeneralCountryListResponse typedBody = Newtonsoft.Json.JsonConvert.DeserializeObject<GeneralCountryListResponse>(responseData_);
                    UpdateApiStatus(typedBody, status, response_);
                    throw new CoditechException(status.ErrorCode, status.ErrorMessage, status.StatusCode);
                }
            }
            finally
            {
                if (disposeResponse_)
                    response_.Dispose();
            }
        }
    }
}
