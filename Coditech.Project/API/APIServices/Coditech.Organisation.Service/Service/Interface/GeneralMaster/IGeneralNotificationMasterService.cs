using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IGeneralNotificationMasterService
    {
        GeneralNotificationListModel GetNotificationList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        GeneralNotificationModel CreateNotification(GeneralNotificationModel model);
        GeneralNotificationModel GetNotification(long generalNotificationId);
        bool UpdateNotification(GeneralNotificationModel model);
        bool DeleteNotification(ParameterModel parametermodel);
        GeneralNotificationListModel GetActiveNotificationList();
    }
}