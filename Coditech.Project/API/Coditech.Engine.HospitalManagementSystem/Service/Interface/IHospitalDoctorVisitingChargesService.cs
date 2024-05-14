using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IHospitalDoctorVisitingChargesService
    {
        HospitalDoctorVisitingChargesListModel GetHospitalDoctorVisitingChargesList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        HospitalDoctorVisitingChargesModel CreateHospitalDoctorVisitingCharges(HospitalDoctorVisitingChargesModel model);
        HospitalDoctorVisitingChargesModel GetHospitalDoctorVisitingCharges(short hospitalDoctorVisitingChargesId);
        bool UpdateHospitalDoctorVisitingCharges(HospitalDoctorVisitingChargesModel model);
        bool DeleteHospitalDoctorVisitingCharges(ParameterModel parameterModel);
    }
}
