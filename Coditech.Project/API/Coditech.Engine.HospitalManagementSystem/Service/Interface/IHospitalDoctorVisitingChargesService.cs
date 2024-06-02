using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IHospitalDoctorVisitingChargesService
    {
        HospitalDoctorVisitingChargesListModel GetHospitalDoctorVisitingChargesList(string selectedCentreCode, short selectedDepartmentId, FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        HospitalDoctorVisitingChargesListModel GetHospitalDoctorVisitingChargesByDoctorIdList(int HospitalDoctorId, FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        HospitalDoctorVisitingChargesModel CreateHospitalDoctorVisitingCharges(HospitalDoctorVisitingChargesModel model);
        HospitalDoctorVisitingChargesModel GetHospitalDoctorVisitingCharges(long hospitalDoctorVisitingChargesId , int hospitalDoctorId);
        bool UpdateHospitalDoctorVisitingCharges(HospitalDoctorVisitingChargesModel model);
        bool DeleteHospitalDoctorVisitingCharges(ParameterModel parameterModel);
    }
}
