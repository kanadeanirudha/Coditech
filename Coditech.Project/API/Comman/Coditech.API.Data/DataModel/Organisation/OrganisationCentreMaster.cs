﻿using System.ComponentModel.DataAnnotations;

namespace Coditech.API.Data
{
    public partial class OrganisationCentreMaster
    {
        [Key]
        public int OrganisationCentreMasterId { get; set; }
        public string CentreCode { get; set; }
        public string CentreName { get; set; }
        public string HoCoRoScFlag { get; set; }
        public Nullable<int> HoId { get; set; }
        public Nullable<int> CoId { get; set; }
        public Nullable<int> RoId { get; set; }
        public string CentreSpecialization { get; set; }
        public string CentreAddress { get; set; }
        public int GeneralCityMasterId { get; set; }
        public string Pincode { get; set; }
        public string EmailId { get; set; }
        public string Url { get; set; }
        public string CellPhone { get; set; }
        public string FaxNumber { get; set; }
        public string PhoneNumberOffice { get; set; }
        public Nullable<System.DateTime> CentreEstablishmentDatetime { get; set; }
        public byte OrganisationId { get; set; }
        public Nullable<int> CentreLoginNumber { get; set; }
        public string InstituteCode { get; set; }
        public string TimeZone { get; set; }
        public Nullable<decimal> Latitude { get; set; }
        public Nullable<decimal> Longitude { get; set; }
        public Nullable<double> CampusArea { get; set; }
        public string CINNumber { get; set; }
        public string GSTINNumber { get; set; }
        public string PanNumber { get; set; }
        public string PFNumber { get; set; }
        public string ESICNumber { get; set; }
        public string WaterMark { get; set; }
        public long? LogoMediaId { get; set; }
        public long? LogoSmallMediaId { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
