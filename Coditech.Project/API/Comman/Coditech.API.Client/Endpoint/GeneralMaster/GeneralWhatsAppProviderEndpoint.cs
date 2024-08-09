using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class GeneralWhatsAppProviderEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralWhatsAppProviderMaster/GetWhatsAppProviderList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateWhatsAppProviderAsync() =>
      $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralWhatsAppProviderMaster/CreateWhatsAppProvider";

        public string GetWhatsAppProviderAsync(short generalWhatsAppProviderId) =>
           $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralWhatsAppProviderMaster/GetWhatsAppProvider?generalWhatsAppProviderId={generalWhatsAppProviderId}";

        public string UpdateWhatsAppProviderAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralWhatsAppProviderMaster/UpdateWhatsAppProvider";
        public string DeleteWhatsAppProviderAsync() =>
                 $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralWhatsAppProviderMaster/DeleteWhatsAppProvider";

    }


}

