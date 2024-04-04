using Coditech.Common.API.Model;
using Coditech.Common.Helper;
using Coditech.Resources;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Admin.ViewModel
{
    public class HospitalDoctorsSchedulesViewModel : BaseViewModel
    {
        public long HospitalDoctorScheduleId { get; set; }
        public int HospitalDoctorId { get; set; }
        public int HospitalWorkEnumId { get; set; }
        public int WeekDayEnumId { get; set; }
        public TimeSpan? FromTime { get; set; }
        public TimeSpan? UptoTime { get; set; }
        public byte TimeSlot { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SelectedCentreCode { get; set; }
        public string SelectedDepartmentId { get; set; }
        public List<GeneralEnumaratorModel> AllWeekDays { get; set; }
        public long EmployeeId { get; set; }
        public long PersonId { get; set; }
    }
}