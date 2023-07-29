using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IGeneralDepartmentClient : IBaseClient
    {
        /// <summary>
        /// Gets list of General Department.
        /// </summary>
        /// <returns>Success</returns>
        /// <exception cref="CoditechException">A server side error occurred.</exception>
        GeneralDepartmentListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create general Department.
        /// </summary>
        /// <param name="generalDepartmentViewModel">General Department View Model.</param>
        /// <returns>Returns created model.</returns>
        GeneralDepartmentResponse CreateDepartment(GeneralDepartmentModel body);

        /// <summary>
        /// Get general Department by generalDepartment id.
        /// </summary>
        /// <param name="generalDepartmentId">GeneralDepartment id to get generalDepartment details.</param>
        /// <returns>Success</returns>
        /// <exception cref="CoditechException">A server side error occurred.</exception>
        GeneralDepartmentResponse GetDepartment(int generalDepartmentId);

        /// <summary>
        /// Update generalDepartment.
        /// </summary>
        /// <param name="body">model to update.</param>
        /// <returns>Success</returns>
        /// <exception cref="CoditechException">A server side error occurred.</exception>
        GeneralDepartmentResponse UpdateDepartment(GeneralDepartmentModel body);

        /// <summary>
        /// Delete generalDepartment.
        /// </summary>
        /// <param name="body">GeneralDepartment Id.</param>
        /// <returns>Success</returns>
        /// <exception cref="CoditechException">A server side error occurred.</exception>
        TrueFalseResponse DeleteDepartment(ParameterModel body);
    }
}
