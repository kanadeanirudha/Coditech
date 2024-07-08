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
        ResetPasswordModel ResetPassword(string resetPasswordToken, string newPassword);
        ResetPasswordSendLinkModel ResetPasswordSendLink(string userName);
       
    }
}
