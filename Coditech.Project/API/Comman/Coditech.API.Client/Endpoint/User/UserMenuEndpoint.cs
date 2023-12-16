using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;

namespace Coditech.API.Endpoint
{
    public class UserMenuEndpoint : BaseEndpoint
    {
        public string GetActiveMenuListAsync(string moduleCode) =>
            $"{CoditechAdminSettings.CoditechUserApiRootUri}/User/GetActiveMenuList?moduleCode={moduleCode}";
    }
}
