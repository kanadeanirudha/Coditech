﻿using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class DBTMTraineeDetailsEndpoint : BaseEndpoint
    {
        public string ListAsync(string selectedCentreCode, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechDBTMApiRootUri}/DBTMTraineeDetails/GetDBTMTraineeDetailsList?selectedCentreCode={selectedCentreCode}{BuildEndpointQueryString(true, expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }

        public string GetDBTMTraineeOtherDetailsAsync(long dBTMTraineeDetailId) =>
            $"{CoditechAdminSettings.CoditechDBTMApiRootUri}/DBTMTraineeDetails/GetDBTMTraineeOtherDetails?dBTMTraineeDetailId={dBTMTraineeDetailId}";

        public string UpdateDBTMTraineeOtherDetailsAsync() =>
               $"{CoditechAdminSettings.CoditechDBTMApiRootUri}/DBTMTraineeDetails/UpdateDBTMTraineeOtherDetails";

        public string DeleteDBTMTraineeDetailsAsync() =>
                  $"{CoditechAdminSettings.CoditechDBTMApiRootUri}/DBTMTraineeDetails/DeleteDBTMTraineeDetails";
    }
}
