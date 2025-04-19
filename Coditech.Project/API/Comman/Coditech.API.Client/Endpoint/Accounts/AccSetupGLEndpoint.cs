using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
namespace Coditech.API.Endpoint
{
    public class AccSetupGLEndpoint : BaseEndpoint
    {
        public string GetAccSetupGLTreeAsync(string selectedcentreCode, byte accSetupBalanceSheetTypeId, int accSetupBalanceSheetId) =>
           $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AccSetupGL/GetAccSetupGLTree?selectedcentreCode={selectedcentreCode}&accSetupBalanceSheetTypeId={accSetupBalanceSheetTypeId}&accSetupBalanceSheetId={accSetupBalanceSheetId}";
        public string CreateAccountSetupGLAsync() =>
           $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AccSetupGL/CreateAccountSetupGL";
        public string GetAccountSetupGLAsync(int accSetupGLId) =>
          $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AccSetupGL/GetAccountSetupGL?accSetupGLId={accSetupGLId}";
        public string UpdateAccountAsync() =>
              $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AccSetupGL/UpdateAccount";
        public string UpdateAccountSetupGLAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AccSetupGL/UpdateAccountSetupGL"; 
        public string AddChildAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AccSetupGL/AddChild";
        public string DeleteAccountSetupGLAsync() =>
                 $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AccSetupGL/DeleteAccountSetupGL";
    }
}
