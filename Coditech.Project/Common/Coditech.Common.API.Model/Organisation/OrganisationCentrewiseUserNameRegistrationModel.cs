﻿using Coditech.Common.API.Model;

using System.ComponentModel.DataAnnotations;

namespace Coditech.Model
{
    public class OrganisationCentrewiseUserNameRegistrationModel : BaseModel
    {
        public OrganisationCentrewiseUserNameRegistrationModel()
        {
            CentrewiseUserNameRegistrationList = new List<OrganisationCentrewiseUserNameRegistrationModel>();
        }
        public List<OrganisationCentrewiseUserNameRegistrationModel> CentrewiseUserNameRegistrationList { get; set; }

        [Required]
        public int OrganisationCentrewiseUserNameRegistrationId { get; set; }
        public int OrganisationCentreMasterId { get; set; }

        [Required]
        [MaxLength(100)]
        public string CentreName{ get; set; }

        [Required]
        [MaxLength(15)]
        public string CentreCode { get; set; }

        [Required]
        [MaxLength(20)]
        public string UserType { get; set; }

        [Required]
        [MaxLength(20)]
        public string UserNameBasedOn { get; set; }
    }
}
