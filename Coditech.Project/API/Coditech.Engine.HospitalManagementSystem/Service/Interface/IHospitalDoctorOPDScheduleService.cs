using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IHospitalDoctorOPDScheduleService
    {
        HospitalDoctorOPDScheduleListModel GetHospitalDoctorOPDScheduleList(string selectedCentreCode, short selectedDepartmentId, FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        HospitalDoctorOPDScheduleModel GetHospitalDoctorOPDSchedule(int hospitalDoctorId,int hospitalDoctorOPDScheduleId);
        bool UpdateHospitalDoctorOPDSchedule(HospitalDoctorOPDScheduleModel model);
    }
}
