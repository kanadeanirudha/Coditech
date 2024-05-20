using Coditech.Common.API.Model;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Model
{
    public partial class OrganisationCentrewiseEmailTemplateModel : BaseModel
    {
        public short OrganisationCentrewiseEmailTemplateId { get; set; }

        public short OrganisationCentreMasterId { get; set; }

        [Required]
        [MaxLength(15)]
        public string CentreCode { get; set; }

        [Required]
        [MaxLength(100)]
        public string EmailTemplateCode { get; set; }

        [Required]
        [MaxLength(200)]
        public string Subject { get; set; }

        [Required]
        public string EmailTemplate { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public string CentreName { get; set; }
    }
}
