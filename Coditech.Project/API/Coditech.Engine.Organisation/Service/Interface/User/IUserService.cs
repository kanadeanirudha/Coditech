using Coditech.API.Data;
using Coditech.Common.API.Model;

namespace Coditech.API.Service
{
    public interface IUserService
    {
        UserModel Login(UserLoginModel model);
        List<UserModuleModel> GetActiveModuleList();
        List<UserMainMenuModel> GetActiveMenuList(string moduleCode);
        GeneralPersonModel InsertPersonInformation(GeneralPersonModel model);
        GeneralPersonModel GetPersonInformation(long personId);
        bool UpdatePersonInformation(GeneralPersonModel model);
        GeneralPersonAddressListModel GetGeneralPersonAddresses(long personId);
        GeneralPersonAddressModel InsertUpdateGeneralPersonAddress(GeneralPersonAddressModel model);
        ChangePasswordModel ChangePassword(ChangePasswordModel model);
        ResetPasswordModel ResetPassword(ResetPasswordModel model);
        ResetPasswordSendLinkModel ResetPasswordSendLink(string userName, bool isMobileRequest);
        bool AcceptTermsAndConditions(string userType, long entityId);
    }
}
