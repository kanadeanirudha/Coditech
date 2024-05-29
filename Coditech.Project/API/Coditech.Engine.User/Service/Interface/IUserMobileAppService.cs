using Coditech.Common.API.Model;

namespace Coditech.API.Service
{
    public interface IUserMobileAppService
    {
        UserMobileAppModel Login(UserLoginModel model);
        ChangePasswordModel ChangePassword(ChangePasswordModel model);
    }
}
