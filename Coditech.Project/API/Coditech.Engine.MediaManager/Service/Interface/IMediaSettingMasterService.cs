using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IMediaSettingMasterService
    {
        MediaSettingMasterListModel GetMediaSettingMasterList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        MediaSettingMasterModel CreateMediaSettingMaster(MediaSettingMasterModel model);
        MediaSettingMasterModel GetMediaSettingMaster(short mediaSettingMasterId);
        bool UpdateMediaSettingMaster(MediaSettingMasterModel model);
        bool DeleteMediaSettingMaster(ParameterModel parameterModel);
    }
}
