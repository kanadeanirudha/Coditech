using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IAdminRoleMasterService
    {
        AdminRoleMasterListModel GetAdminRoleMasterList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        AdminRoleModel CreateAdminRoleMaster(AdminRoleModel model);
        AdminRoleModel GetAdminRoleMasterDetailsById(int adminRoleMasterId);
        bool UpdateAdminRoleMaster(AdminRoleModel model);
        bool DeleteAdminRoleMaster(ParameterModel parameterModel);
    }
}
