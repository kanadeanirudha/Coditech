using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface ICoditechApplicationSettingService
    {
        CoditechApplicationSettingListModel GetCoditechApplicationSettingList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        CoditechApplicationSettingModel CreateCoditechApplicationSetting(CoditechApplicationSettingModel model);
        CoditechApplicationSettingModel GetCoditechApplicationSetting(short coditechApplicationSettingId);
        bool UpdateCoditechApplicationSetting(CoditechApplicationSettingModel model);
        bool DeleteCoditechApplicationSetting(ParameterModel parameterModel);
    }
}
