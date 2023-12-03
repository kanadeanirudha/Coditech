using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;

namespace Coditech.API.Endpoint
{
    public class UserModuleEndpoint : BaseEndpoint
    {
        public string GetActiveModuleAsync(short userId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/User/GetActiveModuleList?userModuleMasterId={userId}";
    }
}
