using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IEmployeeServiceService
    {
        EmployeeServiceListModel GetEmployeeServiceList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        EmployeeServiceModel GetEmployeeService(long employeeId, long personId, long employeeServiceId);
        bool UpdateEmployeeService(EmployeeServiceModel model);
        bool DeleteEmployeeService(ParameterModel parameterModel);
    }
}
