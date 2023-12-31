﻿using Coditech.Admin.Utilities;
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
    }
}
