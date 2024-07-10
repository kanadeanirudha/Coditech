using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IHospitalPatientRegistrationService
    {
        HospitalPatientRegistrationListModel GetPatientRegistrationList(string selectedCentreCode, FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        HospitalPatientRegistrationModel GetPatientRegistrationOtherDetail(long hospitalPatientRegistrationId);
        bool DeletePatientRegistration(ParameterModel parameterModel);
    }
}
