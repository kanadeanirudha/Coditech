using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IEmployeeMasterService
    {
        EmployeeMasterListModel GetEmployeeList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        EmployeeMasterModel GetEmployeeOtherDetail(long employeeId);
        bool UpdateEmployeeOtherDetail(EmployeeMasterModel model);
        bool DeleteEmployee(ParameterModel parameterModel);
    }
}
