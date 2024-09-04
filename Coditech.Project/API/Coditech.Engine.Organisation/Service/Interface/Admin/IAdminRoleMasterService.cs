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
        AdminRoleMenuDetailsModel GetAdminRoleMenuDetailsById(int adminRoleMasterId, string moduleCode);
        bool InsertUpdateAdminRoleMenuDetails(AdminRoleMenuDetailsModel adminRoleMenuDetailsModel);
        AdminRoleApplicableDetailsListModel RoleAllocatedToUserList(int adminRoleMasterId, FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        AdminRoleApplicableDetailsModel GetAssociateUnAssociateAdminRoleToUser(int adminRoleMasterId, int adminRoleApplicableDetailId);
        bool AssociateUnAssociateAdminRoleToUser(AdminRoleApplicableDetailsModel adminRoleApplicableDetailsModel);
        AdminRoleMediaFolderActionModel GetAdminRoleWiseMediaFolderActionById(int adminRoleMasterId);
        bool InsertUpdateAdminRoleWiseMediaFolderAction(AdminRoleMediaFolderActionModel adminRoleMediaFolderActionModel);
        AdminRoleMediaFoldersModel GetAdminRoleWiseMediaFoldersById(int adminRoleMasterId);
        bool InsertUpdateAdminRoleWiseMediaFolders(AdminRoleMediaFoldersModel adminRoleMediaFoldersModel);
    }
}
