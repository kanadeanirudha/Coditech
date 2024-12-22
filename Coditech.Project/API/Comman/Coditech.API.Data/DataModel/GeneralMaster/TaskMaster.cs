using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coditech.API.Data
{
    public partial class TaskMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short TaskMasterId { get; set; }
        public string TaskCode { get; set; }
        public string TableName { get; set; }
        public string TaskDescription { get; set; }
        public bool IsActive { get; set; }
    }
}


