using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IHospitalPatientAppointmentService
    {
        HospitalPatientAppointmentListModel GetHospitalPatientAppointmentList(string selectedCentreCode, short selectedDepartmentId, FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        HospitalPatientAppointmentModel CreateHospitalPatientAppointment(HospitalPatientAppointmentModel model);
        HospitalPatientAppointmentModel GetHospitalPatientAppointment(long hospitalPatientAppointmentId);
        bool UpdateHospitalPatientAppointment(HospitalPatientAppointmentModel model);
        bool DeleteHospitalPatientAppointment(ParameterModel parameterModel);
    }
}
