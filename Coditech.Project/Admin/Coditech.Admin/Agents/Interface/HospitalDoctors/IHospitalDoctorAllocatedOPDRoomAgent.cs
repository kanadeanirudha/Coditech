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
        HospitalDoctorAllocatedOPDRoomListViewModel GetHospitalDoctorAllocatedOPDRoomList(string selectedCentreCode, short selectedDepartmentId, DataTableViewModel dataTableModel);

        /// <summary>
        /// Get HospitalDoctorAllocatedOPDRoom by hospitalDoctorAllocatedOPDRoomId.
        /// </summary>
        /// <param name="hospitalDoctorAllocatedOPDRoomId">hospitalDoctorAllocatedOPDRoomId</param>
        /// <returns>Returns HospitalDoctorAllocatedOPDRoomViewModel.</returns>
        HospitalDoctorAllocatedOPDRoomViewModel GetHospitalDoctorAllocatedOPDRoom(int hospitalDoctorId, int hospitalDoctorAllocatedOPDRoomId);

        /// <summary>
        /// Update HospitalDoctorAllocatedOPDRoom.
        /// </summary>
        /// <param name="hospitalDoctorAllocatedOPDRoomViewModel">hospitalDoctorAllocatedOPDRoomViewModel.</param>
        /// <returns>Returns updated HospitalDoctorAllocatedOPDRoomViewModel</returns>
        HospitalDoctorAllocatedOPDRoomViewModel UpdateHospitalDoctorAllocatedOPDRoom(HospitalDoctorAllocatedOPDRoomViewModel hospitalDoctorAllocatedOPDRoomViewModel);
    }
}
