using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IGeneralDepartmentClient : IBaseClient
    {
        /// <summary>
        /// Get list of General Department.
        /// </summary>
        /// <returns>GeneralDepartmentListResponse</returns>
        GeneralDepartmentListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create Department.
        /// </summary>
        /// <param name="GeneralDepartmentModel">GeneralDepartmentModel.</param>
        /// <returns>Returns GeneralDepartmentResponse.</returns>
        GeneralDepartmentResponse CreateDepartment(GeneralDepartmentModel body);

        /// <summary>
        /// Get Department by generalDepartmentId.
        /// </summary>
        /// <param name="generalDepartmentId">generalDepartmentId</param>
        /// <returns>Returns GeneralDepartmentResponse.</returns>
        GeneralDepartmentResponse GetDepartment(int generalDepartmentId);

        /// <summary>
        /// Update Department.
        /// </summary>
        /// <param name="GeneralDepartmentModel">GeneralDepartmentModel.</param>
        /// <returns>Returns updated GeneralDepartmentResponse</returns>
        GeneralDepartmentResponse UpdateDepartment(GeneralDepartmentModel body);

        /// <summary>
        /// Delete Department.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteDepartment(ParameterModel body);
    }
}
