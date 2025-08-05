using Coditech.Admin.Utilities;
using Coditech.API.Client.Endpoint;
using Coditech.Common.API.Model;

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
        public string GetAccountPrequisiteAsync(int balanceSheetId) =>
         $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralCommon/GetAccountPrequisite?balanceSheetId={balanceSheetId}";
        public string FetchPostalCodeAsync(string postalCode) =>
        $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralCommon/FetchPostalCode?postalCode={postalCode}";
        public string ValidateAddressAsync() =>
        $"{CoditechAdminSettings.CoditechOrganisationApiRootUri}/GeneralCommon/ValidateAddress";

    }
}
