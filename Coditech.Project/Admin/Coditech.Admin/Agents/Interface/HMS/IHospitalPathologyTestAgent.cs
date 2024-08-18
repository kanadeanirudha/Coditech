using Coditech.Admin.ViewModel;

namespace Coditech.Admin.Agents
{
    public interface IHospitalPathologyTestAgent
    {
        /// <summary>
        /// Get list of HospitalPathologyTest.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>HospitalPathologyTestListViewModel</returns>
        HospitalPathologyTestListViewModel GetHospitalPathologyTestList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create HospitalPathologyTest.
        /// </summary>
        /// <param name="hospitalPathologyTestViewModel"> Hospital Pathology Test View Model.</param>
        /// <returns>Returns created model.</returns>
        HospitalPathologyTestViewModel CreateHospitalPathologyTest(HospitalPathologyTestViewModel hospitalPathologyTestViewModel);

        /// <summary>
        /// Get HospitalPathologyTest by hospitalPathologyTestId.
        /// </summary>
        /// <param name="hospitalPathologyTestId">hospitalPathologyTestId</param>
        /// <returns>Returns HospitalPathologyTestViewModel.</returns>
        HospitalPathologyTestViewModel GetHospitalPathologyTest(long hospitalPathologyTestId);

        /// <summary>
        /// Update HospitalPathologyTest.
        /// </summary>
        /// <param name="hospitalPathologyTestViewModel">hospitalPathologyTestViewModel.</param>
        /// <returns>Returns updated HospitalPathologyTestViewModel</returns>
        HospitalPathologyTestViewModel UpdateHospitalPathologyTest(HospitalPathologyTestViewModel hospitalPathologyTestViewModel);

        /// <summary>
        /// Delete HospitalPathologyTest.
        /// </summary>
        /// <param name="hospitalPathologyTestIds">hospitalPathologyTestIds.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteHospitalPathologyTest(string hospitalPathologyTestIds, out string errorMessage);
    }
}
