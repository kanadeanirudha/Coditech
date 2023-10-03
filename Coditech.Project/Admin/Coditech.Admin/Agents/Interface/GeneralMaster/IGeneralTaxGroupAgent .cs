using Coditech.Admin.ViewModel;

namespace Coditech.Admin.Agents
{
    public interface IGeneralTaxGroupAgent
    {
        /// <summary>
        /// Get list of General TaxGroupMaster.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>GeneralTaxGroupMasterListViewModel</returns>
        GeneralTaxGroupMasterListViewModel GetTaxGroupMasterList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create TaxGroupMaster.
        /// </summary>
        /// <param name="generalTaxGroupMasterViewModel">General Tax Group Master View Model.</param>
        /// <returns>Returns created model.</returns>
        GeneralTaxGroupMasterViewModel CreateTaxGroupMaster(GeneralTaxGroupMasterViewModel generalTaxGroupMasterViewModel);

        /// <summary>
        /// Get TaxGroupMaster by taxGroupMasterId.
        /// </summary>
        /// <param name="taxGroupMasterId">taxGroupMasterId</param>
        /// <returns>Returns GeneralTaxGroupMasterViewModel.</returns>
        GeneralTaxGroupMasterViewModel GetTaxGroupMaster(short taxGroupMasterId);

        /// <summary>
        /// Update TaxGroupMaster.
        /// </summary>
        /// <param name="generalTaxGroupMasterViewModel">generalTaxGroupMasterViewModel.</param>
        /// <returns>Returns updated GeneralTaxGroupMasterViewModel</returns>
        GeneralTaxGroupMasterViewModel UpdateTaxGroupMaster(GeneralTaxGroupMasterViewModel generalTaxGroupMasterViewModel);

        /// <summary>
        /// Delete TaxGroupMaster.
        /// </summary>
        /// <param name="taxGroupMasterId">taxGroupMasterId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteTaxGroupMaster(string taxGroupMasterId, out string errorMessage);
    }
}
