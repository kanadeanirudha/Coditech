using Coditech.Admin.ViewModel;

namespace Coditech.Admin.Agents
{
    public interface IGeneralGymMemberDetailsAgent
    {
        /// <summary>
        /// Get list of Gym Member.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>GymMemberDetailsListViewModel</returns>
        GymMemberDetailsListViewModel GetGymMemberDetailsList(DataTableViewModel dataTableModel);
    }
}
