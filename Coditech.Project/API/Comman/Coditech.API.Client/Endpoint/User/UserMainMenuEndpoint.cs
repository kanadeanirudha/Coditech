using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;

namespace Coditech.API.Endpoint
{
    public class UserMainMenuEndpoint : BaseEndpoint
    {
        public string GetActiveMenuListAsync(short moduleCode) =>
            $"{CoditechAdminSettings.CoditechUserApiRootUri}/User/GetActiveMenuListList?moduleCode={moduleCode}";
    }
}
