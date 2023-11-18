using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IAdminRoleMasterService
    {
        AdminRoleListModel GetAdminRoleList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        AdminRoleModel GetAdminRoleDetailsById(int adminRoleMasterId);
        bool UpdateAdminRole(AdminRoleModel model);
        bool DeleteAdminRoleMaster(ParameterModel parameterModel);
    }
}
