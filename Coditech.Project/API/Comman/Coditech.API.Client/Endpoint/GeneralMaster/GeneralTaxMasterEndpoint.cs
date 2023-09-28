using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class GeneralTaxMasterEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralTaxMaster/GetTaxMasterList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateTaxMasterAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralTaxMaster/CreateTaxMaster";

        public string GetTaxMasterAsync(short taxMasterId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralTaxMaster/GetTaxMaster?generalTaxMasterId={taxMasterId}";

        public string UpdateTaxMasterAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralTaxMaster/UpdateTaxMaster";

        public string DeleteTaxMasterAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralTaxMaster/DeleteTaxMaster";
    }
}
