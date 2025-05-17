using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;

namespace Coditech.API.Endpoint
{
    public class AccGLTransactionEndpoint : BaseEndpoint
    {


        public string CreateGLTransactionAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AccGLTransaction/CreateGLTransaction";

        public string GetGLTransactionAsync(long accGLTransactionId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AccGLTransaction/GetGLTransaction?accGLTransactionId={accGLTransactionId}";

        public string UpdateGLTransactionAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AccGLTransaction/UpdateGLTransaction";
        public string GetAccSetupGLAccountListAsync(string searchKeyword, int accSetupGLId, string userType, string transactionTypeCode, int balanceSheet) =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AccGLTransaction/GetAccSetupGLAccountList?searchKeyword={searchKeyword}&accSetupGLId={accSetupGLId}&userType={userType}&transactionTypeCode={transactionTypeCode}&balanceSheet={balanceSheet}";
        public string GetPersonsAsync(string searchKeyword, int userTypeId, int balanceSheet) =>
                       $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AccGLTransaction/GetPersons?searchKeyword={searchKeyword}&userTypeId={userTypeId}&balanceSheet={balanceSheet}";
    }
}
