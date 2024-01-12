using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model.Response;

namespace Coditech.Admin.Agents
{
    public interface IGeneralMeasurementUnitAgent
    {
        /// <summary>
        /// Get list of General MeasurementUnit.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>GeneralMeasurementUnitListViewModel</returns>
        GeneralMeasurementUnitListViewModel GetMeasurementUnitList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create MeasurementUnit.
        /// </summary>
        /// <param name="generalMeasurementUnitViewModel">General MeasurementUnit View Model.</param>
        /// <returns>Returns created model.</returns>
        GeneralMeasurementUnitViewModel CreateMeasurementUnit(GeneralMeasurementUnitViewModel generalMeasurementUnitViewModel);

        /// <summary>
        /// Get MeasurementUnit by generalMeasurementUnitId.
        /// </summary>
        /// <param name="generalMeasurementUnitId">generalMeasurementUnitId</param>
        /// <returns>Returns GeneralMeasurementUnitViewModel.</returns>
        GeneralMeasurementUnitViewModel GetMeasurementUnit(short generalMeasurementUnitId);

        /// <summary>
        /// Update MeasurementUnit.
        /// </summary>
        /// <param name="generalMeasurementUnitViewModel">generalMeasurementUnitViewModel.</param>
        /// <returns>Returns updated GeneralMeasurementUnitViewModel</returns>
        GeneralMeasurementUnitViewModel UpdateMeasurementUnit(GeneralMeasurementUnitViewModel generalMeasurementUnitViewModel);

        /// <summary>
        /// Delete MeasurementUnit.
        /// </summary>
        /// <param name="generalMeasurementUnitId">generalMeasurementUnitId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteMeasurementUnit(string generalMeasurementUnitId, out string errorMessage);
        GeneralMeasurementUnitListResponse GetMeasurementUnitList();
    }
}
