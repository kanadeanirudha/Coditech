using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;

namespace Coditech.API.Endpoint
{
    public class UserEndpoint : BaseEndpoint
    {
        public string InsertPersonInformationAsync() =>
            $"{CoditechAdminSettings.CoditechUserApiRootUri}/User/InsertPersonInformation";

    }
}
