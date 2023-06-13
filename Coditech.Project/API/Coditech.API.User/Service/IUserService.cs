using Coditech.Common.API.Model;

namespace Coditech.API.Service
{
    public interface IUserService
    {
        OrganisationModel Login(UserLoginModel model);
    }
}
