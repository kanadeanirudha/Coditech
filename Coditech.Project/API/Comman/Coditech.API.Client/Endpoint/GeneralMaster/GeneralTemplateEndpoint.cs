using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class GeneralTemplateEndpoint : BaseEndpoint
    {
        public string GetTemplateAsync(int generalTemplateId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralTemplate/GetTemplate?generalTemplateId={generalTemplateId}";
    }
}
