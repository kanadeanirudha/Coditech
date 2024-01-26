using Coditech.API.Data;
using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Responses;

namespace Coditech.API.Service
{
    public interface IUserService
    {
        UserModel Login(UserLoginModel model);
        List<UserModuleMaster> GetActiveModuleList();
        List<UserMainMenuMaster> GetActiveMenuList(string moduleCode);
        GeneralPersonModel InsertPersonInformation(GeneralPersonModel model);
        GeneralPersonModel GetPersonInformation(long personId);        
        bool UpdatePersonInformation(GeneralPersonModel model);
        GeneralPersonAddressListModel GetGeneralPersonAddresses(long personId);
        GeneralPersonAddressModel InsertUpdateGeneralPersonAddress(GeneralPersonAddressModel model);
    }
}
