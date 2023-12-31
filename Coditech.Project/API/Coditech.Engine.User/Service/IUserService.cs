﻿using Coditech.API.Data;
using Coditech.Common.API.Model;

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
    }
}
