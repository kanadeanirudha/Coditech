using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class GeneralTaxGroupEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralTaxGroupMaster/GetTaxGroupMasterList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateTaxGroupAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralTaxGroupMaster/CreateTaxGroupMaster";

        public string GetTaxGroupAsync(short taxGroupMasterId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralTaxGroupMaster/GetTaxGroupMaster?generalTaxGroupMasterId={taxGroupMasterId}";

        public string UpdateTaxGroupAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralTaxGroupMaster/UpdateTaxGroupMaster";

        public string DeleteTaxGroupAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralTaxGroupMaster/DeleteTaxGroupMaster";
    }
}
