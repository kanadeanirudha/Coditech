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

        public string GetPersonAddressDetailAsync(long personId) =>
            $"{CoditechAdminSettings.CoditechUserApiRootUri}/User/GetPersonAddressDetail?personId={personId}";

        public string UpdatePersonInformationAsync() =>
               $"{CoditechAdminSettings.CoditechUserApiRootUri}/User/UpdatePersonInformation";

    }
}
