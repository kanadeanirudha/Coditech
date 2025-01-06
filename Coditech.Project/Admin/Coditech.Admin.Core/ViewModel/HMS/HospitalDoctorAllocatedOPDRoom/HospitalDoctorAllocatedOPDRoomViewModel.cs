using Coditech.Common.Helper;

using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class HospitalDoctorAllocatedOPDRoomViewModel : BaseViewModel
    {
        public int HospitalDoctorAllocatedOPDRoomId { get; set; }
        public int HospitalDoctorId { get; set; }
        [Display(Name = "Room Name")]
        public int OrganisationCentrewiseBuildingRoomId { get; set; }

        [Display(Name = "Building Name")]
        public short? OrganisationCentrewiseBuildingMasterId { get; set; }
        public string RoomName { get; set; }
        public string ImagePath { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MedicalSpecialization { get; set; }
        public string MobileNumber { get; set; }
        public string EmailId { get; set; }
        public string SelectedCentreCode { get; set; }
        public string SelectedDepartmentId { get; set; }
    }
}