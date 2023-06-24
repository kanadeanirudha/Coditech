using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public class GeneralDepartmentModel : BaseModel
    {
        public GeneralDepartmentModel()
        {

        }
        public short GeneralDepartmentMasterId { get; set; }
        [Required]
        public string DepartmentName { get; set; }
        [Required]
        public string DepartmentShortCode { get; set; }
        public string PrintShortDesc { get; set; }
        public bool WorkActivity { get; set; }
    }
}
