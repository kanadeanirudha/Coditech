using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IGeneralUserMainMenuMasterService
    {
        GeneralUserMainMenuListModel GetUserMainMenuList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        GeneralUserMainMenuModel CreateUserMainMenu(GeneralUserMainMenuModel model);
        GeneralUserMainMenuModel GetUserMainMenu(short generalUserMainMenuMasterId);
        bool UpdateUserMainMenu(GeneralUserMainMenuModel model);
        bool DeleteUserMainMenu(ParameterModel parameterModel);
    }
}
