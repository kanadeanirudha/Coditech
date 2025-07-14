using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;

namespace Coditech.API.Endpoint
{
    public class UserEndpoint : BaseEndpoint
    {
        public string InsertPersonInformationAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/User/InsertPersonInformation";

        public string GetPersonInformationAsync(long personId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/User/GetPersonInformation?personId={personId}";

        public string GetGeneralPersonAddressesAsync(long personId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/User/GetGeneralPersonAddresses?personId={personId}";

        public string UpdatePersonInformationAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/User/UpdatePersonInformation";

        public string InsertUpdateGeneralPersonAddressAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/User/InsertUpdateGeneralPersonAddress";

        public string GetActiveMenuListAsync(string moduleCode) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/User/GetActiveMenuList?moduleCode={moduleCode}";
        public string GetActiveModuleAsync() =>
           $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/User/GetActiveModuleList";

        public string UserLoginAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/User/Login";

        public string ChangePasswordAsync() =>
           $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/User/ChangePassword";

        public string ResetPasswordAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/User/ResetPassword";

        public string ResetPasswordSendLinkAsync(string userName) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/User/ResetPasswordSendLink?userName={userName}";
        public string GetUserTypeListAsync() =>
           $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/User/GetUserTypeList";

        public string GetUserDetailByUserNameAsync(string userName) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/User/GetUserDetailByUserName?userName={userName}";
    }
}
       
        
      