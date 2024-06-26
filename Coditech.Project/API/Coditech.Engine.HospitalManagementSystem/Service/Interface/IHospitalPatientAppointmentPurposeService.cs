using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IHospitalPatientAppointmentPurposeService
    {
        HospitalPatientAppointmentPurposeListModel GetHospitalPatientAppointmentPurposeList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        HospitalPatientAppointmentPurposeModel CreateHospitalPatientAppointmentPurpose(HospitalPatientAppointmentPurposeModel model);
        HospitalPatientAppointmentPurposeModel GetHospitalPatientAppointmentPurpose(short HospitalPatientAppointmentPurposeId);
        bool UpdateHospitalPatientAppointmentPurpose(HospitalPatientAppointmentPurposeModel model);
        bool DeleteHospitalPatientAppointmentPurpose(ParameterModel parameterModel);
    }
}