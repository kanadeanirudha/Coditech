using Coditech.Common.API.Model;
using Coditech.Common.Helper.Utilities;

using System.Collections.Specialized;

namespace Coditech.API.Service
{
    public interface IHospitalDoctorAllocatedOPDRoomService
    {
        HospitalDoctorAllocatedOPDRoomListModel GetHospitalDoctorAllocatedOPDRoomList(FilterCollection filters, NameValueCollection sorts, NameValueCollection expands, int pagingStart, int pagingLength);
        HospitalDoctorAllocatedOPDRoomModel CreateHospitalDoctorAllocatedOPDRoom(HospitalDoctorAllocatedOPDRoomModel model);
        HospitalDoctorAllocatedOPDRoomModel GetHospitalDoctorAllocatedOPDRoom(int hospitalDoctorAllocatedOPDRoomId);
        bool UpdateHospitalDoctorAllocatedOPDRoom(HospitalDoctorAllocatedOPDRoomModel model);
        bool DeleteHospitalDoctorAllocatedOPDRoom(ParameterModel parameterModel);
    }
}
