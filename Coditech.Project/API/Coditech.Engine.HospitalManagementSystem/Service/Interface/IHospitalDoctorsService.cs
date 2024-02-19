using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IHospitalDoctorsService
    {
        HospitalDoctorsListModel GetHospitalDoctorsList(string selectedCentreCode, short selectedDepartmentId, bool isAssociated, FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        HospitalDoctorsModel CreateHospitalDoctors(HospitalDoctorsModel model);
        HospitalDoctorsModel GetHospitalDoctors(long hospitalDoctorId);
        bool UpdateHospitalDoctors(HospitalDoctorsModel model);
        bool DeleteHospitalDoctors(ParameterModel parameterModel);
    }
}
