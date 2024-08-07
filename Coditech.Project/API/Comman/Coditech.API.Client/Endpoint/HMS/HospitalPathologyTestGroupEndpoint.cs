using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class HospitalPathologyTestGroupEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalPathologyTestGroup/GetHospitalPathologyTestGroupList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }
        public string CreateHospitalPathologyTestGroupAsync() =>
            $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalPathologyTestGroup/CreateHospitalPathologyTestGroup";
        public string GetHospitalPathologyTestGroupAsync(int hospitalPathologyTestGroupId) =>
            $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalPathologyTestGroup/GetHospitalPathologyTestGroup?hospitalPathologyTestGroupId={hospitalPathologyTestGroupId}";

        public string UpdateHospitalPathologyTestGroupAsync() =>
               $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalPathologyTestGroup/UpdateHospitalPathologyTestGroup";

        public string DeleteHospitalPathologyTestGroupAsync() =>
                  $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalPathologyTestGroup/DeleteHospitalPathologyTestGroup";
    }
}
