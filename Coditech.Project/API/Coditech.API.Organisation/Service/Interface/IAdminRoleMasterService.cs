using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IAdminRoleMasterService
    {
        AdminRoleMasterListModel GetAdminRoleMasterList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        AdminRoleMasterModel CreateAdminRoleMaster(AdminRoleMasterModel model);
        AdminRoleMasterModel GetAdminRoleMasterDetailsById(short adminRoleMasterId); 
        bool UpdateAdminRoleMaster(AdminRoleMasterModel model);
        bool DeleteAdminRoleMaster(ParameterModel parameterModel);
    }
}
