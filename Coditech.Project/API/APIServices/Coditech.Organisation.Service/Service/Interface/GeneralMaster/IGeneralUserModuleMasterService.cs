using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IGeneralUserModuleMasterService
    {
        UserModuleListModel GetUserModuleList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        UserModuleModel CreateUserModule(UserModuleModel model);
        UserModuleModel GetUserModule(short userModuleMasterId);
        bool UpdateUserModule(UserModuleModel model);
        bool DeleteUserModule(ParameterModel parameterModel);
    }
}
