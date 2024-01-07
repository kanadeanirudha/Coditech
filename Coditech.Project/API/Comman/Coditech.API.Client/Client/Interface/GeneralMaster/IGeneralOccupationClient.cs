using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IGeneralOccupationClient : IBaseClient
    {
        /// <summary>
        /// Get list of General Occupation.
        /// </summary>
        /// <returns>GeneralOccupationListResponse</returns>
        GeneralOccupationListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create Occupation.
        /// </summary>
        /// <param name="GeneralOccupationModel">GeneralOccupationModel.</param>
        /// <returns>Returns GeneralOccupationResponse.</returns>
        GeneralOccupationResponse CreateOccupation(GeneralOccupationModel body);

        /// <summary>
        /// Get Occupation by generalOccupationId.
        /// </summary>
        /// <param name="generalOccupationId">generalOccupationId</param>
        /// <returns>Returns GeneralOccupationResponse.</returns>
        GeneralOccupationResponse GetOccupation(short generalOccupationId);

        /// <summary>
        /// Update Occupation.
        /// </summary>
        /// <param name="GeneralOccupationModel">GeneralOccupationModel.</param>
        /// <returns>Returns updated GeneralOccupationResponse</returns>
        GeneralOccupationResponse UpdateOccupation(GeneralOccupationModel body);

        /// <summary>
        /// Delete Occupation.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteOccupation(ParameterModel body);
    }
}
