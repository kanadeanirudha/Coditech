using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;

namespace Coditech.API.Client
{
    public interface IGeneralCommonClient : IBaseClient
    {
        GeneralMessagesResponse SendOTP(GeneralMessagesModel generalMessagesModel);
        CoditechApplicationSettingListResponse GetCoditechApplicationSettingList(string applicationCodes);
    }
}
