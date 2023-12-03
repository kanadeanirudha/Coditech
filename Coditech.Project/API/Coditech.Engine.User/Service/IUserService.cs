using Coditech.Common.API.Model;

namespace Coditech.API.Service
{
    public interface IUserService
    {
        UserModel Login(UserLoginModel model);
        UserModuleModel GetActiveModuleList(short userModuleMasterId);
    }
}
