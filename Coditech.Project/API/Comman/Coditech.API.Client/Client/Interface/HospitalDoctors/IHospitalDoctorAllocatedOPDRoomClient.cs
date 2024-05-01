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
        HospitalDoctorAllocatedOPDRoomListResponse List(IEnumerable<string> expand, IEnumerable<FilterTuple> filter, IDictionary<string, string> sort, int? pageIndex, int? pageSize);

        /// <summary>
        /// Create HospitalDoctorAllocatedOPDRoom.
        /// </summary>
        /// <param name="HospitalDoctorAllocatedOPDRoomModel">HospitalDoctorAllocatedOPDRoomModel.</param>
        /// <returns>Returns HospitalDoctorAllocatedOPDRoomResponse.</returns>
        HospitalDoctorAllocatedOPDRoomResponse CreateHospitalDoctorAllocatedOPDRoom(HospitalDoctorAllocatedOPDRoomModel body);

        /// <summary>
        /// Get HospitalDoctorAllocatedOPDRoom by hospitalDoctorAllocatedOPDRoomId.
        /// </summary>
        /// <param name="hospitalDoctorAllocatedOPDRoomId">hospitalDoctorAllocatedOPDRoomId</param>
        /// <returns>Returns HospitalDoctorAllocatedOPDRoomResponse.</returns>
        HospitalDoctorAllocatedOPDRoomResponse GetHospitalDoctorAllocatedOPDRoom(int hospitalDoctorAllocatedOPDRoomId);

        /// <summary>
        /// Update HospitalDoctorAllocatedOPDRoom.
        /// </summary>
        /// <param name="HospitalDoctorAllocatedOPDRoomModel">HospitalDoctorAllocatedOPDRoomModel.</param>
        /// <returns>Returns updated HospitalDoctorAllocatedOPDRoomResponse</returns>
        HospitalDoctorAllocatedOPDRoomResponse UpdateHospitalDoctorAllocatedOPDRoom(HospitalDoctorAllocatedOPDRoomModel body);

        /// <summary>
        /// Delete HospitalDoctorAllocatedOPDRoom.
        /// </summary>
        /// <param name="ParameterModel">ParameterModel.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        TrueFalseResponse DeleteHospitalDoctorAllocatedOPDRoom(ParameterModel body);
    }
}
