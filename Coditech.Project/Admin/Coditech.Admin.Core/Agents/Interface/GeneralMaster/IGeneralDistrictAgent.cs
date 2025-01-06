using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model.Response;

namespace Coditech.Admin.Agents
{
    public interface IGeneralDistrictAgent
    {
        /// <summary>
        /// Get list of General District.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>GeneralDistrictListViewModel</returns>
        GeneralDistrictListViewModel GetDistrictList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create District.
        /// </summary>
        /// <param name="generalDistrictViewModel">General District View Model.</param>
        /// <returns>Returns created model.</returns>
        GeneralDistrictViewModel CreateDistrict(GeneralDistrictViewModel generalDistrictViewModel);

        /// <summary>
        /// Get District by generalDistrictId.
        /// </summary>
        /// <param name="generalDistrictId">generalDistrictId</param>
        /// <returns>Returns GeneralDistrictViewModel.</returns>
        GeneralDistrictViewModel GetDistrict(short generalDistrictId);

        /// <summary>
        /// Update District.
        /// </summary>
        /// <param name="generalDistrictViewModel">generalDistrictViewModel.</param>
        /// <returns>Returns updated GeneralDistrictViewModel</returns>
        GeneralDistrictViewModel UpdateDistrict(GeneralDistrictViewModel generalDistrictViewModel);

        /// <summary>
        /// Delete District.
        /// </summary>
        /// <param name="generalDistrictId">generalDistrictId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteDistrict(string generalDistrictId, out string errorMessage);
       
    }
}
