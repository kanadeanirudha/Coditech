using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;

namespace Coditech.API.Endpoint
{
    public class UserEndpoint : BaseEndpoint
    {
        public string InsertPersonInformationAsync() =>
            $"{CoditechAdminSettings.CoditechUserApiRootUri}/User/InsertPersonInformation";

        public string GetPersonInformationAsync(long personId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GymMemberDetails/GetPersonInformation?personId={personId}";

        public string UpdatePersonInformationAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GymMemberDetails/UpdatePersonInformation";

    }
}
