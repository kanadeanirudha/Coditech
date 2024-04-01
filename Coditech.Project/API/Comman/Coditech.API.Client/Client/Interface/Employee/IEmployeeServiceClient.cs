using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IEmployeeServiceClient : IBaseClient
    {
        /// <summary>
        /// Get list of General Service.
        /// </summary>
        /// <returns>EmployeeServiceListResponse</returns>
        EmployeeServiceListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create Service.
        /// </summary>
        /// <param name="EmployeeServiceModel">EmployeeServiceModel.</param>
        /// <returns>Returns EmployeeServiceResponse.</returns>
        //EmployeeServiceResponse CreateEmployeeService(EmployeeServiceModel body);

        /// <summary>
        /// Get Service by employeeServiceId.
        /// </summary>
        /// <param name="employeeServiceId">employeeServiceId</param>
        /// <returns>Returns EmployeeServiceResponse.</returns>
        EmployeeServiceResponse GetEmployeeService(long employeeServiceId);

        /// <summary>
        /// Update Service.
        /// </summary>
        /// <param name="EmployeeServiceModel">EmployeeServiceModel.</param>
        /// <returns>Returns updated EmployeeServiceResponse</returns>
        EmployeeServiceResponse UpdateEmployeeService(EmployeeServiceModel body);

        /// <summary>
        /// Delete Service.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteEmployeeService(ParameterModel body);
    }
}
