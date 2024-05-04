using Coditech.Common.API.Model;
using Coditech.Common.API.Model.Response;
using Coditech.Common.API.Model.Responses;
using Coditech.Common.Helper.Utilities;

namespace Coditech.API.Client
{
    public interface IHospitalDoctorAllocatedOPDRoomClient : IBaseClient
    {
        /// <summary>
        /// Get list of HospitalDoctorAllocatedOPDRoom.
        /// </summary>
        /// <returns>HospitalDoctorAllocatedOPDRoomListResponse</returns>
        HospitalDoctorAllocatedOPDRoomListResponse List(string selectedCentreCode, short selectedDepartmentId, IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Get HospitalDoctorAllocatedOPDRoom by hospitalDoctorAllocatedOPDRoomId.
        /// </summary>
        /// <param name="hospitalDoctorAllocatedOPDRoomId">hospitalDoctorAllocatedOPDRoomId</param>
        /// <returns>Returns HospitalDoctorAllocatedOPDRoomResponse.</returns>
        HospitalDoctorAllocatedOPDRoomResponse GetHospitalDoctorAllocatedOPDRoom(int hospitalDoctorId, int hospitalDoctorAllocatedOPDRoomId);

        /// <summary>
        /// Update HospitalDoctorAllocatedOPDRoom.
        /// </summary>
        /// <param name="HospitalDoctorAllocatedOPDRoomModel">HospitalDoctorAllocatedOPDRoomModel.</param>
        /// <returns>Returns updated HospitalDoctorAllocatedOPDRoomResponse</returns>
        HospitalDoctorAllocatedOPDRoomResponse UpdateHospitalDoctorAllocatedOPDRoom(HospitalDoctorAllocatedOPDRoomModel body);
    }
}
