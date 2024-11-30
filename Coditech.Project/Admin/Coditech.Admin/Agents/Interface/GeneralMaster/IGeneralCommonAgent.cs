using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model.Responses;

namespace Coditech.Admin.Agents
{
    public interface IGeneralCommonAgent
    {
        GeneralMessagesViewModel SendOTP(GeneralMessagesViewModel generalMessagesViewModel);

        MediaManagerResponse UploadImage(IFormFile file);

        CoditechApplicationSettingListViewModel GetCoditechApplicationSettingList(string applicationCodes);
    }
}
