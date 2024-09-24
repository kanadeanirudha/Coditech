using Coditech.Common.API.Model;

namespace Coditech.API.Service
{
    public interface ICoditechGeneralApiService
    {
        CoditechApplicationSettingListModel GetCoditechApplicationSettingList(string applicationCodes);
        string GetDomainAPIKey(string requestKey);
    }
}
