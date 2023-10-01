using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IGeneralDesignationClient : IBaseClient
    {
        /// <summary>
        /// Get list of General Designation.
        /// </summary>
        /// <returns>GeneralDesignationListResponse</returns>
        GeneralDesignationListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create Designation.
        /// </summary>
        /// <param name="GeneralDesignationModel">GeneralDesignationModel.</param>
        /// <returns>Returns GeneralDesignationResponse.</returns>
        GeneralDesignationResponse CreateDesignation(GeneralDesignationModel body);

        /// <summary>
        /// Get Designation by designationId.
        /// </summary>
        /// <param name="designationId">designationId</param>
        /// <returns>Returns GeneralDesignationResponse.</returns>
        GeneralDesignationResponse GetDesignation(short designationId);

        /// <summary>
        /// Update Designation.
        /// </summary>
        /// <param name="GeneralDesignationModel">GeneralDesignationModel.</param>
        /// <returns>Returns updated GeneralDesignationResponse</returns>
        GeneralDesignationResponse UpdateDesignation(GeneralDesignationModel body);

        /// <summary>
        /// Delete Designation.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteDesignation(ParameterModel body);
    }
}
