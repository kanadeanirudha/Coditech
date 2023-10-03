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
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralCreateTaxGroupMaster/CreateTaxGroupMaster";

        public string GetTaxGroupAsync(short taxGroupMasterId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralCreateTaxGroupMaster/GetTaxGroupMaster?generalTaxGroupMasterId={taxGroupMasterId}";

        public string UpdateTaxGroupAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralCreateTaxGroupMaster/UpdateTaxGroupMaster";

        public string DeleteTaxGroupAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralCreateTaxGroupMaster/DeleteTaxGroupMaster";
    }
}
