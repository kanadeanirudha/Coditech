using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model.Response;

namespace Coditech.Admin.Agents
{
    public interface IGeneralCityAgent
    {
        /// <summary>
        /// Get list of General City.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>GeneralCityListViewModel</returns>
        GeneralCityListViewModel GetCityList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create City.
        /// </summary>
        /// <param name="generalCityViewModel">General City View Model.</param>
        /// <returns>Returns created model.</returns>
        GeneralCityViewModel CreateCity(GeneralCityViewModel generalCityViewModel);

        /// <summary>
        /// Get City by cityId.
        /// </summary>
        /// <param name="cityId"> cityId</param>
        /// <returns>Returns GeneralCityViewModel.</returns>
        GeneralCityViewModel GetCity(int cityId);

        /// <summary>
        /// Update City.
        /// </summary>
        /// <param name="generalCityViewModel">generalCityViewModel.</param>
        /// <returns>Returns updated GeneralCityViewModel</returns>
        GeneralCityViewModel UpdateCity(GeneralCityViewModel generalCityViewModel);

        /// <summary>
        /// Delete City.
        /// </summary>
        /// <param name="cityId">cityId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteCity(string cityId, out string errorMessage);
        GeneralCityListResponse GetAllCityList();
    }
}
