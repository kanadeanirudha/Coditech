using Coditech.Admin.ViewModel;

namespace Coditech.Admin.Agents
{
    public interface IHospitalPathologyTestPricesAgent
    {
        /// <summary>
        /// Get list of HospitalPathologyTestPrices.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>HospitalPathologyTestPricesListViewModel</returns>
        HospitalPathologyTestPricesListViewModel GetHospitalPathologyTestPricesList(int hospitalPathologyPriceCategoryEnumId, DataTableViewModel dataTableModel);

        /// <summary>
        /// Create HospitalPathologyTestPrices.
        /// </summary>
        /// <param name="hospitalPathologyTestPricesViewModel">Hospital Pathology Test Prices View Model.</param>
        /// <returns>Returns created model.</returns>
        HospitalPathologyTestPricesViewModel CreateHospitalPathologyTestPrices(HospitalPathologyTestPricesViewModel hospitalPathologyTestPricesViewModel);

        /// <summary>
        /// Get HospitalPathologyTestPrices by hospitalPathologyTestPricesId.
        /// </summary>
        /// <param name="hospitalPathologyTestPricesId">hospitalPathologyTestPricesId</param>
        /// <returns>Returns HospitalPathologyTestPricesViewModel.</returns>
        HospitalPathologyTestPricesViewModel GetHospitalPathologyTestPrices(long hospitalPathologyTestPricesId);

        /// <summary>
        /// Update HospitalPathologyTestPrices.
        /// </summary>
        /// <param name="hospitalPathologyTestPricesViewModel">hospitalPathologyTestPricesViewModel.</param>
        /// <returns>Returns updated HospitalPathologyTestPricesViewModel</returns>
        HospitalPathologyTestPricesViewModel UpdateHospitalPathologyTestPrices(HospitalPathologyTestPricesViewModel hospitalPathologyTestPricesViewModel);

        /// <summary>
        /// Delete HospitalPathologyTestPrices.
        /// </summary>
        /// <param name="hospitalPathologyTestPricesIds">hospitalPathologyTestPricesIds.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteHospitalPathologyTestPrices(string hospitalPathologyTestPricesIds, out string errorMessage);
    }
}
