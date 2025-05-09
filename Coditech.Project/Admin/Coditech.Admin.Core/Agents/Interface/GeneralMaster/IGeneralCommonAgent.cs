using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model.Responses;
using Microsoft.AspNetCore.Http;

namespace Coditech.Admin.Agents
{
    public interface IGeneralCommonAgent
    {
        GeneralMessagesViewModel SendOTP(GeneralMessagesViewModel generalMessagesViewModel);

        MediaManagerResponse UploadImage(IFormFile file);

        CoditechApplicationSettingListViewModel GetCoditechApplicationSettingList(string applicationCodes);
        bool GetAccountPrequisite();
    }
}
