using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Coditech.API.Data
{
    public class OrganisationCentrewiseJoiningCode
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long OrganisationCentrewiseJoiningCodeId { get; set; }
        public string CentreCode { get; set; }
        public string JoiningCode { get; set; }
        public int Quantity { get; set; }
        public bool IsExpired { get; set; }
        public int JoiningCodeTypeEnumId { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
