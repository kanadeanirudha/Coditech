using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class GeneralTrainerEndpoint : BaseEndpoint
    {
        public string ListAsync(string selectedCentreCode, short selectedDepartmentId, bool isAssociated,IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralTrainerMaster/GetTrainerList?selectedCentreCode={selectedCentreCode}&selectedDepartmentId={selectedDepartmentId}&isAssociated={isAssociated}{BuildEndpointQueryString(true, expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }
        public string CreateTrainerAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralTrainerMaster/CreateTrainer";
        public string GetTrainerAsync(long generalTrainerId) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralTrainerMaster/GetTrainer?generalTrainerId={generalTrainerId}";

        public string UpdateTrainerAsync() =>
               $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralTrainerMaster/UpdateTrainer";

        public string DeleteTrainerAsync() =>
                  $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralTrainerMaster/DeleteTrainer";
    }
}
