using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;

namespace Coditech.API.Endpoint
{
    public class UserEndpoint : BaseEndpoint
    {
        public string InsertPersonInformationAsync() =>
            $"{CoditechAdminSettings.CoditechUserApiRootUri}/User/InsertPersonInformation";

        public string GetPersonInformationAsync(long personId) =>
            $"{CoditechAdminSettings.CoditechUserApiRootUri}/User/GetPersonInformation?personId={personId}";

        public string GetGeneralPersonAddressesAsync(long personId) =>
            $"{CoditechAdminSettings.CoditechUserApiRootUri}/User/GetGeneralPersonAddresses?personId={personId}";

        public string UpdatePersonInformationAsync() =>
               $"{CoditechAdminSettings.CoditechUserApiRootUri}/User/UpdatePersonInformation";

        public string InsertUpdateGeneralPersonAddressAsync() =>
            $"{CoditechAdminSettings.CoditechUserApiRootUri}/User/InsertUpdateGeneralPersonAddress";

        public string GetActiveMenuListAsync(string moduleCode) =>
            $"{CoditechAdminSettings.CoditechUserApiRootUri}/User/GetActiveMenuList?moduleCode={moduleCode}";
        public string GetActiveModuleAsync() =>
           $"{CoditechAdminSettings.CoditechUserApiRootUri}/User/GetActiveModuleList";

        public string ChangePasswordAsync() =>
            $"{CoditechAdminSettings.CoditechUserApiRootUri}/User/ChangePassword";

        public string ResetPasswordAsync() =>
            $"{CoditechAdminSettings.CoditechUserApiRootUri}/User/ResetPassword";

        public string ResetPasswordSendLinkAsync(string userName) =>
            $"{CoditechAdminSettings.CoditechUserApiRootUri}/User/ResetPasswordSendLink?userName={userName}";
    }
}
       
        
      