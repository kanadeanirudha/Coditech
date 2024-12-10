using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;

namespace Coditech.API.Endpoint
{
    public class GeneralCommonEndpoint : BaseEndpoint
    {
        public string SendOTPAsync() =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralCommon/SendOTP";

        public string GetCoditechApplicationSettingListAsync(string applicationCodes) =>
            $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralCommon/GetCoditechApplicationSettingList?applicationCodes={applicationCodes}";

        public string GetDropdownListByCodeAsync(string groupCodes) =>
           $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralCommon/GetDropdownListByCode?groupCodes={groupCodes}";
    }
}
