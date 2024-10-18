using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public class DBTMTestModel : BaseModel
    {
        public int DBTMTestMasterId { get; set; }
        public short DBTMActivityCategoryId { get; set; }

        [Required]
        [MaxLength(200)]
        public string TestName { get; set; }

        [Required]
        [MaxLength(50)]
        public string TestCode { get; set; }
        public bool IsActive { get; set; }
    }
}
