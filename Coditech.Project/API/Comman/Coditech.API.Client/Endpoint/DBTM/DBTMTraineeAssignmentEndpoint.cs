using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class DBTMTraineeAssignmentEndpoint : BaseEndpoint
    {
        public string ListAsync(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechDBTMApiRootUri}/DBTMTraineeAssignment/GetDBTMTraineeAssignmentList{BuildEndpointQueryString(expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }
        public string CreateDBTMTraineeAssignmentAsync() =>
            $"{CoditechAdminSettings.CoditechDBTMApiRootUri}/DBTMTraineeAssignment/CreateDBTMTraineeAssignment";

        public string GetDBTMTraineeAssignmentAsync(long dBTMTraineeAssignmentId) =>
            $"{CoditechAdminSettings.CoditechDBTMApiRootUri}/DBTMTraineeAssignment/GetDBTMTraineeAssignment?dBTMTraineeAssignmentId={dBTMTraineeAssignmentId}";

        public string UpdateDBTMTraineeAssignmentAsync() =>
               $"{CoditechAdminSettings.CoditechDBTMApiRootUri}/DBTMTraineeAssignment/UpdateDBTMTraineeAssignment";

        public string DeleteDBTMTraineeAssignmentAsync() =>
                  $"{CoditechAdminSettings.CoditechDBTMApiRootUri}/DBTMTraineeAssignment/DeleteDBTMTraineeAssignment";
        public string GetDBTMTrainerByCentreCode(string centreCode)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechDBTMApiRootUri}/DBTMTraineeAssignment/GetTrainerByCentreCode?centreCode={centreCode}";
            return endpoint;
        }
        public string GetTraineeDetailsByCentreCodeAndgeneralTrainerId(string centreCode, long generalTrainerId) =>
           $"{CoditechAdminSettings.CoditechDBTMApiRootUri}/DBTMTraineeAssignment/GetTraineeDetailByCentreCodeAndgeneralTrainerId?centreCode={centreCode}&generalTrainerId={generalTrainerId}";

        public string SendAssignmentReminderAsync(string dBTMTraineeAssignmentId) =>
            $"{CoditechAdminSettings.CoditechDBTMApiRootUri}/DBTMTraineeAssignment/SendAssignmentReminder?dBTMTraineeAssignmentId={dBTMTraineeAssignmentId}";
    }
}
