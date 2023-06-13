using Coditech.API.Data;
using Coditech.Common.API.Model;

namespace Coditech.API.Service
{
    public class UserService : IUserService
    {
        private readonly UserDBContext _userDBContext;
        public UserService(UserDBContext userDBContext)
        {
            _userDBContext = userDBContext;
        }

        public virtual OrganisationModel Login(UserLoginModel model)
        {
            OrganisationModel userModel = null;
            UserMaster userMaster = _userDBContext.UserMaster.FirstOrDefault(x => x.UserName == model.UserName && x.Password == model.Password);
            return userModel;
        }
    }
}
