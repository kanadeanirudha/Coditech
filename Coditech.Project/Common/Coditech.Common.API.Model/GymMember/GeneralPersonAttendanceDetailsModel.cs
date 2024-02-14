
using System.ComponentModel.DataAnnotations;


namespace Coditech.Common.API.Model
    {
    public class GeneralPersonAttendanceDetailsModel : BaseModel
    {
        public long GeneralPersonAttendanceDetailId { get; set; }
        public int GymMemberDetailId { get; set; }

        public int PersonId { get; set; }

        [Required]
        public int GeneralAttendanceStateEnumId { get; set; }

        public DateTime AttendanceDate { get; set; }

        [Required]
        public DateTime LoginTime { get; set; }
        
        [Required]
        public DateTime LogoutTime { get; set; }

       
    }
}
