using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;
namespace Coditech.API.Endpoint
{
    public class OrganisationCentrewiseJoiningCodeEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/OrganisationCentrewiseJoiningCode/GetOrganisationCentrewiseJoiningCodeList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string CreateOrganisationCentrewiseJoiningCodeAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/OrganisationCentrewiseJoiningCode/CreateOrganisationCentrewiseJoiningCode";

        public string OrganisationCentrewiseJoiningCodeSendAsync(string joiningCode, string emailId, string mobileNumber) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/OrganisationCentrewiseJoiningCode/OrganisationCentrewiseJoiningCodeSend?joiningCode={joiningCode}&emailId={emailId}&mobileNumber={mobileNumber}";
    }
}
