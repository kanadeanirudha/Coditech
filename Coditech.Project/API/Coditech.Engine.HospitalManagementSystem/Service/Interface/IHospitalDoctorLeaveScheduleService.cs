using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IHospitalDoctorLeaveScheduleService
    {
        HospitalDoctorLeaveScheduleListModel GetHospitalDoctorLeaveScheduleList(string selectedCentreCode, short selectedDepartmentId, FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        HospitalDoctorLeaveScheduleModel CreateHospitalDoctorLeaveSchedule(HospitalDoctorLeaveScheduleModel model);
        HospitalDoctorLeaveScheduleModel GetHospitalDoctorLeaveSchedule(long hospitalDoctorLeaveScheduleId);
        bool UpdateHospitalDoctorLeaveSchedule(HospitalDoctorLeaveScheduleModel model);
        bool DeleteHospitalDoctorLeaveSchedule(ParameterModel parameterModel);
    }
}
