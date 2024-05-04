using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IGeneralUserMainMenuMasterService
    {
        UserMainMenuListModel GetUserMainMenuList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        UserMainMenuModel CreateUserMainMenu(UserMainMenuModel model);
        UserMainMenuModel GetUserMainMenu(short generalUserMainMenuMasterId);
        bool UpdateUserMainMenu(UserMainMenuModel model);
        bool DeleteUserMainMenu(ParameterModel parameterModel);
    }
}
