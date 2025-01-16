using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public class DBTMActivitiesModel : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TestName { get; set; }
        public DateTime Date { get; set; }
        [MaxLength(100)]
        [Required]
        public string DeviceSerialCode { get; set; }
    }
}
