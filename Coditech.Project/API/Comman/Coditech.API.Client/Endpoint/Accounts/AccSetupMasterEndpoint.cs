using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class AccSetupMasterEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AccSetupMaster/GetAccSetupMasterList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateAccSetupMasterAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AccSetupMaster/CreateAccSetupMaster";

        public string GetAccSetupMasterAsync(short accSetupMasterId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AccSetupMaster/GetAccSetupMaster?accSetupMasterId={accSetupMasterId}";
       
        public string UpdateAccSetupMasterAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AccSetupMaster/UpdateAccSetupMaster";

        public string DeleteAccSetupMasterAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AccSetupMaster/DeleteAccSetupMaster";
    }
}
