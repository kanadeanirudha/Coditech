using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IGeneralWhatsAppProviderClient : IBaseClient
    {
        /// <summary>
        /// Get list of General WhatsAppProvider.
        /// </summary>
        /// <returns>GeneralWhatsAppProviderListResponse</returns>
        GeneralWhatsAppProviderListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        GeneralWhatsAppProviderResponse CreateWhatsAppProvider(GeneralWhatsAppProviderModel body);

        /// <summary>
        /// Get WhatsApp by generaWhatsAppProviderId.
        /// </summary>
        /// <param name="generalWhatsAppProviderId">generalWhatsAppProviderId</param>
        /// <returns>Returns GeneralWhatsAppProviderResponse.</returns>

        GeneralWhatsAppProviderResponse GetWhatsAppProvider(short generalWhatsAppProviderId);

        /// <summary>
        /// Update WhatsAppProvider.
        /// </summary>
        /// <param name="GeneralWhatsAppProviderModel">GeneralWhatsAppProviderModel.</param>
        /// <returns>Returns updated GeneralWhatsAppProviderResponse</returns>
        GeneralWhatsAppProviderResponse UpdateWhatsAppProvider(GeneralWhatsAppProviderModel body);

        /// <summary>
        /// Delete WhatsAppProvider.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>

        TrueFalseResponse DeleteWhatsAppProvider(ParameterModel body);


    }
}
