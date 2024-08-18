using Coditech.Admin.ViewModel;

namespace Coditech.Admin.Agents
{
    public interface IHospitalPathologyTestGroupAgent
    {
        /// <summary>
        /// Get list of HospitalPathologyTestGroup.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>HospitalPathologyTestGroupListViewModel</returns>
        HospitalPathologyTestGroupListViewModel GetHospitalPathologyTestGroupList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create HospitalPathologyTestGroup.
        /// </summary>
        /// <param name="hospitalPathologyTestGroupViewModel"> Hospital Pathology Test Group View Model.</param>
        /// <returns>Returns created model.</returns>
        HospitalPathologyTestGroupViewModel CreateHospitalPathologyTestGroup(HospitalPathologyTestGroupViewModel hospitalPathologyTestGroupViewModel);

        /// <summary>
        /// Get HospitalPathologyTestGroup by hospitalPathologyTestGroupId.
        /// </summary>
        /// <param name="hospitalPathologyTestGroupId">hospitalPathologyTestGroupId</param>
        /// <returns>Returns HospitalPathologyTestGroupViewModel.</returns>
        HospitalPathologyTestGroupViewModel GetHospitalPathologyTestGroup(int hospitalPathologyTestGroupId);

        /// <summary>
        /// Update HospitalPathologyTestGroup.
        /// </summary>
        /// <param name="hospitalPathologyTestGroupViewModel">hospitalPathologyTestGroupViewModel.</param>
        /// <returns>Returns updated HospitalPathologyTestGroupViewModel</returns>
        HospitalPathologyTestGroupViewModel UpdateHospitalPathologyTestGroup(HospitalPathologyTestGroupViewModel hospitalPathologyTestGroupViewModel);

        /// <summary>
        /// Delete HospitalPathologyTestGroup.
        /// </summary>
        /// <param name="hospitalPathologyTestGroupId">hospitalPathologyTestGroupId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteHospitalPathologyTestGroup(string hospitalPathologyTestGroupId, out string errorMessage);
    }
}
