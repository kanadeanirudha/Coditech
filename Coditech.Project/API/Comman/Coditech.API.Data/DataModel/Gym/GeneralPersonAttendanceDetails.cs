using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coditech.API.Data.DataModel.Gym
{
    public partial class GeneralPersonAttendanceDetails
    {
        [Key]
        public long GeneralPersonAttendanceDetailId { get; set; }
        public long PersonId { get; set; }
        public int GeneralAttendanceStateEnumId { get; set; }
        public int GymMemberDetailId { get; set; }
        public DateTime AttendanceDate { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime LogoutTime { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
