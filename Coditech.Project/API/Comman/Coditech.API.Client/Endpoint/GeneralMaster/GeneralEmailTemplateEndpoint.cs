using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class GeneralEmailTemplateEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralEmailTemplate/GetEmailTemplateList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateEmailTemplateAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralEmailTemplate/CreateEmailTemplate";

        public string GetEmailTemplateAsync(short generalEmailTemplateId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralEmailTemplate/GetEmailTemplate?generalEmailTemplateId={generalEmailTemplateId}";

        public string UpdateEmailTemplateAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralEmailTemplate/UpdateEmailTemplate";

        public string DeleteEmailTemplateAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralEmailTemplate/DeleteEmailTemplate";
    }


}
