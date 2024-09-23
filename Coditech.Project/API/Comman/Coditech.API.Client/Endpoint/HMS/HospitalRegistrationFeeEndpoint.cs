﻿using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Endpoint
{
    public class HospitalRegistrationFeeEndpoint : BaseEndpoint
    {
        public string ListAsync(string selectedCentreCode, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize)
        {
            string endpoint = $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalRegistrationFee/GetRegistrationFeeList?selectedCentreCode={selectedCentreCode}{BuildEndpointQueryString(true,expand, filter, sort, pageIndex, pageSize)}";
            return endpoint;
        }
        public string GetRegistrationFeeAsync(int hospitalRegistrationFeeId) =>
            $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalRegistrationFee/GetRegistrationFee?hospitalRegistrationFeeId={hospitalRegistrationFeeId}";

        public string DeleteRegistrationFeeAsync() =>
                  $"{CoditechAdminSettings.CoditechHospitalManagementSystemApiRootUri}/HospitalRegistrationFee/DeleteHospitalRegistrationFee";
    }
}
