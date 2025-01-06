using Coditech.Admin.ViewModel;

namespace Coditech.Admin.Agents
{
    public interface IGeneralNationalityAgent
    {
        /// <summary>
        /// Get list of General Nationality.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>GeneralNationalityListViewModel</returns>
        GeneralNationalityListViewModel GetNationalityList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create Nationality.
        /// </summary>
        /// <param name="generalNationalityViewModel">General Nationality View Model.</param>
        /// <returns>Returns created model.</returns>
        GeneralNationalityViewModel CreateNationality(GeneralNationalityViewModel generalNationalityViewModel);

        /// <summary>
        /// Get Nationality by  nationalityId.
        /// </summary>
        /// <param name="nationalityId"> nationalityId</param>
        /// <returns>Returns GeneralNationalityViewModel.</returns>
        GeneralNationalityViewModel GetNationality(short nationalityId);

        /// <summary>
        /// Update Nationality.
        /// </summary>
        /// <param name="generalNationalityViewModel">generalNationalityViewModel.</param>
        /// <returns>Returns updated GeneralNationalityViewModel</returns>
        GeneralNationalityViewModel UpdateNationality(GeneralNationalityViewModel generalNationalityViewModel);

        /// <summary>
        /// Delete Nationality.
        /// </summary>
        /// <param name="nationalityId">nationalityId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteNationality(string nationalityId, out string errorMessage);
    }
}
