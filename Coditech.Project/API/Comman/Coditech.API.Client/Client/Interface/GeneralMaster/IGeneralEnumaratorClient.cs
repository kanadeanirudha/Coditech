using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IGeneralEnumaratorClient : IBaseClient
    {
        /// <summary>
        /// Get list of General Enumarator.
        /// </summary>
        /// <returns>GeneralEnumaratorListResponse</returns>
        GeneralEnumaratorListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create Enumarator.
        /// </summary>
        /// <param name="GeneralEnumaratorModel">GeneralEnumaratorModel.</param>
        /// <returns>Returns GeneralEnumaratorResponse.</returns>
        GeneralEnumaratorResponse CreateEnumarator(GeneralEnumaratorModel body);

        /// <summary>
        /// Get Enumarator by generalEnumaratorId.
        /// </summary>
        /// <param name="generalEnumaratorId">generalEnumaratorId</param>
        /// <returns>Returns GeneralEnumaratorResponse.</returns>
        GeneralEnumaratorResponse GetEnumarator(int generalEnumaratorId);

        /// <summary>
        /// Update Enumarator.
        /// </summary>
        /// <param name="GeneralEnumaratorModel">GeneralEnumaratorModel.</param>
        /// <returns>Returns updated GeneralEnumaratorResponse</returns>
        GeneralEnumaratorResponse UpdateEnumarator(GeneralEnumaratorModel body);

        /// <summary>
        /// Delete Enumarator.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteEnumarator(ParameterModel body);
    }
}
