using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public  interface IGeneralSmsProviderClient : IBaseClient
    {
        /// <summary>
        /// Get list of General SmsProvider.
        /// </summary>
        /// <returns>GeneralgeneralSmsproviderListResponse</returns>
        GeneralSmsProviderListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);
        
        /// <summary>
        /// Create GeneralSmsProvider.
        /// </summary>
        /// <param name="GeneralSmsProviderModel">GeneralSmsprovidermodel.</param>
        /// <returns>Returns GeneralSmsProviderResponse.</returns>

        GeneralSmsProviderResponse CreateSmsProvider(GeneralSmsProviderModel body);
        
        /// <summary>
        /// Get Country by generaSmsProviderId.
        /// </summary>
        /// <param name="generalSmsProviderId">generalSmsProviderId</param>
        /// <returns>Returns GeneralSmsProviderResponse.</returns>

        GeneralSmsProviderResponse GetSmsProvider(short generalSmsProviderId);
       
        /// <summary>
        /// Update SmsProvider.
        /// </summary>
        /// <param name="GeneralSmsProviderModel">GeneralSmsProviderModel.</param>
        /// <returns>Returns updated GeneralSmsProviderResponse</returns>
        GeneralSmsProviderResponse UpdateSmsProvider(GeneralSmsProviderModel body);

        /// <summary>
        /// Delete SmsProvider.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>

        TrueFalseResponse DeleteSmsProvider(ParameterModel body);
    }
}
