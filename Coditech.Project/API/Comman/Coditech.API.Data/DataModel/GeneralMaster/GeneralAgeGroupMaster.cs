using System.ComponentModel.DataAnnotations;
namespace Coditech.API.Data
{
    public partial class GeneralAgeGroupMaster
    {
        [Key]
        public int GeneralAgeGroupMasterId { get; set; }
        public string AgeGroupName { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

