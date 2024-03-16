using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class AdminRoleMasterEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AdminRoleMaster/GetAdminRoleList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string GetAdminRoleDetailsByIdAsync(int adminRoleMasterId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AdminRoleMaster/GetAdminRoleDetailsById?adminRoleMasterId={adminRoleMasterId}";

        public string UpdateAdminRoleAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AdminRoleMaster/UpdateAdminRole";

        public string DeleteAdminRoleAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AdminRoleMaster/DeleteAdminRole";

        public string GetAdminRoleMenuDetailsByIdAsync(int adminRoleMasterId, string moduleCode) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AdminRoleMaster/GetAdminRoleMenuDetailsById?adminRoleMasterId={adminRoleMasterId}&moduleCode={moduleCode}";

        public string InsertUpdateAdminRoleMenuDetailsAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AdminRoleMaster/InsertUpdateAdminRoleMenuDetails";

        public string RoleAllocatedToUserListAsync(int adminRoleMasterId, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AdminRoleMaster/RoleAllocatedToUserList?adminRoleMasterId={adminRoleMasterId}{BuildEndpointQueryString(true, expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string GetAssociateUnAssociateAdminRoleToUserAsync(int adminRoleMasterId, int adminRoleApplicableDetailId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AdminRoleMaster/GetAssociateUnAssociateAdminRoleToUser?adminRoleMasterId={adminRoleMasterId}&adminRoleApplicableDetailId={adminRoleApplicableDetailId}";

        public string AssociateUnAssociateAdminRoleToUserAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AdminRoleMaster/AssociateUnAssociateAdminRoleToUser";

    }
}
