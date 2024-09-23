using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IHospitalRegistrationFeeService
    {
        HospitalRegistrationFeeListModel GetHospitalRegistrationFeeList(string selectedCentreCode, FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        HospitalRegistrationFeeModel CreateRegistrationFee(HospitalRegistrationFeeModel model);
        HospitalRegistrationFeeModel GetRegistrationFee(int hospitalRegistrationFeeId);
        bool UpdateRegistrationFee(HospitalRegistrationFeeModel model);
        bool DeleteRegistrationFee(ParameterModel parameterModel);
    }
}
