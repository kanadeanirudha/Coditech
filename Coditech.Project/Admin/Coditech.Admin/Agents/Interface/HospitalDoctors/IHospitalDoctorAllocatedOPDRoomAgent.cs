using Coditech.Admin.ViewModel;
using Coditech.Common.API.Model.Response;

namespace Coditech.Admin.Agents
{
    public interface IHospitalDoctorAllocatedOPDRoomAgent
    {
        /// <summary>
        /// Get list of AllocatedOPDRoom.
        /// </summary>
        /// <param name="dataTableModel">DataTable ViewModel.</param>
        /// <returns>HospitalDoctorAllocatedOPDRoomListViewModel</returns>
        HospitalDoctorAllocatedOPDRoomListViewModel GetHospitalDoctorAllocatedOPDRoomList(DataTableViewModel dataTableModel);

        /// <summary>
        /// Create HospitalDoctorAllocatedOPDRoom.
        /// </summary>
        /// <param name="hospitalDoctorAllocatedOPDRoomViewModel">Hospital Doctor Allocated OPD Room View Model.</param>
        /// <returns>Returns created model.</returns>
        HospitalDoctorAllocatedOPDRoomViewModel CreateHospitalDoctorAllocatedOPDRoom(HospitalDoctorAllocatedOPDRoomViewModel hospitalDoctorAllocatedOPDRoomViewModel);

        /// <summary>
        /// Get HospitalDoctorAllocatedOPDRoom by hospitalDoctorAllocatedOPDRoomId.
        /// </summary>
        /// <param name="hospitalDoctorAllocatedOPDRoomId">hospitalDoctorAllocatedOPDRoomId</param>
        /// <returns>Returns HospitalDoctorAllocatedOPDRoomViewModel.</returns>
        HospitalDoctorAllocatedOPDRoomViewModel GetHospitalDoctorAllocatedOPDRoom(int hospitalDoctorAllocatedOPDRoomId);

        /// <summary>
        /// Update HospitalDoctorAllocatedOPDRoom.
        /// </summary>
        /// <param name="hospitalDoctorAllocatedOPDRoomViewModel">hospitalDoctorAllocatedOPDRoomViewModel.</param>
        /// <returns>Returns updated HospitalDoctorAllocatedOPDRoomViewModel</returns>
        HospitalDoctorAllocatedOPDRoomViewModel UpdateHospitalDoctorAllocatedOPDRoom(HospitalDoctorAllocatedOPDRoomViewModel hospitalDoctorAllocatedOPDRoomViewModel);

        /// <summary>
        /// Delete HospitalDoctorAllocatedOPDRoom.
        /// </summary>
        /// <param name="hospitalDoctorAllocatedOPDRoomId">hospitalDoctorAllocatedOPDRoomId.</param>
        /// <returns>Returns true if deleted successfully else return false.</returns>
        bool DeleteHospitalDoctorAllocatedOPDRoom(string hospitalDoctorAllocatedOPDRoomId, out string errorMessage);
        HospitalDoctorAllocatedOPDRoomListResponse GetHospitalDoctorAllocatedOPDRoomList();
    }
}
