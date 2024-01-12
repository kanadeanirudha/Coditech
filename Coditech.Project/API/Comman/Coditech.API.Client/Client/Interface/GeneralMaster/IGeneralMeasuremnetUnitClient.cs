using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IGeneralMeasurementUnitClient : IBaseClient
    {
        /// <summary>
        /// Get list of General MeasurementUnit.
        /// </summary>
        /// <returns>GeneralMeasurementUnitListResponse</returns>
        GeneralMeasurementUnitListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create MeasurementUnit.
        /// </summary>
        /// <param name="GeneralMeasurementUnitModel">GeneralMeasurementUnitModel.</param>
        /// <returns>Returns GeneralMeasurementUnitResponse.</returns>
        GeneralMeasurementUnitResponse CreateMeasurementUnit(GeneralMeasurementUnitModel body);

        /// <summary>
        /// Get MeasurementUnit by generalMeasurementUnitId.
        /// </summary>
        /// <param name="generalMeasurementUnitId">generalMeasurementUnitId</param>
        /// <returns>Returns GeneralMeasurementUnitResponse.</returns>
        GeneralMeasurementUnitResponse GetMeasurementUnit(short generalMeasurementUnitId);

        /// <summary>
        /// Update MeasurementUnit.
        /// </summary>
        /// <param name="GeneralMeasurementUnitModel">GeneralMeasurementUnitModel.</param>
        /// <returns>Returns updated GeneralMeasurementUnitResponse</returns>
        GeneralMeasurementUnitResponse UpdateMeasurementUnit(GeneralMeasurementUnitModel body);

        /// <summary>
        /// Delete MeasurementUnit.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteMeasurementUnit(ParameterModel body);
    }
}
