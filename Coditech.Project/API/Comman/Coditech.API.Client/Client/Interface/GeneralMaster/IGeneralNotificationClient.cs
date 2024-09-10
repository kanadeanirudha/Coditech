using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IGeneralNotificationClient : IBaseClient
    {
        /// <summary>
        /// Get list of General Notification.
        /// </summary>
        /// <returns>GeneralNotificationListResponse</returns>
        GeneralNotificationListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create Notification by generaNotificationId.
        /// </summary>
        /// <param name="generalNotificationId">generalNotificationId</param>
        /// <returns>Returns GeneralNotificationResponse.</returns>
       GeneralNotificationResponse CreateNotification(GeneralNotificationModel body);
        /// <summary>
        /// Get Notification by generalNotificationId.
        /// </summary>
        /// <param name="generalNotificationId">generalNotificationId</param>
        /// <returns>Returns GeneralNotificationResponse.</returns>

        GeneralNotificationResponse GetNotification(long generalNotificationId);

        /// <summary>
        /// Update Notification
        /// </summary>
        /// <param name="GeneralNotificationModel">GeneralNotificationModel.</param>
        /// <returns>Returns updated GeneralNotificationResponse</returns>
        GeneralNotificationResponse UpdateNotification(GeneralNotificationModel body);
        /// <summary>
        /// Delete Notification.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>

        TrueFalseResponse DeleteNotification(ParameterModel body);
    }
}
