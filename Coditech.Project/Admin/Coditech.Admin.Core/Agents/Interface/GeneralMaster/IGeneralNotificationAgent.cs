using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model.Response;

namespace Coditech.Admin.Agents

{
    public interface IGeneralNotificationAgent
    {

        /// <summary>
        /// Get list of General Notification.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>GeneralNotificationListViewModel</returns>
        GeneralNotificationListViewModel GetNotificationList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create Notification.
        /// </summary>
        /// <param name="generalNotificationViewModel">General Notification View Model.</param>
        /// <returns>Returns created model.</returns>
        GeneralNotificationViewModel CreateNotification(GeneralNotificationViewModel generalNotificationViewModel);
        /// <summary>
        /// Get Notification by NotificationId.
        /// </summary>
        /// <param name="generalNotificationId">generalNotificationId</param>
        /// <returns>Returns GeneralNotificationViewModel.</returns>

        GeneralNotificationViewModel GetNotification(long generalNotificationId);
        /// <summary>
        /// Update Notification.
        /// </summary>
        /// <param name="generalNotificationViewModel">generalNotificationViewModel.</param>
        /// <returns>Returns updated GeneralNotificationViewModel</returns>
        GeneralNotificationViewModel UpdateNotification(GeneralNotificationViewModel generalNotificationViewModel);

        /// <summary>
        /// Delete Notification.
        /// </summary>
        /// <param name="GeneralNotificationId">GeneralNotificationId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteNotification(string GeneralNotificationId, out string errorMessage);
        GeneralNotificationListResponse GetNotificationList();

    }
}