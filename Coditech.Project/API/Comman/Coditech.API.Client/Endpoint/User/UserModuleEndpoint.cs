using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;

namespace Coditech.API.Endpoint
{
    public class UserModuleEndpoint : BaseEndpoint
    {
        public string GetActiveModuleAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/User/GetActiveModuleList";
    }
}
