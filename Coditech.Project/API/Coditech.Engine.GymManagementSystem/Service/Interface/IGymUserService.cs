using Coditech.Common.API.Model;

namespace Coditech.API.Service
{
    public interface IGymUserService
    {
        GymUserModel Login(UserLoginModel model);
        ChangePasswordModel ChangePassword(ChangePasswordModel model);
    }
}
