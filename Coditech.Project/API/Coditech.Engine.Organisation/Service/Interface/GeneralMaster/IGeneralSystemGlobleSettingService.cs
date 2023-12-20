using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IGeneralSystemGlobleSettingService
    {
        GeneralSystemGlobleSettingListModel GetSystemGlobleSettingList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        GeneralSystemGlobleSettingModel CreateSystemGlobleSetting(GeneralSystemGlobleSettingModel model);
        GeneralSystemGlobleSettingModel GetSystemGlobleSetting(short generalSystemGlobleSettingId);
        bool UpdateSystemGlobleSetting(GeneralSystemGlobleSettingModel model);
        bool DeleteSystemGlobleSetting(ParameterModel parameterModel);
    }
}
