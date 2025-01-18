using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface ITaskApprovalSettingService
    {
        TaskApprovalSettingListModel GetTaskApprovalSettingList(string selectedCentreCode,FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        TaskApprovalSettingModel GetTaskApprovalSetting( short taskMasterId, string centreCode);
        TaskApprovalSettingModel AddUpdateTaskApprovalSetting(TaskApprovalSettingModel model);
        TaskApprovalSettingModel GetUpdateTaskApprovalSetting(short taskMasterId, string centreCode, int taskApprovalSettingId);
        bool UpdateTaskApprovalSetting(TaskApprovalSettingModel model);
        bool DeleteTaskApprovalSetting(ParameterModel parameterModel);
    }
}
