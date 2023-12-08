using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class GeneralPersonEndpoint : BaseEndpoint
    {
        public string InsertPersonInformationAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GymMemberDetails/InsertPersonInformation";
    }
}
