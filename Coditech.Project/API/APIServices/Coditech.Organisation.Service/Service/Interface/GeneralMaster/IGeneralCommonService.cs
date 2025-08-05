using Coditech.Common.API.Model;

namespace Coditech.API.Service
{
    public interface IGeneralCommonService
    {
        List<GeneralEnumaratorModel> GetDropdownListByCode(string groupCodes);
        CoditechApplicationSettingListModel GetCoditechApplicationSettingList(string applicationCodes);
        string GetDomainAPIKey(string requestKey);
        GeneralMessagesModel SendOTP(GeneralMessagesModel generalMessagesModel);
        AccPrequisiteModel GetAccountPrequisite(int balanceSheetId);
        List<BindAddressToPostalCodeModel> FetchPostalCode(string code);
        BindAddressToPostalCodeModel ValidateAddress(BindAddressToPostalCodeModel bindAddressToPostalCodeModel);

    }
}
