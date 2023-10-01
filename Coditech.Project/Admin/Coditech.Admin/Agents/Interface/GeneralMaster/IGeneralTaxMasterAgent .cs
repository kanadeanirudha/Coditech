using Coditech.Admin.ViewModel;

namespace Coditech.Admin.Agents
{
    public interface IGeneralTaxMasterAgent
    {
        /// <summary>
        /// Get list of General TaxMaster.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>GeneralTaxMasterListViewModel</returns>
        GeneralTaxMasterListViewModel GetTaxMasterList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create TaxMaster.
        /// </summary>
        /// <param name="generalTaxMasterViewModel">General TaxMaster View Model.</param>
        /// <returns>Returns created model.</returns>
        GeneralTaxMasterViewModel CreateTaxMaster(GeneralTaxMasterViewModel generalTaxMasterViewModel);

        /// <summary>
        /// Get TaxMaster by taxMasterId.
        /// </summary>
        /// <param name="taxMasterId">taxMasterId</param>
        /// <returns>Returns GeneralTaxMasterViewModel.</returns>
        GeneralTaxMasterViewModel GetTaxMaster(short taxMasterId);

        /// <summary>
        /// Update TaxMaster.
        /// </summary>
        /// <param name="generalTaxMasterViewModel">generalTaxMasterViewModel.</param>
        /// <returns>Returns updated GeneralTaxMasterViewModel</returns>
        GeneralTaxMasterViewModel UpdateTaxMaster(GeneralTaxMasterViewModel generalTaxMasterViewModel);

        /// <summary>
        /// Delete TaxMaster.
        /// </summary>
        /// <param name="taxMasterId">taxMasterId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteTaxMaster(string taxMasterId, out string errorMessage);
    }
}
