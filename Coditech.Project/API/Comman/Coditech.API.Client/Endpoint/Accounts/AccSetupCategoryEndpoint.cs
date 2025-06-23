using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;

namespace Coditech.API.Endpoint
{
    public class AccSetupCategoryEndpoint : BaseEndpoint
    {
        public string GetAccSetupCategoryAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/AccSetupCategory/GetAccSetupCategory";
    }
}
