﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Coditech.Common.API.Model
{
    public class OrganisationModel : BaseModel
    {
        public OrganisationModel()
        {
        }
        public byte OrganisationMasterId { get; set; }
        public string EstablishmentCode { get; set; }
        [Required]
        public string OrganisationName { get; set; }
        public System.DateTime FoundationDatetime { get; set; }
        [Required]
        public string FounderMember { get; set; }
        [Required]
        public string Address1 { get; set; }
        [Required]
        public int GeneralCityMasterId { get; set; }
        [Required]
        public string Pincode { get; set; }
        [Required]
        public string EmailId { get; set; }
        public string Url { get; set; }
        public string OfficeComment { get; set; }
        public string MissionStatement { get; set; }
        [Required]
        public string MobileNumber { get; set; }
        public string FaxNumber { get; set; }
        public string OfficePhone1 { get; set; }
        public string OfficePhone2 { get; set; }
        [Required]
        public string OrganisationCode { get; set; }
        public string PFNumber { get; set; }
        public string ESICNumber { get; set; }
    }
}
