using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model.Response;

namespace Coditech.Admin.Agents
{
    public interface IGymMemberBodyMeasurementAgent
    {
        /// <summary>
        /// Get list of General MemberBodyMeasurement.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>GymMemberBodyMeasurementListViewModel</returns>
        GymMemberBodyMeasurementListViewModel GetMemberBodyMeasurementList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create MemberBodyMeasurement.
        /// </summary>
        /// <param name="GymMemberBodyMeasurementViewModel">General MemberBodyMeasurement View Model.</param>
        /// <returns>Returns created model.</returns>
        GymMemberBodyMeasurementViewModel CreateMemberBodyMeasurement(GymMemberBodyMeasurementViewModel GymMemberBodyMeasurementViewModel);

        /// <summary>
        /// Get MemberBodyMeasurement by GymMemberBodyMeasurementId.
        /// </summary>
        /// <param name="GymMemberBodyMeasurementId">GymMemberBodyMeasurementId</param>
        /// <returns>Returns GymMemberBodyMeasurementViewModel.</returns>
        GymMemberBodyMeasurementViewModel GetMemberBodyMeasurement(long GymMemberBodyMeasurementId);

        /// <summary>
        /// Update MemberBodyMeasurement.
        /// </summary>
        /// <param name="GymMemberBodyMeasurementViewModel">GymMemberBodyMeasurementViewModel.</param>
        /// <returns>Returns updated GymMemberBodyMeasurementViewModel</returns>
        GymMemberBodyMeasurementViewModel UpdateMemberBodyMeasurement(GymMemberBodyMeasurementViewModel GymMemberBodyMeasurementViewModel);

        /// <summary>
        /// Delete MemberBodyMeasurement.
        /// </summary>
        /// <param name="GymMemberBodyMeasurementId">GymMemberBodyMeasurementId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteMemberBodyMeasurement(string GymMemberBodyMeasurementId, out string errorMessage);
        GymMemberBodyMeasurementListResponse GetMemberBodyMeasurementList();
    }
}
